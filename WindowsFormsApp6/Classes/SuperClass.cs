using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.classes
{
    [Serializable]
    public class SuperClass
    {
        private string type;
        private string nameless;
        private string rest;
        public IDictionary<string, string> dict;

        public SuperClass()
        {

        }

        public SuperClass(string type,string nameless,string rest)
        {
            this.rest = rest;
            this.type = type;
            this.nameless = nameless;

            this.dict = new Dictionary<string, string>();

            createDict(rest);
        }

        public string getType()
        {
            return this.type;
        }

        public string getNameless()
        {
            return this.nameless;
        }

        public void setNameless(string nameless)
        {
            this.nameless = nameless;
        }

        protected void createDict(string rest)
        {
            string[] separator = { "\r","\n" };
            string[] tempSplit = rest.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in tempSplit)
            {
                if (s.Contains('"'))
                {
                    if (s.Split(':').Length == 3)
                    {
                        string[] temp = s.Split(':');
                        string tempValue = temp[1] + ":" + temp[2].Trim('\r', '\n');

                        dict[temp[0].Trim(' ')] = tempValue;

                    }
                    else
                    {
                        string[] temp = s.Split(':');
                        dict[temp[0].Trim(' ')] = temp[1].Trim(' ', '\r', '\n');
                    }
                }
                else
                {
                    string[] temp = s.Split(':');
                    dict[temp[0].Trim(' ')] = temp[1].Trim(' ','\r','\n', '"');
                }
            }
        }

        public string createWriteString()
        {
            string writeString = this.getType() + " : " + this.getNameless() + " {";
            foreach (var v in this.dict)
            {
                writeString = writeString + "\n " + v.Key.ToString() + ": " + v.Value.ToString();
            }

            writeString = writeString + "\n}\n\n";

            return writeString;
        }

        public IDictionary<string, string> GetDictionary()
        {
            return this.dict;
        }
    }
}
