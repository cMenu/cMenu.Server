using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using cMenu.Metaobjects;
using cMenu.Metaobjects.Extended;
using cMenu.Metaobjects.Extended.Helpers;
using cMenu.UI.ManagementTools.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace cMenu.UI.ManagementTools.Forms.Common
{
    public partial class frmMtServerObjectTree : DockContent
    {
        protected CMTApplicationConnection _connection;
        protected bool _showIdentifiers = false;

        public CMTApplicationConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }
        public bool ShowIdentifiers
        {
            get { return _showIdentifiers; }
            set 
            { 
                _showIdentifiers = value;
                _refresh();
            }
        }

        public event OnObjectsTreeSelectionChangedDelegate OnObjectsTreeSelectionChanged;
        public event OnFormExecuteActionDelegate OnFormExecuteAction;

        protected int _showPopup(MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Right)
                return -2;

            var SelectedNode = this.treeObjects.GetNodeAt(e.Location);
            if (SelectedNode != null)
                this.treeObjects.SelectedNode = SelectedNode;
            this.treeObjects.ContextMenuStrip = (SelectedNode == null ? this.popTree : this.popList);
            return -1;
        }
        protected int _showProperties()
        {
            if (this.treeObjects.SelectedNode == null)
                return -2;
            if (this.OnFormExecuteAction != null)
            {
                var Object = (CMetaobject)this.treeObjects.SelectedNode.Tag;
                var Parameters = new Hashtable();
                Parameters.Add(CMTApplicationActionsParametersConsts.CONST_ACTION_PARAM_METAOBJECT, Object);
                this.OnFormExecuteAction(this, Parameters, CMTApplicationActionsConsts.CONST_ACTION_SHOW_PROPERTIES);
            }
            return -1;
        }
        protected int _getImageIndex(CMetaobject Object)
        {
            var R = 0;
            switch (Object.Class)
            {
                case EnMetaobjectClass.EFolder: R = 0; break;
                case EnMetaobjectClass.ESystemUserGroup: R = 1; break;
                case EnMetaobjectClass.ESystemUser: R = 2; break;
                case EnMetaobjectClass.ESystemPolicy: R = 3; break;
                case EnMetaobjectClass.EClientDevice: R = 4; break;
            }
            return R;
        }
        protected int _loadSimpleRepository()
        {            
            this.treeObjects.Nodes.Clear();
            CFolder RootFolder = new CFolder(CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY, this._connection.Provider);
            RootFolder.GetChildren(this._connection.Provider);
            this._connection.CurrentUser.GetSecurityRecords(this._connection.Provider);
            var Objects = CMetaobjectHelper.sFilterObjectsByUser(new List<CMetaobject>() { RootFolder }, this._connection.CurrentUser);

            this._loadMetaobjectsTree(Objects[0], null);

            return -1;
        }
        protected int _loadSecurityRepository()
        {
            this.treeObjects.Nodes.Clear();
            CFolder RootFolder = new CFolder(CEmbeddedObjectsConsts.CONST_FOLDER_SHADOW_ROOT_KEY, this._connection.Provider);
            RootFolder.GetChildren(this._connection.Provider);
            this._loadMetaobjectsTree(RootFolder, null);
            return -1;
        }
        protected int _loadMetaobjectsTree(CMetaobject Root, TreeNode RootNode)
        {
            TreeNode Node = new TreeNode(Root.Name + (this._showIdentifiers ? " (ID: " + Root.ID.ToString().ToUpper() + ")" : ""));
            Node.ImageIndex = this._getImageIndex(Root);
            Node.SelectedImageIndex = this._getImageIndex(Root);
            Node.Tag = Root;

            if (RootNode == null)
                this.treeObjects.Nodes.Add(Node);
            else
                RootNode.Nodes.Add(Node);
            if (Root.Children.Count != 0)
                foreach (CMetaobject ChildMetaobject in Root.Children)
                    this._loadMetaobjectsTree(ChildMetaobject, Node);

            return -1;
        }

        protected TreeNode _findNodeByObject(TreeNode RootNode, CMetaobject Object)
        {
            TreeNodeCollection Nodes = (RootNode == null ? this.treeObjects.Nodes : RootNode.Nodes);

            foreach (TreeNode Node in Nodes)
            {
                if (Node.Nodes.Count != 0)
                {
                    var R = this._findNodeByObject(Node, Object);
                    if (R != null)
                        return R;
                }
                if ((Node.Tag as CMetaobject).ID.ToString().ToUpper() == Object.ID.ToString().ToUpper())
                    return Node;
            }
            return null;
        }
        protected int _selectRootNode()
        {
            this.treeObjects.SelectedNode = this.treeObjects.Nodes[0];
            return -1;
        }
        protected int _refresh()
        {
            if (this._connection.SecurityConnection)
                this._loadSecurityRepository();
            else
                this._loadSimpleRepository();
            this._selectRootNode();
            return -1;
        }
        protected int _expandAll()
        {
            this.treeObjects.ExpandAll();
            return -1;
        }
        protected int _collapseAll()
        {
            this.treeObjects.CollapseAll();
            return -1;
        }
        protected int _selectionChanged(TreeNode SelectedNode)
        {
            if (this.OnObjectsTreeSelectionChanged != null)
                this.OnObjectsTreeSelectionChanged(this, SelectedNode.Tag);
            return -1;
        }

        public frmMtServerObjectTree()
        {
            InitializeComponent();
            this.CloseButtonVisible = false;
        }

        public int RefreshTree()
        {
            return this._refresh();
        }
        public void OnObjectsListSelectionChangedHandler(object Sender, object SelectedObject)
        {
            var Node = this._findNodeByObject(null, (CMetaobject)SelectedObject);
            if (Node == null)
                return;

            this.treeObjects.SelectedNode = Node;
        }

        private void mItemExpandAll_Click(object sender, EventArgs e)
        {
            this._expandAll();
        }
        private void mItemCollapseAll_Click(object sender, EventArgs e)
        {
            this._collapseAll();
        }
        private void treeObjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this._selectionChanged(e.Node);
        }
        private void treeObjects_MouseDown(object sender, MouseEventArgs e)
        {
            this._showPopup(e);
        }
    }
}
