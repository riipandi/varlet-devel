using System;
using System.Threading;
using System.Windows.Forms;

namespace VarletUi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mtex = new Mutex(true, "VarletUi", out bool instanceCountOne))
            {
                if (instanceCountOne)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    // _ = new TrayContext();
                    Application.Run(new FormMain());
                    mtex.ReleaseMutex();
                }
                else
                {
                    MessageBox.Show(Application.ProductName + " already running!", "Warning");
                }
            }
        }
    }
}