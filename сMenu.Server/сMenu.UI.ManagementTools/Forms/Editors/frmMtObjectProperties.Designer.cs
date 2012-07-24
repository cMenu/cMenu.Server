namespace cMenu.UI.ManagementTools.Forms.Editors
{
    partial class frmMtObjectProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMtObjectProperties));
            this.pnlDelimiter = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.pgMain = new System.Windows.Forms.TabPage();
            this.editDesc = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.grMain = new System.Windows.Forms.GroupBox();
            this.editKey = new System.Windows.Forms.TextBox();
            this.lblKey = new System.Windows.Forms.Label();
            this.btnNewID = new System.Windows.Forms.Button();
            this.editID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.editName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.pgInnerLinks = new System.Windows.Forms.TabPage();
            this.treeInnerLinks = new System.Windows.Forms.TreeView();
            this.ilTree = new System.Windows.Forms.ImageList(this.components);
            this.lblInLinks = new System.Windows.Forms.Label();
            this.pgOuterLinks = new System.Windows.Forms.TabPage();
            this.treeOuterLinks = new System.Windows.Forms.TreeView();
            this.lblOutLinks = new System.Windows.Forms.Label();
            this.tabMain.SuspendLayout();
            this.pgMain.SuspendLayout();
            this.grMain.SuspendLayout();
            this.pgInnerLinks.SuspendLayout();
            this.pgOuterLinks.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDelimiter
            // 
            this.pnlDelimiter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDelimiter.BackColor = System.Drawing.Color.Silver;
            this.pnlDelimiter.Location = new System.Drawing.Point(12, 394);
            this.pnlDelimiter.Name = "pnlDelimiter";
            this.pnlDelimiter.Size = new System.Drawing.Size(435, 1);
            this.pnlDelimiter.TabIndex = 15;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(291, 401);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(372, 401);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.pgMain);
            this.tabMain.Controls.Add(this.pgInnerLinks);
            this.tabMain.Controls.Add(this.pgOuterLinks);
            this.tabMain.Location = new System.Drawing.Point(12, 12);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(435, 376);
            this.tabMain.TabIndex = 18;
            // 
            // pgMain
            // 
            this.pgMain.Controls.Add(this.editDesc);
            this.pgMain.Controls.Add(this.lblDesc);
            this.pgMain.Controls.Add(this.grMain);
            this.pgMain.Location = new System.Drawing.Point(4, 22);
            this.pgMain.Name = "pgMain";
            this.pgMain.Padding = new System.Windows.Forms.Padding(3);
            this.pgMain.Size = new System.Drawing.Size(427, 350);
            this.pgMain.TabIndex = 0;
            this.pgMain.Text = "Общее";
            this.pgMain.UseVisualStyleBackColor = true;
            // 
            // editDesc
            // 
            this.editDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editDesc.Location = new System.Drawing.Point(13, 131);
            this.editDesc.Multiline = true;
            this.editDesc.Name = "editDesc";
            this.editDesc.Size = new System.Drawing.Size(400, 206);
            this.editDesc.TabIndex = 27;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(12, 113);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(60, 13);
            this.lblDesc.TabIndex = 26;
            this.lblDesc.Text = "Описание:";
            // 
            // grMain
            // 
            this.grMain.Controls.Add(this.editKey);
            this.grMain.Controls.Add(this.lblKey);
            this.grMain.Controls.Add(this.btnNewID);
            this.grMain.Controls.Add(this.editID);
            this.grMain.Controls.Add(this.lblID);
            this.grMain.Controls.Add(this.editName);
            this.grMain.Controls.Add(this.lblName);
            this.grMain.Location = new System.Drawing.Point(13, 6);
            this.grMain.Name = "grMain";
            this.grMain.Size = new System.Drawing.Size(400, 104);
            this.grMain.TabIndex = 19;
            this.grMain.TabStop = false;
            this.grMain.Text = "Общее";
            // 
            // editKey
            // 
            this.editKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editKey.Location = new System.Drawing.Point(49, 72);
            this.editKey.Name = "editKey";
            this.editKey.ReadOnly = true;
            this.editKey.Size = new System.Drawing.Size(340, 20);
            this.editKey.TabIndex = 24;
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(7, 75);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(36, 13);
            this.lblKey.TabIndex = 25;
            this.lblKey.Text = "Ключ:";
            // 
            // btnNewID
            // 
            this.btnNewID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewID.Location = new System.Drawing.Point(325, 43);
            this.btnNewID.Name = "btnNewID";
            this.btnNewID.Size = new System.Drawing.Size(64, 23);
            this.btnNewID.TabIndex = 23;
            this.btnNewID.Text = "Новый";
            this.btnNewID.UseVisualStyleBackColor = true;
            this.btnNewID.Click += new System.EventHandler(this.btnNewID_Click);
            // 
            // editID
            // 
            this.editID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editID.Location = new System.Drawing.Point(49, 45);
            this.editID.Name = "editID";
            this.editID.ReadOnly = true;
            this.editID.Size = new System.Drawing.Size(270, 20);
            this.editID.TabIndex = 21;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(6, 48);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(21, 13);
            this.lblID.TabIndex = 22;
            this.lblID.Text = "ID:";
            // 
            // editName
            // 
            this.editName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editName.Location = new System.Drawing.Point(49, 19);
            this.editName.Name = "editName";
            this.editName.Size = new System.Drawing.Size(339, 20);
            this.editName.TabIndex = 19;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 22);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(32, 13);
            this.lblName.TabIndex = 20;
            this.lblName.Text = "Имя:";
            // 
            // pgInnerLinks
            // 
            this.pgInnerLinks.Controls.Add(this.treeInnerLinks);
            this.pgInnerLinks.Controls.Add(this.lblInLinks);
            this.pgInnerLinks.Location = new System.Drawing.Point(4, 22);
            this.pgInnerLinks.Name = "pgInnerLinks";
            this.pgInnerLinks.Padding = new System.Windows.Forms.Padding(3);
            this.pgInnerLinks.Size = new System.Drawing.Size(427, 350);
            this.pgInnerLinks.TabIndex = 1;
            this.pgInnerLinks.Text = "Входящие связи";
            this.pgInnerLinks.UseVisualStyleBackColor = true;
            // 
            // treeInnerLinks
            // 
            this.treeInnerLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeInnerLinks.ImageIndex = 0;
            this.treeInnerLinks.ImageList = this.ilTree;
            this.treeInnerLinks.Location = new System.Drawing.Point(9, 22);
            this.treeInnerLinks.Name = "treeInnerLinks";
            this.treeInnerLinks.SelectedImageIndex = 0;
            this.treeInnerLinks.Size = new System.Drawing.Size(409, 319);
            this.treeInnerLinks.TabIndex = 1;
            // 
            // ilTree
            // 
            this.ilTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTree.ImageStream")));
            this.ilTree.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTree.Images.SetKeyName(0, "folder.png");
            this.ilTree.Images.SetKeyName(1, "users.png");
            this.ilTree.Images.SetKeyName(2, "user.png");
            this.ilTree.Images.SetKeyName(3, "document-bookmark.png");
            this.ilTree.Images.SetKeyName(4, "monitor.png");
            // 
            // lblInLinks
            // 
            this.lblInLinks.AutoSize = true;
            this.lblInLinks.Location = new System.Drawing.Point(6, 6);
            this.lblInLinks.Name = "lblInLinks";
            this.lblInLinks.Size = new System.Drawing.Size(41, 13);
            this.lblInLinks.TabIndex = 0;
            this.lblInLinks.Text = "Связи:";
            // 
            // pgOuterLinks
            // 
            this.pgOuterLinks.Controls.Add(this.treeOuterLinks);
            this.pgOuterLinks.Controls.Add(this.lblOutLinks);
            this.pgOuterLinks.Location = new System.Drawing.Point(4, 22);
            this.pgOuterLinks.Name = "pgOuterLinks";
            this.pgOuterLinks.Padding = new System.Windows.Forms.Padding(3);
            this.pgOuterLinks.Size = new System.Drawing.Size(427, 350);
            this.pgOuterLinks.TabIndex = 2;
            this.pgOuterLinks.Text = "Исходящие связи";
            this.pgOuterLinks.UseVisualStyleBackColor = true;
            // 
            // treeOuterLinks
            // 
            this.treeOuterLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeOuterLinks.ImageIndex = 0;
            this.treeOuterLinks.ImageList = this.ilTree;
            this.treeOuterLinks.Location = new System.Drawing.Point(9, 22);
            this.treeOuterLinks.Name = "treeOuterLinks";
            this.treeOuterLinks.SelectedImageIndex = 0;
            this.treeOuterLinks.Size = new System.Drawing.Size(409, 319);
            this.treeOuterLinks.TabIndex = 3;
            // 
            // lblOutLinks
            // 
            this.lblOutLinks.AutoSize = true;
            this.lblOutLinks.Location = new System.Drawing.Point(6, 6);
            this.lblOutLinks.Name = "lblOutLinks";
            this.lblOutLinks.Size = new System.Drawing.Size(41, 13);
            this.lblOutLinks.TabIndex = 2;
            this.lblOutLinks.Text = "Связи:";
            // 
            // frmMtObjectProperties
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(459, 436);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.pnlDelimiter);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMtObjectProperties";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Свойства объекта";
            this.tabMain.ResumeLayout(false);
            this.pgMain.ResumeLayout(false);
            this.pgMain.PerformLayout();
            this.grMain.ResumeLayout(false);
            this.grMain.PerformLayout();
            this.pgInnerLinks.ResumeLayout(false);
            this.pgInnerLinks.PerformLayout();
            this.pgOuterLinks.ResumeLayout(false);
            this.pgOuterLinks.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDelimiter;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage pgMain;
        private System.Windows.Forms.GroupBox grMain;
        private System.Windows.Forms.TextBox editKey;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.Button btnNewID;
        private System.Windows.Forms.TextBox editID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox editName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TabPage pgInnerLinks;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox editDesc;
        private System.Windows.Forms.TabPage pgOuterLinks;
        private System.Windows.Forms.Label lblInLinks;
        private System.Windows.Forms.TreeView treeInnerLinks;
        private System.Windows.Forms.TreeView treeOuterLinks;
        private System.Windows.Forms.Label lblOutLinks;
        private System.Windows.Forms.ImageList ilTree;
    }
}