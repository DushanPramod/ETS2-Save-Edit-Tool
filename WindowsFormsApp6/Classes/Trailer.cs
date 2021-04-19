using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp6.HelpDataClasses;

namespace WindowsFormsApp6.classes
{
    [Serializable]
    public class Trailer : SuperClass
    {
        private int cargo_mass;

        private string displayName;
        private int id;
        public static int count;

        private VehiclePaintJobAccessory paintJob;
        private Trailer slaveTrailer;
        private TrailerDef trailerDefinition;
        private string bodyType;

        private int accessories;

        public string licensePlate;
        public string licensePlateCity;
        public int licensePlateColorR;
        public int licensePlateColorG;
        public int licensePlateColorB;
        public int licensePlateTrans;

        public Trailer(string type, string nameless, string rest) : base(type, nameless, rest)
        {
            this.licensePlateTrans = 255;
            this.licensePlateColorR = 255;
            this.licensePlateColorG = 255;
            this.licensePlateColorB = 255;
            licensePlate = "";

            count += 1;
            id = count;

            bodyType = "Unknown";
            trailerDefinition = null;
            slaveTrailer = null;
        }

        public void setID(int id)
        {
            this.id = id;
        }

        public int getAccessoriesCount()
        {
            if (slaveTrailer == null)
            {
                return Int16.Parse(this.dict["accessories"]);
            }
            else
            {
                return Int16.Parse(this.dict["accessories"]) + 1 + slaveTrailer.getAccessoriesCount();
            }
        }

        public int getThisAccessoriesCount()
        {
            return Int16.Parse(this.dict["accessories"]);
        }

        public ArrayList GetAccessoriesNamelessArray()
        {
            int accCount = this.getThisAccessoriesCount();
            ArrayList temp = new ArrayList();
            for (int i = 0; i < accCount; i++)
            {
                string key = "accessories[" + i + "]";
                temp.Add(this.dict[key]);
            }

            string slaveTrailerName = this.dict["slave_trailer"].Trim('\n','\r',' ');

            if (!slaveTrailerName.Contains("nameless"))
            {
                return temp;
            }
            else
            {
                temp.Add(slaveTrailerName);
                temp.AddRange(((Trailer)Program.mainF4.mainDictionary[slaveTrailerName]).GetAccessoriesNamelessArray());
                
                return temp;
            }
        }

        public int getDamage(IDictionary<string, VehicleAccessory> vehicleAccessoriesDict, IDictionary<string, VehicleWheelAccessory> vehicleWheelAccessoriesDict)
        {
            float damage = 0;
            foreach (string accessory in this.GetAccessoriesNamelessArray())
            {
                if (vehicleAccessoriesDict.ContainsKey(accessory))
                {
                    if (vehicleAccessoriesDict[accessory].GetDictionary()["data_path"].Contains("body"))
                    {
                        float temp = Method.ToFloatFromString(vehicleAccessoriesDict[accessory].GetDictionary()["wear"]);
                        if (temp > damage)
                        {
                            damage = temp;
                        }
                    }
                    else if (vehicleAccessoriesDict[accessory].GetDictionary()["data_path"].Contains("chassis"))
                    {
                        float temp = Method.ToFloatFromString(vehicleAccessoriesDict[accessory].GetDictionary()["wear"]);
                        if (temp > damage)
                        {
                            damage = temp;
                        }
                    }

                }
                else if (vehicleWheelAccessoriesDict.ContainsKey(accessory))
                {
                    if (vehicleWheelAccessoriesDict[accessory].GetDictionary()["data_path"].Contains("_tire"))
                    {
                        float temp = Method.ToFloatFromString(vehicleWheelAccessoriesDict[accessory].GetDictionary()["wear"]);
                        if (temp > damage)
                        {
                            damage = temp;
                        }
                    }
                    else if (vehicleWheelAccessoriesDict[accessory].GetDictionary()["data_path"].Contains("t_wheel"))
                    {
                        float temp = Method.ToFloatFromString(vehicleWheelAccessoriesDict[accessory].GetDictionary()["wear"]);
                        if (temp > damage)
                        {
                            damage = temp;
                        }
                    }
                }
            }

            //return (int)Math.Round((decimal)damage * 100, 0);
            if (this.slaveTrailer == null)
            {
                return (int)Math.Round((decimal)damage * 100, 0);
            }
            else
            {
                if (this.slaveTrailer.getDamage(vehicleAccessoriesDict,vehicleWheelAccessoriesDict) > (int)Math.Round((decimal)damage * 100, 0))
                {
                    return this.slaveTrailer.getDamage(vehicleAccessoriesDict, vehicleWheelAccessoriesDict);
                }
                else
                {
                    return (int)Math.Round((decimal)damage * 100, 0);
                }
            }
        }

        public void damageFix(IDictionary<string, VehicleAccessory> vehicleAccessoriesDict, IDictionary<string, VehicleWheelAccessory> vehicleWheelAccessoriesDict)
        {
            foreach (string accessory in this.GetAccessoriesNamelessArray())
            {
                if (vehicleAccessoriesDict.ContainsKey(accessory))
                {
                    if (vehicleAccessoriesDict[accessory].GetDictionary()["data_path"].Contains("body"))
                    {
                        vehicleAccessoriesDict[accessory].GetDictionary()["wear"] = "0";
                    }
                    else if (vehicleAccessoriesDict[accessory].GetDictionary()["data_path"].Contains("chassis"))
                    {
                        vehicleAccessoriesDict[accessory].GetDictionary()["wear"] = "0";
                    }

                }
                else if (vehicleWheelAccessoriesDict.ContainsKey(accessory))
                {
                    if (vehicleWheelAccessoriesDict[accessory].GetDictionary()["data_path"].Contains("_tire"))
                    {
                        vehicleWheelAccessoriesDict[accessory].GetDictionary()["wear"] = "0";
                    }
                    
                    else if (vehicleWheelAccessoriesDict[accessory].GetDictionary()["data_path"].Contains("t_wheel"))
                    {
                        vehicleWheelAccessoriesDict[accessory].GetDictionary()["wear"] = "0";
                    }
                }
            }

            if (this.slaveTrailer != null)
            {
               this.slaveTrailer.damageFix(vehicleAccessoriesDict,vehicleWheelAccessoriesDict);
            }
        }

        public string getTrailerDefNamless()
        {
            return this.dict["trailer_definition"];
        }

        public void setTrailerDefNameless(string nameless)
        {
            this.dict["trailer_definition"] = nameless;
        }

        public TrailerDef GetTrailerDef()
        {
            return this.trailerDefinition;
        }

        public void setTrailerDef(TrailerDef trailerDef)
        {
            if (trailerDef != null)
            {
                this.trailerDefinition = trailerDef;
                this.dict["trailer_definition"] = this.trailerDefinition.getNameless();
            }
        }

        public static void updateForm(Form4 form4)
        {
            if (form4.player.isTrailerConnected())
            {
                Trailer temp = form4.trailerDictionary[form4.player.getAssignedTrailer()];
                form4.comboBox11.SelectedItem = temp.getDisplayName();
                if (temp.trailerDefinition != null)
                {
                    form4.comboBox12.SelectedItem = temp.trailerDefinition.getDisplayName();
                    form4.label10.Text = temp.trailerDefinition.getBodyType();
                    form4.label11.Text = temp.trailerDefinition.getChassiMass();
                    form4.label42.Text = temp.trailerDefinition.getBodyMass();
                    form4.label43.Text = temp.trailerDefinition.getLenght();
                    form4.label44.Text = temp.trailerDefinition.getSourceName();
                }
            }
        }

        public float getCargoDamage()
        {
            return (float)Math.Round((decimal)(Method.ToFloatFromString(this.dict["cargo_damage"])*100),2);
        }

        public void fixCargoDamage()
        {
            this.dict["cargo_damage"] = "0";
        }

        public string getSlaveTrailerNameless()
        {
            string temp = this.dict["slave_trailer"];
            if (temp.Contains("nameless"))
            {
                return this.dict["slave_trailer"];
            }
            else
            {
                return null;
            }
        }

        public void setNumberPlate(string plateNo, string city, int alfa, int r, int g, int b)
        {
            string stringAlfa = alfa.ToString("X2");
            string stringR = (r).ToString("X2");
            string stringG = (g).ToString("X2");
            string stringB = (b).ToString("X2");

            if (getSlaveTrailerNameless() == null)
            {
                this.dict["license_plate"] = '"' + "<color value=" + stringAlfa + stringB + stringG + stringR + ">" + plateNo + "|_" + city + '"';
            }
            else
            {
                this.dict["license_plate"] = '"' + "<color value=" + stringAlfa + stringB + stringG + stringR + ">" + plateNo + "|_" + city + '"';
                ((Trailer)Program.mainF4.mainDictionary[getSlaveTrailerNameless()]).setNumberPlate(plateNo,city,alfa,r,g,b);
            }
            
        }

        public int getCargoMass()
        {
            string temp = this.dict["cargo_mass"].Trim(' ', '\r', '\n');
            if (temp.Contains("&"))
            {
                cargo_mass = (int)Method.hexToFloat(temp.Remove(0, 1));
            }
            else
            {
                cargo_mass = int.Parse(temp);
            }

            return  cargo_mass;
        }

        public void setCargoMass(int i)
        {
            this.dict["cargo_mass"] = i.ToString();
        }

        public string getBodyType(Form4 form4)
        {
            if (form4.trailerDefDictionary.ContainsKey(this.dict["trailer_definition"].Trim(' ', '\r', '\n')))
            {
                bodyType = form4.trailerDefDictionary[this.dict["trailer_definition"].Trim(' ', '\r', '\n')]
                    .getBodyType();
            }

            return bodyType;
        }

        public string getDisplayName()
        {
            this.displayName = id.ToString() + ") " + licensePlate + "  " + bodyType;
            return this.displayName.ToString();
        }

        public void setTrailerParams(Form4 form4)
        {
            accessories = Int16.Parse(this.dict["accessories"]);
            for (int i = 0; i < accessories; i++)
            {
                string tempKey = "accessories[" + i.ToString() + "]";
                string tempNameless = this.dict[tempKey].TrimEnd('\r', '\n');
                if (form4.vehiclePaintJobAccessoryDictionary.ContainsKey(tempNameless))
                {
                    this.paintJob = form4.vehiclePaintJobAccessoryDictionary[tempNameless];
                }

            }

            if (form4.trailerDefDictionary.ContainsKey(this.dict["trailer_definition"].Trim(' ','\r','\n')))
            {
                this.trailerDefinition = form4.trailerDefDictionary[this.dict["trailer_definition"].Trim(' ', '\r', '\n')];
            }


            if (form4.trailerDefDictionary.ContainsKey(this.dict["trailer_definition"].Trim(' ', '\r', '\n')))
            {
                bodyType = form4.trailerDefDictionary[this.dict["trailer_definition"].Trim(' ', '\r', '\n')]
                    .getBodyType();
            }

            string tempLicensePlate = this.dict["license_plate"];
            this.licensePlateCity = tempLicensePlate.Trim('\r', '\n', '"').Split('|')[1].Trim(' ','_');
            tempLicensePlate = tempLicensePlate.Trim('\r', '\n', '"').Split('|')[0];

            bool isBracketIn = false;
            foreach (var VARIABLE in tempLicensePlate)
            {
                if (!isBracketIn && (VARIABLE != '<' && VARIABLE != '>'))
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
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].Contains("color=") || temp[i].Contains("value="))
                {
                    platecolor = temp[i].Trim(' ', '\r', '\n').Substring(6, 8);
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

            displayName = id.ToString() + ") " + licensePlate + "  " + getBodyType(form4);
            if (this.dict["slave_trailer"].Contains("nameless"))
            {
                this.slaveTrailer = (Trailer) form4.mainDictionary[this.dict["slave_trailer"]];
            }

        }

        public VehiclePaintJobAccessory getTrailerPaintJob()
        {
            return this.paintJob;
        }

        public void setTrailerPaintJob(IDictionary<string, VehiclePaintJobAccessory> paintJobsDict, VehiclePaintJobAccessory paintJob)
        {
            paintJobsDict[this.paintJob.getNameless()].setPaintJobValues(paintJob);
        }

    }
}
