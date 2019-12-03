using System;
using System.Drawing;
using System.Windows.Forms;

namespace VarletUi
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Interface controller
        /// </summary>
        private void FormMain_Load(object sender, EventArgs e)
        {
            InitiateWindow();
        }

        private void InitiateWindow()
        {
            int py, px;
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
    }
}
