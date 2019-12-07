using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using static System.Windows.Forms.Application;

namespace VarletUi
{
    public class TrayContext : ApplicationContext
    {
        #region Private Members
        private readonly NotifyIcon TrayIcon;
        private readonly ContextMenuStrip TrayContextMenu;
        private readonly ToolStripMenuItem TrayMenuItemOptions;
        private readonly ToolStripMenuItem TrayMenuItemDisplayForm;
        private readonly ToolStripMenuItem TrayMenuItemExit;
        #endregion

        public TrayContext()
        {
            var res = new ComponentResourceManager(typeof(FormMain));

            //Instantiate the component Module to hold everything
            TrayIcon = new NotifyIcon(new System.ComponentModel.Container())
            {
                Visible = true,
                Icon = ((System.Drawing.Icon)(res.GetObject("$this.Icon"))),
                BalloonTipText = ProductName + " minimized to tray.",
                Text = ProductName + "v" + ProductVersion,
            };
            TrayIcon.DoubleClick += new System.EventHandler(TrayIcon_DoubleClick);

            // Initiate the context menu and items
            TrayContextMenu = new ContextMenuStrip();
            TrayIcon.ContextMenuStrip = TrayContextMenu;

            // Context menu item
            TrayMenuItemDisplayForm = new ToolStripMenuItem() { Text = "Open " + ProductName };
            TrayMenuItemDisplayForm.Click += new EventHandler(TrayMenuItemDisplayForm_Click);

            TrayMenuItemOptions = new ToolStripMenuItem() { Text = "&Options" };
            TrayMenuItemOptions.Click += new EventHandler(TrayMenuItemOptions_Click);

            TrayMenuItemExit = new ToolStripMenuItem() { Text = "E&xit" };
            TrayMenuItemExit.Click += new EventHandler(TrayMenuItemExit_Click);

            // Attach context menu item
            TrayContextMenu.Items.Add(TrayMenuItemDisplayForm);
            TrayContextMenu.Items.Add(TrayMenuItemOptions);
            TrayContextMenu.Items.Add(new ToolStripSeparator());
            TrayContextMenu.Items.Add(TrayMenuItemExit);
        }

        private static void TrayMenuItemOptions_Click(object sender, EventArgs e)
        {
            try
            {
                ShowMainForm();
                var fs = new FormSettings();
                foreach (Form fc in OpenForms) {
                    if (fc.Name == fs.Name) fc.Dispose();
                }

                (new FormMain()).lblSettings_Click(sender, e);
            }
            catch (FormatException)
            {
                // do something here
            }
        }

        public void ShowTrayIconNotification()
        {
            TrayIcon.ShowBalloonTip(3000);
            TrayIcon.Dispose();
        }

        private static void TrayIcon_DoubleClick(object Sender, EventArgs e)
        {
            ShowMainForm();
        }

        private static void TrayMenuItemDisplayForm_Click(object sender, EventArgs e)
        {
            ShowMainForm();
        }

        public void ExitApplication()
        {
            ExitThreadCore();
        }

        private void TrayMenuItemExit_Click(object sender, EventArgs e)
        {
            ExitThreadCore();
        }

        protected override void ExitThreadCore()
        {
            base.ExitThreadCore();
            if (MessageBox.Show("Exit Varlet Controller?", ProductName, MessageBoxButtons.YesNo) !=  DialogResult.Yes) return;
            TrayIcon.Dispose();
            Application.ExitThread();
        }

        private static void ShowMainForm()
        {
            try {
                var fm = new FormMain();
                foreach (Form fc in OpenForms) {
                    if (fc.Name == fm.Name) fc.Hide();
                }
                fm.Show();
                fm.Activate();
                fm.BringToFront();
                fm.Focus();
            } catch (FormatException) {
                // do something here
            }
        }
    }
}
