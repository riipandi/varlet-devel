using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
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

            var svcName = Globals.ServiceNameHttp;
            var checkInstall = Services.IsServiceInstalled(svcName);
            var checkRunning = Services.IsServiceRunning(svcName);
            if (checkInstall) pictStatusHttpd.BackColor = Color.OrangeRed;
            if (checkRunning) pictStatusHttpd.BackColor = Color.Green;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            e.Cancel = true;
            (new TrayContext()).ShowTrayIconNotification();
            Hide();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            (new TrayContext()).ExitApplication();
        }

        private void InitiateWindow()
        {
            Text = Application.ProductName + " v" + Globals.Version;
            Activate();
            BringToFront();
            Focus();
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
            (new FormSettings()).ShowDialog();
        }

        private void CheckAvailablePHP()
        {
            var appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var pkgPhp = appPath + @"\pkg\php";

            try
            {
                if (!Directory.Exists(pkgPhp)) return;
                foreach (var t in Directory.GetDirectories(pkgPhp))  {
                    comboPhpVersion.Items.Add(Path.GetFileName(t));
                }
                comboPhpVersion.SelectedIndex = comboPhpVersion.FindStringExact(Globals.DefaultPhpVersion);
            }
            catch (FormatException)
            {
                // do something here
            }
        }

        private void lblAbout_Click(object sender, EventArgs e)
        {
            Common.OpenUrl("https://github.com/riipandi/varlet");
        }

        private void lblHostFile_Click(object sender, EventArgs e)
        {
            try {
                var file = Environment.SystemDirectory + @"\drivers\etc\hosts";
                Common.OpenWithNotepad(file, true);
            } catch (FormatException) {
                // do something here
            }
        }
    }
}
