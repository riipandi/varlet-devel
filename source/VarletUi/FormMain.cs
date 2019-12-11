using System;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Variety;
using static System.Drawing.Color;

namespace VarletUi
{
    public partial class FormMain : Form
    {
        private static bool RunMinimized { get; set; }

        public FormMain(string parameter = "normal")
        {
            InitializeComponent();
            LoadConfigFile();
            if (parameter != "/minimized") return;
            RunMinimized = true;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ListAvailablePhp();
            CheckServiceStatus();
            Text = "Varlet v" + References.AppVersionSemantic + " build " +References.AppBuildNumber;
            if (RunMinimized) {
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
                BeginInvoke(new MethodInvoker(Close));
                RunMinimized = false;
            }
            Activate();
            Focus();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason.Equals(CloseReason.UserClosing)) {
                base.OnFormClosing(e);
                e.Cancel = true;
                (new TrayContext()).ShowTrayIconNotification();
                Hide();
            } else {
                Application.ExitThread();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            (new TrayContext()).ExitApplication();
        }

        private static void LoadConfigFile()
        {
            if (!File.Exists(References.AppConfigFile)) Config.Initialize();
        }

        private void CheckServiceStatus() {
            if (Services.IsInstalled(References.ServiceNameHttp)) {
                pictApacheStatus.BackColor = Red;
                lblApacheConfig.Enabled = true;
                lblApacaheLog.Enabled = true;
                lblPhpIni.Enabled = true;
                btnServices.Enabled = true;
                btnServices.Text = "Start Services";
                if (Services.IsRunning(References.ServiceNameHttp)) {
                    btnServices.Text = "Stop Services";
                    pictApacheStatus.BackColor = Green;
                    cmbPhpVersion.Enabled = false;
                    lblApacheConfig.Enabled = false;
                    lblPhpIni.Enabled = false;
                }
            }

            if (Services.IsInstalled(References.ServiceNameSmtp)) {
                pictMailhogStatus.BackColor = Red;
                lblMailhogOpen.Enabled = false;
                lblMailhogLog.Enabled = true;
                btnServices.Enabled = true;
                btnServices.Text = "Start Services";
                if (Services.IsRunning(References.ServiceNameSmtp)) {
                    btnServices.Text = "Stop Services";
                    pictMailhogStatus.BackColor = Green;
                    lblMailhogOpen.Enabled = true;
                    lblMailhogLog.Enabled = true;
                }
            }
        }

        public void lblSetting_Click(object sender, EventArgs e)
        {
            new FormSetting().ShowDialog();
        }

        private void lblAbout_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/riipandi/varlet");
        }

        private void lblMailhogOpen_Click(object sender, EventArgs e)
        {
            Process.Start("http://localhost:8025");
        }

        private void lblPhpIni_Click(object sender, EventArgs e)
        {
            var file = References.AppRootPath(@"\pkg\php\"+cmbPhpVersion.Text+@"\php.ini");
            if (!File.Exists(file))  {
                MessageBox.Show("File "+file+" not found!");
            } else  {
                Utilities.OpenWithNotepad(file);
            }
        }

        private void lblApacheConfig_Click(object sender, EventArgs e)
        {
            var path = References.AppRootPath(@"\pkg\httpd\conf");
            if (!Directory.Exists(path)) return;
            var proc = new Process {StartInfo = {
                FileName = "explorer.exe",  Arguments = path,  UseShellExecute = false
            }};
            proc.Start();
        }

        private void lblApacaheLog_Click(object sender, EventArgs e)
        {
            var file = References.AppRootPath(@"\tmp\httpd_error.log");
            if (!File.Exists(file))  {
                MessageBox.Show("File "+file+" not found!");
            } else  {
                Utilities.OpenWithNotepad(file);
            }
        }

        private void lblMailhogLog_Click(object sender, EventArgs e)
        {
            var file = References.AppRootPath(@"\tmp\mailhogservice.err.log");
            if (!File.Exists(file))  {
                MessageBox.Show("File "+file+" not found!");
            } else  {
                Utilities.OpenWithNotepad(file);
            }
        }

        public void lblSitesManager_Click(object sender, EventArgs e)
        {
            new FormSites().ShowDialog();
        }

        private void ListAvailablePhp()
        {
            var pkgPhp = References.AppRootPath(@"\pkg\php");
            if (!Directory.Exists(pkgPhp)) return;
            foreach (var t in Directory.GetDirectories(pkgPhp))  {
                cmbPhpVersion.Items.Add(Path.GetFileName(t));
            }
            cmbPhpVersion.SelectedIndex = !string.IsNullOrEmpty(Config.Get("SelectedPhpVersion")) ?
                cmbPhpVersion.FindStringExact(Config.Get("SelectedPhpVersion")) : 0;
        }

        public void btnTerminal_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(References.WwwDirectory)) {
                MessageBox.Show("Directory " + References.WwwDirectory + " doesn't exist!");
                return;
            }

            if (Directory.Exists(References.ProgramFilesDir(@"\PowerShell"))) {
                var proc = new Process {StartInfo = {
                    FileName = "pwsh.exe",
                    Arguments = "-NoLogo -WorkingDirectory \"" + References.WwwDirectory + "\"",
                    UseShellExecute = false
                }};
                proc.Start();
            } else  {
                var proc = new Process {StartInfo = {
                    FileName = "cmd.exe",
                    Arguments = "/k \"cd /d " + References.WwwDirectory + "\"",
                    UseShellExecute = false
                }};
                proc.Start();
            }
        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            if (!File.Exists(References.AppRootPath(@"\pkg\httpd\conf\httpd.conf"))) {
                MessageBox.Show("Apache configuration file not found!");
                return;
            }

            ChangePhpVersion();
            btnServices.Enabled = false;
            switch (btnServices.Text)
            {
                case "Stop Services":
                    btnServices.Enabled = false;
                    StoppingServices();
                    Refresh();
                    break;
                case "Start Services":
                    btnServices.Enabled = false;
                    StartingServices();
                    Refresh();
                    break;
            }
        }

        private void StartingServices()
        {
            if (Services.IsInstalled(References.ServiceNameHttp)) {
                while (!Services.IsRunning(References.ServiceNameHttp))  {
                    btnServices.Text = "Starting Services";
                    Services.Start(References.ServiceNameHttp);
                    if (Services.IsRunning(References.ServiceNameHttp)) {
                        pictApacheStatus.BackColor = Color.Green;
                        btnServices.Text = "Stop Services";
                        cmbPhpVersion.Enabled = false;
                        lblApacheConfig.Enabled = false;
                        lblPhpIni.Enabled = false;
                        CheckServiceStatus();
                        break;
                    }
                }
            }

            if (Services.IsInstalled(References.ServiceNameSmtp)) {
                while (!Services.IsRunning(References.ServiceNameSmtp))  {
                    btnServices.Text = "Starting Services";
                    Services.Start(References.ServiceNameSmtp);
                    if (Services.IsRunning(References.ServiceNameSmtp)) {
                        pictMailhogStatus.BackColor = Color.Green;
                        lblMailhogOpen.Enabled = true;
                        lblMailhogLog.Enabled = true;
                        btnServices.Text = "Stop Services";
                        CheckServiceStatus();
                        break;
                    }
                }
            }
        }

        private void StoppingServices()
        {
            if (Services.IsInstalled(References.ServiceNameHttp)) {
                while (Services.IsRunning(References.ServiceNameHttp))  {
                    btnServices.Text = "Stopping Services";
                    Services.Stop(References.ServiceNameHttp);
                    if (!Services.IsRunning(References.ServiceNameHttp)) {
                        pictApacheStatus.BackColor = Color.Red;
                        btnServices.Text = "Start Services";
                        cmbPhpVersion.Enabled = true;
                        lblApacheConfig.Enabled = true;
                        lblPhpIni.Enabled = true;
                        CheckServiceStatus();
                        break;
                    }
                }
            }
            if (Services.IsInstalled(References.ServiceNameSmtp)) {
                while (Services.IsRunning(References.ServiceNameSmtp))  {
                    btnServices.Text = "Stopping Services";
                    Services.Stop(References.ServiceNameSmtp);
                    if (!Services.IsRunning(References.ServiceNameSmtp)) {
                        pictMailhogStatus.BackColor = Color.Red;
                        lblMailhogOpen.Enabled = false;
                        lblMailhogLog.Enabled = false;
                        btnServices.Text = "Start Services";
                        CheckServiceStatus();
                        break;
                    }
                }
            }
        }

        private void ChangePhpVersion()
        {
            var cfgApache = References.AppRootPath(@"\pkg\httpd\conf\httpd.conf");

            const string keyword = "PHPVERSION";
            var oldVersion = Config.Get("SelectedPhpVersion");
            var newVersion = cmbPhpVersion.Text;

            var sr = new StreamReader(cfgApache);
            string currentLine;
            var foundText = false;

            do  {
                currentLine = sr.ReadLine();
                if(currentLine != null)  {
                    foundText = currentLine.Contains(keyword);
                }
            }  while(currentLine != null && !foundText);

            if (foundText)
            {
                var result = currentLine.Substring(currentLine.IndexOf(keyword) + keyword.Length);
                oldVersion = result;
                sr.Close();
            }

            // Update PHP Version on Apache Configuration
            Utilities.ReplaceStringInFile(cfgApache, oldVersion, " \"" + newVersion + "\"");

            // Update PHP Version on Composer
            var phpExe = References.AppRootPath(@"\pkg\php\"+cmbPhpVersion.Text+@"\php.exe");
            var composerPhar = References.AppRootPath(@"\utils\composer.phar");
            var content = "@echo off\n\""+phpExe+"\" \""+composerPhar+"\" %*";
            File.WriteAllText(References.AppRootPath(@"\utils\composer.bat"), content);
        }
    }
}
