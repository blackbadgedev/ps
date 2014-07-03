using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace PicSorter
{
    public partial class Form1 : Form
    {
        //Directory list
        Dictionary<Button, string> directories = new Dictionary<Button, string>();
        Input input = new Input();
        FolderBrowserDialog ofd = null;
        public Form1()
        {
            InitializeComponent();

            directories.Add(button1, "");
            directories.Add(button2, "");
            directories.Add(button3, "");
            directories.Add(button4, "");
            directories.Add(button5, "");
            directories.Add(button6, "");
            directories.Add(button7, "");
            directories.Add(button8, "");
            directories.Add(button9, "");

            this.KeyPress += Form1_KeyPress;
        }
        public Button getUnassignedButton()
        {
            foreach (Button b in directories.Keys)
            {
                if (b.Text == "")
                    return b;                
            }

            return null;
        }
        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                //Associate keys to different buttons and send that button to the click handler
                case '7': button_Click(directories.ElementAt<KeyValuePair<Button, string>>(0).Key, null); break;
                case '8': button_Click(directories.ElementAt<KeyValuePair<Button, string>>(1).Key, null); break;
                case '9': button_Click(directories.ElementAt<KeyValuePair<Button, string>>(2).Key, null); break;
                case '4': button_Click(directories.ElementAt<KeyValuePair<Button, string>>(3).Key, null); break;
                case '5': button_Click(directories.ElementAt<KeyValuePair<Button, string>>(4).Key, null); break;
                case '6': button_Click(directories.ElementAt<KeyValuePair<Button, string>>(5).Key, null); break;
                case '1': button_Click(directories.ElementAt<KeyValuePair<Button, string>>(6).Key, null); break;
                case '2': button_Click(directories.ElementAt<KeyValuePair<Button, string>>(7).Key, null); break;
                case '3': button_Click(directories.ElementAt<KeyValuePair<Button, string>>(8).Key, null); break;
                default: break;
            }
        }
        private void registerDirectory(Button button, string directory)
        {
            //Available button
            if (button.Text == "")
            {
                //Get folder name
                DirectoryInfo di = new DirectoryInfo(directory);

                //Assign folder name to button
                button.Text = di.Name;

                //Assign directory to button
                directories[button] = directory;

                //Hook up the function
                button.Click += button_Click;
            }
        }

        void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            //If the button has been wired up
            if (button.Name != "")
            {
                MessageBox.Show("Not Implemented: send file to directory " + directories[button]);


            }
            else
            {
                //This button is not assigned to a directory, so ask for one
                DialogResult result = MessageBox.Show("Create new folder?", "", MessageBoxButtons.YesNo);
                
                //Create new folder
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    input.Show();
                }
            }                
        }
        public void newFolder(string folderName)
        {            
            if (folderName.Length > 0)
            {
                string fullFolderPath = ofd.SelectedPath + folderName;
                Directory.CreateDirectory(fullFolderPath);

                if (Directory.Exists(fullFolderPath) == false)
                    MessageBox.Show("Problem creating folder " + fullFolderPath);
            }          
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Pick a directory
           ofd = new FolderBrowserDialog();
            
            //Show dialogue box
            DialogResult result = ofd.ShowDialog();

            //Cancel or Okay
            if (result == System.Windows.Forms.DialogResult.Cancel)
                Application.Exit();
            else if (result == System.Windows.Forms.DialogResult.OK)
            {
                
                //Parse sub directories and map them
                foreach (string directory in Directory.GetDirectories(ofd.SelectedPath))
                {

                    //Find an available button, or break out of this loop if we have no buttons left
                    Button button = getUnassignedButton();
                    if (button == null)
                        break;
                    
                    //Ask the user if they want to auto map this folder
                    DialogResult userResponseDoIncludeFolder = MessageBox.Show("Auto map " + directory, "", MessageBoxButtons.YesNo);

                    if (userResponseDoIncludeFolder == System.Windows.Forms.DialogResult.Yes)
                    {
                        registerDirectory(button, directory);
                    }                    
                }
            }
        }
    }
}
