namespace iMenu.UI.ManagementTools.Forms.Common
{
    partial class frmMtConnectionManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMtConnectionManager));
            this.grSrvrMngr = new System.Windows.Forms.GroupBox();
            this.btnConnDel = new System.Windows.Forms.Button();
            this.btnConnEdit = new System.Windows.Forms.Button();
            this.btnConnAdd = new System.Windows.Forms.Button();
            this.listConnections = new System.Windows.Forms.ListView();
            this.listSrvrCol1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listSrvrCol2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listSrvrCol3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlDelimiter = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.listSrvrCol4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listSrvrCol5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grSrvrMngr.SuspendLayout();
            this.SuspendLayout();
            // 
            // grSrvrMngr
            // 
            this.grSrvrMngr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grSrvrMngr.Controls.Add(this.btnConnDel);
            this.grSrvrMngr.Controls.Add(this.btnConnEdit);
            this.grSrvrMngr.Controls.Add(this.btnConnAdd);
            this.grSrvrMngr.Controls.Add(this.listConnections);
            this.grSrvrMngr.Location = new System.Drawing.Point(12, 12);
            this.grSrvrMngr.Name = "grSrvrMngr";
            this.grSrvrMngr.Size = new System.Drawing.Size(529, 371);
            this.grSrvrMngr.TabIndex = 2;
            this.grSrvrMngr.TabStop = false;
            this.grSrvrMngr.Text = "Список";
            // 
            // btnConnDel
            // 
            this.btnConnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConnDel.Enabled = false;
            this.btnConnDel.Location = new System.Drawing.Point(171, 340);
            this.btnConnDel.Name = "btnConnDel";
            this.btnConnDel.Size = new System.Drawing.Size(75, 23);
            this.btnConnDel.TabIndex = 4;
            this.btnConnDel.Text = "Удалить";
            this.btnConnDel.UseVisualStyleBackColor = true;
            this.btnConnDel.Click += new System.EventHandler(this.btnConnDel_Click);
            // 
            // btnConnEdit
            // 
            this.btnConnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConnEdit.Enabled = false;
            this.btnConnEdit.Location = new System.Drawing.Point(90, 340);
            this.btnConnEdit.Name = "btnConnEdit";
            this.btnConnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnConnEdit.TabIndex = 3;
            this.btnConnEdit.Text = "Изменить";
            this.btnConnEdit.UseVisualStyleBackColor = true;
            this.btnConnEdit.Click += new System.EventHandler(this.btnConnEdit_Click);
            // 
            // btnConnAdd
            // 
            this.btnConnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConnAdd.Location = new System.Drawing.Point(9, 340);
            this.btnConnAdd.Name = "btnConnAdd";
            this.btnConnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnConnAdd.TabIndex = 2;
            this.btnConnAdd.Text = "Добавить";
            this.btnConnAdd.UseVisualStyleBackColor = true;
            this.btnConnAdd.Click += new System.EventHandler(this.btnConnAdd_Click);
            // 
            // listConnections
            // 
            this.listConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listConnections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listSrvrCol1,
            this.listSrvrCol2,
            this.listSrvrCol3,
            this.listSrvrCol4,
            this.listSrvrCol5});
            this.listConnections.FullRowSelect = true;
            this.listConnections.GridLines = true;
            this.listConnections.Location = new System.Drawing.Point(9, 19);
            this.listConnections.MultiSelect = false;
            this.listConnections.Name = "listConnections";
            this.listConnections.Size = new System.Drawing.Size(509, 315);
            this.listConnections.TabIndex = 0;
            this.listConnections.UseCompatibleStateImageBehavior = false;
            this.listConnections.View = System.Windows.Forms.View.Details;
            this.listConnections.SelectedIndexChanged += new System.EventHandler(this.listConnections_SelectedIndexChanged);
            // 
            // listSrvrCol1
            // 
            this.listSrvrCol1.Text = "Наименование";
            this.listSrvrCol1.Width = 171;
            // 
            // listSrvrCol2
            // 
            this.listSrvrCol2.Text = "Идентификатор";
            this.listSrvrCol2.Width = 137;
            // 
            // listSrvrCol3
            // 
            this.listSrvrCol3.Text = "Тип БД";
            this.listSrvrCol3.Width = 75;
            // 
            // pnlDelimiter
            // 
            this.pnlDelimiter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDelimiter.BackColor = System.Drawing.Color.Silver;
            this.pnlDelimiter.Location = new System.Drawing.Point(12, 389);
            this.pnlDelimiter.Name = "pnlDelimiter";
            this.pnlDelimiter.Size = new System.Drawing.Size(529, 1);
            this.pnlDelimiter.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(466, 396);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // listSrvrCol4
            // 
            this.listSrvrCol4.Text = "Адрес сервера БД";
            // 
            // listSrvrCol5
            // 
            this.listSrvrCol5.Text = "Имя БД";
            // 
            // frmMtConnectionManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 431);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlDelimiter);
            this.Controls.Add(this.grSrvrMngr);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMtConnectionManager";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список подключений";
            this.grSrvrMngr.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grSrvrMngr;
        private System.Windows.Forms.Button btnConnDel;
        private System.Windows.Forms.Button btnConnEdit;
        private System.Windows.Forms.Button btnConnAdd;
        private System.Windows.Forms.ListView listConnections;
        private System.Windows.Forms.ColumnHeader listSrvrCol1;
        private System.Windows.Forms.ColumnHeader listSrvrCol2;
        private System.Windows.Forms.ColumnHeader listSrvrCol3;
        private System.Windows.Forms.Panel pnlDelimiter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ColumnHeader listSrvrCol4;
        private System.Windows.Forms.ColumnHeader listSrvrCol5;
    }
}