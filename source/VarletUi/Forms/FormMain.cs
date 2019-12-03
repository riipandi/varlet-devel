using System;
using System.Drawing;
using System.Windows.Forms;

namespace VarletUi
{
    public partial class FormMain : Form
    {
        private int py, px;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitiateWindow();
        }

        private void InitiateWindow()
        {
            var res = Screen.PrimaryScreen.Bounds;

            py = res.Height - (Size.Height + 200);
            px = res.Width - (Size.Width + 400);

            this.Location = new Point(px, py);
            this.Text = Application.ProductName + " v" + Application.ProductVersion;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            e.Cancel = true;
            Hide();
        }

        public void btnPreference_Click(object sender, EventArgs e)
        {
            var fs = new FormSetting();

            px = (this.Location.X + this.Width / 4) - (fs.Width / 2);
            py = (this.Location.Y + this.Height / 2) - (fs.Height / 2);
            
            // fs.StartPosition = FormStartPosition.Manual;
            fs.StartPosition = FormStartPosition.CenterParent;
            fs.Location = new Point(px, py);
            fs.ShowDialog(this);
        }
    }
}
