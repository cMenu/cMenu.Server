namespace cMenu.UI.Server.Host
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
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.серверToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemRun = new System.Windows.Forms.ToolStripMenuItem();
            this.itemStop = new System.Windows.Forms.ToolStripMenuItem();
            this.itemRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.itemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.imgStatus = new System.Windows.Forms.PictureBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblStatusValue = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.pgMain = new System.Windows.Forms.TabPage();
            this.grManagement = new System.Windows.Forms.GroupBox();
            this.grMain = new System.Windows.Forms.GroupBox();
            this.lblAddrValue = new System.Windows.Forms.Label();
            this.lblAddr = new System.Windows.Forms.Label();
            this.lblDbTypeValue = new System.Windows.Forms.Label();
            this.lblIDValue = new System.Windows.Forms.Label();
            this.lblNameValue = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblDbType = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pgFuncs = new System.Windows.Forms.TabPage();
            this.listFuncs = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInternal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colModuleFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblFunc = new System.Windows.Forms.Label();
            this.pgLog = new System.Windows.Forms.TabPage();
            this.listLog = new System.Windows.Forms.ListView();
            this.lblLog = new System.Windows.Forms.Label();
            this.popMain.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgStatus)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.pgMain.SuspendLayout();
            this.grMain.SuspendLayout();
            this.pgFuncs.SuspendLayout();
            this.pgLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // iconTray
            // 
            this.iconTray.ContextMenuStrip = this.popMain;
            this.iconTray.Icon = ((System.Drawing.Icon)(resources.GetObject("iconTray.Icon")));
            this.iconTray.Visible = true;
            this.iconTray.DoubleClick += new System.EventHandler(this.iconTray_DoubleClick);
            // 
            // popMain
            // 
            this.popMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem,
            this.серверToolStripMenuItem,
            this.toolStripMenuItem1,
            this.itemExit});
            this.popMain.Name = "popMain";
            this.popMain.Size = new System.Drawing.Size(175, 98);
            this.popMain.Opening += new System.ComponentModel.CancelEventHandler(this.popMain_Opening);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Image = global::cMenu.UI.Server.Host.Properties.Resources.ICO_SETTINGS;
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.настройкиToolStripMenuItem.Text = "&Настройки";
            // 
            // серверToolStripMenuItem
            // 
            this.серверToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemRun,
            this.itemStop,
            this.itemRestart});
            this.серверToolStripMenuItem.Image = global::cMenu.UI.Server.Host.Properties.Resources.ICO_SERVER;
            this.серверToolStripMenuItem.Name = "серверToolStripMenuItem";
            this.серверToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.серверToolStripMenuItem.Text = "&Сервер";
            // 
            // itemRun
            // 
            this.itemRun.Image = global::cMenu.UI.Server.Host.Properties.Resources.ICO_RUN;
            this.itemRun.Name = "itemRun";
            this.itemRun.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.itemRun.Size = new System.Drawing.Size(197, 22);
            this.itemRun.Text = "&Запустить";
            this.itemRun.Click += new System.EventHandler(this.itemRun_Click);
            // 
            // itemStop
            // 
            this.itemStop.Image = global::cMenu.UI.Server.Host.Properties.Resources.ICO_STOP;
            this.itemStop.Name = "itemStop";
            this.itemStop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.itemStop.Size = new System.Drawing.Size(197, 22);
            this.itemStop.Text = "&Остановить";
            this.itemStop.Click += new System.EventHandler(this.itemStop_Click);
            // 
            // itemRestart
            // 
            this.itemRestart.Image = global::cMenu.UI.Server.Host.Properties.Resources.ICO_ARROW_DOUBLE;
            this.itemRestart.Name = "itemRestart";
            this.itemRestart.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.itemRestart.Size = new System.Drawing.Size(197, 22);
            this.itemRestart.Text = "&Перезапустить";
            this.itemRestart.Click += new System.EventHandler(this.itemRestart_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(171, 6);
            // 
            // itemExit
            // 
            this.itemExit.Image = global::cMenu.UI.Server.Host.Properties.Resources.ICO_CLOSE;
            this.itemExit.Name = "itemExit";
            this.itemExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.itemExit.Size = new System.Drawing.Size(174, 22);
            this.itemExit.Text = "&Выход";
            this.itemExit.Click += new System.EventHandler(this.itemExit_Click);
            // 
            // pnlStatus
            // 
            this.pnlStatus.Controls.Add(this.btnSettings);
            this.pnlStatus.Controls.Add(this.imgStatus);
            this.pnlStatus.Controls.Add(this.btnRestart);
            this.pnlStatus.Controls.Add(this.btnStart);
            this.pnlStatus.Controls.Add(this.btnStop);
            this.pnlStatus.Controls.Add(this.lblStatusValue);
            this.pnlStatus.Controls.Add(this.lblStatus);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatus.Location = new System.Drawing.Point(0, 0);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(581, 28);
            this.pnlStatus.TabIndex = 6;
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Image = global::cMenu.UI.Server.Host.Properties.Resources.ICO_SETTINGS;
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.Location = new System.Drawing.Point(554, 3);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(23, 23);
            this.btnSettings.TabIndex = 14;
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // imgStatus
            // 
            this.imgStatus.Image = global::cMenu.UI.Server.Host.Properties.Resources.ICO_READY;
            this.imgStatus.Location = new System.Drawing.Point(5, 7);
            this.imgStatus.Name = "imgStatus";
            this.imgStatus.Size = new System.Drawing.Size(16, 16);
            this.imgStatus.TabIndex = 13;
            this.imgStatus.TabStop = false;
            // 
            // btnRestart
            // 
            this.btnRestart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestart.Image = global::cMenu.UI.Server.Host.Properties.Resources.ICO_ARROW_DOUBLE;
            this.btnRestart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestart.Location = new System.Drawing.Point(531, 3);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(23, 23);
            this.btnRestart.TabIndex = 2;
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Image = global::cMenu.UI.Server.Host.Properties.Resources.ICO_RUN;
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStart.Location = new System.Drawing.Point(485, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(23, 23);
            this.btnStart.TabIndex = 9;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Image = global::cMenu.UI.Server.Host.Properties.Resources.ICO_STOP;
            this.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStop.Location = new System.Drawing.Point(508, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(23, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.AutoSize = true;
            this.lblStatusValue.Location = new System.Drawing.Point(112, 8);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Size = new System.Drawing.Size(0, 13);
            this.lblStatusValue.TabIndex = 6;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStatus.Location = new System.Drawing.Point(23, 8);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(89, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Статус сервера:";
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.tabMain);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 28);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(581, 396);
            this.pnlContent.TabIndex = 11;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.pgMain);
            this.tabMain.Controls.Add(this.pgFuncs);
            this.tabMain.Controls.Add(this.pgLog);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Multiline = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(581, 396);
            this.tabMain.TabIndex = 0;
            // 
            // pgMain
            // 
            this.pgMain.Controls.Add(this.grManagement);
            this.pgMain.Controls.Add(this.grMain);
            this.pgMain.Location = new System.Drawing.Point(4, 22);
            this.pgMain.Name = "pgMain";
            this.pgMain.Padding = new System.Windows.Forms.Padding(3);
            this.pgMain.Size = new System.Drawing.Size(573, 370);
            this.pgMain.TabIndex = 0;
            this.pgMain.Text = "Главная";
            this.pgMain.UseVisualStyleBackColor = true;
            // 
            // grManagement
            // 
            this.grManagement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grManagement.Location = new System.Drawing.Point(8, 119);
            this.grManagement.Name = "grManagement";
            this.grManagement.Size = new System.Drawing.Size(557, 243);
            this.grManagement.TabIndex = 1;
            this.grManagement.TabStop = false;
            this.grManagement.Text = "Управление сервером";
            // 
            // grMain
            // 
            this.grMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grMain.Controls.Add(this.lblAddrValue);
            this.grMain.Controls.Add(this.lblAddr);
            this.grMain.Controls.Add(this.lblDbTypeValue);
            this.grMain.Controls.Add(this.lblIDValue);
            this.grMain.Controls.Add(this.lblNameValue);
            this.grMain.Controls.Add(this.lblID);
            this.grMain.Controls.Add(this.lblDbType);
            this.grMain.Controls.Add(this.lblName);
            this.grMain.Location = new System.Drawing.Point(8, 6);
            this.grMain.Name = "grMain";
            this.grMain.Size = new System.Drawing.Size(557, 107);
            this.grMain.TabIndex = 0;
            this.grMain.TabStop = false;
            this.grMain.Text = "Общая информация";
            // 
            // lblAddrValue
            // 
            this.lblAddrValue.AutoSize = true;
            this.lblAddrValue.Location = new System.Drawing.Point(96, 81);
            this.lblAddrValue.Name = "lblAddrValue";
            this.lblAddrValue.Size = new System.Drawing.Size(0, 13);
            this.lblAddrValue.TabIndex = 10;
            // 
            // lblAddr
            // 
            this.lblAddr.AutoSize = true;
            this.lblAddr.Location = new System.Drawing.Point(6, 81);
            this.lblAddr.Name = "lblAddr";
            this.lblAddr.Size = new System.Drawing.Size(86, 13);
            this.lblAddr.TabIndex = 9;
            this.lblAddr.Text = "Адрес сервера:";
            // 
            // lblDbTypeValue
            // 
            this.lblDbTypeValue.AutoSize = true;
            this.lblDbTypeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDbTypeValue.Location = new System.Drawing.Point(104, 60);
            this.lblDbTypeValue.Name = "lblDbTypeValue";
            this.lblDbTypeValue.Size = new System.Drawing.Size(0, 13);
            this.lblDbTypeValue.TabIndex = 8;
            // 
            // lblIDValue
            // 
            this.lblIDValue.AutoSize = true;
            this.lblIDValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblIDValue.Location = new System.Drawing.Point(96, 39);
            this.lblIDValue.Name = "lblIDValue";
            this.lblIDValue.Size = new System.Drawing.Size(0, 13);
            this.lblIDValue.TabIndex = 7;
            // 
            // lblNameValue
            // 
            this.lblNameValue.AutoSize = true;
            this.lblNameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblNameValue.Location = new System.Drawing.Point(91, 18);
            this.lblNameValue.Name = "lblNameValue";
            this.lblNameValue.Size = new System.Drawing.Size(0, 13);
            this.lblNameValue.TabIndex = 6;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblID.Location = new System.Drawing.Point(6, 39);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(90, 13);
            this.lblID.TabIndex = 5;
            this.lblID.Text = "Идентификатор:";
            // 
            // lblDbType
            // 
            this.lblDbType.AutoSize = true;
            this.lblDbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDbType.Location = new System.Drawing.Point(6, 60);
            this.lblDbType.Name = "lblDbType";
            this.lblDbType.Size = new System.Drawing.Size(108, 13);
            this.lblDbType.TabIndex = 4;
            this.lblDbType.Text = "Сервер баз данных:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblName.Location = new System.Drawing.Point(6, 18);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(86, 13);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Наименование:";
            // 
            // pgFuncs
            // 
            this.pgFuncs.Controls.Add(this.listFuncs);
            this.pgFuncs.Controls.Add(this.lblFunc);
            this.pgFuncs.Location = new System.Drawing.Point(4, 22);
            this.pgFuncs.Name = "pgFuncs";
            this.pgFuncs.Padding = new System.Windows.Forms.Padding(3);
            this.pgFuncs.Size = new System.Drawing.Size(573, 370);
            this.pgFuncs.TabIndex = 2;
            this.pgFuncs.Text = "Функции";
            this.pgFuncs.UseVisualStyleBackColor = true;
            // 
            // listFuncs
            // 
            this.listFuncs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listFuncs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colID,
            this.colEnabled,
            this.colInternal,
            this.colPath,
            this.colModuleFile});
            this.listFuncs.Location = new System.Drawing.Point(8, 24);
            this.listFuncs.Name = "listFuncs";
            this.listFuncs.Size = new System.Drawing.Size(554, 318);
            this.listFuncs.TabIndex = 3;
            this.listFuncs.UseCompatibleStateImageBehavior = false;
            this.listFuncs.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Имя";
            // 
            // colID
            // 
            this.colID.Text = "Идентификатор";
            // 
            // colEnabled
            // 
            this.colEnabled.Text = "Состояние";
            // 
            // colInternal
            // 
            this.colInternal.Text = "Тип функции";
            // 
            // colPath
            // 
            this.colPath.Text = "Папка";
            // 
            // colModuleFile
            // 
            this.colModuleFile.Text = "Имя модуля";
            // 
            // lblFunc
            // 
            this.lblFunc.AutoSize = true;
            this.lblFunc.Location = new System.Drawing.Point(8, 8);
            this.lblFunc.Name = "lblFunc";
            this.lblFunc.Size = new System.Drawing.Size(165, 13);
            this.lblFunc.TabIndex = 2;
            this.lblFunc.Text = "Зарегистрированные функции:";
            // 
            // pgLog
            // 
            this.pgLog.Controls.Add(this.listLog);
            this.pgLog.Controls.Add(this.lblLog);
            this.pgLog.Location = new System.Drawing.Point(4, 22);
            this.pgLog.Name = "pgLog";
            this.pgLog.Padding = new System.Windows.Forms.Padding(3);
            this.pgLog.Size = new System.Drawing.Size(573, 370);
            this.pgLog.TabIndex = 1;
            this.pgLog.Text = "Лог";
            this.pgLog.UseVisualStyleBackColor = true;
            // 
            // listLog
            // 
            this.listLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listLog.Location = new System.Drawing.Point(8, 24);
            this.listLog.Name = "listLog";
            this.listLog.Size = new System.Drawing.Size(554, 318);
            this.listLog.TabIndex = 1;
            this.listLog.UseCompatibleStateImageBehavior = false;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(8, 8);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(80, 13);
            this.lblLog.TabIndex = 0;
            this.lblLog.Text = "Лог операций:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 424);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlStatus);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сервер приложений iMenu 0.1a";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.popMain.ResumeLayout(false);
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgStatus)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.pgMain.ResumeLayout(false);
            this.grMain.ResumeLayout(false);
            this.grMain.PerformLayout();
            this.pgFuncs.ResumeLayout(false);
            this.pgFuncs.PerformLayout();
            this.pgLog.ResumeLayout(false);
            this.pgLog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon iconTray;
        private System.Windows.Forms.ContextMenuStrip popMain;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem itemExit;
        private System.Windows.Forms.ToolStripMenuItem серверToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemRun;
        private System.Windows.Forms.ToolStripMenuItem itemStop;
        private System.Windows.Forms.ToolStripMenuItem itemRestart;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStatusValue;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage pgMain;
        private System.Windows.Forms.GroupBox grManagement;
        private System.Windows.Forms.GroupBox grMain;
        private System.Windows.Forms.Label lblDbTypeValue;
        private System.Windows.Forms.Label lblIDValue;
        private System.Windows.Forms.Label lblNameValue;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblDbType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TabPage pgFuncs;
        private System.Windows.Forms.ListView listFuncs;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colEnabled;
        private System.Windows.Forms.ColumnHeader colInternal;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.ColumnHeader colModuleFile;
        private System.Windows.Forms.Label lblFunc;
        private System.Windows.Forms.TabPage pgLog;
        private System.Windows.Forms.ListView listLog;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox imgStatus;
        private System.Windows.Forms.Label lblAddrValue;
        private System.Windows.Forms.Label lblAddr;
        private System.Windows.Forms.Button btnSettings;
    }
}

