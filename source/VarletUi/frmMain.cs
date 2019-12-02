using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VarletUi
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Put window at bottom-right
            Rectangle res = Screen.PrimaryScreen.Bounds;
            this.Location = new Point(res.Width - (Size.Width + 400), res.Height - (Size.Height + 200));
            this.Text = Application.ProductName + " v" + Application.ProductVersion;
        }
    }
}
