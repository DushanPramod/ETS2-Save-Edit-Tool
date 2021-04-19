using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public static class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>


        public static Form4 mainF4;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainF4 = new Form4();
            Application.Run(mainF4);
            //Application.Run(f1);
        }
    }
}
