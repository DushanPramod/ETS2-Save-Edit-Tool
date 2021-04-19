using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;
using WindowsFormsApp6;


using Limilabs.Client.IMAP;
using Limilabs.Mail;

public static class Method
{
    //public static System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
    public static void Serialize(Config config , String path )
    {

        using(FileStream fs = new FileStream(path, FileMode.Create)) {
            XmlSerializer xSer = new XmlSerializer(typeof(Config));

            xSer.Serialize(fs, config);
        }
    }
    public static void SerializeObject(Object obj, String path)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);

        formatter.Serialize(stream, obj);
        stream.Close();
    }

    public static Config deserialize(String path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Open)) //double check that...
        {
            XmlSerializer _xSer = new XmlSerializer(typeof(Config));

            var myObject = _xSer.Deserialize(fs);

            return (Config)myObject;
        }

    }

    public static Object deserializeObject(String path)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream  stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        Object obj = (Object)formatter.Deserialize(stream);
        stream.Close();
        return obj;
    }

    public static string chooseFile()
    {
        OpenFileDialog choofdlog = new OpenFileDialog();
        choofdlog.DefaultExt = "exe";
        choofdlog.Filter = "exe files (*.exe)|*.exe";
        choofdlog.FilterIndex = 1;
        choofdlog.Multiselect = true;

        if (choofdlog.ShowDialog() == DialogResult.OK)
            return choofdlog.FileName;
        else
        {
            return null;
        }
    }

    public static string chooseFileWithEx(string s)
    {
        OpenFileDialog choofdlog = new OpenFileDialog();
        choofdlog.DefaultExt = s;
        choofdlog.Filter = (s + " files (*." + s) + "|*." + s   ;
        choofdlog.FilterIndex = 1;
        choofdlog.Multiselect = true;

        if (choofdlog.ShowDialog() == DialogResult.OK)
            return choofdlog.FileName;
        else
        {
            return null;
        }
    }

    public static string chooseFolder()
    {
        FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        {
            return folderBrowserDialog1.SelectedPath;
        }
        else
        {
            return null;
        }
    }

    public static void ProcessStart(String path)
    {
        Process notePad = new Process();
        notePad.StartInfo.FileName = path;

        notePad.Start();
    }
    public static float hexToFloat(string HexRep)
    {

        // Converting to integer
        Int32 IntRep = Int32.Parse(HexRep, NumberStyles.AllowHexSpecifier);
        // Integer to Byte[] and presenting it for float conversion
        float f = BitConverter.ToSingle(BitConverter.GetBytes(IntRep), 0);
        // There you go
        return f;
    }

    public static float ToFloatFromString(string val)
    {
        if (val.Contains("&"))
        {
            return hexToFloat(val.Remove(0, 1));
        }
        else
        {
            return float.Parse(val);
        }
    }

    public static void sendEmail(String emailAddress, String password, String subject, String body, bool isAttachment)
    {
        try
        {
            MailMessage mail = new MailMessage();

            mail.To.Add(emailAddress);
            //mail.To.Add(txtto.Text.ToString());

            mail.From = new MailAddress(emailAddress);
            mail.Subject = subject;
            mail.Body = body;
            if (isAttachment)
            {
                mail.Attachments.Add(new Attachment(@"emailData\send.txt"));
            }
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("localhost", 587);

            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Credentials = new System.Net.NetworkCredential
                (emailAddress, password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.Send(mail);
            //MessageBox.Show("Mail Send");
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message);
        }
    }

    public static void deleteExpireEmail(String emailAddress, String password, long uid)
    {
        using (Imap imap = new Imap())
        {
            imap.ConnectSSL("imap.gmail.com", 993);
            imap.UseBestLogin(emailAddress, password);
            imap.SelectInbox();
            // get all email uids
            imap.DeleteMessageByUID(uid);
        }
    }

    public static void downloadAttachment(IMail email)
    {
        if (email.Attachments.Count > 0)
        {
            email.Attachments.ForEach(mime => mime.Save(@"emailData\recieve.txt"));
        }
    }


    public static List<IMail> getAllMails(String emailAddress, String password)
    {
        using (Imap imap = new Imap())
        {
            imap.ConnectSSL("imap.gmail.com", 993);
            imap.UseBestLogin(emailAddress, password);
            imap.SelectInbox();
            // get all email uids
            List<long> uids = imap.Search(Flag.All);
            List<IMail> messageList = new List<IMail>();

            foreach (var uid in uids)
            {
                var eml = imap.GetMessageByUID(uid);
                IMail email = new MailBuilder().CreateFromEml(eml);

                DateTime dateTime = email.Date.Value;
                if ((DateTime.Now - dateTime).TotalHours > 24 && !email.Subject.Equals("Members"))
                {
                    imap.DeleteMessageByUID(uid);
                }
                else
                {
                    //if(email.Subject.Equals("Members"))
                    messageList.Add(email);
                }
            }

            return messageList;
        }

    }




}