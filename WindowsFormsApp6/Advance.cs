using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp6.classes;

namespace WindowsFormsApp6
{
    public partial class Advance : Form
    {
        private Form4 f4;

        List<TextBox> accossoryTextBoxList = new List<TextBox>();
        List<TextBox> namelessTextBoxList = new List<TextBox>();
        List<RichTextBox> richTextBoxList = new List<RichTextBox>();
        List<Label> accossoryNumberLabels = new List<Label>();
        List<Button> removeButtons = new List<Button>();
        List<Button> saveButtons = new List<Button>();

        private TextBox vehicleTextBox;
        private TextBox namelessTextBox;
        private RichTextBox vehicleRichTextBox;

        Vehicle tempVehicle = null;

        private Button addButton;
        private Button okButton;
        private Button cancelButton;


        public Advance()
        {
            InitializeComponent();
        }

        private void Advance_Load(object sender, EventArgs e)
        {
            f4 = (Form4) this.Owner;

            
            foreach (var VARIABLE in f4.vehicleDictionary)
            {
                if (VARIABLE.Value.getDisplayName() == f4.comboBox4.SelectedItem.ToString())
                {
                    tempVehicle = VARIABLE.Value;
                    break;
                }
            }

            if (tempVehicle != null)
            {
                this.Text = tempVehicle.getDisplayName();

                vehicleTextBox = new TextBox();
                this.Controls.Add(vehicleTextBox);
                vehicleTextBox.Top = 25;
                vehicleTextBox.Left = 25;
                vehicleTextBox.Width = 250;
                vehicleTextBox.Height = 30;
                vehicleTextBox.Text = tempVehicle.getType();
                vehicleTextBox.Font = new Font(Font.FontFamily, 10);

                namelessTextBox = new TextBox();
                this.Controls.Add(namelessTextBox);
                namelessTextBox.Top = 25;
                namelessTextBox.Left = 300;
                namelessTextBox.Width = 200;
                namelessTextBox.Height = 30;
                namelessTextBox.Text = tempVehicle.getNameless();
                namelessTextBox.Font = new Font(Font.FontFamily, 10);

                vehicleRichTextBox = new RichTextBox();
                this.Controls.Add(vehicleRichTextBox);
                vehicleRichTextBox.Top = 55;
                vehicleRichTextBox.Left = 45;
                vehicleRichTextBox.Width = 700;
                vehicleRichTextBox.Height = tempVehicle.dict.Count * 20;
                vehicleRichTextBox.Font = new Font(Font.FontFamily, 11);
                vehicleRichTextBox.WordWrap = false;

                foreach (var VARIABLE in tempVehicle.dict)
                {
                    vehicleRichTextBox.Text = vehicleRichTextBox.Text + VARIABLE.Key + ": " + VARIABLE.Value + "\n";
                }



                int i = 0;
                int bottonPlace = vehicleRichTextBox.Bottom;

                foreach (var VARIABLE in tempVehicle.GetAccessoriesNamelessDictionary())
                {
                    accossoryTextBoxList.Add(new TextBox());
                    namelessTextBoxList.Add(new TextBox());
                    richTextBoxList.Add(new RichTextBox());
                    accossoryNumberLabels.Add(new Label());
                    removeButtons.Add(new Button());
                    saveButtons.Add(new Button());

                    this.Controls.Add(accossoryTextBoxList[i]);
                    this.Controls.Add(namelessTextBoxList[i]);
                    this.Controls.Add(richTextBoxList[i]);
                    this.Controls.Add(accossoryNumberLabels[i]);
                    this.Controls.Add(removeButtons[i]);
                    this.Controls.Add(saveButtons[i]);


                    accossoryTextBoxList[i].Top = bottonPlace + 25;
                    accossoryTextBoxList[i].Left = 100;
                    accossoryTextBoxList[i].Width = 250;
                    accossoryTextBoxList[i].Height = 30;
                    accossoryTextBoxList[i].Text = f4.mainDictionary[VARIABLE.Value].getType();
                    accossoryTextBoxList[i].Font = new Font(Font.FontFamily, 10);


                    namelessTextBoxList[i].Top = bottonPlace + 25;
                    namelessTextBoxList[i].Left = 375;
                    namelessTextBoxList[i].Width = 200;
                    namelessTextBoxList[i].Height = 30;
                    namelessTextBoxList[i].Text = f4.mainDictionary[VARIABLE.Value].getNameless();
                    namelessTextBoxList[i].Font = new Font(Font.FontFamily, 10);

                    accossoryNumberLabels[i].Top = bottonPlace + 25;
                    accossoryNumberLabels[i].Left = 25;
                    accossoryNumberLabels[i].Text = VARIABLE.Key.ToString();
                    accossoryNumberLabels[i].Font = new Font(Font.FontFamily, 10);


                    removeButtons[i].Top = bottonPlace + 25;
                    removeButtons[i].Left = namelessTextBoxList[i].Right + 25;
                    removeButtons[i].Text = "Remove";
                    removeButtons[i].BackColor = Color.Red;
                    removeButtons[i].Click += new EventHandler(RemoveButton);


                    saveButtons[i].Top = bottonPlace + 25;
                    saveButtons[i].Left = removeButtons[i].Right + 10;
                    saveButtons[i].Text = "Save to List";
                    saveButtons[i].BackColor = Color.DarkSlateBlue;
                    saveButtons[i].Click += new EventHandler(SaveAccosory);


                    richTextBoxList[i].Top = bottonPlace + 55;
                    richTextBoxList[i].Left = 40;
                    richTextBoxList[i].Width = 700;
                    richTextBoxList[i].Height = f4.mainDictionary[VARIABLE.Value].dict.Count * 22;
                    richTextBoxList[i].Font = new Font(Font.FontFamily, 11);
                    richTextBoxList[i].WordWrap = false;

                    foreach (var VARIABLE2 in f4.mainDictionary[VARIABLE.Value].dict)
                    {
                        richTextBoxList[i].Text =
                            richTextBoxList[i].Text + VARIABLE2.Key + ": " + VARIABLE2.Value + "\n";
                    }

                    bottonPlace = richTextBoxList[i].Bottom;
                    i += 1;
                }

                addButton = new Button();
                this.Controls.Add(addButton);
                addButton.Top = richTextBoxList[richTextBoxList.Count - 1].Bottom + 25;
                addButton.Left = namelessTextBoxList[0].Right + 25;
                addButton.BackColor = Color.Lime;
                addButton.Text = "Add";
                addButton.Click += new EventHandler(AddNew);

                okButton = new Button();
                this.Controls.Add(okButton);
                okButton.Top = addButton.Bottom + 30;
                okButton.Left = addButton.Left - 50;
                okButton.BackColor = Color.Chartreuse;
                okButton.Text = "Ok";
                okButton.Click += new EventHandler(OkButton);

                cancelButton = new Button();
                this.Controls.Add(cancelButton);
                cancelButton.Top = addButton.Bottom + 30;
                cancelButton.Left = okButton.Right + 15;
                cancelButton.BackColor = Color.Coral;
                cancelButton.Text = "Cancel";
                cancelButton.Click += new EventHandler(CancelButton);
            }
            else
            {
                string message = "Vehicle not selected";
                string title = "Error";
                MessageBox.Show(message, title);
                this.Close();

            }
        }

        private void RemoveButton(object sender, EventArgs e)
        {
            string message = "Do you want to remove this accessory?";
            string title = "Remove accessory";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                int index = removeButtons.IndexOf((Button) sender);

                string nameless = namelessTextBoxList[index].Text.Trim(' ');
                if (f4.mainDictionary.ContainsKey(nameless))
                {
                    f4.mainDictionary.Remove(nameless);
                }

                accossoryTextBoxList[index].Enabled = false;
                accossoryTextBoxList.RemoveAt(index);

                namelessTextBoxList[index].Enabled = false;
                namelessTextBoxList.RemoveAt(index);

                accossoryNumberLabels[index].Text = "Removed";
                accossoryNumberLabels[index].Enabled = false;
                accossoryNumberLabels.RemoveAt(index);

                richTextBoxList[index].Enabled = false;
                richTextBoxList.RemoveAt(index);

                saveButtons[index].Enabled = false;
                saveButtons.RemoveAt(index);

                removeButtons[index].Enabled = false;
                removeButtons.RemoveAt(index);

                for (int i = 0; i < accossoryNumberLabels.Count; i++)
                {
                    accossoryNumberLabels[i].Text = i.ToString();
                }


            }
        }

        private void AddNew(object sender, EventArgs e)
        {
            string message = "Do you want to add new accessory?";
            string title = "Add accessory";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {

                accossoryTextBoxList.Add(new TextBox());
                namelessTextBoxList.Add(new TextBox());
                richTextBoxList.Add(new RichTextBox());
                accossoryNumberLabels.Add(new Label());
                removeButtons.Add(new Button());
                saveButtons.Add(new Button());

                int i = accossoryTextBoxList.Count - 1;

                this.Controls.Add(accossoryTextBoxList[i]);
                this.Controls.Add(namelessTextBoxList[i]);
                this.Controls.Add(richTextBoxList[i]);
                this.Controls.Add(accossoryNumberLabels[i]);
                this.Controls.Add(removeButtons[i]);
                this.Controls.Add(saveButtons[i]);

                int bottonPlace = richTextBoxList[richTextBoxList.Count - 2].Bottom;

                accossoryTextBoxList[i].Top = bottonPlace + 25;
                accossoryTextBoxList[i].Left = 100;
                accossoryTextBoxList[i].Width = 250;
                accossoryTextBoxList[i].Height = 30;
                //accossoryTextBoxList[i].Text = f4.mainDictionary[VARIABLE.Value].getType();
                accossoryTextBoxList[i].Font = new Font(Font.FontFamily, 10);


                namelessTextBoxList[i].Top = bottonPlace + 25;
                namelessTextBoxList[i].Left = 375;
                namelessTextBoxList[i].Width = 200;
                namelessTextBoxList[i].Height = 30;
                //namelessTextBoxList[i].Text = f4.mainDictionary[VARIABLE.Value].getNameless();
                namelessTextBoxList[i].Font = new Font(Font.FontFamily, 10);

                accossoryNumberLabels[i].Top = bottonPlace + 25;
                accossoryNumberLabels[i].Left = 25;
                accossoryNumberLabels[i].Text = (accossoryNumberLabels.Count - 1).ToString();
                accossoryNumberLabels[i].Font = new Font(Font.FontFamily, 10);

                removeButtons[i].Top = bottonPlace + 25;
                removeButtons[i].Left = namelessTextBoxList[i].Right + 25;
                removeButtons[i].Text = "Remove";
                removeButtons[i].BackColor = Color.Red;
                removeButtons[i].Click += new EventHandler(RemoveButton);


                saveButtons[i].Top = bottonPlace + 25;
                saveButtons[i].Left = removeButtons[i].Right + 10;
                saveButtons[i].Text = "Save to List";
                saveButtons[i].BackColor = Color.DarkSlateBlue;
                saveButtons[i].Click += new EventHandler(SaveAccosory);



                richTextBoxList[i].Top = bottonPlace + 55;
                richTextBoxList[i].Left = 40;
                richTextBoxList[i].Width = 700;
                richTextBoxList[i].Height = 100;
                richTextBoxList[i].Font = new Font(Font.FontFamily, 11);
                richTextBoxList[i].WordWrap = false;

                addButton.Top = richTextBoxList[richTextBoxList.Count - 1].Bottom + 25;
                okButton.Top = addButton.Bottom + 30;
                cancelButton.Top = addButton.Bottom + 30;
                ;
            }
        }

        private void SaveAccosory(object sender, EventArgs e)
        {
            
            int index = saveButtons.IndexOf((Button)sender);

            string value = null;
            if (Form4.InputBox("Truck Item", "Enter Name :-", ref value) == DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (!f4.savedTruckItemDictionary.ContainsKey(value))
                    {
                        f4.savedTruckItemDictionary.Add(value,richTextBoxList[index].Text);
                        Method.SerializeObject(f4.savedTruckItemDictionary, "truckAccessories.txt");
                    }
                    else
                    {
                        string message = "This name already exists";
                        string title = "Error";
                        MessageBox.Show(message, title);

                    }
                }
            }

        }

        private void OkButton(object sender, EventArgs e)
        {
            bool nullnameless = false;
            bool nullvehicleacc = false;
            bool nullrichtextbox = false;

            foreach (var VARIABLE in namelessTextBoxList)
            {
                if (String.IsNullOrEmpty(VARIABLE.Text))
                {
                    nullnameless = true;
                    break;
                }
            }
            foreach (var VARIABLE in accossoryTextBoxList)
            {
                if (String.IsNullOrEmpty(VARIABLE.Text))
                {
                    nullvehicleacc = true;
                    break;
                }
            }
            foreach (var VARIABLE in richTextBoxList)
            {
                if (String.IsNullOrEmpty(VARIABLE.Text))
                {
                    nullrichtextbox = true;
                    break;
                }
            }

            if (nullnameless)
            {
                string message = "A empty nameless exist";
                string title = "Error";
                MessageBox.Show(message, title);

            }
            else if(nullvehicleacc)
            {
                string message = "A empty accessory name exist";
                string title = "Error";
                MessageBox.Show(message, title);
            }
            else if (nullrichtextbox)
            {
                string message = "A empty accessory exist";
                string title = "Error";
                MessageBox.Show(message, title);
            }
            else
            {
                f4.mainDictionary[tempVehicle.getNameless()].dict.Clear();

                string[] temp = vehicleRichTextBox.Text.Split('\r', '\n');

               
                foreach (string VARIABLE in temp)
                {
                    if (!String.IsNullOrEmpty(VARIABLE))
                    {
                        string key = VARIABLE.Split(':')[0].Trim(' ');
                        string value = VARIABLE.Split(':')[1].Trim(' ');
    
                        f4.mainDictionary[tempVehicle.getNameless()].dict.Add(key,value);
                    }
                }

                for (int i = 0; i < namelessTextBoxList.Count; i++)
                {
                    string nameless = namelessTextBoxList[i].Text.Trim(' ');
                    string[] tempSplit = richTextBoxList[i].Text.Split('\r', '\n');

                    if (f4.mainDictionary.ContainsKey(nameless))
                    {
                        foreach (string s in tempSplit)
                        {
                            if (!String.IsNullOrEmpty(s))
                            {
                                if (s.Contains('"'))
                                {
                                    if (s.Split(':').Length == 3)
                                    {
                                        temp = s.Split(':');
                                        string tempValue = temp[1] + ":" + temp[2].Trim('\r', '\n');

                                        f4.mainDictionary[nameless].dict[temp[0].Trim(' ')] = tempValue;

                                    }
                                    else
                                    {
                                        temp = s.Split(':');
                                        f4.mainDictionary[nameless].dict[temp[0].Trim(' ')] =
                                            temp[1].Trim(' ', '\r', '\n');
                                    }
                                }
                                else
                                {
                                    temp = s.Split(':');
                                    f4.mainDictionary[nameless].dict[temp[0].Trim(' ')] =
                                        temp[1].Trim(' ', '\r', '\n', '"');
                                }
                            }
                        }
                    }
                    else
                    {
                        SuperClass tempSuperClass = new SuperClass(accossoryTextBoxList[i].Text.Trim(' '), nameless,richTextBoxList[i].Text);
                        f4.mainDictionary.Add(nameless,tempSuperClass);
                    }
                }
            }

            this.Close();
        }

        private void CancelButton(object sender, EventArgs e)
        {
            string message = "Do you want to close this window?";
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
