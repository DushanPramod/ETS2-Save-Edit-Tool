using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp6.HelpDataClasses;

namespace WindowsFormsApp6.classes
{
    public class Player : SuperClass
    {
        public Player(string type, string nameless, string rest) : base(type, nameless, rest)
        {

        }

        public string getAssignedTruck()
        {
            string s = this.dict["assigned_truck"].Trim(' ', '\r', '\n');
            if (!s.Contains("nameless"))
            {
                return null;
            }
            else
            {
                return s;
            }
            
        }

        public ArrayList GetTrailerNamelessArrayList()
        {
            int trailerCount = int.Parse(this.dict["trailers"]);

            ArrayList temp = new ArrayList();
            for (int i = 0; i < trailerCount; i++)
            {
                temp.Add(this.dict["trailers[" + i + "]"]);
            }

            return temp;
        }

        public void setAssignedTruck(string nameless)
        {
            if (this.dict["assigned_truck"].Trim(' ', '\r', '\n').Equals(this.dict["my_truck"].Trim(' ', '\r', '\n')))
            {
                this.dict["assigned_truck"] = nameless;
                this.dict["my_truck"] = nameless;
            }
            else
            {
                this.dict["assigned_truck"] = nameless;
            }
            
        }

        public void setAssignedTrailer(string nameless)
        {
            if (this.dict["assigned_trailer"].Trim(' ', '\r', '\n').Equals(this.dict["my_trailer"].Trim(' ', '\r', '\n')))
            {
                this.dict["assigned_trailer"] = nameless;
                this.dict["my_trailer"] = nameless;
            }
            else
            {
                this.dict["assigned_trailer"] = nameless;
            }
            
        }

        public string getAssignedTrailer()
        {
            return this.dict["assigned_trailer"].Trim(' ', '\r', '\n');
        }

        public bool isTrailerConnected()
        {
            string b = this.dict["assigned_trailer_connected"].Trim(' ', '\r', '\n');
            if (b == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void setPlacements(Placement placement)
        {
            this.dict["truck_placement"] = placement.getTruckPlacement();
            this.dict["trailer_placement"] = placement.getTrailerPlacement();

        }

        public bool isJobConnected()
        {
            if (this.dict["current_job"] == "null")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool isSingleTrailer()
        {
            if (this.dict["slave_trailer_placements"] == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
