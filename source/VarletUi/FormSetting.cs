using System;
using System.Windows.Forms;

namespace VarletUi
{
    public partial class FormSetting : Form
    {
        public FormSetting()
        {
            InitializeComponent();
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            // do something
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
    }
}
