using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.classes
{
    [Serializable]
    public class TrailerDef : SuperClass
    {
        private int id;
        public static int count;
        private string displayName;

        public TrailerDef(string type, string nameless, string rest) : base(type, nameless, rest)
        {
            count += 1;
            this.id = count;
        }

        public string getDisplayName()
        {
            return this.id.ToString() + ")  " + getBodyType();
        }
        public string getBodyType()
        {
            return this.dict["body_type"].Trim(' ', '\r', '\n');
        }

        public string getChassiMass()
        {
            string temp = this.dict["chassis_mass"].Trim(' ', '\r', '\n');
            if (temp.Contains("&"))
            {
                return Method.hexToFloat(temp.Remove(0, 1)).ToString();
            }
            else
            {
                return temp;
            }
        }
        public string getBodyMass()
        {
            string temp = this.dict["body_mass"].Trim(' ', '\r', '\n');
            if (temp.Contains("&"))
            {
                return Method.hexToFloat(temp.Remove(0, 1)).ToString();
            }
            else
            {
                return temp;
            }
        }
        public string getLenght()
        {
            string temp = this.dict["length"].Trim(' ', '\r', '\n');
            if (temp.Contains("&"))
            {
                return Method.hexToFloat(temp.Remove(0, 1)).ToString();
            }
            else
            {
                return temp;
            }
        }

        public string getSourceName()
        {
            return this.dict["source_name"].Trim(' ', '\r', '\n');
        }

    }
}
