using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.classes
{
    [Serializable]
    public class VehicleAccessory : SuperClass
    {
        public VehicleAccessory(string type, string nameless, string rest) : base(type, nameless, rest)
        {

        }

        public string getDataPath()
        {
            return this.dict["data_path"];
        }

        public void setEngine(string series, string engine)
        {
            this.dict["data_path"] = '"' + "/def/vehicle/truck/" + series + "/engine/" + engine + ".sii" + '"';
        }

        public void setTransmission(string series,string type)
        {
            this.dict["data_path"] = '"' + "/def/vehicle/truck/" + series + "/transmission/" + type + ".sii" + '"';
        }
    }
}
