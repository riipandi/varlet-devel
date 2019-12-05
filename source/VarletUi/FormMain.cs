using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.VisualStyles;
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
            InitiateWindow();
            CheckAvailablePHP();

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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            e.Cancel = true;
            Hide();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            (new TrayContext()).ExitApplication();
        }

        private void InitiateWindow()
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

        public void btnSettings_Click(object sender, EventArgs e)
        {
            /*
            var fs = new FormSetting();

            px = (this.Location.X + this.Width / 4) - (fs.Width / 2);
            py = (this.Location.Y + this.Height / 2) - (fs.Height / 2);

            // fs.StartPosition = FormStartPosition.Manual;
            fs.StartPosition = FormStartPosition.CenterParent;
            fs.Location = new Point(px, py);
            fs.ShowDialog(this);
             */

            (new FormSettings()).ShowDialog();
        }

        private void CheckAvailablePHP()
        {
            string selectedVersion, path;
            selectedVersion = "php-7.3-ts";
            path = "C:\\Varlet\\php";

            var arr = Directory.GetDirectories(path);

            try
            {
                foreach (var t in arr)
                {
                    comboPhpVersion.Items.Add(Path.GetFileName(t));
                }

                comboPhpVersion.SelectedIndex = comboPhpVersion.FindStringExact(selectedVersion);
            }
            catch (FormatException)
            {
                // do something here
            }
        }
    }
}
