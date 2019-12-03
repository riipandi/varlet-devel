using System;
using System.Windows.Forms;
using Variety;

namespace VarletUi
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        
        private void FormMain_Load(object sender, EventArgs e)
        {
            var cf = Config.Load();
            cf.PhpVersion = Globals.DefaultPhpVersion;
            cf.InstallHttpService = true;
            cf.InstalMailhogService = true;
            cf.Save(Globals.ConfigFileName());
            
            var svcName = Globals.ServiceNameHttp();
            bool checkInstall = Services.IsServiceInstalled(svcName);
            bool checkRunning = Services.IsServiceRunning(svcName);
            string msg = ((checkInstall == true) && (checkRunning == true)) ? "Installed" : "Not installed!";
            // MessageBox.Show(msg);

            this.Text = Application.ProductName + " v" + Application.ProductVersion;
        }
    }
}
