using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class TruckAcc : Form
    {
        private Form4 f4;

        public static TruckAcc instance = null;
        private TruckAcc()
        {
            InitializeComponent();
        }

        public static TruckAcc getInstance()
        {
            if (instance == null)
            {
                instance = new TruckAcc();
            }

            return instance;
        }

        private void TruckAcc_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            richTextBox1.Text = "";

            f4 = (Form4) Owner;
            foreach (var VARIABLE in f4.savedTruckItemDictionary)
            {
                listBox1.Items.Add(VARIABLE.Key);
            }

            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
                richTextBox1.Text = f4.savedTruckItemDictionary[listBox1.SelectedItem.ToString()];
            }
            
        }

        private void TruckAcc_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
            //this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem!=null)
            {
                richTextBox1.Text = f4.savedTruckItemDictionary[listBox1.SelectedItem.ToString()];
            }
            else
            {
                richTextBox1.Text = "";
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                f4.savedTruckItemDictionary.Remove(listBox1.SelectedItem.ToString());
                listBox1.Items.Remove(listBox1.SelectedItem);
                richTextBox1.Text = "";
                Method.SerializeObject(f4.savedTruckItemDictionary, "truckAccessories.txt");
            }
           
        }


    }
}
