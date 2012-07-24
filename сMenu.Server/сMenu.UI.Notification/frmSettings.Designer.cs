namespace cMenu.UI.Notification
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.grMain = new System.Windows.Forms.GroupBox();
            this.editDesc = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.editAddr = new System.Windows.Forms.TextBox();
            this.lblAddr = new System.Windows.Forms.Label();
            this.editName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.grAdd = new System.Windows.Forms.GroupBox();
            this.chStartService = new System.Windows.Forms.CheckBox();
            this.chAutostart = new System.Windows.Forms.CheckBox();
            this.btnAudio = new System.Windows.Forms.Button();
            this.editAudio = new System.Windows.Forms.TextBox();
            this.lblAudio = new System.Windows.Forms.Label();
            this.chAudio = new System.Windows.Forms.CheckBox();
            this.pnlHLControls = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.grMain.SuspendLayout();
            this.grAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grMain);
            this.pnlMain.Controls.Add(this.grAdd);
            this.pnlMain.Controls.Add(this.pnlHLControls);
            this.pnlMain.Controls.Add(this.btnOK);
            this.pnlMain.Controls.Add(this.btnApply);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(556, 400);
            this.pnlMain.TabIndex = 0;
            // 
            // grMain
            // 
            this.grMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grMain.Controls.Add(this.editDesc);
            this.grMain.Controls.Add(this.lblDesc);
            this.grMain.Controls.Add(this.editAddr);
            this.grMain.Controls.Add(this.lblAddr);
            this.grMain.Controls.Add(this.editName);
            this.grMain.Controls.Add(this.lblName);
            this.grMain.Location = new System.Drawing.Point(12, 12);
            this.grMain.Name = "grMain";
            this.grMain.Size = new System.Drawing.Size(532, 204);
            this.grMain.TabIndex = 11;
            this.grMain.TabStop = false;
            this.grMain.Text = "Настройки сервиса";
            // 
            // editDesc
            // 
            this.editDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editDesc.Location = new System.Drawing.Point(10, 117);
            this.editDesc.Multiline = true;
            this.editDesc.Name = "editDesc";
            this.editDesc.Size = new System.Drawing.Size(514, 77);
            this.editDesc.TabIndex = 3;
            this.editDesc.Click += new System.EventHandler(this.editName_TextChanged);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(7, 101);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(60, 13);
            this.lblDesc.TabIndex = 4;
            this.lblDesc.Text = "Описание:";
            // 
            // editAddr
            // 
            this.editAddr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editAddr.Location = new System.Drawing.Point(10, 78);
            this.editAddr.Name = "editAddr";
            this.editAddr.Size = new System.Drawing.Size(514, 20);
            this.editAddr.TabIndex = 2;
            this.editAddr.Click += new System.EventHandler(this.editName_TextChanged);
            // 
            // lblAddr
            // 
            this.lblAddr.AutoSize = true;
            this.lblAddr.Location = new System.Drawing.Point(9, 61);
            this.lblAddr.Name = "lblAddr";
            this.lblAddr.Size = new System.Drawing.Size(41, 13);
            this.lblAddr.TabIndex = 2;
            this.lblAddr.Text = "Адрес:";
            // 
            // editName
            // 
            this.editName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editName.Location = new System.Drawing.Point(10, 38);
            this.editName.Name = "editName";
            this.editName.Size = new System.Drawing.Size(514, 20);
            this.editName.TabIndex = 1;
            this.editName.Click += new System.EventHandler(this.editName_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(9, 19);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(86, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Наименование:";
            // 
            // grAdd
            // 
            this.grAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grAdd.Controls.Add(this.chStartService);
            this.grAdd.Controls.Add(this.chAutostart);
            this.grAdd.Controls.Add(this.btnAudio);
            this.grAdd.Controls.Add(this.editAudio);
            this.grAdd.Controls.Add(this.lblAudio);
            this.grAdd.Controls.Add(this.chAudio);
            this.grAdd.Location = new System.Drawing.Point(12, 222);
            this.grAdd.Name = "grAdd";
            this.grAdd.Size = new System.Drawing.Size(532, 130);
            this.grAdd.TabIndex = 12;
            this.grAdd.TabStop = false;
            this.grAdd.Text = "Дополнительно";
            // 
            // chStartService
            // 
            this.chStartService.AutoSize = true;
            this.chStartService.Location = new System.Drawing.Point(10, 104);
            this.chStartService.Name = "chStartService";
            this.chStartService.Size = new System.Drawing.Size(241, 17);
            this.chStartService.TabIndex = 8;
            this.chStartService.Text = "Запускать сервис при старте приложения";
            this.chStartService.UseVisualStyleBackColor = true;
            this.chStartService.Click += new System.EventHandler(this.chStartService_CheckedChanged);
            // 
            // chAutostart
            // 
            this.chAutostart.AutoSize = true;
            this.chAutostart.Location = new System.Drawing.Point(10, 81);
            this.chAutostart.Name = "chAutostart";
            this.chAutostart.Size = new System.Drawing.Size(175, 17);
            this.chAutostart.TabIndex = 7;
            this.chAutostart.Text = "Запускать вместе с Windows";
            this.chAutostart.UseVisualStyleBackColor = true;
            this.chAutostart.Click += new System.EventHandler(this.chaAutostart_CheckedChanged);
            // 
            // btnAudio
            // 
            this.btnAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAudio.Enabled = false;
            this.btnAudio.Location = new System.Drawing.Point(499, 55);
            this.btnAudio.Name = "btnAudio";
            this.btnAudio.Size = new System.Drawing.Size(25, 20);
            this.btnAudio.TabIndex = 6;
            this.btnAudio.Text = "...";
            this.btnAudio.UseVisualStyleBackColor = true;
            this.btnAudio.Click += new System.EventHandler(this.btnAudio_Click);
            // 
            // editAudio
            // 
            this.editAudio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editAudio.Enabled = false;
            this.editAudio.Location = new System.Drawing.Point(10, 55);
            this.editAudio.Name = "editAudio";
            this.editAudio.Size = new System.Drawing.Size(483, 20);
            this.editAudio.TabIndex = 5;
            this.editAudio.Click += new System.EventHandler(this.editName_TextChanged);
            // 
            // lblAudio
            // 
            this.lblAudio.AutoSize = true;
            this.lblAudio.Location = new System.Drawing.Point(9, 39);
            this.lblAudio.Name = "lblAudio";
            this.lblAudio.Size = new System.Drawing.Size(69, 13);
            this.lblAudio.TabIndex = 1;
            this.lblAudio.Text = "Аудио файл:";
            // 
            // chAudio
            // 
            this.chAudio.AutoSize = true;
            this.chAudio.Location = new System.Drawing.Point(12, 19);
            this.chAudio.Name = "chAudio";
            this.chAudio.Size = new System.Drawing.Size(246, 17);
            this.chAudio.TabIndex = 4;
            this.chAudio.Text = "Проигрывать аудио файл при уведомлении";
            this.chAudio.UseVisualStyleBackColor = true;
            this.chAudio.Click += new System.EventHandler(this.chAudio_CheckedChanged);
            // 
            // pnlHLControls
            // 
            this.pnlHLControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHLControls.BackColor = System.Drawing.Color.Silver;
            this.pnlHLControls.Location = new System.Drawing.Point(12, 358);
            this.pnlHLControls.Name = "pnlHLControls";
            this.pnlHLControls.Size = new System.Drawing.Size(532, 1);
            this.pnlHLControls.TabIndex = 10;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(307, 365);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(388, 365);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 14;
            this.btnApply.Text = "Применить";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(469, 365);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 400);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.pnlMain.ResumeLayout(false);
            this.grMain.ResumeLayout(false);
            this.grMain.PerformLayout();
            this.grAdd.ResumeLayout(false);
            this.grAdd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox grMain;
        private System.Windows.Forms.TextBox editDesc;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox editAddr;
        private System.Windows.Forms.Label lblAddr;
        private System.Windows.Forms.TextBox editName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.GroupBox grAdd;
        private System.Windows.Forms.CheckBox chStartService;
        private System.Windows.Forms.CheckBox chAutostart;
        private System.Windows.Forms.Button btnAudio;
        private System.Windows.Forms.TextBox editAudio;
        private System.Windows.Forms.Label lblAudio;
        private System.Windows.Forms.CheckBox chAudio;
        private System.Windows.Forms.Panel pnlHLControls;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;

    }
}

