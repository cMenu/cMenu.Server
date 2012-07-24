namespace cMenu.UI.Notification
{
    partial class frmHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistory));
            this.pnlHLControls = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblNotifications = new System.Windows.Forms.Label();
            this.listNotifications = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // pnlHLControls
            // 
            this.pnlHLControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHLControls.BackColor = System.Drawing.Color.Silver;
            this.pnlHLControls.Location = new System.Drawing.Point(12, 407);
            this.pnlHLControls.Name = "pnlHLControls";
            this.pnlHLControls.Size = new System.Drawing.Size(515, 1);
            this.pnlHLControls.TabIndex = 10;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(452, 414);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblNotifications
            // 
            this.lblNotifications.AutoSize = true;
            this.lblNotifications.Location = new System.Drawing.Point(12, 9);
            this.lblNotifications.Name = "lblNotifications";
            this.lblNotifications.Size = new System.Drawing.Size(80, 13);
            this.lblNotifications.TabIndex = 12;
            this.lblNotifications.Text = "Уведомления:";
            // 
            // listNotifications
            // 
            this.listNotifications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listNotifications.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colSource,
            this.colType,
            this.colDate,
            this.colHeader,
            this.colContent});
            this.listNotifications.FullRowSelect = true;
            this.listNotifications.HideSelection = false;
            this.listNotifications.Location = new System.Drawing.Point(15, 25);
            this.listNotifications.Name = "listNotifications";
            this.listNotifications.Size = new System.Drawing.Size(512, 376);
            this.listNotifications.TabIndex = 13;
            this.listNotifications.UseCompatibleStateImageBehavior = false;
            this.listNotifications.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Имя";
            // 
            // colSource
            // 
            this.colSource.DisplayIndex = 2;
            this.colSource.Text = "Отправитель";
            // 
            // colType
            // 
            this.colType.DisplayIndex = 3;
            this.colType.Text = "Тип";
            // 
            // colDate
            // 
            this.colDate.DisplayIndex = 1;
            this.colDate.Text = "Дата";
            // 
            // colHeader
            // 
            this.colHeader.DisplayIndex = 5;
            this.colHeader.Text = "Заголовок";
            // 
            // colContent
            // 
            this.colContent.DisplayIndex = 4;
            this.colContent.Text = "Содержимое";
            // 
            // frmHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 449);
            this.Controls.Add(this.listNotifications);
            this.Controls.Add(this.lblNotifications);
            this.Controls.Add(this.pnlHLControls);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "История уведомлений";
            this.Load += new System.EventHandler(this.frmHistory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHLControls;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblNotifications;
        private System.Windows.Forms.ListView listNotifications;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colSource;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colContent;
        private System.Windows.Forms.ColumnHeader colHeader;
    }
}