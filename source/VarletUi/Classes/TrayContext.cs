using System;
using System.Windows.Forms;
using System.ComponentModel;

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
            ComponentResourceManager res = new ComponentResourceManager(typeof(FormMain));

            //Instantiate the component Module to hold everything
            TrayIcon = new NotifyIcon(new System.ComponentModel.Container())
            {
                Icon = ((System.Drawing.Icon)(res.GetObject("$this.Icon"))),
                Text = Application.ProductName + "v" + Application.ProductVersion,
                Visible = true
            };

            TrayIcon.DoubleClick += new System.EventHandler(this.TrayIcon_DoubleClick);

            // Initiate the context menu and items
            TrayContextMenu = new ContextMenuStrip();
            TrayIcon.ContextMenuStrip = TrayContextMenu;

            // Context menu item
            TrayMenuItemDisplayForm = new ToolStripMenuItem() { Text = "Open Varlet" };
            TrayMenuItemDisplayForm.Click += new EventHandler(TrayMenuItemDisplayForm_Click);

            TrayMenuItemOptions = new ToolStripMenuItem() { Text = "&Options" };
            // TrayMenuItemOptions.Click += new EventHandler(TrayMenuItemOptions_Click);

            TrayMenuItemExit = new ToolStripMenuItem() { Text = "E&xit" };
            TrayMenuItemExit.Click += new EventHandler(TrayMenuItemExit_Click);

            // Attach context menu item
            TrayContextMenu.Items.Add(TrayMenuItemDisplayForm);
            TrayContextMenu.Items.Add(TrayMenuItemOptions);
            TrayContextMenu.Items.Add(new ToolStripSeparator());
            TrayContextMenu.Items.Add(TrayMenuItemExit);
        }

        private void TrayIcon_DoubleClick(object Sender, EventArgs e)
        {
            ShowMainForm();
        }

        private void TrayMenuItemDisplayForm_Click(object sender, EventArgs e)
        {
            ShowMainForm();
        }

        private void TrayMenuItemExit_Click(object sender, EventArgs e)
        {
            ExitThreadCore();
        }

        protected override void ExitThreadCore()
        {
            base.ExitThreadCore();
            Application.ExitThread();
        }

        private void ShowMainForm()
        {
            FormMain fm = new FormMain();
            fm.Show();
        }
    }
}
