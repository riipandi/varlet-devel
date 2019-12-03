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
using System.Collections.Generic;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            TrayContext oContext = new TrayContext();
            Application.Run(new FrmMain());
        }
    }
}
