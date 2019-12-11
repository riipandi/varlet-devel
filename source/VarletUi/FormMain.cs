using System;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.Application;
using Variety;

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
            Text = "Varlet v" + References.AppVersionSemantic + " build " +References.AppBuildNumber;
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
        
        private static void LoadConfigFile()
        {
            if (!File.Exists(References.AppConfigFile)) Config.Initialize();
        }
    }
}
