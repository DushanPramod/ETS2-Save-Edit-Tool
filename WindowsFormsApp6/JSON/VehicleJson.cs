using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp6.classes;

namespace WindowsFormsApp6.JSON
{
    class VehicleJson
    {
        public string type;
        public string nameless;
        public string rest;
        public List<string> KeyList;
        public List<string> VaList;




        public int id;
        public static int count;
        public string displayName;

        public int accessories;
        public string engineNameless;
        public string transmissionNameless;


        public string series;
        public string seriesName;

        public string engineSeries;
        public string engineCapacity;

        public string transmissionSeries;
        public string transmissionType;

        public VehiclePaintJobAccessory paintJob;

        public string licensePlate;
        public string licensePlateCity;
        public int licensePlateColorR;
        public int licensePlateColorG;
        public int licensePlateColorB;
        public int licensePlateTrans;
    }
}
