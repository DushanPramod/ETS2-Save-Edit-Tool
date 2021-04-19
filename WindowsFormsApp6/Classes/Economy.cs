using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.classes
{
    public class Economy : SuperClass
    {
        private long experience_points;
        private long bus_experience_points;
        public Economy(string type, string nameless, string rest) : base(type, nameless,rest)
        {
            this.experience_points = Int64.Parse(this.dict["experience_points"]);
        }

        public long getExperiencePoints()
        {
            return this.experience_points;
        }

        public void setExperiencePoints(long l)
        {
            this.experience_points = l;
            this.dict["experience_points"] = l.ToString();
        }

        public long getBusExperiencePoints()
        {
            return this.experience_points;
        }

        public void setBusExperiencePoints(long l)
        {
            this.bus_experience_points = l;
            this.dict["experience_points"] = l.ToString();
        }


    }
}
