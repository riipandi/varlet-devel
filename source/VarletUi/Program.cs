/**
 * Varlet Controller
 *
 * The following code was provided for controlling Varlet.
 *
 * Author Name: Aris Ripandi <aris@ripandi.id>
 * Author Url: https://github.com/riipandi
 *
 */

using System;
using System.Threading;
using System.Windows.Forms;

namespace VarletUi
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool instanceCountOne = false;

            using (Mutex mtex = new Mutex(true, "VarletUi", out instanceCountOne))
            {
                if (instanceCountOne) {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    _ = new TrayContext();
                    Application.Run(new FormMain());
                    mtex.ReleaseMutex();
                } else {
                    MessageBox.Show(Application.ProductName + " already running!", "Warning");
                }
            }
        }
    }
}
