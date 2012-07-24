namespace cMenu.UI.ManagementTools.Forms.Common
{
    partial class frmMtServerObjectTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMtServerObjectTree));
            this.treeObjects = new System.Windows.Forms.TreeView();
            this.popTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mItemExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ilTree = new System.Windows.Forms.ImageList(this.components);
            this.popList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mItemObjectCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemObjectFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemObjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemObjectOrg = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemObjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemObjectCateg = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemObjectService = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemObjectSecurity = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemObjectUserGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemObjectUser = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemObjectPolicy = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemObjectDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemObjectLink = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mItemObjectProp = new System.Windows.Forms.ToolStripMenuItem();
            this.popTree.SuspendLayout();
            this.popList.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeObjects
            // 
            this.treeObjects.ContextMenuStrip = this.popTree;
            this.treeObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeObjects.HideSelection = false;
            this.treeObjects.ImageIndex = 0;
            this.treeObjects.ImageList = this.ilTree;
            this.treeObjects.Indent = 15;
            this.treeObjects.ItemHeight = 20;
            this.treeObjects.Location = new System.Drawing.Point(0, 0);
            this.treeObjects.Name = "treeObjects";
            this.treeObjects.SelectedImageIndex = 0;
            this.treeObjects.ShowNodeToolTips = true;
            this.treeObjects.Size = new System.Drawing.Size(408, 517);
            this.treeObjects.StateImageList = this.ilTree;
            this.treeObjects.TabIndex = 0;
            this.treeObjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeObjects_AfterSelect);
            this.treeObjects.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeObjects_MouseDown);
            // 
            // popTree
            // 
            this.popTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mItemExpandAll,
            this.mItemCollapseAll});
            this.popTree.Name = "contextMenuStrip1";
            this.popTree.Size = new System.Drawing.Size(157, 48);
            // 
            // mItemExpandAll
            // 
            this.mItemExpandAll.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_NODE_SELECT_ALL;
            this.mItemExpandAll.Name = "mItemExpandAll";
            this.mItemExpandAll.Size = new System.Drawing.Size(156, 22);
            this.mItemExpandAll.Text = "Развернуть все";
            this.mItemExpandAll.Click += new System.EventHandler(this.mItemExpandAll_Click);
            // 
            // mItemCollapseAll
            // 
            this.mItemCollapseAll.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_NODE_SELECT;
            this.mItemCollapseAll.Name = "mItemCollapseAll";
            this.mItemCollapseAll.Size = new System.Drawing.Size(156, 22);
            this.mItemCollapseAll.Text = "Свернуть все";
            this.mItemCollapseAll.Click += new System.EventHandler(this.mItemCollapseAll_Click);
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
            // popList
            // 
            this.popList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mItemObjectCreate,
            this.mItemObjectLink,
            this.toolStripMenuItem1,
            this.mItemObjectProp});
            this.popList.Name = "popList";
            this.popList.Size = new System.Drawing.Size(153, 98);
            // 
            // mItemObjectCreate
            // 
            this.mItemObjectCreate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mItemObjectFolder,
            this.mItemObjectMenu,
            this.mItemObjectSecurity});
            this.mItemObjectCreate.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_DOC_NEW;
            this.mItemObjectCreate.Name = "mItemObjectCreate";
            this.mItemObjectCreate.Size = new System.Drawing.Size(152, 22);
            this.mItemObjectCreate.Text = "Создать";
            // 
            // mItemObjectFolder
            // 
            this.mItemObjectFolder.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_FOLDER;
            this.mItemObjectFolder.Name = "mItemObjectFolder";
            this.mItemObjectFolder.Size = new System.Drawing.Size(149, 22);
            this.mItemObjectFolder.Text = "Папка";
            // 
            // mItemObjectMenu
            // 
            this.mItemObjectMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mItemObjectOrg,
            this.mItemObjectMenuItem,
            this.mItemObjectCateg,
            this.mItemObjectService});
            this.mItemObjectMenu.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_MENUS;
            this.mItemObjectMenu.Name = "mItemObjectMenu";
            this.mItemObjectMenu.Size = new System.Drawing.Size(149, 22);
            this.mItemObjectMenu.Text = "Меню";
            // 
            // mItemObjectOrg
            // 
            this.mItemObjectOrg.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_ORG;
            this.mItemObjectOrg.Name = "mItemObjectOrg";
            this.mItemObjectOrg.Size = new System.Drawing.Size(146, 22);
            this.mItemObjectOrg.Text = "Организация";
            // 
            // mItemObjectMenuItem
            // 
            this.mItemObjectMenuItem.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_MENU;
            this.mItemObjectMenuItem.Name = "mItemObjectMenuItem";
            this.mItemObjectMenuItem.Size = new System.Drawing.Size(146, 22);
            this.mItemObjectMenuItem.Text = "Меню";
            // 
            // mItemObjectCateg
            // 
            this.mItemObjectCateg.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_CATEG;
            this.mItemObjectCateg.Name = "mItemObjectCateg";
            this.mItemObjectCateg.Size = new System.Drawing.Size(146, 22);
            this.mItemObjectCateg.Text = "Категория";
            // 
            // mItemObjectService
            // 
            this.mItemObjectService.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_SERVICE;
            this.mItemObjectService.Name = "mItemObjectService";
            this.mItemObjectService.Size = new System.Drawing.Size(146, 22);
            this.mItemObjectService.Text = "Сервис";
            // 
            // mItemObjectSecurity
            // 
            this.mItemObjectSecurity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mItemObjectUserGroup,
            this.mItemObjectUser,
            this.mItemObjectPolicy,
            this.mItemObjectDevice});
            this.mItemObjectSecurity.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_USER_SECURITY;
            this.mItemObjectSecurity.Name = "mItemObjectSecurity";
            this.mItemObjectSecurity.Size = new System.Drawing.Size(149, 22);
            this.mItemObjectSecurity.Text = "Безопасность";
            // 
            // mItemObjectUserGroup
            // 
            this.mItemObjectUserGroup.Image = ((System.Drawing.Image)(resources.GetObject("mItemObjectUserGroup.Image")));
            this.mItemObjectUserGroup.Name = "mItemObjectUserGroup";
            this.mItemObjectUserGroup.Size = new System.Drawing.Size(207, 22);
            this.mItemObjectUserGroup.Text = "Группа пользователей";
            // 
            // mItemObjectUser
            // 
            this.mItemObjectUser.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_USER;
            this.mItemObjectUser.Name = "mItemObjectUser";
            this.mItemObjectUser.Size = new System.Drawing.Size(207, 22);
            this.mItemObjectUser.Text = "Пользователь";
            // 
            // mItemObjectPolicy
            // 
            this.mItemObjectPolicy.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_POLICY;
            this.mItemObjectPolicy.Name = "mItemObjectPolicy";
            this.mItemObjectPolicy.Size = new System.Drawing.Size(207, 22);
            this.mItemObjectPolicy.Text = "Политика безопасности";
            // 
            // mItemObjectDevice
            // 
            this.mItemObjectDevice.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_DEVICE;
            this.mItemObjectDevice.Name = "mItemObjectDevice";
            this.mItemObjectDevice.Size = new System.Drawing.Size(207, 22);
            this.mItemObjectDevice.Text = "Устройство";
            // 
            // mItemObjectLink
            // 
            this.mItemObjectLink.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_CONNECT;
            this.mItemObjectLink.Name = "mItemObjectLink";
            this.mItemObjectLink.Size = new System.Drawing.Size(152, 22);
            this.mItemObjectLink.Text = "Связать";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // mItemObjectProp
            // 
            this.mItemObjectProp.Image = global::cMenu.UI.ManagementTools.Properties.Resources.ICO_PROPERTIES;
            this.mItemObjectProp.Name = "mItemObjectProp";
            this.mItemObjectProp.Size = new System.Drawing.Size(152, 22);
            this.mItemObjectProp.Text = "Свойства";
            // 
            // frmMtServerObjectTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 517);
            this.Controls.Add(this.treeObjects);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMtServerObjectTree";
            this.Text = "Объекты репозитория";
            this.popTree.ResumeLayout(false);
            this.popList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeObjects;
        private System.Windows.Forms.ImageList ilTree;
        private System.Windows.Forms.ContextMenuStrip popTree;
        private System.Windows.Forms.ToolStripMenuItem mItemExpandAll;
        private System.Windows.Forms.ToolStripMenuItem mItemCollapseAll;
        private System.Windows.Forms.ContextMenuStrip popList;
        private System.Windows.Forms.ToolStripMenuItem mItemObjectCreate;
        private System.Windows.Forms.ToolStripMenuItem mItemObjectFolder;
        private System.Windows.Forms.ToolStripMenuItem mItemObjectMenu;
        private System.Windows.Forms.ToolStripMenuItem mItemObjectOrg;
        private System.Windows.Forms.ToolStripMenuItem mItemObjectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mItemObjectCateg;
        private System.Windows.Forms.ToolStripMenuItem mItemObjectService;
        private System.Windows.Forms.ToolStripMenuItem mItemObjectSecurity;
        private System.Windows.Forms.ToolStripMenuItem mItemObjectUserGroup;
        private System.Windows.Forms.ToolStripMenuItem mItemObjectUser;
        private System.Windows.Forms.ToolStripMenuItem mItemObjectPolicy;
        private System.Windows.Forms.ToolStripMenuItem mItemObjectDevice;
        private System.Windows.Forms.ToolStripMenuItem mItemObjectLink;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mItemObjectProp;
    }
}