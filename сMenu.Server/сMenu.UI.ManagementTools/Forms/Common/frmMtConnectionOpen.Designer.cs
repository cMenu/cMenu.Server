namespace cMenu.UI.ManagementTools.Forms.Common
{
    partial class frmMtConnectionOpen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMtConnectionOpen));
            this.pnlDelimiter = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imgMain = new System.Windows.Forms.PictureBox();
            this.cmbConnections = new System.Windows.Forms.ComboBox();
            this.lblConn = new System.Windows.Forms.Label();
            this.grCred = new System.Windows.Forms.GroupBox();
            this.editPass = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.editUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.chSecuredConn = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgMain)).BeginInit();
            this.grCred.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDelimiter
            // 
            this.pnlDelimiter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDelimiter.BackColor = System.Drawing.Color.Silver;
            this.pnlDelimiter.Location = new System.Drawing.Point(12, 225);
            this.pnlDelimiter.Name = "pnlDelimiter";
            this.pnlDelimiter.Size = new System.Drawing.Size(386, 1);
            this.pnlDelimiter.TabIndex = 12;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(242, 232);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(323, 232);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // imgMain
            // 
            this.imgMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.imgMain.Image = global::cMenu.UI.ManagementTools.Properties.Resources.IMG_SPLASH;
            this.imgMain.Location = new System.Drawing.Point(0, 0);
            this.imgMain.Name = "imgMain";
            this.imgMain.Size = new System.Drawing.Size(410, 67);
            this.imgMain.TabIndex = 15;
            this.imgMain.TabStop = false;
            // 
            // cmbConnections
            // 
            this.cmbConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbConnections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnections.FormattingEnabled = true;
            this.cmbConnections.Location = new System.Drawing.Point(12, 88);
            this.cmbConnections.Name = "cmbConnections";
            this.cmbConnections.Size = new System.Drawing.Size(386, 21);
            this.cmbConnections.TabIndex = 17;
            this.cmbConnections.SelectedIndexChanged += new System.EventHandler(this.cmbConnections_SelectedIndexChanged);
            // 
            // lblConn
            // 
            this.lblConn.AutoSize = true;
            this.lblConn.Location = new System.Drawing.Point(9, 72);
            this.lblConn.Name = "lblConn";
            this.lblConn.Size = new System.Drawing.Size(76, 13);
            this.lblConn.TabIndex = 16;
            this.lblConn.Text = "Подключение";
            // 
            // grCred
            // 
            this.grCred.Controls.Add(this.editPass);
            this.grCred.Controls.Add(this.lblPass);
            this.grCred.Controls.Add(this.editUser);
            this.grCred.Controls.Add(this.lblUser);
            this.grCred.Enabled = false;
            this.grCred.Location = new System.Drawing.Point(15, 115);
            this.grCred.Name = "grCred";
            this.grCred.Size = new System.Drawing.Size(383, 80);
            this.grCred.TabIndex = 18;
            this.grCred.TabStop = false;
            this.grCred.Text = "Дополнительно";
            // 
            // editPass
            // 
            this.editPass.Location = new System.Drawing.Point(118, 45);
            this.editPass.Name = "editPass";
            this.editPass.PasswordChar = '*';
            this.editPass.Size = new System.Drawing.Size(254, 20);
            this.editPass.TabIndex = 22;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(6, 48);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(48, 13);
            this.lblPass.TabIndex = 21;
            this.lblPass.Text = "Пароль:";
            // 
            // editUser
            // 
            this.editUser.Location = new System.Drawing.Point(118, 19);
            this.editUser.Name = "editUser";
            this.editUser.Size = new System.Drawing.Size(254, 20);
            this.editUser.TabIndex = 20;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(6, 22);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(106, 13);
            this.lblUser.TabIndex = 19;
            this.lblUser.Text = "Имя пользователя:";
            // 
            // chSecuredConn
            // 
            this.chSecuredConn.AutoSize = true;
            this.chSecuredConn.Location = new System.Drawing.Point(15, 201);
            this.chSecuredConn.Name = "chSecuredConn";
            this.chSecuredConn.Size = new System.Drawing.Size(166, 17);
            this.chSecuredConn.TabIndex = 19;
            this.chSecuredConn.Text = "Репозиторий безопасности";
            this.chSecuredConn.UseVisualStyleBackColor = true;
            // 
            // frmMtConnectionOpen
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(410, 267);
            this.Controls.Add(this.chSecuredConn);
            this.Controls.Add(this.grCred);
            this.Controls.Add(this.cmbConnections);
            this.Controls.Add(this.lblConn);
            this.Controls.Add(this.imgMain);
            this.Controls.Add(this.pnlDelimiter);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMtConnectionOpen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Подключиться к серверу";
            ((System.ComponentModel.ISupportInitialize)(this.imgMain)).EndInit();
            this.grCred.ResumeLayout(false);
            this.grCred.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlDelimiter;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox imgMain;
        private System.Windows.Forms.ComboBox cmbConnections;
        private System.Windows.Forms.Label lblConn;
        private System.Windows.Forms.GroupBox grCred;
        private System.Windows.Forms.TextBox editUser;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox editPass;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.CheckBox chSecuredConn;
    }
}