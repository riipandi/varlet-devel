using System;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace VarletUi
{
    public partial class FormSetting : Form
    {
        private const string RegistryStartupPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

        public FormSetting()
        {
            InitializeComponent();
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            var key = Registry.CurrentUser.OpenSubKey(RegistryStartupPath);
            if (key?.GetValue("Varlet") != null) {
                chkRunVarletStartup.Checked = true;
                key.Close();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            (new FormMain()).Refresh();
        }

        private void btnChooseDir_Click(object sender, EventArgs e)
        {
            using (var fd = new FolderBrowserDialog()) {
                if (fd.ShowDialog(this)== DialogResult.OK) {
                    txtDocumentRoot.Text = fd.SelectedPath;
                }
            }
        }

        private void chkRunVarletStartup_CheckedChanged(object sender, EventArgs e)
        {
            var key = Registry.CurrentUser.OpenSubKey(RegistryStartupPath, true);
            if (key == null) return;
            if (chkRunVarletStartup.Checked)  {
                key.SetValue("Varlet", Application.ExecutablePath + " /minimized");
            } else  {
                key.DeleteValue("Varlet");
            }
            key.Close();
        }
    }
}
