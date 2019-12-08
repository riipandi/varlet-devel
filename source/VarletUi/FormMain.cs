using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
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
            } else {
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
            lblConfigHttpd.Enabled = true;
            lblConfigSmtp.Enabled = true;
            lblPhpIni.Enabled = true;
        }

        private void CheckServiceStatus() {
            btnServices.Enabled = true;
            if (Services.IsInstalled(Globals.ServiceNameHttp)) {
                pictStatusHttpd.BackColor = Color.Red;
                if (Services.IsRunning(Globals.ServiceNameHttp)) {
                    pictStatusHttpd.BackColor = Color.Green;
                    btnServices.Text = "Stop Services";
                    comboPhpVersion.Enabled = false;
                    lblConfigHttpd.Enabled = false;
                    lblPhpIni.Enabled = false;
                }
            }
            if (Services.IsInstalled(Globals.ServiceNameSmtp)) {
                pictStatusSmtp.BackColor = Color.Red;
                if (Services.IsRunning(Globals.ServiceNameSmtp)) {
                    pictStatusSmtp.BackColor = Color.Green;
                    lblLogfileSmtp.Enabled = true;
                    lblConfigSmtp.Enabled = true;
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
                    btnServices.Enabled = false;
                    btnServices.Text = "Stopping Services";
                    StoppingServices();
                    Refresh();
                    break;
                case "Start Services":
                    btnServices.Enabled = false;
                    btnServices.Text = "Starting Services";
                    ChangePhpVersion();
                    StartingServices();
                    Refresh();
                    break;
            }
        }

        private void StartingServices()
        {
            if (Services.IsInstalled(Globals.ServiceNameHttp)) {
                while (!Services.IsRunning(Globals.ServiceNameHttp))  {
                    Services.Start(Globals.ServiceNameHttp);
                    if (Services.IsRunning(Globals.ServiceNameHttp)) {
                        pictStatusHttpd.BackColor = Color.Green;
                        btnServices.Text = "Stop Services";
                        comboPhpVersion.Enabled = false;
                        lblConfigHttpd.Enabled = false;
                        lblPhpIni.Enabled = false;
                        CheckServiceStatus();
                        break;
                    }
                }
            }

            if (Services.IsInstalled(Globals.ServiceNameSmtp)) {
                while (!Services.IsRunning(Globals.ServiceNameSmtp))  {
                    Services.Start(Globals.ServiceNameSmtp);
                    if (Services.IsRunning(Globals.ServiceNameSmtp)) {
                        pictStatusSmtp.BackColor = Color.Green;
                        lblConfigSmtp.Enabled = true;
                        btnServices.Text = "Stop Services";
                        CheckServiceStatus();
                        break;
                    }
                }
            }
        }

        private void StoppingServices()
        {
            if (Services.IsInstalled(Globals.ServiceNameHttp)) {
                while (Services.IsRunning(Globals.ServiceNameHttp))  {
                    Services.Stop(Globals.ServiceNameHttp);
                    if (!Services.IsRunning(Globals.ServiceNameHttp)) {
                        pictStatusHttpd.BackColor = Color.Red;
                        btnServices.Text = "Start Services";
                        comboPhpVersion.Enabled = true;
                        lblConfigHttpd.Enabled = true;
                        lblPhpIni.Enabled = true;
                        CheckServiceStatus();
                        break;
                    }
                }
            }
            if (Services.IsInstalled(Globals.ServiceNameSmtp)) {
                while (Services.IsRunning(Globals.ServiceNameSmtp))  {
                    Services.Stop(Globals.ServiceNameSmtp);
                    if (!Services.IsRunning(Globals.ServiceNameSmtp)) {
                        pictStatusSmtp.BackColor = Color.Red;
                        lblConfigSmtp.Enabled = false;
                        btnServices.Text = "Start Services";
                        CheckServiceStatus();
                        break;
                    }
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

        private void ChangePhpVersion()
        {
            var file = Common.GetAppPath(@"\pkg\httpd\conf\httpd.conf");
            const string keyword = "PHPVERSION";
            var oldVersion = Config.Get("SelectedPhpVersion");

            var sr = new StreamReader(file);
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

            var newVersion = " \"" + comboPhpVersion.Text + "\"";
            ReplaceStringInFile(file, oldVersion, newVersion);
        }

        private static void ReplaceStringInFile(string filename, string search, string replace)
        {
            var sr = new StreamReader(filename);
            var rows = Regex.Split(sr.ReadToEnd(), "\r\n");
            sr.Close();

            var sw = new StreamWriter(filename);
            for (var i = 0; i < rows.Length; i++)
            {
                if (rows[i].Contains(search))
                {
                    rows[i] = rows[i].Replace(search, replace);
                }
                sw.WriteLine(rows[i]);
            }
            sw.Close();
        }

        private void CheckAvailablePhp()
        {
            var pkgPhp = Common.GetAppPath(@"\pkg\php");
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
            var file = Common.GetAppPath(@"\tmp\httpd_error.log");
            if (!File.Exists(file))  {
                MessageBox.Show("File "+file+" not found!");
            } else  {
                Common.OpenWithNotepad(file);
            }
        }

        private void lblLogfileSmtp_Click(object sender, EventArgs e)
        {
            var file = Common.GetAppPath(@"\tmp\mailhogservice.err.log");
            if (!File.Exists(file))  {
                MessageBox.Show("File " + file + " not found!");
            } else  {
                Common.OpenWithNotepad(file);
            }
        }

        private void lblPhpIni_Click(object sender, EventArgs e)
        {
            var file = Common.GetAppPath(@"\pkg\php\"+comboPhpVersion.Text+@"\php.ini");
            if (!File.Exists(file))  {
                MessageBox.Show("File "+file+" not found!");
            } else  {
                Common.OpenWithNotepad(file);
            }
        }

        private void lblConfigHttpd_Click(object sender, EventArgs e)
        {
            var path = Common.GetAppPath(@"\pkg\httpd\conf");
            if (!Directory.Exists(path)) return;
            var proc = new Process {StartInfo = {
                FileName = "explorer.exe",  Arguments = path,  UseShellExecute = false
            }};
            proc.Start();
        }

        private void lblConfigSmtp_Click(object sender, EventArgs e)
        {
            // do something
        }

        // TODO: Make it work!
        private void comboPhpVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.Set("SelectedPhpVersion", comboPhpVersion.Text);
        }
    }
}
