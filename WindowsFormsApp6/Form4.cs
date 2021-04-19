using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.classes;
using WindowsFormsApp6.HelpDataClasses;
using WindowsFormsApp6.Helpers;
using Funbit.Ets.Telemetry.Server.Data;

namespace WindowsFormsApp6
{
    [Serializable]
    public partial class Form4 : Form
    {
        private int[] colors ;
        public IDictionary<string,string> savedTruckItemDictionary = new Dictionary<string, string>();

        private static String config_path = null;
        Config config = new Config();
        IDictionary<string, string> profileDictionary = new Dictionary<string, string>();
        private SiiDecoder decoder;
        public TruckDetail truckDetail;

        public IDictionary<int, Placement> SavedPlacements;
        //public string truckDetaiilsPath = Environment.SpecialFolder.MyDocuments + @"\ETS2 Speed Recorder\truckDetails.txt";




        private bool isAdmin = true;
        private bool isRegister = true;
        private bool isSaveFileLoad = false;


        //ArrayList saveDataArrayList = new ArrayList();
        private string[] saveFileData;
        private string[] licenPlateCitys;
        OptinHide hideOption = new OptinHide();
        public Economy economy;
        public Bank playerBank;
        public Player player;

        public IDictionary<string, SuperClass> mainDictionary = new Dictionary<string, SuperClass>();
        public IDictionary<string,SuperClass> RemovedClasses = new Dictionary<string, SuperClass>();


        IDictionary<string, Economy> economyDictionary = new Dictionary<string, Economy>();
        IDictionary<string, Bank> bankDictionary = new Dictionary<string, Bank>();
        IDictionary<string, Player> playerDictionary = new Dictionary<string, Player>();
        public IDictionary<string, Trailer> trailerDictionary = new Dictionary<string, Trailer>();
        public IDictionary<string, VehicleAccessory> vehicleAccessoryDictionary = new Dictionary<string, VehicleAccessory>();
        public IDictionary<string, VehicleWheelAccessory> vehicleWheelAccessoryDictionary = new Dictionary<string, VehicleWheelAccessory>();
        IDictionary<string, VehicleAddonAccessory> vehicleAddonAccessoryDictionary = new Dictionary<string, VehicleAddonAccessory>();
        public IDictionary<string, VehiclePaintJobAccessory> vehiclePaintJobAccessoryDictionary = new Dictionary<string, VehiclePaintJobAccessory>();
        IDictionary<string, TrailerUtilizationLog> trailerUtilizationLogDictionary = new Dictionary<string, TrailerUtilizationLog>();
        public IDictionary<string, TrailerDef> trailerDefDictionary = new Dictionary<string, TrailerDef>();
        public IDictionary<string, Vehicle> vehicleDictionary = new Dictionary<string, Vehicle>();
        IDictionary<string, ProfitLog> profitLogDictionary = new Dictionary<string, ProfitLog>();
        IDictionary<string, ProfitLogEntry> profitLogEntryDictionary = new Dictionary<string, ProfitLogEntry>();
        IDictionary<string, DriverPlayer> driverPlayerDictionary = new Dictionary<string, DriverPlayer>();
        IDictionary<string, Company> companyDictionary = new Dictionary<string, Company>();
        IDictionary<string, JobOfferData> jobOfferDataDictionary = new Dictionary<string, JobOfferData>();
        IDictionary<string, Garage> garageDictionary = new Dictionary<string, Garage>();
        IDictionary<string, GameProgress> gameProgressDictionary = new Dictionary<string, GameProgress>();
        IDictionary<string, TransportData> transportDataDictionary = new Dictionary<string, TransportData>();
        IDictionary<string, EconomyEventQueue> economyEventQueueDictionary = new Dictionary<string, EconomyEventQueue>();
        IDictionary<string, EconomyEvent> economyEventDictionary = new Dictionary<string, EconomyEvent>();
        IDictionary<string, MailCtrl> mailCtrlDictionary = new Dictionary<string, MailCtrl>();
        IDictionary<string, MailDef> mailDefDictionary = new Dictionary<string, MailDef>();
        IDictionary<string, OversizeOfferCtrl> oversizeOfferCtrlDictionary = new Dictionary<string, OversizeOfferCtrl>();
        IDictionary<string, OversizeRouteOffers> oversizeRouteOffersDictionary = new Dictionary<string, OversizeRouteOffers>();
        IDictionary<string, OversizeOffer> oversizeOfferDictionary = new Dictionary<string, OversizeOffer>();
        IDictionary<string, DeliveryLog> deliveryLogDictionary = new Dictionary<string, DeliveryLog>();
        IDictionary<string, DeliveryLogEntry> deliveryLogEntryDictionary = new Dictionary<string, DeliveryLogEntry>();
        IDictionary<string, PoliceCtrl> policeCtrlDictionary = new Dictionary<string, PoliceCtrl>();
        IDictionary<string, MapAction> mapActionDictionary = new Dictionary<string, MapAction>();
        IDictionary<string, DriverAi> driverAiDictionary = new Dictionary<string, DriverAi>();
        IDictionary<string, JobInfo> jobInfoDictionary = new Dictionary<string, JobInfo>();
        IDictionary<string, Registryy> registryDictionary = new Dictionary<string, Registryy>();
        IDictionary<string, BusStop> busStopDictionary = new Dictionary<string, BusStop>();
        IDictionary<string, BusJobLog> busJobLogDictionary = new Dictionary<string, BusJobLog>();
        IDictionary<string, Other> otherDictionary = new Dictionary<string, Other>();


        public Form4()
        {
            InitializeComponent();
            truckDetail = new TruckDetail();

            licenPlateCitys = new[]
            {
                "austria", "belgium", "bulgaria", "czech","denmark","estonia",
                "finland","france","germany","hungary","italy","latvia","lithuania",
                "luxembourg","netherlands","norway","poland","romania","russia",
                "slovakia","sweden","switzerland","turkey","uk"

            };
            ;

            foreach (var VARIABLE in licenPlateCitys)
            {
                comboBox14.Items.Add(VARIABLE);
                comboBox15.Items.Add(VARIABLE);
            }

            if (File.Exists("hide_option.txt"))
            {
                string[] optin = File.ReadAllText("hide_option.txt").Split(' ', '\r', '\n');
                if (optin.Contains(Encryptor.MD5Hash("cargo_mass")))
                {
                    //System.Console.WriteLine("cargo_mass");
                    hideOption.setisCargoMass(true);
                }
                if (optin.Contains(Encryptor.MD5Hash("cargo_damage")))
                {
                    //System.Console.WriteLine("cargo_damage");
                    hideOption.setiscargoFix(true);
                }
                if (optin.Contains(Encryptor.MD5Hash("trailer_damage")))
                {
                    //System.Console.WriteLine("trailer_damage");
                    hideOption.setistrailerFix(true);
                }
                if (optin.Contains(Encryptor.MD5Hash("trailer_def")))
                {
                    //System.Console.WriteLine("trailer_def");
                    hideOption.setisTrailerDef(true);
                }

            }

            if (File.Exists("colors.txt"))
            {
                this.colors = (int[]) Method.deserializeObject("colors.txt");
            }

            if (File.Exists("truckAccessories.txt"))
            {
                this.savedTruckItemDictionary =
                    (Dictionary<string, string>) Method.deserializeObject("truckAccessories.txt");
            }

            this.SavedPlacements = Placement.ReadPlacementsDictionary();
            Placement.setCount(playerDictionary.Count+1);

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            //System.Console.WriteLine(Encryptor.MD5Hash("cargo_mass"));
            //System.Console.WriteLine(Encryptor.MD5Hash("cargo_damage"));
            //System.Console.WriteLine(Encryptor.MD5Hash("trailer_damage"));
            //System.Console.WriteLine(Encryptor.MD5Hash("trailer_def"));

            
                toolOptionHide();
                button19.Enabled = false;

                config_path = "config.txt";
                this.config = Method.deserialize(config_path);
                label7.Text = config.ets2Path;

                if (Directory.Exists(config.ets2Path + "\\profiles"))
                {
                    foreach (string dir in Directory.GetDirectories(config.ets2Path + "\\profiles"))
                    {
                        //System.Console.WriteLine(img.Remove(0, (config.ets2Path + "\\profiles").ToString().Length+1));
                        string hexProfileName = dir.Remove(0, (config.ets2Path + "\\profiles").ToString().Length + 1);
                        string profileName = hexToString(hexProfileName);
                        //System.Console.WriteLine(hexProfileName + "  " + profileName);
                        profileDictionary[profileName] = hexProfileName;
                        comboBox13.Items.Add(profileName);
                    }
                    comboBox13.SelectedIndex = 0;
                    this.decoder = new SiiDecoder(config.sii_decryptorPath, config.ets2Path);
                    button18.Enabled = true;
                }
                else
                {
                    comboBox13.Items.Clear();
                    button18.Enabled = false;
                }

        }

        public void clearAll()
        {
            //comboBox5.Items.Clear();
            //comboBox6.Items.Clear();
            //comboBox7.Items.Clear();
            //comboBox8.Items.Clear();

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();

            comboBox9.Items.Clear();
            comboBox10.Items.Clear();
            comboBox11.Items.Clear();
            comboBox12.Items.Clear();


            mainDictionary.Clear();
            RemovedClasses.Clear();
            economyDictionary.Clear();
            bankDictionary.Clear();
            playerDictionary.Clear();
            trailerDictionary.Clear();
            vehicleAccessoryDictionary.Clear();
            vehicleWheelAccessoryDictionary.Clear();

            vehicleAddonAccessoryDictionary.Clear();
            vehiclePaintJobAccessoryDictionary.Clear();
            trailerUtilizationLogDictionary.Clear();
            trailerDefDictionary.Clear();
            vehicleDictionary.Clear();
            profitLogDictionary.Clear();

            profitLogEntryDictionary.Clear();
            driverPlayerDictionary.Clear();
            companyDictionary.Clear();
            jobOfferDataDictionary.Clear();
            garageDictionary.Clear();
            gameProgressDictionary.Clear();
            transportDataDictionary.Clear();

            economyEventQueueDictionary.Clear();
            economyEventDictionary.Clear();
            mailCtrlDictionary.Clear();
            mailDefDictionary.Clear();
            oversizeOfferCtrlDictionary.Clear();
            oversizeRouteOffersDictionary.Clear();
            oversizeOfferDictionary.Clear();
            deliveryLogDictionary.Clear();

            deliveryLogEntryDictionary.Clear();
            policeCtrlDictionary.Clear();
            mapActionDictionary.Clear();
            driverAiDictionary.Clear();
            jobInfoDictionary.Clear();
            registryDictionary.Clear();
            busStopDictionary.Clear();
            busJobLogDictionary.Clear();
            otherDictionary.Clear();

            economy = null;
            playerBank = null;
            player = null;


            


            Vehicle.count = 0;
            Trailer.count = 0;
            TrailerDef.count = 0;

            numericUpDown3.Value = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            button24.BackColor = Color.White;
            button25.BackColor = Color.White;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            label41.Text = "0";
            numericUpDown6.Value = 0;
            label45.Text = "0";
            label46.Text = "0";


        }

        public void creatDictionaries()
        {
            for (int i = 1; i < saveFileData.Length; i = i + 2)
            {
                string[] tempSplit = this.saveFileData[i].Split(':');
                string rest = this.saveFileData[i + 1];
                string type = tempSplit[0].Trim(' ');
                string nameless = tempSplit[1].Trim(' ');

                //mainDictionary[nameless] = new SuperClass(type, nameless, rest);

                if (type == "economy")
                {
                    this.economy = new Economy(type, nameless, rest);
                    economyDictionary[nameless] = this.economy;
                    mainDictionary[nameless] = economyDictionary[nameless];
                }
                else if (type == "bank")
                {
                    this.playerBank = new Bank(type, nameless, rest);
                    bankDictionary[nameless] = this.playerBank;
                    mainDictionary[nameless] = bankDictionary[nameless];
                }
                else if (type == "player")
                {
                    this.player = new Player(type, nameless, rest);
                    playerDictionary[nameless] = this.player;
                    mainDictionary[nameless] = playerDictionary[nameless];
                }
                else if (type == "trailer")
                {
                    trailerDictionary[nameless] = new Trailer(type, nameless, rest);
                    mainDictionary[nameless] = trailerDictionary[nameless];
                }
                else if (type == "vehicle_accessory")
                {
                    vehicleAccessoryDictionary[nameless] = new VehicleAccessory(type, nameless, rest);
                    mainDictionary[nameless] = vehicleAccessoryDictionary[nameless];
                }
                else if (type == "vehicle_wheel_accessory")
                {
                    vehicleWheelAccessoryDictionary[nameless] = new VehicleWheelAccessory(type, nameless, rest);
                    mainDictionary[nameless] = vehicleWheelAccessoryDictionary[nameless];
                }
                else if (type == "vehicle_addon_accessory")
                {
                    vehicleAddonAccessoryDictionary[nameless] = new VehicleAddonAccessory(type, nameless, rest);
                    mainDictionary[nameless] = vehicleAddonAccessoryDictionary[nameless];
                }
                else if (type == "vehicle_paint_job_accessory")
                {
                    vehiclePaintJobAccessoryDictionary[nameless] = new VehiclePaintJobAccessory(type, nameless, rest);
                    mainDictionary[nameless] = vehiclePaintJobAccessoryDictionary[nameless];
                }
                else if (type == "trailer_utilization_log")
                {
                    trailerUtilizationLogDictionary[nameless] = new TrailerUtilizationLog(type, nameless, rest);
                    mainDictionary[nameless] = trailerUtilizationLogDictionary[nameless];
                }
                else if (type == "trailer_def")
                {
                    trailerDefDictionary[nameless] = new TrailerDef(type, nameless, rest);
                    mainDictionary[nameless] = trailerDefDictionary[nameless];
                }
                else if (type == "vehicle")
                {
                    vehicleDictionary[nameless] = new Vehicle(type, nameless, rest);
                    mainDictionary[nameless] = vehicleDictionary[nameless];
                }
                else if (type == "profit_log")
                {
                    profitLogDictionary[nameless] = new ProfitLog(type, nameless, rest);
                    mainDictionary[nameless] = profitLogDictionary[nameless];

                }
                else if (type == "profit_log_entry")
                {
                    profitLogEntryDictionary[nameless] = new ProfitLogEntry(type, nameless, rest);
                    mainDictionary[nameless] = profitLogEntryDictionary[nameless];

                }
                else if (type == "driver_player")
                {
                    driverPlayerDictionary[nameless] = new DriverPlayer(type, nameless, rest);
                    mainDictionary[nameless] = driverPlayerDictionary[nameless];

                }
                else if (type == "company")
                {
                    companyDictionary[nameless] = new Company(type, nameless, rest);
                    mainDictionary[nameless] = companyDictionary[nameless];

                }
                else if (type == "job_offer_data")
                {
                    jobOfferDataDictionary[nameless] = new JobOfferData(type, nameless, rest);
                    mainDictionary[nameless] = jobOfferDataDictionary[nameless];

                }
                else if (type == "garage")
                {
                    garageDictionary[nameless] = new Garage(type, nameless, rest);
                    mainDictionary[nameless] = garageDictionary[nameless];

                }
                else if (type == "game_progress")
                {
                    gameProgressDictionary[nameless] = new GameProgress(type, nameless, rest);
                    mainDictionary[nameless] = gameProgressDictionary[nameless];

                }
                else if (type == "transport_data")
                {
                    transportDataDictionary[nameless] = new TransportData(type, nameless, rest);
                    mainDictionary[nameless] = transportDataDictionary[nameless];

                }
                else if (type == "economy_event_queue")
                {
                    economyEventQueueDictionary[nameless] = new EconomyEventQueue(type, nameless, rest);
                    mainDictionary[nameless] = economyEventQueueDictionary[nameless];

                }
                else if (type == "economy_event")
                {
                    economyEventDictionary[nameless] = new EconomyEvent(type, nameless, rest);
                    mainDictionary[nameless] = economyEventDictionary[nameless];

                }
                else if (type == "mail_ctrl")
                {
                    mailCtrlDictionary[nameless] = new MailCtrl(type, nameless, rest);
                    mainDictionary[nameless] = mailCtrlDictionary[nameless];

                }
                else if (type == "mail_def")
                {
                    mailDefDictionary[nameless] = new MailDef(type, nameless, rest);
                    mainDictionary[nameless] = mailDefDictionary[nameless];

                }
                else if (type == "oversize_offer_ctrl")
                {
                    oversizeOfferCtrlDictionary[nameless] = new OversizeOfferCtrl(type, nameless, rest);
                    mainDictionary[nameless] = oversizeOfferCtrlDictionary[nameless];

                }
                else if (type == "oversize_route_offers")
                {
                    oversizeRouteOffersDictionary[nameless] = new OversizeRouteOffers(type, nameless, rest);
                    mainDictionary[nameless] = oversizeRouteOffersDictionary[nameless];

                }
                else if (type == "oversize_offer")
                {
                    oversizeOfferDictionary[nameless] = new OversizeOffer(type, nameless, rest);
                    mainDictionary[nameless] = oversizeOfferDictionary[nameless];

                }
                else if (type == "delivery_log")
                {
                    deliveryLogDictionary[nameless] = new DeliveryLog(type, nameless, rest);
                    mainDictionary[nameless] = deliveryLogDictionary[nameless];

                }
                else if (type == "delivery_log_entry")
                {
                    deliveryLogEntryDictionary[nameless] = new DeliveryLogEntry(type, nameless, rest);
                    mainDictionary[nameless] = deliveryLogEntryDictionary[nameless];

                }
                else if (type == "police_ctrl")
                {
                    policeCtrlDictionary[nameless] = new PoliceCtrl(type, nameless, rest);
                    mainDictionary[nameless] = policeCtrlDictionary[nameless];

                }
                else if (type == "map_action")
                {
                    mapActionDictionary[nameless] = new MapAction(type, nameless, rest);
                    mainDictionary[nameless] = mapActionDictionary[nameless];

                }
                else if (type == "driver_ai")
                {
                    driverAiDictionary[nameless] = new DriverAi(type, nameless, rest);
                    mainDictionary[nameless] = driverAiDictionary[nameless];

                }
                else if (type == "job_info")
                {
                    jobInfoDictionary[nameless] = new JobInfo(type, nameless, rest);
                    mainDictionary[nameless] = jobInfoDictionary[nameless];

                }
                else if (type == "registry")
                {
                    registryDictionary[nameless] = new Registryy(type, nameless, rest);
                    mainDictionary[nameless] = registryDictionary[nameless];

                }
                else if (type == "bus_stop")
                {
                    busStopDictionary[nameless] = new BusStop(type, nameless, rest);
                    mainDictionary[nameless] = busStopDictionary[nameless];

                }
                else if (type == "bus_job_log")
                {
                    busJobLogDictionary[nameless] = new BusJobLog(type, nameless, rest);
                    mainDictionary[nameless] = busJobLogDictionary[nameless];

                }
                else
                {
                    otherDictionary[nameless] = new Other(type, nameless, rest);
                    mainDictionary[nameless] = otherDictionary[nameless];

                }
            }

            ArrayList temp = new ArrayList();

            foreach (var VARIABLE in trailerDictionary)
            {
                if (VARIABLE.Value.getSlaveTrailerNameless() != null)
                {
                    temp.Add(VARIABLE.Value.getSlaveTrailerNameless());
                }
            }

            foreach (string VARIABLE in temp)
            {
                trailerDictionary.Remove(VARIABLE);
            }
            int id = 1;
            foreach (var VARIABLE in trailerDictionary)
            {
                VARIABLE.Value.setID(id);
                id += 1;
            }
            //id = 1;

        }

        public void ReadSaveFile(string profile)
        {
                string file_path = config.ets2Path + "\\profiles\\" + profile + "\\save\\quicksave\\game.sii";

                string readText = File.ReadAllText(file_path);
                string[] separator = {"{", "}"};

                this.saveFileData = readText.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
                //char[] charsToTrim = { ' ', '\n' };

                for (int i = 0; i < this.saveFileData.Length; i++)
                {
                    this.saveFileData[i] = this.saveFileData[i].Trim('\r', '\n');
                }

                var temp = new List<string>();
                foreach (var s in this.saveFileData)
                {
                    if (!string.IsNullOrEmpty(s))
                        temp.Add(s);
                }

                this.saveFileData = temp.ToArray();
        }

        public void writeSaveFile(string profile)
        {
            /*string s = "SiiNunit\n{\n";
            foreach (var v in this.mainDictionary)
            {
                s = s + v.Value.createWriteString();
            }

            s = s + "}";
            System.Console.WriteLine(s);
            using (StreamWriter writer = new StreamWriter(@"C:\Users\dusha\Desktop\f.txt"))
            {
                writer.WriteLine(s);
            }*/
            string file_path = config.ets2Path + "\\profiles\\" + profile + "\\save\\quicksave\\game.sii";
            using (StreamWriter writer = new StreamWriter(file_path))
            {
                    writer.WriteLine("SiiNunit\n{");
                    foreach (var v in this.mainDictionary)
                    {
                        if (!RemovedClasses.ContainsKey(v.Key))
                        {
                            writer.WriteLine(v.Value.getType() + " : " + v.Value.getNameless() + " {");
                            foreach (var vv in v.Value.GetDictionary())
                            {
                                writer.Write(" " + vv.Key + ": " + vv.Value + "\n");
                            }

                            writer.WriteLine("}\n");
                        }
                    }
                    writer.Write("}");
            }

        }

        public void allOptionHide()
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
                numericUpDown5.Enabled = false;

                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
                comboBox7.Enabled = false;
                comboBox8.Enabled = false;
                comboBox9.Enabled = false;
                comboBox10.Enabled = false;
                comboBox11.Enabled = false;
                comboBox12.Enabled = false;
                comboBox13.Enabled = false;
                comboBox14.Enabled = false;
                comboBox15.Enabled = false;

                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
                button10.Enabled = true;
                button11.Enabled = false;
                button12.Enabled = false;
                button13.Enabled = false;
                button14.Enabled = false;
                button15.Enabled = false;
                button16.Enabled = false;
                button17.Enabled = false;
                button18.Enabled = false;
                button19.Enabled = false;

                textBox1.Enabled = false;
                textBox2.Enabled = false;

            }

        public void toolOptionHide()
        {
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            numericUpDown3.Enabled = false;
            numericUpDown4.Enabled = false;
            numericUpDown5.Enabled = false;

            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            comboBox6.Enabled = false;
            comboBox7.Enabled = false;
            comboBox8.Enabled = false;
            comboBox9.Enabled = false;
            comboBox10.Enabled = false;
            comboBox11.Enabled = false;
            comboBox12.Enabled = false;

            comboBox14.Enabled = false;
            comboBox15.Enabled = false;


            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;
            button13.Enabled = false;
            button14.Enabled = false;
            button15.Enabled = false;
            button16.Enabled = false;

            button20.Enabled = false;
            button21.Enabled = false;
            button26.Enabled = false;


            textBox1.Enabled = false;
            textBox2.Enabled = false;
        }
        
        public string hexToString(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return Encoding.UTF8.GetString(bytes);
        }

        private void button17_Click(object sender, EventArgs e)
        {

            string temp = Method.chooseFolder();
            if (!String.IsNullOrEmpty(temp))
            {
                config.ets2Path = temp;
                Method.Serialize(config,config_path);
            }

            label7.Text = config.ets2Path;

            if (Directory.Exists(config.ets2Path + "\\profiles"))
            {
                comboBox13.Items.Clear();

                foreach (string dir in Directory.GetDirectories(config.ets2Path + "\\profiles"))
                {
                    //System.Console.WriteLine(img.Remove(0, (config.ets2Path + "\\profiles").ToString().Length+1));
                    string hexProfileName = dir.Remove(0, (config.ets2Path + "\\profiles").ToString().Length + 1);
                    string profileName = hexToString(hexProfileName);
                    //System.Console.WriteLine(hexProfileName + "  " + profileName);
                    profileDictionary[profileName] = hexProfileName;
                    comboBox13.Items.Add(profileName);

                }
                comboBox13.SelectedIndex = 0;
                this.decoder = new SiiDecoder(config.sii_decryptorPath, config.ets2Path);
                button18.Enabled = true;
                button19.Enabled = false;
            }
            else
            {
                
                button18.Enabled = false;
                button19.Enabled = false;
            }
        }

        private void comboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            button19.Enabled = false;
            button18.Enabled = true;
            clearAll();
            toolOptionHide();
        }

        private void button18_Click(object sender, EventArgs e)
        {

            string[] activationKey = File.ReadAllText("activation.txt").Split(' ','\r','\n');

            if (!activationKey.Contains(Encryptor.MD5Hash("24f4-6f37-f326-515f" + Environment.UserName)))
            {
                RegisterWindow registerWindow = new RegisterWindow();

                DialogResult result = registerWindow.ShowDialog();

                string key = registerWindow.textBox1.Text;

                if (result == DialogResult.OK)
                {
                    //System.Console.WriteLine(value);
                    if (key == "24f4-6f37-f326-515f")
                    {
                        File.WriteAllText("activation.txt",Encryptor.MD5Hash(key + Environment.UserName));

                        string message = "Activation Successful";
                        string title = "Activation";
                        MessageBox.Show(message, title);
                    }
                    else
                    {
                        string message = "Activation faile";
                        string title = "Activation";
                        MessageBox.Show(message, title);
                    }
                }
            }
            else
            {
                clearAll();
                decoder.decode_file(profileDictionary[comboBox13.SelectedItem.ToString()]);
                ReadSaveFile(profileDictionary[comboBox13.SelectedItem.ToString()]);
                creatDictionaries();
                foreach (var VARIABLE in vehicleDictionary)
                {
                    VARIABLE.Value.setVehicleParams(this, this.truckDetail);
                }
                foreach (var VARIABLE in trailerDictionary)
                {
                    VARIABLE.Value.setTrailerParams(this);
                }
                updateSaveDetailToForm();
                button19.Enabled = true;
            }

            
        }
        
        public TruckDetail GeTruckDetails(string truckDetailPath)
        {
            if (!File.Exists(truckDetailPath))
            {
                Method.SerializeObject(new TruckDetail(), truckDetailPath);
                
            }
            return (TruckDetail)Method.deserializeObject(truckDetailPath);
        }

        public void updateSaveDetailToForm()
        {
            IDictionary<int,Placement> tempDictionary = new Dictionary<int, Placement>();
            int i = 1;
            foreach (var VARIABLE in SavedPlacements)
            {
                tempDictionary.Add(i,VARIABLE.Value);
                i += 1;
            }
            SavedPlacements.Clear();
            foreach (var VARIABLE in tempDictionary)
            {
                SavedPlacements.Add(VARIABLE.Key,VARIABLE.Value);
            }
            Placement.setCount(SavedPlacements.Count);

            Placement.loadToComboBox(comboBox3,this.SavedPlacements,this.player);
            Placement.loadToListBox(listBox4,this.SavedPlacements);
            
            numericUpDown1.Text = this.playerBank.getMoneyAccount().ToString();
            numericUpDown2.Text = this.economy.getExperiencePoints().ToString();
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;

            foreach (var VARIABLE in vehicleDictionary)
            {
                comboBox1.Items.Add(VARIABLE.Value.getDisplayName());
            }
            foreach (var VARIABLE in vehicleDictionary)
            {
                comboBox4.Items.Add(VARIABLE.Value.getDisplayName());
                comboBox9.Items.Add(VARIABLE.Value.getDisplayName());
            }
            //System.Console.WriteLine(trailerDictionary.Count);
            foreach (var VARIABLE in trailerDictionary)
            {
                comboBox11.Items.Add(VARIABLE.Value.getDisplayName());
                comboBox10.Items.Add(VARIABLE.Value.getDisplayName());
            }
            foreach (var VARIABLE in trailerDefDictionary)
            {
                comboBox12.Items.Add(VARIABLE.Value.getDisplayName());
            }

            if (player.getAssignedTruck() != null)
            {
                comboBox1.SelectedItem = vehicleDictionary[player.getAssignedTruck()].getDisplayName();
                textBox1.Text = vehicleDictionary[player.getAssignedTruck()].licensePlate;
                label45.Text = vehicleDictionary[player.getAssignedTruck()]
                    .getDamage(this.vehicleAccessoryDictionary, this.vehicleWheelAccessoryDictionary).ToString();
                

                int r = vehicleDictionary[player.getAssignedTruck()].licensePlateColorR;
                int g = vehicleDictionary[player.getAssignedTruck()].licensePlateColorG;
                int b = vehicleDictionary[player.getAssignedTruck()].licensePlateColorB;

                button24.BackColor = Color.FromArgb(255,r,g,b);
                numericUpDown4.Value = vehicleDictionary[player.getAssignedTruck()].licensePlateTrans;
                if (comboBox14.Items.Contains(vehicleDictionary[player.getAssignedTruck()].licensePlateCity))
                {
                    comboBox14.SelectedItem = vehicleDictionary[player.getAssignedTruck()].licensePlateCity;
                }

                textBox1.Enabled = true;
                button1.Enabled = true;
                numericUpDown4.Enabled = true;
                comboBox14.Enabled = true;
                comboBox1.Enabled = true;


                button9.Enabled = true;
                button7.Enabled = true;
                //truck

                comboBox4.SelectedItem = vehicleDictionary[player.getAssignedTruck()].getDisplayName();
                

            }

            Trailer.updateForm(this);
            Vehicle.updateForm(this);

            comboBox3.Enabled = true;
            comboBox4.Enabled = true;
            comboBox9.Enabled = true;
            comboBox10.Enabled = true;
            comboBox11.Enabled = true;
            
            if (this.hideOption.getisTrailerDef())
            {
                comboBox12.Enabled = true;
            }

            button3.Enabled = true;
            button4.Enabled = true;
            button8.Enabled = true;
            button11.Enabled = true;
            button12.Enabled = true;
            button13.Enabled = true;
            button14.Enabled = true;
            button15.Enabled = true;
            button16.Enabled = true;
            button21.Enabled = true;
            button26.Enabled = true;

            if (player.isTrailerConnected())
            {
                label4.Text = "Trailer Connected :- True";
                label46.Text = trailerDictionary[player.getAssignedTrailer()].getDamage(this.vehicleAccessoryDictionary,
                    this.vehicleWheelAccessoryDictionary).ToString();
                label49.Text = trailerDictionary[player.getAssignedTrailer()].getCargoDamage().ToString();

                comboBox2.Enabled = true;
                
                if (this.hideOption.getisCargoMass())
                {
                    numericUpDown3.Enabled = true;
                }

                if (this.hideOption.getiscargoFix())
                {
                    button20.Enabled = true;
                }

                textBox2.Enabled = true;
                button2.Enabled = true;
                numericUpDown5.Enabled = true;
                comboBox15.Enabled = true;

                if (this.hideOption.getistrailerFix() || this.player.GetDictionary()["current_job"] == "null")
                {
                    button10.Enabled = true;
                }
                else
                {
                    button10.Enabled = false;
                }


                foreach (var VARIABLE in trailerDictionary)
                {
                    comboBox2.Items.Add(VARIABLE.Value.getDisplayName());
                }
                comboBox2.SelectedItem = trailerDictionary[player.getAssignedTrailer()].getDisplayName();
                numericUpDown3.Value = trailerDictionary[player.getAssignedTrailer()].getCargoMass();

                textBox2.Text = trailerDictionary[player.getAssignedTrailer()].licensePlate;

                int r = trailerDictionary[player.getAssignedTrailer()].licensePlateColorR;
                int g = trailerDictionary[player.getAssignedTrailer()].licensePlateColorG;
                int b = trailerDictionary[player.getAssignedTrailer()].licensePlateColorB;

                button25.BackColor = Color.FromArgb(255, r,  g,  b);
                numericUpDown5.Value = trailerDictionary[player.getAssignedTrailer()].licensePlateTrans;
                if (comboBox15.Items.Contains(trailerDictionary[player.getAssignedTrailer()].licensePlateCity))
                {
                    comboBox15.SelectedItem = trailerDictionary[player.getAssignedTrailer()].licensePlateCity;
                }
            }
            else
            {
                label4.Text = "Trailer Connected :- False";

                comboBox2.Enabled = false;
                numericUpDown3.Enabled = false;
                textBox2.Enabled = false;
                button2.Enabled = false;
                numericUpDown5.Enabled = false;
                comboBox15.Enabled = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vehicle tempVehicle = null;
            foreach (var VARIABLE in vehicleDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox1.SelectedItem)
                {
                    tempVehicle = VARIABLE.Value;
                    break;
                }
            }

            if (tempVehicle != null)
            {
                textBox1.Text = tempVehicle.licensePlate;
                button24.BackColor = Color.FromArgb(255, tempVehicle.licensePlateColorR, tempVehicle.licensePlateColorG,
                    tempVehicle.licensePlateColorB);
                numericUpDown4.Value = tempVehicle.licensePlateTrans;
                if (comboBox14.Items.Contains(tempVehicle.licensePlateCity))
                {
                    comboBox14.SelectedItem = tempVehicle.licensePlateCity;
                }
                player.setAssignedTruck(tempVehicle.getNameless());
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Trailer tempTrailer = null;
            foreach (var VARIABLE in trailerDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox2.SelectedItem.ToString())
                {
                    tempTrailer = VARIABLE.Value;
                    break;
                    
                }

            }

            if (tempTrailer != null)
            {
                textBox2.Text = tempTrailer.licensePlate;
                button25.BackColor = Color.FromArgb(255, tempTrailer.licensePlateColorR, tempTrailer.licensePlateColorG,
                    tempTrailer.licensePlateColorB);

                label49.Text = tempTrailer.getCargoDamage().ToString();

                numericUpDown5.Value = tempTrailer.licensePlateTrans;
                if (comboBox15.Items.Contains(tempTrailer.licensePlateCity))
                {
                    comboBox15.SelectedItem = tempTrailer.licensePlateCity;
                }
                player.setAssignedTrailer(tempTrailer.getNameless());

                numericUpDown3.Value = tempTrailer.getCargoMass();
                if (this.hideOption.getisCargoMass())
                {
                    numericUpDown3.Enabled = true;
                }

                if (this.hideOption.getiscargoFix())
                {
                    button20.Enabled = true;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Vehicle tempVehicle = null;
            foreach (var VARIABLE in vehicleDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox1.SelectedItem)
                {
                    tempVehicle = VARIABLE.Value;
                    break;
                }
            }

            if (tempVehicle != null)
            {
                tempVehicle.setNumberPlate(textBox1.Text,comboBox14.Text,(int)numericUpDown4.Value,
                    button24.BackColor.R, button24.BackColor.G, button24.BackColor.B);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ColorDialog dlg = new ColorDialog();
            
            dlg.Color = button24.BackColor;
            dlg.CustomColors = this.colors;
            dlg.ShowDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                button24.BackColor = dlg.Color;
                this.colors = (int[])dlg.CustomColors.Clone();
                Method.SerializeObject(this.colors, "colors.txt");
            }

           
            Vehicle tempVehicle = null;
            foreach (var VARIABLE in vehicleDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox1.SelectedItem)
                {
                    tempVehicle = VARIABLE.Value;
                    break;
                }
            }

            if (tempVehicle != null)
            {
                tempVehicle.setNumberPlate(textBox1.Text, comboBox14.Text, (int)numericUpDown4.Value,
                    button24.BackColor.R, button24.BackColor.G, button24.BackColor.B);
            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            Vehicle tempVehicle = null;
            foreach (var VARIABLE in vehicleDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox1.SelectedItem)
                {
                    tempVehicle = VARIABLE.Value;
                    break;
                }
            }

            if (tempVehicle != null)
            {
                tempVehicle.setNumberPlate(textBox1.Text, comboBox14.Text, (int)numericUpDown4.Value,
                    button24.BackColor.R, button24.BackColor.G, button24.BackColor.B);
            }
        }

        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vehicle tempVehicle = null;
            foreach (var VARIABLE in vehicleDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox1.SelectedItem)
                {
                    tempVehicle = VARIABLE.Value;
                    break;
                }
            }

            if (tempVehicle != null)
            {
                tempVehicle.setNumberPlate(textBox1.Text, comboBox14.Text, (int)numericUpDown4.Value,
                    button24.BackColor.R, button24.BackColor.G, button24.BackColor.B);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            writeSaveFile(profileDictionary[comboBox13.SelectedItem.ToString()]);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            playerBank.setMoneyAccount((long)numericUpDown1.Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            economy.setExperiencePoints((long)numericUpDown2.Value);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            Trailer tempTrailer = null;
            foreach (var VARIABLE in trailerDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox2.SelectedItem.ToString())
                {
                    tempTrailer = VARIABLE.Value;
                    break;

                }

            }

            if (tempTrailer != null)
            {
                tempTrailer.setCargoMass((int)numericUpDown3.Value);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Trailer tempTrailer = null;
            foreach (var VARIABLE in trailerDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox2.SelectedItem.ToString())
                {
                    tempTrailer = VARIABLE.Value;
                    break;
                }
            }

            if (tempTrailer != null)
            {
                tempTrailer.setNumberPlate(textBox2.Text, comboBox15.Text, (int)numericUpDown5.Value,
                    button25.BackColor.R, button25.BackColor.G, button25.BackColor.B);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.CustomColors = this.colors;
            dlg.Color = button25.BackColor;

            dlg.ShowDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                button25.BackColor = dlg.Color;
                this.colors = (int[])dlg.CustomColors.Clone();
                Method.SerializeObject(this.colors,"colors.txt");
            }

            Trailer tempTrailer = null;
            foreach (var VARIABLE in trailerDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox2.SelectedItem.ToString())
                {
                    tempTrailer = VARIABLE.Value;
                    break;
                }
            }

            if (tempTrailer != null)
            {
                tempTrailer.setNumberPlate(textBox2.Text, comboBox15.Text, (int)numericUpDown5.Value,
                    button25.BackColor.R, button25.BackColor.G, button25.BackColor.B);
            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            Trailer tempTrailer = null;
            foreach (var VARIABLE in trailerDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox2.SelectedItem.ToString())
                {
                    tempTrailer = VARIABLE.Value;
                    break;
                }
            }

            if (tempTrailer != null)
            {
                tempTrailer.setNumberPlate(textBox2.Text, comboBox15.Text, (int)numericUpDown5.Value,
                    button25.BackColor.R, button25.BackColor.G, button25.BackColor.B);
            }
        }

        private void comboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            Trailer tempTrailer = null;
            foreach (var VARIABLE in trailerDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox2.SelectedItem.ToString())
                {
                    tempTrailer = VARIABLE.Value;
                    break;
                }
            }

            if (tempTrailer != null)
            {
                tempTrailer.setNumberPlate(textBox2.Text, comboBox15.Text, (int)numericUpDown5.Value,
                    button25.BackColor.R, button25.BackColor.G, button25.BackColor.B);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vehicle tempVehicle = null;
            foreach (var VARIABLE in vehicleDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox4.SelectedItem.ToString())
                {
                    tempVehicle = VARIABLE.Value;
                    break;
                }
            }

            if (tempVehicle != null)
            {
                button9.Enabled = true;
                    label45.Text =
                        tempVehicle.getDamage(this.vehicleAccessoryDictionary, this.vehicleWheelAccessoryDictionary)
                            .ToString();
                    comboBox5.SelectedItem = TruckDetail.seriesToNameDictionary[tempVehicle.engineSeries];
                    comboBox6.Items.Clear();
                    foreach (var VARIABLE in TruckDetail.seresToEngineDictionary[tempVehicle.engineSeries])
                    {
                        comboBox6.Items.Add(VARIABLE.Key);
                    }

                    comboBox6.SelectedItem =
                        TruckDetail.getEngineToEngineName(tempVehicle.engineSeries, tempVehicle.engineCapacity);



                    comboBox8.SelectedItem = TruckDetail.seriesToNameDictionary[tempVehicle.transmissionSeries];
                    comboBox7.Items.Clear();
                    foreach (var VARIABLE in TruckDetail.seresToTransmissionDictionary[tempVehicle.transmissionSeries])
                    {
                        comboBox7.Items.Add(VARIABLE.Key);
                    }

                    comboBox7.SelectedItem =
                        TruckDetail.getTransmissionToTransmissionName(tempVehicle.transmissionSeries,
                            tempVehicle.transmissionType);

                    label41.Text = tempVehicle.getFuelRelative();
                    numericUpDown6.Value = int.Parse(tempVehicle.getFuelRelative().Split('.')[0]);
             
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox6.Items.Clear();
            
            foreach (var VARIABLE in TruckDetail.seresToEngineDictionary[TruckDetail.getNameToSeries(comboBox5.SelectedItem.ToString())])
            {
                comboBox6.Items.Add(VARIABLE.Key);
            }
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox7.Items.Clear();
            
            foreach (var VARIABLE in TruckDetail.seresToTransmissionDictionary[TruckDetail.getNameToSeries(comboBox8.SelectedItem.ToString())])
            {
                comboBox7.Items.Add(VARIABLE.Key);
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vehicle tempVehicle = null;
            foreach (var VARIABLE in vehicleDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox4.SelectedItem.ToString())
                {
                    tempVehicle = VARIABLE.Value;
                    break;
                }
            }

            if (tempVehicle != null)
            {
                //System.Console.WriteLine(comboBox5.Text);
                string series = TruckDetail.getNameToSeries(comboBox5.Text);
                
                if (comboBox6.SelectedItem != null && series != null)
                {
                    //System.Console.WriteLine("AAAAA");
                    string type = TruckDetail.seresToEngineDictionary[series][comboBox6.Text];
                    tempVehicle.setEngine(this, series, type);
                }
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vehicle tempVehicle = null;
            foreach (var VARIABLE in vehicleDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox4.SelectedItem.ToString())
                {
                    tempVehicle = VARIABLE.Value;
                    break;
                }
            }

            if (tempVehicle != null)
            {
                string series = TruckDetail.getNameToSeries(comboBox8.Text);

                if (comboBox7.SelectedItem != null && series != null)
                {
                    string type = TruckDetail.seresToTransmissionDictionary[series][comboBox7.Text];
                    tempVehicle.setTransmission(this, series, type);
                }
            }
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown6.Value >= 1)
            {
                Vehicle tempVehicle = null;
                foreach (var VARIABLE in vehicleDictionary)
                {
                    if (VARIABLE.Value.getDisplayName() == comboBox4.SelectedItem.ToString())
                    {
                        tempVehicle = VARIABLE.Value;
                        break;
                    }
                }

                if (tempVehicle != null)
                {
                    tempVehicle.setFuelRelative(numericUpDown6.Value.ToString());
                    label41.Text = numericUpDown6.Value.ToString();
                }
            }
            
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            Trailer tempTrailer = null;
            foreach (var VARIABLE in trailerDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox11.Text)
                {
                    tempTrailer = VARIABLE.Value;
                    break;
                }
            }

            if (tempTrailer != null)
            {
                if (this.hideOption.getisTrailerDef())
                {
                    comboBox12.Enabled = true;
                }
                if (this.hideOption.getistrailerFix())
                {
                    button10.Enabled = true;
                }

                label46.Text =
                    tempTrailer.getDamage(this.vehicleAccessoryDictionary, this.vehicleWheelAccessoryDictionary).ToString();
                if (tempTrailer.GetTrailerDef()!= null)
                {
                    TrailerDef tempTrailerDef = tempTrailer.GetTrailerDef();
                    label10.Text = tempTrailerDef.getBodyType();
                    label11.Text = tempTrailerDef.getChassiMass();
                    label42.Text = tempTrailerDef.getBodyMass();
                    label43.Text = tempTrailerDef.getLenght();
                    label44.Text = tempTrailerDef.getSourceName();
                    comboBox12.SelectedItem = tempTrailerDef.getDisplayName();
                }
                else
                {
                    comboBox12.SelectedItem = null;
                    label10.Text = "null";
                    label11.Text = "null";
                    label42.Text = "null";
                    label43.Text = "null";
                    label44.Text = "null";
                }
            }
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            Trailer tempTrailer = null;
            foreach (var VARIABLE in trailerDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox11.Text)
                {
                    tempTrailer = VARIABLE.Value;
                    break;

                }

            }

            if (tempTrailer != null)
            {
                TrailerDef tempTrailerDef = null;
                foreach (var VARIABLE in trailerDefDictionary)
                {
                    if (VARIABLE.Value.getDisplayName() == comboBox12.Text)
                    {
                        tempTrailerDef = VARIABLE.Value;
                        break;
                    }
                }
                tempTrailer.setTrailerDef(tempTrailerDef);

                if (tempTrailerDef != null)
                {
                    //TrailerDef tempTrailerDef = tempTrailer.GetTrailerDef();
                    label10.Text = tempTrailerDef.getBodyType();
                    label11.Text = tempTrailerDef.getChassiMass();
                    label42.Text = tempTrailerDef.getBodyMass();
                    label43.Text = tempTrailerDef.getLenght();
                    label44.Text = tempTrailerDef.getSourceName();
                    comboBox12.SelectedItem = tempTrailerDef.getDisplayName();
                }
                else
                {
                    comboBox12.SelectedItem = null;
                    label10.Text = "null";
                    label11.Text = "null";
                    label42.Text = "null";
                    label43.Text = "null";
                    label44.Text = "null";
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();

            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = folderBrowserDialog1.SelectedPath;

                Vehicle tempVehicle = null;
                foreach (var VARIABLE in vehicleDictionary)
                {
                    if (VARIABLE.Value.getDisplayName() == comboBox4.SelectedItem.ToString())
                    {
                        tempVehicle = VARIABLE.Value;
                        break;
                    }
                }
                
                if (tempVehicle != null)
                {
                    IDictionary<int,SuperClass> exportTruck = new Dictionary<int,SuperClass>();
                    exportTruck.Add(0,tempVehicle);

                    int accCount = tempVehicle.getAccessoriesCount();
                    
                    Dictionary<int, string> namelessArrayList = tempVehicle.GetAccessoriesNamelessDictionary();
                    for (int i = 0; i < accCount; i++)
                    {
                        exportTruck.Add(i+1,mainDictionary[namelessArrayList[i]]);

                        //System.Console.WriteLine(i + mainDictionary[namelessArrayList[i].ToString()].getNameless());
                    }

                    string filePath = folderName + @"\" + tempVehicle.getDisplayName() + ".tru";
                    //System.Console.WriteLine(filePath);
                    Method.SerializeObject(exportTruck,filePath);
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Vehicle tempVehicle = null;
            foreach (var VARIABLE in vehicleDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox4.SelectedItem.ToString())
                {
                    tempVehicle = VARIABLE.Value;
                    break;
                }
            }

            if (tempVehicle != null)
            {
                try
                {
                    string path = Method.chooseFileWithEx("tru");

                    if (File.Exists(path))
                    {
                        IDictionary<int, SuperClass> importedVehicleDict = (Dictionary<int, SuperClass>)Method.deserializeObject(path);

                        Vehicle importedVehicle = (Vehicle)importedVehicleDict[0];

                        foreach (var VARIABLE in tempVehicle.GetAccessoriesNamelessDictionary())
                        {
                            RemovedClasses.Add(VARIABLE.Value, mainDictionary[VARIABLE.Value]);
                        }
                        importedVehicleDict.Remove(0);
                        importedVehicle.setNameless(tempVehicle.getNameless());
                        this.mainDictionary[tempVehicle.getNameless()] = importedVehicle;

                        foreach (var VARIABLE in importedVehicleDict)
                        {
                            this.mainDictionary.Add(VARIABLE.Value.getNameless(), VARIABLE.Value);

                        }
                    }
                }
                catch (Exception exception)
                {
                    string title = "Error";
                    MessageBox.Show(exception.Message, title);
                }
                
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Trailer tempTrailer = null;
            foreach (var VARIABLE in trailerDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox11.SelectedItem.ToString())
                {
                    tempTrailer = VARIABLE.Value;
                    break;
                }
            }

            try
            {
                if (tempTrailer != null)
                {
                    string path = Method.chooseFileWithEx("tre");

                    if (File.Exists(path))
                    {
                        IDictionary<int, SuperClass> importedTrailerDict =
                            (Dictionary<int, SuperClass>) Method.deserializeObject(path);

                        Trailer importedTrailer = (Trailer) importedTrailerDict[0];
                        /*foreach (var VARIABLE in tempVehicle.GetAccessoriesNamelessDictionary())
                        {
                            this.mainDictionary.Remove(VARIABLE.Value);
                        }*/
                        foreach (string VARIABLE in tempTrailer.GetAccessoriesNamelessArray())
                        {
                            RemovedClasses.Add(VARIABLE, mainDictionary[VARIABLE]);
                        }

                        importedTrailerDict.Remove(0);
                        importedTrailer.setNameless(tempTrailer.getNameless());
                        importedTrailer.setTrailerDefNameless(tempTrailer.getTrailerDefNamless());
                        this.mainDictionary[tempTrailer.getNameless()] = importedTrailer;

                        //int n = 0;

                        foreach (var VARIABLE in importedTrailerDict)
                        {
                            this.mainDictionary.Add(VARIABLE.Value.getNameless(), VARIABLE.Value);
                            //System.Console.WriteLine(n + "    " + VARIABLE.Value.getNameless());
                            //n += 1;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }

            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();

            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = folderBrowserDialog1.SelectedPath;

                Trailer tempTrailer = null;
                foreach (var VARIABLE in trailerDictionary)
                {
                    if (VARIABLE.Value.getDisplayName() == comboBox11.SelectedItem.ToString())
                    {
                        tempTrailer = VARIABLE.Value;
                        break;
                    }
                }

                if (tempTrailer != null)
                {
                    IDictionary<int, SuperClass> exportTrailer = new Dictionary<int, SuperClass>();
                    exportTrailer.Add(0, tempTrailer);

                    int accCount = tempTrailer.getAccessoriesCount();
                    
                    ArrayList namelessArrayList = tempTrailer.GetAccessoriesNamelessArray();

                    /*System.Console.WriteLine(namelessArrayList.Count);
                    foreach(string s in namelessArrayList)
                    {
                        System.Console.WriteLine(s);
                    }*/

                    int i = 1;
                    foreach (string VARIABLE in namelessArrayList)
                    {
                        exportTrailer.Add(i, mainDictionary[VARIABLE]);
                        i += 1;
                    }

                    string filePath = folderName + @"\" + tempTrailer.getDisplayName() + ".tre";

                    Method.SerializeObject(exportTrailer, filePath);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Vehicle tempVehicle = null;
            foreach (var VARIABLE in vehicleDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox4.SelectedItem.ToString())
                {
                    tempVehicle = VARIABLE.Value;
                    break;
                }
            }

            if (tempVehicle != null)
            {
                tempVehicle.damageFix(this.vehicleAccessoryDictionary,this.vehicleWheelAccessoryDictionary);
                label45.Text = "0";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Trailer tempTrailer = null;
            foreach (var VARIABLE in trailerDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox11.SelectedItem.ToString())
                {
                    tempTrailer = VARIABLE.Value;
                    break;
                }
            }

            tempTrailer.damageFix(this.vehicleAccessoryDictionary,this.vehicleWheelAccessoryDictionary);
            label46.Text = "0";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Trailer tempTrailer = null;
            foreach (var VARIABLE in trailerDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox2.SelectedItem.ToString())
                {
                    tempTrailer = VARIABLE.Value;
                    break;

                }

            }

            if (tempTrailer != null)
            {
                label49.Text = "0";
                tempTrailer.fixCargoDamage();
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string message = "Do you want to purchase all garage?";
            string title = "Garage Purchase";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                foreach (var VARIABLE in this.garageDictionary)
                {
                    if (VARIABLE.Value.GetDictionary()["status"] == "0" || VARIABLE.Value.GetDictionary()["status"] == "1")
                    {
                        VARIABLE.Value.GetDictionary()["status"] = "3";

                        VARIABLE.Value.GetDictionary()["vehicles"] = "5";
                        VARIABLE.Value.GetDictionary()["drivers"] = "5";

                        VARIABLE.Value.GetDictionary()["vehicles[0]"] = "null";
                        VARIABLE.Value.GetDictionary()["vehicles[1]"] = "null";
                        VARIABLE.Value.GetDictionary()["vehicles[2]"] = "null";
                        VARIABLE.Value.GetDictionary()["vehicles[3]"] = "null";
                        VARIABLE.Value.GetDictionary()["vehicles[4]"] = "null";

                        VARIABLE.Value.GetDictionary()["drivers[0]"] = "null";
                        VARIABLE.Value.GetDictionary()["drivers[1]"] = "null";
                        VARIABLE.Value.GetDictionary()["drivers[2]"] = "null";
                        VARIABLE.Value.GetDictionary()["drivers[3]"] = "null";
                        VARIABLE.Value.GetDictionary()["drivers[4]"] = "null";
                    }
                }
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            string message = "DAF - Bremen\n" +
                             "IVECO - Hamburg\n" +
                             "MAN - Berlin\n" +
                             "Mercedes Benz - Rotterdam\n" + 
                             "Renult - Dusseldorf\n" + 
                             "Scania - Hannover\n" + 
                             "Volvo - Luxembourg\n\n" +
                             "Do you want to discover this truck dealers?";




            string title = "Discover Truck Dealers";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                ArrayList citiList = new ArrayList()
                {
                    "bremen",
                    "hamburg",
                    "berlin",
                    "rotterdam",
                    "dusseldorf",
                    "hannover",
                    "luxembourg"
                };

                int unlocked_delers_count = int.Parse(this.economy.GetDictionary()["unlocked_dealers"]);

                ArrayList unlocked_delers = new ArrayList();

                for (int i = 0; i < unlocked_delers_count; i++)
                {
                    unlocked_delers.Add(this.economy.GetDictionary()["unlocked_dealers[" + i + "]"]);
                }

                foreach (string dealer in citiList)
                {
                    if (!unlocked_delers.Contains(dealer))
                    {
                        this.economy.GetDictionary()["unlocked_dealers[" + this.economy.GetDictionary()["unlocked_dealers"] + "]"] = dealer;
                        this.economy.GetDictionary()["unlocked_dealers"] =
                            (int.Parse(this.economy.GetDictionary()["unlocked_dealers"]) + 1).ToString();
                    }
                }
            }


            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string value = null;
            if (InputBox("Placement Name", "Enter Name :-", ref value) == DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(value))
                {
                    Placement.createThisPlacement(value,this.player,this.SavedPlacements);
                    Placement.loadToComboBox(comboBox3,this.SavedPlacements,this.player);
                    Placement.loadToListBox(listBox4, this.SavedPlacements);
                }
            }
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Placement.addPlacementToPlayer(this.player,Placement.GetSelectedPlacementFromComboBox(comboBox3, this.SavedPlacements));
            
            //Placement temPlacement = Placement.GetSelectedPlacementFromComboBox(comboBox3, this.SavedPlacements);
            //System.Console.WriteLine(temPlacement.getTruckPlacement());
            //System.Console.WriteLine(temPlacement.getTrailerPlacement());
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Placement.RemovePlacementFromList(Placement.GetSelectedPlacementFromListBox(listBox4,this.SavedPlacements), this.SavedPlacements);
            Placement.loadToListBox(listBox4,this.SavedPlacements);
            Placement.loadToComboBox(comboBox3,this.SavedPlacements,this.player);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Vehicle tempVehicle = null;
            foreach (var VARIABLE in vehicleDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox4.SelectedItem.ToString())
                {
                    tempVehicle = VARIABLE.Value;
                    break;
                }
            }

            if (tempVehicle != null)
            {
                Vehicle temp = null;
                foreach (var VARIABLE in vehicleDictionary)
                {
                    if (VARIABLE.Value.getDisplayName() == comboBox9.SelectedItem.ToString())
                    {
                        temp = VARIABLE.Value;
                        break;
                    }
                }

                tempVehicle.setVehiclePaintJob(this.vehiclePaintJobAccessoryDictionary, temp.getVehiclePaintJob());
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Trailer tempTrailer = null;
            foreach (var VARIABLE in trailerDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == comboBox11.Text)
                {
                    tempTrailer = VARIABLE.Value;
                    break;
                }
            }

            if (tempTrailer != null)
            {
                Trailer temp = null;
                foreach (var VARIABLE in trailerDictionary)
                {
                    if (VARIABLE.Value.getDisplayName() == comboBox10.Text)
                    {
                        temp = VARIABLE.Value;
                        break;
                    }
                }
                tempTrailer.setTrailerPaintJob(vehiclePaintJobAccessoryDictionary, temp.getTrailerPaintJob());
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            try
            {
                string path = Method.chooseFileWithEx("pla");

                if (File.Exists(path))
                {
                    Placement tempPlacement = (Placement) Method.deserializeObject(path);
                    Placement.AddPlascementToList(tempPlacement, this.SavedPlacements);
                    Placement.loadToListBox(listBox4, this.SavedPlacements);
                    Placement.loadToComboBox(comboBox3, this.SavedPlacements, this.player);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
            
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            Placement tempPlacement = Placement.GetSelectedPlacementFromListBox(listBox4, this.SavedPlacements);

            var folderBrowserDialog1 = new FolderBrowserDialog();

            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = folderBrowserDialog1.SelectedPath;

                if (tempPlacement != null)
                {

                    string filePath = folderName + @"\" + tempPlacement.getDisplayName() + ".pla";

                    Method.SerializeObject(tempPlacement, filePath);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Advance a = new Advance();
            a.Show(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool bFormNameOpen = false;
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "TruckAcc")
                {
                    bFormNameOpen = true;
                }
            }

            if (!bFormNameOpen)
            {
                TruckAcc.getInstance().Show(this);
            }
            else
            {
                TruckAcc.getInstance().BringToFront();
            }
            
        }

        private void button22_Click(object sender, EventArgs e)
        {
            textBox3.Text = Ets2TelemetryDataReader.Instance.Read().Truck.Placement.X.ToString();
            textBox4.Text = Ets2TelemetryDataReader.Instance.Read().Truck.Placement.Y.ToString();
            textBox5.Text = Ets2TelemetryDataReader.Instance.Read().Truck.Placement.Z.ToString();

            textBox6.Text = Ets2TelemetryDataReader.Instance.Read().Trailer.Placement.X.ToString();
            textBox7.Text = Ets2TelemetryDataReader.Instance.Read().Trailer.Placement.Y.ToString();
            textBox8.Text = Ets2TelemetryDataReader.Instance.Read().Trailer.Placement.Z.ToString();

            textBox11.Text = Ets2TelemetryDataReader.Instance.Read().Truck.Placement.Heading.ToString();
            textBox10.Text = Ets2TelemetryDataReader.Instance.Read().Truck.Placement.Pitch.ToString();
            textBox9.Text = Ets2TelemetryDataReader.Instance.Read().Truck.Placement.Roll.ToString();

            textBox14.Text = Ets2TelemetryDataReader.Instance.Read().Trailer.Placement.Heading.ToString();
            textBox13.Text = Ets2TelemetryDataReader.Instance.Read().Trailer.Placement.Pitch.ToString();
            textBox12.Text = Ets2TelemetryDataReader.Instance.Read().Trailer.Placement.Roll.ToString();
        }

        private void button29_Click(object sender, EventArgs e)
        {

        }

        private void button30_Click(object sender, EventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {

        }

        private void button27_Click(object sender, EventArgs e)
        {

        }
    }
}
