using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.classes
{
    [Serializable]
    public class VehiclePaintJobAccessory : SuperClass
    {
        private string vehicle;

        public VehiclePaintJobAccessory(string type, string nameless, string rest) : base(type, nameless, rest)
        {

        }

        public string getVehicle()
        {
            return this.vehicle;
        }

        public void setDict(IDictionary<string,string> dictionary)
        {
            this.dict = dictionary;
        }

        public void setVehicle(string vehicle)
        {
            this.vehicle = vehicle;
        }

        public static VehiclePaintJobAccessory getSelectedVehiclePaintJob(Vehicle vehicle)
        {
            return vehicle.getVehiclePaintJob();
        }

        public static VehiclePaintJobAccessory getSelectedTrailerPaintJob(Trailer trailer)
        {
            return trailer.getTrailerPaintJob();
        }

        public void setPaintJobValues(VehiclePaintJobAccessory paintjob)
        {
            //this.dict = paintjob.dict;
            /*foreach (var VARIABLE in dict)
            {
                
            }*/
            //System.Console.WriteLine("");
            //System.Console.WriteLine(this.dict["data_path"]);
            this.dict = paintjob.dict;
            //System.Console.WriteLine(this.dict["data_path"]);
        }
    }
}
