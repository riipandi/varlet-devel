using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Variety;
using static System.Windows.Forms.Application;
// using System.Security.Principal;

namespace VarletUi
{
    public partial class FormMain : Form
    {
        private static bool RunMinimized { get; set; }

        /*
        private static bool IsUserAdministrator()
        {
            bool isAdmin;
            try {
                var user = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            } catch (UnauthorizedAccessException ex) {
                isAdmin = false;
            } catch (Exception ex) {
                isAdmin = false;
            }
            return isAdmin;
        }
        */

        public FormMain(string parameter = "normal")
        {
            InitializeComponent();
            CheckServiceStatus();
            if (parameter != "/minimized") return;
            RunMinimized = true;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitializeWindow();
            CheckAvailablePhp();
            if (!RunMinimized) return;
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            BeginInvoke(new MethodInvoker(Close));
            RunMinimized = false;
        }

        private void InitializeWindow()
        {
            var cf = Config.Load();
            cf.PhpVersion = Globals.DefaultPhpVersion;
            cf.InstallHttpService = true;
            cf.InstalMailhogService = true;
            cf.Save(Globals.AppConfigFile());
            Text = "Varlet v" + Globals.Version;
        }

        private void CheckServiceStatus() {
            btnServices.Text = "Start Services";
            comboPhpVersion.Enabled = true;
            lblReloadHttpd.Enabled = false;
            lblLogfileHttpd.Enabled = false;
            lblReloadSmtp.Enabled = false;
            lblLogfileSmtp.Enabled = false;

            if (Services.IsServiceInstalled(Globals.ServiceNameHttp))  {
                pictStatusHttpd.BackColor = Color.Red;
                if (Services.IsServiceRunning(Globals.ServiceNameHttp))
                {
                    pictStatusHttpd.BackColor = Color.Green;
                    btnServices.Text = "Stop Services";
                    comboPhpVersion.Enabled = false;
                    lblReloadHttpd.Enabled = true;
                    lblLogfileHttpd.Enabled = true;
                    Services.IsHttpServiceRun = true;
                }
            }

            if (Services.IsServiceInstalled(Globals.ServiceNameSmtp))  {
                pictStatusSmtp.BackColor = Color.Red;
                if (Services.IsServiceRunning(Globals.ServiceNameSmtp))  {
                    pictStatusSmtp.BackColor = Color.Green;
                    btnServices.Text = "Stop Services";
                    lblReloadSmtp.Enabled = true;
                    lblLogfileSmtp.Enabled = true;
                    Services.IsSmtpServiceRun = true;
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason.Equals(CloseReason.UserClosing)) {
                base.OnFormClosing(e);
                e.Cancel = true;
                (new TrayContext()).ShowTrayIconNotification();
                Hide();
            } else  {
                ExitThread();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            (new TrayContext()).ExitApplication();
        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            if ((Services.IsHttpServiceRun == true) || (Services.IsSmtpServiceRun == true))  {
                StoppingService();
                Services.IsHttpServiceRun = false;
                Services.IsSmtpServiceRun = false;
            } else {
                StartingService();
                Services.IsHttpServiceRun = true;
                Services.IsSmtpServiceRun = true;
            }
            CheckServiceStatus();
        }

        private void StartingService()
        {
            pictStatusHttpd.BackColor = Color.Green;
            btnServices.Text = "Stop Services";
            comboPhpVersion.Enabled = false;
            lblReloadHttpd.Enabled = true;
            lblLogfileHttpd.Enabled = true;
            lblReloadSmtp.Enabled = true;
            lblLogfileSmtp.Enabled = true;
        }

        private void StoppingService()
        {
            pictStatusHttpd.BackColor = Color.Red;
            btnServices.Text = "Start Services";
            comboPhpVersion.Enabled = true;
            lblReloadHttpd.Enabled = false;
            lblLogfileHttpd.Enabled = false;
            lblReloadSmtp.Enabled = false;
            lblLogfileSmtp.Enabled = false;
        }

        private void btnTerminal_Click(object sender, EventArgs e)
        {
            var wwwDir = Common.GetAppPath() + @"\www";
            try
            {
                if (Directory.Exists(Common.DirProgramFiles(@"\PowerShell"))) {
                    var proc = new Process {StartInfo =
                    {
                        FileName = "pwsh.exe",
                        Arguments = "-NoLogo -WorkingDirectory " + wwwDir,
                        UseShellExecute = true
                    }};
                    proc.Start();
                } else  {
                    var proc = new Process {StartInfo =
                    {
                        FileName = "cmd.exe",
                        Arguments = "/k \"cd /d " + wwwDir + "\"",
                        UseShellExecute = true
                    }};
                    proc.Start();
                }
            } catch (FormatException) {
                // do something here
            }
        }

        private void CheckAvailablePhp()
        {
            var pkgPhp = Common.GetAppPath() + @"\pkg\php";

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

        public void lblSettings_Click(object sender, EventArgs e)
        {
            new FormSettings().ShowDialog();
        }

        private void lblLogfileHttpd_Click(object sender, EventArgs e)
        {
            var file = Common.GetAppPath() + @"\tmp\httpd_error.log";
            if (!File.Exists(file))  {
                MessageBox.Show("File "+file+" not found!");
            } else  {
                Common.OpenWithNotepad(file);
            }
        }

        private void lblLogfileSmtp_Click(object sender, EventArgs e)
        {
            var file = Common.GetAppPath() + @"\tmp\mailhogservice.err.log";
            if (!File.Exists(file))  {
                MessageBox.Show("File "+file+" not found!");
            } else  {
                Common.OpenWithNotepad(file);
            }
        }

        private void lblPhpIni_Click(object sender, EventArgs e)
        {
            var file = Common.GetAppPath() + @"\pkg\php\"+comboPhpVersion.Text+@"\php.ini";
            if (!File.Exists(file))  {
                MessageBox.Show("File "+file+" not found!");
            } else  {
                Common.OpenWithNotepad(file);
            }
        }

        private void lblReloadHttpd_Click(object sender, EventArgs e)
        {
            Services.RestartService(Globals.ServiceNameHttp);
            CheckServiceStatus();
        }

        private void lblReloadSmtp_Click(object sender, EventArgs e)
        {
            Services.RestartService(Globals.ServiceNameSmtp);
            CheckServiceStatus();
        }
    }
}
