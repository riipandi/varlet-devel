using System;
using System.Drawing;
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
            InitiateForm();
            
            var cf = Config.Load();
            cf.PhpVersion = Globals.DefaultPhpVersion;
            cf.InstallHttpService = true;
            cf.InstalMailhogService = true;
            cf.Save(Globals.ConfigFileName());

            /*
            var svcName = Globals.ServiceNameHttp;
            var checkInstall = Services.IsServiceInstalled(svcName);
            var checkRunning = Services.IsServiceRunning(svcName);
            var msg = ((checkInstall == true) && (checkRunning == true)) ? "Installed" : "Not installed!";
            */
            this.pictStatusHttpd.BackColor = Color.DarkSlateGray;
        }

        private void InitiateForm()
        {
            this.Text = Application.ProductName + " v" + Application.ProductVersion;
        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            if (Services.IsServiceInstalled(Globals.ServiceNameHttp))
            {
                MessageBox.Show("Service installed!");
            }
            else
            {
                MessageBox.Show("Not yet implemented!");
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            (new FormSettings()).ShowDialog();
        }
    }
}