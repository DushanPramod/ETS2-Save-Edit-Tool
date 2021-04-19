using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace WindowsFormsApp6.Helpers
{
    class SiiDecoder
    {
        private bool file_decoded;
        private string pathToProfile;
        private string decorderPath;

        public SiiDecoder(string sii_deco_path, string savePathToProfile)
        {
            this.file_decoded = false;
            this.decorderPath = sii_deco_path;
            this.pathToProfile = savePathToProfile;
        }

        public void setPathToProfile(string s)
        {
            this.pathToProfile = s;
        }


        public string[] decode_file(string profile)
        {
            string file_path = pathToProfile + "\\profiles\\" + profile  + "\\save\\quicksave";
            int num = 0;
            while (num < 5)
            {
                log_writer("backup file to: " + file_path + "\\game.sii_backup");
                File.Copy(file_path + "\\game.sii", file_path + "\\game.sii_backup", true);
                byte[] keyBytes = new byte[32]
                {
          (byte) 42,
          (byte) 95,
          (byte) 203,
          (byte) 23,
          (byte) 145,
          (byte) 210,
          (byte) 47,
          (byte) 182,
          (byte) 2,
          (byte) 69,
          (byte) 179,
          (byte) 216,
          (byte) 54,
          (byte) 158,
          (byte) 208,
          (byte) 178,
          (byte) 194,
          (byte) 115,
          (byte) 113,
          (byte) 86,
          (byte) 63,
          (byte) 191,
          (byte) 31,
          (byte) 60,
          (byte) 158,
          (byte) 223,
          (byte) 107,
          (byte) 17,
          (byte) 130,
          (byte) 90,
          (byte) 93,
          (byte) 10
                };
                //this.message_board_display_message("m", Resource.Message_Loading_save_file);
                log_writer("loading file into memory");
                byte[] memory = this.load_file_to_memory(file_path + "/game.sii");
                if (memory == null)
                    return (string[])null;
                if (Encoding.UTF8.GetString(memory, 0, 8) == "SiiNunit")
                {
                    log_writer("no decoding needed");
                    return File.ReadAllLines(file_path + "/game.sii");
                }
                //this.message_board_display_message("m", Resource.Message_Decoding_save_file);
                log_writer("Decoding file");
                byte[] iv = new byte[16];
                byte[] encryptedData = new byte[memory.Length - 56];
                Array.Copy((Array)memory, 56, (Array)encryptedData, 0, encryptedData.Length);
                Array.Copy((Array)memory, 36, (Array)iv, 0, iv.Length);
                try
                {
                    byte[] buffer = AESDecrypt(encryptedData, keyBytes, iv);
                    string input = "";
                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (InflaterInputStream inflaterInputStream = new InflaterInputStream((Stream)memoryStream))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)inflaterInputStream))
                                input = streamReader.ReadToEnd();
                        }
                    }
                    ++num;
                    log_writer("file decoded, checking format of file");
                    if (input.StartsWith("BSII"))
                    {
                        string arguments = "\"" + file_path + "\\game.sii\" \"" + file_path + "\\game.sii\"";
                        log_writer("starting SiiDecrypt");
                        Process process = Process.Start(this.decorderPath, arguments);
                        log_writer("SiiDecrypt started with parameters: " + arguments);
                        process.WaitForExit();
                    }
                    else
                    {
                        log_writer("file was decrypted properly");
                        return Regex.Split(input, "\r\n|\r|\n");
                    }
                }
                catch
                {
                    log_writer("could not decode file: + " + file_path + "/game.sii");
                    //this.message_board_display_message("e", Resource.Error_Could_not_decode);
                    return (string[])null;
                }
            }
            //this.message_board_display_message("e", Resource.Error_Could_not_decode);
            log_writer("could not decrypt in 5 attempts");
            return (string[])null;
        }

        public static void log_writer(string error)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(Directory.GetCurrentDirectory() + "\\error.log", true))
                    streamWriter.WriteLine(DateTime.Now.ToString() + ": " + error);
            }
            catch
            {
            }
        }

        private static byte[] AESDecrypt(byte[] encryptedData, byte[] keyBytes, byte[] iv)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Mode = CipherMode.CBC;
            rijndaelManaged.Padding = PaddingMode.None;
            rijndaelManaged.IV = iv;
            rijndaelManaged.KeySize = 128;
            rijndaelManaged.BlockSize = 128;
            rijndaelManaged.Key = keyBytes;
            rijndaelManaged.IV = iv;
            return rijndaelManaged.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }

        private byte[] load_file_to_memory(string path)
        {
            byte[] numArray;
            try
            {
                numArray = File.ReadAllBytes(path);
            }
            catch
            {
                log_writer("Could not find file in: " + this.pathToProfile);
                //this.message_board_display_message("e", Resource.Error_Could_not_find_file);
                this.file_decoded = false;
                return (byte[])null;
            }
            return numArray;
        }

        /*private void message_board_display_message(string status, string message)
        {
            if (status == "e")
            {
                f4.message_board.ForeColor = Color.Red;
                f4.message_board.Text = message;
            }
            if (status == "m")
            {
                f4.message_board.ForeColor = Color.Black;
                f4.message_board.Text = message;
            }
           f4.message_board.Refresh();
        }*/
    }
}
