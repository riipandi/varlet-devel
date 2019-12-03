using System;
using System.Windows.Forms;
using Variety;

namespace VarletUi
{
    public partial class FormMain : Form
    {
        private readonly static string ConfigFile = "varlet.json";

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            var cf = Config.Load();
            cf.ServerPort = "8080";
            cf.Save(ConfigFile);

            var svcName = Globals.ServiceNameHttp();
            bool checkInstall = Services.IsServiceInstalled(svcName);
            bool checkRunning = Services.IsServiceRunning(svcName);
            string msg = ((checkInstall == true) && (checkRunning == true)) ? "Installed" : "Not installed!";
            MessageBox.Show(msg);
        }
    }
}
