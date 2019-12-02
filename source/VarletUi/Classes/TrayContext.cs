using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace VarletUi
{
    public class TrayContext : ApplicationContext
    {
        #region Private Members
        private IContainer mComponents;
        private NotifyIcon TrayIcon;
        private ContextMenuStrip TrayContextMenu;
        private ToolStripMenuItem TrayMenuItemDisplayForm;
        private ToolStripMenuItem TrayMenuItemExit;
        #endregion

        public TrayContext()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FrmMain));

            //Instantiate the component Module to hold everything
            mComponents = new System.ComponentModel.Container();
            TrayIcon = new NotifyIcon(this.mComponents);

            TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            TrayIcon.Text = "System Tray Application Demo";
            TrayIcon.Visible = true;
            TrayIcon.DoubleClick += new System.EventHandler(this.TrayIcon_DoubleClick);

            // Instantiate the context menu and items
            TrayContextMenu = new ContextMenuStrip();
            TrayIcon.ContextMenuStrip = TrayContextMenu;

            // Context menu item
            TrayMenuItemDisplayForm = new ToolStripMenuItem();
            TrayMenuItemDisplayForm.Text = "Open " + Application.ProductName;
            TrayMenuItemDisplayForm.Click += new EventHandler(TrayMenuItemDisplayForm_Click);

            TrayMenuItemExit = new ToolStripMenuItem();
            TrayMenuItemExit.Text = "Exit " + Application.ProductName;
            TrayMenuItemExit.Click += new EventHandler(TrayMenuItemExit_Click);

            // Attach context menu item
            TrayContextMenu.Items.Add(TrayMenuItemDisplayForm);
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
            FrmMain f = new FrmMain();

            f.Show();
            f.Focus();
        }

    }
}
