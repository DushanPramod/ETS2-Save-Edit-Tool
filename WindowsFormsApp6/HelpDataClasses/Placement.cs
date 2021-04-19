using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.classes;

namespace WindowsFormsApp6.HelpDataClasses
{
    [Serializable]
    public class Placement
    {
        private static int count;
        private int id;
        private string name;
        private string truck_placement;
        private string trailer_placement;
        private bool isTrailerConnected;

        public Placement(string name , Player player)
        {
            this.name = name;

            this.truck_placement = player.GetDictionary()["truck_placement"];
            this.trailer_placement = player.GetDictionary()["trailer_placement"];
            if (player.isTrailerConnected())
            {
                this.isTrailerConnected = true;
            }
            else
            {
                this.isTrailerConnected = false;
            }
            

            count += 1;
            id = count;
        }

        public int getID()
        {
            return this.id;
        }

        public void setID(int id)
        {
            this.id = id;
        }

        public static int getCount()
        {
            return count;
        }

        public static void setCount(int c)
        {
            count = c;
        }

        public string getDisplayName()
        {
            if (isTrailerConnected)
            {
                return id + ") T&T " + name;
            }
            else
            {
                return id + ") T " + name;
            }
        }

        public static void createThisPlacement(string name, Player player, IDictionary<int,Placement> placements)
        {

            if (int.Parse(player.GetDictionary()["slave_trailer_placements"]) > 0)
            {
                string message = "This option only for single trailer";
                string title = "Error";
                MessageBox.Show(message, title);
            }
            else
            {
                Placement.setCount(placements.Count);
                Placement temp = new Placement(name, player);
                placements.Add(temp.id, temp);
                WritePlacement(placements);
            }
           
        }

        public static void loadToComboBox(ComboBox box, IDictionary<int,Placement> placements, Player player)
        {
            box.Items.Clear();

            if (player.isJobConnected())
            {
                box.Enabled = false;
            }
            else
            {
               
                foreach (var VARIABLE in placements)
                {
                    box.Items.Add(VARIABLE.Value.getDisplayName());

                }
            }
        }

        public static void loadToListBox(ListBox box, IDictionary<int, Placement> placements)
        {
            box.Items.Clear();
            foreach (var VARIABLE in placements)
            {
                box.Items.Add(VARIABLE.Value.getDisplayName());
            }
        }

        public static void addPlacementToPlayer(Player player, Placement placement)
        {
            if (!player.isJobConnected())
            {
                if (placement.isTrailerConnected && !player.isTrailerConnected())
                {
                    string message = "Trailer not Attached";
                    string title = "Error";
                    MessageBox.Show(message, title);
                }
                else if (!placement.isTrailerConnected && player.isTrailerConnected())
                {
                    string message = "Disconnect your trailer";
                    string title = "Error";
                    MessageBox.Show(message, title);
                }
                else
                {
                    if (!player.isSingleTrailer())
                    {
                        string message = "This option only for single trailer";
                        string title = "Error";
                        MessageBox.Show(message, title);
                    }
                    else
                    {
                        player.setPlacements(placement);
                    }
                    
                }
            }
            else
            {
                string message = "Currently you have a delivery job";
                string title = "Error";
                MessageBox.Show(message, title);
            }
            
        }

        public static Placement GetSelectedPlacementFromComboBox(ComboBox box, IDictionary<int, Placement> placements)
        {
            if (box.SelectedItem != null)
            {
                foreach (var VARIABLE in placements)
                {
                    if (VARIABLE.Value.getDisplayName() == box.SelectedItem.ToString())
                    {
                        return VARIABLE.Value;
                    }
                }
            }
            
            return null;
        }

        public static Placement GetSelectedPlacementFromListBox(ListBox box, IDictionary<int, Placement> placements)
        {
            if (box.SelectedItem != null)
            {
                foreach (var VARIABLE in placements)
                {
                    if (VARIABLE.Value.getDisplayName() == box.SelectedItem.ToString())
                    {
                        return VARIABLE.Value;
                    }
                }

            }

            return null;
        }

        public static void RemovePlacementFromList(Placement placement,IDictionary<int, Placement> placements)
        {
            if (placement != null)
            {
                if (placements.ContainsKey(placement.id))
                {
                    placements.Remove(placement.id);
                }
                Placement.WritePlacement(placements);
            }
            
        }

        public static void AddPlascementToList(Placement placement, IDictionary<int, Placement> placements)
        {
            placement.setID(Placement.count+1);
            Placement.count += 1;
            placements.Add(placement.getID(),placement);
        }

        public string getTruckPlacement()
        {
            return this.truck_placement;
        }

        public string getTrailerPlacement()
        {
            return this.trailer_placement;
        }

        public bool getIsTrailerConnected()
        {
            return this.isTrailerConnected;
        }

        public static IDictionary<int, Placement> ReadPlacementsDictionary()
        {
            if (File.Exists("placements.txt"))
            {
                return (IDictionary<int, Placement>)Method.deserializeObject("placements.txt");
            }
            else
            {
                IDictionary<int,Placement> temp = new Dictionary<int, Placement>();
                Method.SerializeObject(temp, "placements.txt");
                return temp;
            }
        }

        public static void WritePlacement(IDictionary<int,Placement> placements)
        {
            Method.SerializeObject(placements, "placements.txt");
        }
    }
}
