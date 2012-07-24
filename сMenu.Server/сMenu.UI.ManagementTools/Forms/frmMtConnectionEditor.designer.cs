namespace iMenu.UI.ManagementTools.Forms.Common
{
    partial class frmMtConnectionEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMtConnectionEditor));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlDelimiter = new System.Windows.Forms.Panel();
            this.grMain = new System.Windows.Forms.GroupBox();
            this.btnNewID = new System.Windows.Forms.Button();
            this.editID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.editName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.grConn = new System.Windows.Forms.GroupBox();
            this.lblDbType = new System.Windows.Forms.Label();
            this.cmbServerDBType = new System.Windows.Forms.ComboBox();
            this.chSaveCred = new System.Windows.Forms.CheckBox();
            this.editDBName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.editAddr = new System.Windows.Forms.TextBox();
            this.lblAddr = new System.Windows.Forms.Label();
            this.editPass = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.editUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.grMain.SuspendLayout();
            this.grConn.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(408, 282);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(327, 282);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlDelimiter
            // 
            this.pnlDelimiter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDelimiter.BackColor = System.Drawing.Color.Silver;
            this.pnlDelimiter.Location = new System.Drawing.Point(12, 275);
            this.pnlDelimiter.Name = "pnlDelimiter";
            this.pnlDelimiter.Size = new System.Drawing.Size(471, 1);
            this.pnlDelimiter.TabIndex = 4;
            // 
            // grMain
            // 
            this.grMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grMain.Controls.Add(this.btnNewID);
            this.grMain.Controls.Add(this.editID);
            this.grMain.Controls.Add(this.label1);
            this.grMain.Controls.Add(this.editName);
            this.grMain.Controls.Add(this.lblName);
            this.grMain.Location = new System.Drawing.Point(12, 12);
            this.grMain.Name = "grMain";
            this.grMain.Size = new System.Drawing.Size(471, 78);
            this.grMain.TabIndex = 5;
            this.grMain.TabStop = false;
            this.grMain.Text = "Общее";
            // 
            // btnNewID
            // 
            this.btnNewID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewID.Location = new System.Drawing.Point(396, 43);
            this.btnNewID.Name = "btnNewID";
            this.btnNewID.Size = new System.Drawing.Size(64, 23);
            this.btnNewID.TabIndex = 3;
            this.btnNewID.Text = "Новый";
            this.btnNewID.UseVisualStyleBackColor = true;
            this.btnNewID.Click += new System.EventHandler(this.btnNewID_Click);
            // 
            // editID
            // 
            this.editID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editID.Location = new System.Drawing.Point(44, 45);
            this.editID.Name = "editID";
            this.editID.ReadOnly = true;
            this.editID.Size = new System.Drawing.Size(346, 20);
            this.editID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID:";
            // 
            // editName
            // 
            this.editName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editName.Location = new System.Drawing.Point(44, 19);
            this.editName.Name = "editName";
            this.editName.Size = new System.Drawing.Size(416, 20);
            this.editName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 22);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(32, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Имя:";
            // 
            // grConn
            // 
            this.grConn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grConn.Controls.Add(this.lblDbType);
            this.grConn.Controls.Add(this.cmbServerDBType);
            this.grConn.Controls.Add(this.chSaveCred);
            this.grConn.Controls.Add(this.editDBName);
            this.grConn.Controls.Add(this.label2);
            this.grConn.Controls.Add(this.editAddr);
            this.grConn.Controls.Add(this.lblAddr);
            this.grConn.Controls.Add(this.editPass);
            this.grConn.Controls.Add(this.lblPass);
            this.grConn.Controls.Add(this.editUser);
            this.grConn.Controls.Add(this.lblUser);
            this.grConn.Location = new System.Drawing.Point(12, 96);
            this.grConn.Name = "grConn";
            this.grConn.Size = new System.Drawing.Size(471, 171);
            this.grConn.TabIndex = 6;
            this.grConn.TabStop = false;
            this.grConn.Text = "Подключение";
            // 
            // lblDbType
            // 
            this.lblDbType.AutoSize = true;
            this.lblDbType.Location = new System.Drawing.Point(7, 125);
            this.lblDbType.Name = "lblDbType";
            this.lblDbType.Size = new System.Drawing.Size(48, 13);
            this.lblDbType.TabIndex = 12;
            this.lblDbType.Text = "Тип БД:";
            // 
            // cmbServerDBType
            // 
            this.cmbServerDBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServerDBType.FormattingEnabled = true;
            this.cmbServerDBType.Items.AddRange(new object[] {
            "Microfost SQL Server 2005, 2008, 2012"});
            this.cmbServerDBType.Location = new System.Drawing.Point(118, 122);
            this.cmbServerDBType.Name = "cmbServerDBType";
            this.cmbServerDBType.Size = new System.Drawing.Size(342, 21);
            this.cmbServerDBType.TabIndex = 8;
            // 
            // chSaveCred
            // 
            this.chSaveCred.AutoSize = true;
            this.chSaveCred.Location = new System.Drawing.Point(6, 149);
            this.chSaveCred.Name = "chSaveCred";
            this.chSaveCred.Size = new System.Drawing.Size(294, 17);
            this.chSaveCred.TabIndex = 9;
            this.chSaveCred.Text = "Сохранять имя пользователя и пароль в настройках";
            this.chSaveCred.UseVisualStyleBackColor = true;
            // 
            // editDBName
            // 
            this.editDBName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editDBName.Location = new System.Drawing.Point(118, 96);
            this.editDBName.Name = "editDBName";
            this.editDBName.Size = new System.Drawing.Size(342, 20);
            this.editDBName.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Наименование БД:";
            // 
            // editAddr
            // 
            this.editAddr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editAddr.Location = new System.Drawing.Point(118, 70);
            this.editAddr.Name = "editAddr";
            this.editAddr.Size = new System.Drawing.Size(342, 20);
            this.editAddr.TabIndex = 6;
            // 
            // lblAddr
            // 
            this.lblAddr.AutoSize = true;
            this.lblAddr.Location = new System.Drawing.Point(6, 73);
            this.lblAddr.Name = "lblAddr";
            this.lblAddr.Size = new System.Drawing.Size(105, 13);
            this.lblAddr.TabIndex = 6;
            this.lblAddr.Text = "Адрес сервера БД:";
            // 
            // editPass
            // 
            this.editPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editPass.Location = new System.Drawing.Point(118, 44);
            this.editPass.Name = "editPass";
            this.editPass.PasswordChar = '*';
            this.editPass.Size = new System.Drawing.Size(342, 20);
            this.editPass.TabIndex = 5;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(6, 47);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(48, 13);
            this.lblPass.TabIndex = 4;
            this.lblPass.Text = "Пароль:";
            // 
            // editUser
            // 
            this.editUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editUser.Location = new System.Drawing.Point(118, 18);
            this.editUser.Name = "editUser";
            this.editUser.Size = new System.Drawing.Size(342, 20);
            this.editUser.TabIndex = 4;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(6, 21);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(106, 13);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "Имя пользователя:";
            // 
            // frmMtConnectionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 317);
            this.Controls.Add(this.grConn);
            this.Controls.Add(this.grMain);
            this.Controls.Add(this.pnlDelimiter);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMtConnectionEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор подключения";
            this.grMain.ResumeLayout(false);
            this.grMain.PerformLayout();
            this.grConn.ResumeLayout(false);
            this.grConn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel pnlDelimiter;
        private System.Windows.Forms.GroupBox grMain;
        private System.Windows.Forms.TextBox editName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox editID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNewID;
        private System.Windows.Forms.GroupBox grConn;
        private System.Windows.Forms.TextBox editUser;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox editPass;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox editAddr;
        private System.Windows.Forms.Label lblAddr;
        private System.Windows.Forms.TextBox editDBName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chSaveCred;
        private System.Windows.Forms.ComboBox cmbServerDBType;
        private System.Windows.Forms.Label lblDbType;
    }
}