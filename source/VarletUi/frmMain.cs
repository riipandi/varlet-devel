using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VarletUi
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Put window at bottom-right
            Rectangle res = Screen.PrimaryScreen.Bounds;
            this.Location = new Point(res.Width - (Size.Width + 400), res.Height - (Size.Height + 200));
            this.Text = Application.ProductName + " v" + Application.ProductVersion;
        }
    }
}
