using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicSorter
{
    static class Program
    {
        //Main window
       public  static Form1 form1; 

        //Text input popup
        public static Input input;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            form1 = new Form1();
            input = new Input();

            Application.Run(form1);
        }
    }
}
