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

            foreach (Button b in directories.Keys)
            {
                b.Text = "";
                b.Click += b_Click;
            }
                

            this.Focus();
            this.BringToFront();            
        }

        void b_Click(object sender, EventArgs e)
        {
            
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
                    DialogResult userResponseDoIncludeFolder = MessageBox.Show("Auto map " + directory, "", MessageBoxButtons.YesNoCancel);

                    if (userResponseDoIncludeFolder == System.Windows.Forms.DialogResult.Yes)
                    {
                        registerDirectory(button, directory);
                    }
                    else if (userResponseDoIncludeFolder == System.Windows.Forms.DialogResult.Cancel)
                        break;
                }
            }
        }
        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            MessageBox.Show("fuck2");
        }

        private void Form1_PreviewKeyDown_1(object sender, PreviewKeyDownEventArgs e)
        {
            MessageBox.Show("fuck");
        }
    }
}
