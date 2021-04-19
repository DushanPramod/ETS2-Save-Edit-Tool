using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp6.HelpDataClasses;

namespace WindowsFormsApp6.classes
{
    [Serializable]
    public class Vehicle : SuperClass
    {
        private int id;
        public static int count;
        private string displayName;

        private int accessories;
        private string engineNameless;
        private string transmissionNameless;


        private string series;
        private string seriesName;

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

        public Vehicle()
        {

        }

        public Vehicle(string type, string nameless, string rest) : base(type, nameless, rest)
        {
            this.licensePlateTrans = 255;
            this.licensePlateColorR = 255;
            this.licensePlateColorG = 255;
            this.licensePlateColorB = 255;
            licensePlate = "";
            count += 1;
            id = count;
        }

        public string getSeries()
        {
            return this.series;
        }

        public string getLicenNo()
        {
            return licensePlate;
        }

        public int getID()
        {
            return id;
        }

        public int getAccessoriesCount()
        {
            return Int16.Parse(this.dict["accessories"]);
        }

        public Dictionary<int,string> GetAccessoriesNamelessDictionary()
        {
            int accCount = this.getAccessoriesCount();
            Dictionary<int, string> temp = new Dictionary<int, string>();
            for (int i = 0; i < accCount; i++)
            {
                string key = "accessories[" + i + "]";
                temp.Add(i,this.dict[key]);
            }

            return temp;
        }

        public int getDamage(IDictionary<string,VehicleAccessory> vehicleAccessoriesDict, IDictionary<string,VehicleWheelAccessory> vehicleWheelAccessoriesDict)
        {
            float damage = 0;
            foreach (var accessory in this.GetAccessoriesNamelessDictionary())
            {
                if (vehicleAccessoriesDict.ContainsKey(accessory.Value))
                {
                    if (vehicleAccessoriesDict[accessory.Value].GetDictionary()["data_path"].Contains("engine"))
                    {
                        float temp =
                            Method.ToFloatFromString(
                                vehicleAccessoriesDict[accessory.Value].GetDictionary()["wear"]);
                        if (temp > damage)
                        {
                            damage = temp;
                        }
                    }
                    else if (vehicleAccessoriesDict[accessory.Value].GetDictionary()["data_path"]
                        .Contains("transmission"))
                    {
                        float temp =
                            Method.ToFloatFromString(
                                vehicleAccessoriesDict[accessory.Value].GetDictionary()["wear"]);
                        if (temp > damage)
                        {
                            damage = temp;
                        }
                    }
                    else if (vehicleAccessoriesDict[accessory.Value].GetDictionary()["data_path"]
                        .Contains("chassis"))
                    {
                        float temp =
                            Method.ToFloatFromString(
                                vehicleAccessoriesDict[accessory.Value].GetDictionary()["wear"]);
                        if (temp > damage)
                        {
                            damage = temp;
                        }
                    }
                    else if (vehicleAccessoriesDict[accessory.Value].GetDictionary()["data_path"]
                        .Contains("cabin"))
                    {
                        float temp =
                            Method.ToFloatFromString(
                                vehicleAccessoriesDict[accessory.Value].GetDictionary()["wear"]);
                        if (temp > damage)
                        {
                            damage = temp;
                        }
                    }
                }
                else if(vehicleWheelAccessoriesDict.ContainsKey(accessory.Value))
                {
                    if (vehicleWheelAccessoriesDict[accessory.Value].GetDictionary()["data_path"].Contains("_tire"))
                    {
                        float temp =
                            Method.ToFloatFromString(
                                vehicleWheelAccessoriesDict[accessory.Value].GetDictionary()["wear"]);
                        if (temp > damage)
                        {
                            damage = temp;
                        }
                    }
                }
            }

            return (int)Math.Round((double)damage*100,0);
        }

        public void damageFix(IDictionary<string, VehicleAccessory> vehicleAccessoriesDict, IDictionary<string, VehicleWheelAccessory> vehicleWheelAccessoriesDict)
        {
            foreach (var accessory in this.GetAccessoriesNamelessDictionary())
            {
                if (vehicleAccessoriesDict.ContainsKey(accessory.Value))
                {
                    if (vehicleAccessoriesDict[accessory.Value].GetDictionary()["data_path"].Contains("engine"))
                    {
                        vehicleAccessoriesDict[accessory.Value].GetDictionary()["wear"] = "0";
                       
                    }
                    else if (vehicleAccessoriesDict[accessory.Value].GetDictionary()["data_path"]
                        .Contains("transmission"))
                    {
                        vehicleAccessoriesDict[accessory.Value].GetDictionary()["wear"] = "0";

                    }
                    else if (vehicleAccessoriesDict[accessory.Value].GetDictionary()["data_path"]
                        .Contains("chassis"))
                    {
                        vehicleAccessoriesDict[accessory.Value].GetDictionary()["wear"] = "0";

                    }
                    else if (vehicleAccessoriesDict[accessory.Value].GetDictionary()["data_path"]
                        .Contains("cabin"))
                    {
                        vehicleAccessoriesDict[accessory.Value].GetDictionary()["wear"] = "0";

                    }

                }
                else if (vehicleWheelAccessoriesDict.ContainsKey(accessory.Value))
                {
                    if (vehicleWheelAccessoriesDict[accessory.Value].GetDictionary()["data_path"].Contains("_tire"))
                    {
                        vehicleWheelAccessoriesDict[accessory.Value].GetDictionary()["wear"] = "0";
                    }
                }
            }
        }

        public string getDisplayName()
        {
            return displayName;
        }

        public string getFuelRelative()
        {
            string s = this.dict["fuel_relative"].Trim(' ','\r','\n');
            if (s.Contains("&"))
            {
                return Method.hexToFloat(s.Remove(0, 1)).ToString("F");
            }
            else
            {
                return float.Parse(s).ToString("F");
            }
        }

        public void setFuelRelative(string relative)
        {
            this.dict["fuel_relative"] = relative;
        }

        public void setEngine(Form4 form4, string series, string engine)
        {
            VehicleAccessory tempVehicleAccessory = null;
            tempVehicleAccessory = form4.vehicleAccessoryDictionary[this.engineNameless];
            tempVehicleAccessory.setEngine(series,engine);
        }

        public void setTransmission(Form4 form4, string series, string type)
        {
            VehicleAccessory tempVehicleAccessory = null;
            tempVehicleAccessory = form4.vehicleAccessoryDictionary[this.transmissionNameless];
            tempVehicleAccessory.setTransmission(series, type);
        }

        public void setNumberPlate(string plateNo,string city,int alfa, int r, int g,int b)
        {
            string stringAlfa = alfa.ToString("X2");
            string stringR = (r).ToString("X2");
            string stringG = (g).ToString("X2");
            string stringB = (b).ToString("X2");
    
            this.dict["license_plate"] = '"' + "<color value=" + stringAlfa + stringB + stringG + stringR + ">" + plateNo + "|" + city + '"';
        }

        public static void updateForm(Form4 form4)
        {
            form4.comboBox5.Items.Clear();
            form4.comboBox6.Items.Clear();
            form4.comboBox7.Items.Clear();
            form4.comboBox8.Items.Clear();
            form4.comboBox5.Enabled = true;
            form4.comboBox6.Enabled = true;
            form4.comboBox7.Enabled = true;
            form4.comboBox8.Enabled = true;
            
            foreach (var VARIABLE in TruckDetail.seresToEngineDictionary)
            {
                form4.comboBox5.Items.Add(TruckDetail.seriesToNameDictionary[VARIABLE.Key]);
            }


            foreach (var VARIABLE in form4.vehicleDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == form4.comboBox4.SelectedItem)
                {
                    form4.label41.Text = VARIABLE.Value.getFuelRelative();
                    form4.numericUpDown6.Value = int.Parse(VARIABLE.Value.getFuelRelative().Split('.')[0]);

                    form4.comboBox5.SelectedItem = TruckDetail.getSeriesToName(VARIABLE.Value.engineSeries);
                    form4.comboBox6.Items.Clear();
                    foreach (var VARIABLE2 in TruckDetail.seresToEngineDictionary[VARIABLE.Value.engineSeries])
                    {
                        form4.comboBox6.Items.Add(VARIABLE2.Key);
                    }
                    if (TruckDetail.getEngineToEngineName(VARIABLE.Value.engineSeries, VARIABLE.Value.engineCapacity) != null)
                    {
                        form4.comboBox6.SelectedItem = TruckDetail.getEngineToEngineName(VARIABLE.Value.engineSeries,
                            VARIABLE.Value.engineCapacity);
                        break;
                    }

                }
            }
            

            foreach (var VARIABLE in TruckDetail.seresToTransmissionDictionary)
            {
                form4.comboBox8.Items.Add(TruckDetail.seriesToNameDictionary[VARIABLE.Key]);
                
            }
            foreach (var VARIABLE in form4.vehicleDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == form4.comboBox4.SelectedItem)
                {
                    form4.comboBox8.SelectedItem = TruckDetail.getSeriesToName(VARIABLE.Value.transmissionSeries);
                    form4.comboBox7.Items.Clear();
                    foreach (var VARIABLE2 in TruckDetail.seresToTransmissionDictionary[VARIABLE.Value.transmissionSeries])
                    {
                        form4.comboBox7.Items.Add(VARIABLE2.Key);
                    }
                    if (TruckDetail.getTransmissionToTransmissionName(VARIABLE.Value.transmissionSeries, VARIABLE.Value.transmissionType) != null)
                    {
                        form4.comboBox7.SelectedItem = TruckDetail.getTransmissionToTransmissionName(VARIABLE.Value.transmissionSeries,
                            VARIABLE.Value.transmissionType);
                        break;
                    }
                }
            }
        }

        public void setVehicleParams(Form4 form4, TruckDetail truckDetail)
        {
            accessories = Int16.Parse(this.dict["accessories"]);
            for (int i = 0; i < accessories; i++)
            {
                string tempKey = "accessories[" + i.ToString() + "]";
                string tempNameless = this.dict[tempKey].TrimEnd('\r', '\n');
                if (form4.vehicleAccessoryDictionary.ContainsKey(tempNameless))
                {
                    string tempValue = form4.vehicleAccessoryDictionary[tempNameless].getDataPath();
                    if (tempValue.Contains("data.sii"))
                    {
                        this.series = tempValue.Split('/')[4];
                        this.seriesName = TruckDetail.getSeriesToName(this.series);
                    }
                    else if(tempValue.Contains("engine"))
                    {
                        this.engineNameless = tempNameless;
                        this.engineSeries = tempValue.Split('/')[4];
                        this.engineCapacity = tempValue.Split('/')[6].Split('.')[0];
                    }
                    else if(tempValue.Contains("transmission"))
                    {
                        this.transmissionNameless = tempNameless;
                        this.transmissionSeries = tempValue.Split('/')[4];
                        this.transmissionType = tempValue.Split('/')[6].Split('.')[0];
                    }
                }
                else if(form4.vehiclePaintJobAccessoryDictionary.ContainsKey(tempNameless))
                {
                    this.paintJob = form4.vehiclePaintJobAccessoryDictionary[tempNameless];
                }
            }

            string tempLicensePlate = this.dict["license_plate"];
            this.licensePlateCity = tempLicensePlate.Trim('\r', '\n','"').Split('|')[1].Trim(' ');
            tempLicensePlate = tempLicensePlate.Trim('\r', '\n','"').Split('|')[0];

            bool isBracketIn = false;
            foreach (var VARIABLE in tempLicensePlate)
            {
                if (!isBracketIn && (VARIABLE != '<' && VARIABLE != '>' ))
                {
                    this.licensePlate += VARIABLE;
                }

                if (VARIABLE == '<')
                {
                    isBracketIn = true;
                }

                if (VARIABLE == '>')
                {
                    isBracketIn = false;
                }
            }

            string platecolor;
            string[] temp = tempLicensePlate.Split(' ', '<', '>');
            for (int i=0;i<temp.Length;i++)
            {
                if (temp[i].Contains("color=") || temp[i].Contains("value="))
                {
                    platecolor = temp[i].Trim(' ', '\r', '\n').Substring(6,8);
                    this.licensePlateTrans = int.Parse(platecolor.Substring(0, 2),
                        System.Globalization.NumberStyles.HexNumber);
                    this.licensePlateColorB = int.Parse(platecolor.Substring(2, 2),
                        System.Globalization.NumberStyles.HexNumber);
                    this.licensePlateColorG = int.Parse(platecolor.Substring(4, 2),
                        System.Globalization.NumberStyles.HexNumber);
                    this.licensePlateColorR = int.Parse(platecolor.Substring(6, 2),
                        System.Globalization.NumberStyles.HexNumber);

                }
            }

            displayName = id.ToString() + ") " + licensePlate + "  " +seriesName;

        }

        public VehiclePaintJobAccessory getVehiclePaintJob()
        {
            return this.paintJob;
        }

        public void setVehiclePaintJob(IDictionary<string,VehiclePaintJobAccessory> paintJobsDict, VehiclePaintJobAccessory paintJob)
        {
           paintJobsDict[this.paintJob.getNameless()].setPaintJobValues(paintJob);
        }
    }
}
