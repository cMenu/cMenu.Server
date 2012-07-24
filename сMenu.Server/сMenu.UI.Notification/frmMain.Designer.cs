namespace cMenu.UI.Notification
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.iconTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.popMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.историяУведомленийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сервисToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.popMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // iconTray
            // 
            this.iconTray.ContextMenuStrip = this.popMain;
            this.iconTray.Icon = ((System.Drawing.Icon)(resources.GetObject("iconTray.Icon")));
            this.iconTray.Visible = true;
            // 
            // popMain
            // 
            this.popMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsItem,
            this.историяУведомленийToolStripMenuItem,
            this.сервисToolStripMenuItem,
            this.toolStripMenuItem1,
            this.выходToolStripMenuItem});
            this.popMain.Name = "popMain";
            this.popMain.Size = new System.Drawing.Size(242, 120);
            this.popMain.Opening += new System.ComponentModel.CancelEventHandler(this.popMain_Opening);
            // 
            // settingsItem
            // 
            this.settingsItem.Image = global::cMenu.UI.Notification.Properties.Resources.ICO_SETTINGS;
            this.settingsItem.Name = "settingsItem";
            this.settingsItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.settingsItem.Size = new System.Drawing.Size(241, 22);
            this.settingsItem.Text = "&Настройки";
            this.settingsItem.Click += new System.EventHandler(this.settingsItem_Click);
            // 
            // историяУведомленийToolStripMenuItem
            // 
            this.историяУведомленийToolStripMenuItem.Image = global::cMenu.UI.Notification.Properties.Resources.ICO_CALENDAR;
            this.историяУведомленийToolStripMenuItem.Name = "историяУведомленийToolStripMenuItem";
            this.историяУведомленийToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.историяУведомленийToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.историяУведомленийToolStripMenuItem.Text = "&История уведомлений";
            this.историяУведомленийToolStripMenuItem.Click += new System.EventHandler(this.историяУведомленийToolStripMenuItem_Click);
            // 
            // сервисToolStripMenuItem
            // 
            this.сервисToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runItem,
            this.stopItem,
            this.restartItem});
            this.сервисToolStripMenuItem.Image = global::cMenu.UI.Notification.Properties.Resources.ICO_INFO;
            this.сервисToolStripMenuItem.Name = "сервисToolStripMenuItem";
            this.сервисToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.сервисToolStripMenuItem.Text = "&Сервис";
            // 
            // runItem
            // 
            this.runItem.Image = global::cMenu.UI.Notification.Properties.Resources.ICO_RUN;
            this.runItem.Name = "runItem";
            this.runItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.runItem.Size = new System.Drawing.Size(197, 22);
            this.runItem.Text = "&Запустить";
            this.runItem.Click += new System.EventHandler(this.запуститьToolStripMenuItem_Click);
            // 
            // stopItem
            // 
            this.stopItem.Image = global::cMenu.UI.Notification.Properties.Resources.ICO_STOP;
            this.stopItem.Name = "stopItem";
            this.stopItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.stopItem.Size = new System.Drawing.Size(197, 22);
            this.stopItem.Text = "&Остановить";
            this.stopItem.Click += new System.EventHandler(this.остановитьToolStripMenuItem_Click);
            // 
            // restartItem
            // 
            this.restartItem.Image = global::cMenu.UI.Notification.Properties.Resources.ICO_ARROW_DOUBLE;
            this.restartItem.Name = "restartItem";
            this.restartItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.restartItem.Size = new System.Drawing.Size(197, 22);
            this.restartItem.Text = "&Перезапустить";
            this.restartItem.Click += new System.EventHandler(this.перезапуститьToolStripMenuItem_Click_1);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(238, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Image = global::cMenu.UI.Notification.Properties.Resources.ICO_CLOSE;
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.выходToolStripMenuItem.Text = "&Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.Text = "Сервис уведомлений";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.popMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon iconTray;
        private System.Windows.Forms.ContextMenuStrip popMain;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сервисToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runItem;
        private System.Windows.Forms.ToolStripMenuItem stopItem;
        private System.Windows.Forms.ToolStripMenuItem settingsItem;
        private System.Windows.Forms.ToolStripMenuItem историяУведомленийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartItem;
    }
}