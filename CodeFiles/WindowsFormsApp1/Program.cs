using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            string dllPath1 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "itextsharp.dll");
            string dllPath2 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "BouncyCastle.Crypto.dll");

            if (!File.Exists(dllPath1))
            {
                byte[] fileBytes = Properties.Resources.itextsharp;
                File.WriteAllBytes(dllPath1, fileBytes);
            }
            if (!File.Exists(dllPath2))
            {
                byte[] fileBytes = Properties.Resources.BouncyCastle_Crypto;
                File.WriteAllBytes(dllPath2, fileBytes);
            }


            Application.Run(new Form1());
        }
    }
}
