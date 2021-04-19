using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.HelpDataClasses
{
    class OptinHide
    {
        private bool iscargoMass;
        private bool iscargoFix;
        private bool istrailerFix;
        private bool istrailerDef;

        public OptinHide()
        {
            this.iscargoMass = false;
            this.iscargoFix = false;
            this.istrailerFix = false;
            this.istrailerDef = false;
        }
        public bool getisCargoMass()
        {
            return this.iscargoMass;
        }

        public bool getiscargoFix()
        {
            return this.iscargoFix;
        }

        public bool getistrailerFix()
        {
            return this.istrailerFix;
        }

        public bool getisTrailerDef()
        {
            return this.istrailerDef;
        }



        public void setisCargoMass(bool b)
        {
            this.iscargoMass = b;
        }

        public void setiscargoFix(bool b)
        {
           this.iscargoFix = b;
        }

        public void setistrailerFix(bool b)
        {
            this.istrailerFix = b;
        }

        public void setisTrailerDef(bool b)
        {
            this.istrailerDef = b;
        }
    }
}
