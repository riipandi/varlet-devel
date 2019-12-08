using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Variety;
using static System.String;
using static System.Windows.Forms.Application;

namespace VarletUi
{
    public partial class FormMain : Form
    {
        private static bool RunMinimized { get; set; }

        public delegate void InvokeDelegate();

        public FormMain(string parameter = "normal")
        {
            InitializeComponent();
            if (!File.Exists(Globals.AppConfigFile))  {
                Config.Initialize(Globals.AppConfigFile);
            }
            if (parameter != "/minimized") return;
            RunMinimized = true;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitializeWindow();
            CheckAvailablePhp();
            CheckServiceStatus();
            if (!RunMinimized) return;
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            BeginInvoke(new MethodInvoker(Close));
            RunMinimized = false;
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

        private void InitializeWindow()
        {
            Text = "Varlet v" + Globals.AppVersion + " build " + Globals.AppBuildNumber;
            btnServices.Text = "Start Services";
            comboPhpVersion.Enabled = true;
            lblReloadHttpd.Enabled = false;
            lblReloadSmtp.Enabled = false;
        }

        private void CheckServiceStatus() {
            btnServices.Enabled = true;
            if (Services.IsInstalled(Globals.ServiceNameHttp)) {
                pictStatusHttpd.BackColor = Color.Red;
                if (Services.IsRunning(Globals.ServiceNameHttp)) {
                    pictStatusHttpd.BackColor = Color.Green;
                    btnServices.Text = "Stop Services";
                    comboPhpVersion.Enabled = false;
                    lblReloadHttpd.Enabled = true;
                }
            }
            if (Services.IsInstalled(Globals.ServiceNameSmtp)) {
                pictStatusSmtp.BackColor = Color.Red;
                if (Services.IsRunning(Globals.ServiceNameSmtp)) {
                    pictStatusSmtp.BackColor = Color.Green;
                    lblReloadSmtp.Enabled = true;
                    btnServices.Text = "Stop Services";
                }
            }
        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            btnServices.Enabled = false;
            switch (btnServices.Text)
            {
                case "Stop Services":
                    StoppingServices();
                    Refresh();
                    break;
                case "Start Services":
                    StartingServices();
                    Refresh();
                    break;
            }
        }

        private void StartingServices()
        {
            while (!Services.IsRunning(Globals.ServiceNameHttp))  {
                btnServices.Enabled = false;
                btnServices.Text = "Starting Services";
                Services.Start(Globals.ServiceNameHttp);
                if (Services.IsRunning(Globals.ServiceNameHttp)) {
                    pictStatusHttpd.BackColor = Color.Green;
                    btnServices.Text = "Stop Services";
                    comboPhpVersion.Enabled = false;
                    lblReloadHttpd.Enabled = true;
                    CheckServiceStatus();
                    break;
                }
            }
            while (!Services.IsRunning(Globals.ServiceNameSmtp))  {
                btnServices.Enabled = false;
                btnServices.Text = "Starting Services";
                Services.Start(Globals.ServiceNameSmtp);
                if (Services.IsRunning(Globals.ServiceNameSmtp)) {
                    pictStatusSmtp.BackColor = Color.Green;
                    lblReloadSmtp.Enabled = true;
                    btnServices.Text = "Stop Services";
                    CheckServiceStatus();
                    break;
                }
            }
        }

        private void StoppingServices()
        {
            while (Services.IsRunning(Globals.ServiceNameHttp))  {
                btnServices.Enabled = false;
                btnServices.Text = "Stopping Services";
                Services.Stop(Globals.ServiceNameHttp);
                if (!Services.IsRunning(Globals.ServiceNameHttp)) {
                    pictStatusHttpd.BackColor = Color.Red;
                    btnServices.Text = "Start Services";
                    comboPhpVersion.Enabled = true;
                    lblReloadHttpd.Enabled = false;
                    CheckServiceStatus();
                    break;
                }
            }
            while (Services.IsRunning(Globals.ServiceNameSmtp))  {
                btnServices.Enabled = false;
                btnServices.Text = "Stopping Services";
                Services.Stop(Globals.ServiceNameSmtp);
                if (!Services.IsRunning(Globals.ServiceNameSmtp)) {
                    pictStatusSmtp.BackColor = Color.Red;
                    lblReloadSmtp.Enabled = false;
                    btnServices.Text = "Start Services";
                    CheckServiceStatus();
                    break;
                }
            }
        }

        private void btnTerminal_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Common.DirProgramFiles(@"\PowerShell"))) {
                var proc = new Process {StartInfo = {
                    FileName = "pwsh.exe",
                    Arguments = "-NoLogo -WorkingDirectory \"" + Globals.WwwDirectory + "\"",
                    UseShellExecute = false
                }};
                proc.Start();
            } else  {
                var proc = new Process {StartInfo = {
                    FileName = "cmd.exe",
                    Arguments = "/k \"cd /d " + Globals.WwwDirectory + "\"",
                    UseShellExecute = false
                }};
                proc.Start();
            }
        }

        private void CheckAvailablePhp()
        {
            var pkgPhp = Common.GetAppPath() + @"\pkg\php";
            if (!Directory.Exists(pkgPhp)) return;
            foreach (var t in Directory.GetDirectories(pkgPhp))  {
                comboPhpVersion.Items.Add(Path.GetFileName(t));
            }
            comboPhpVersion.SelectedIndex = !IsNullOrEmpty(Config.Get("SelectedPhpVersion")) ?
                comboPhpVersion.FindStringExact(Config.Get("SelectedPhpVersion")) : 0;
        }

        private void lblAbout_Click(object sender, EventArgs e)
        {
            Common.OpenUrl("https://github.com/riipandi/varlet");
        }

        private void lblHostFile_Click(object sender, EventArgs e)
        {
            var file = Environment.SystemDirectory + @"\drivers\etc\hosts";
            Common.OpenWithNotepad(file, true);
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
                MessageBox.Show("File " + file + " not found!");
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
            Services.Restart(Globals.ServiceNameHttp);
        }

        private void lblReloadSmtp_Click(object sender, EventArgs e)
        {
            Services.Restart(Globals.ServiceNameSmtp);
        }

        // TODO: Make it work!
        private void comboPhpVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.Set("SelectedPhpVersion", comboPhpVersion.Text);
        }
    }
}
