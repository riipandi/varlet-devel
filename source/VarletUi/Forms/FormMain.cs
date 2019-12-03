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
            var svcName = Globals.ServiceNameHttp();
            bool checkInstall = Services.IsServiceInstalled(svcName);
            bool checkRunning = Services.IsServiceRunning(svcName);

            string msg = ((checkInstall == true) && (checkRunning == true)) ? "Installed" : "Not installed!";
            MessageBox.Show(msg);
        }
    }
}
