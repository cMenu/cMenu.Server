using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using cMenu.UI.ManagementTools.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace cMenu.UI.ManagementTools.Forms.Common
{
    public partial class frmMtServer : DockContent
    {
        protected CMTApplicationConnection _connection;
        protected frmMtServerObjectTree _objectsTree = new frmMtServerObjectTree();
        protected frmMtServerObjectList _objectsList = new frmMtServerObjectList();

        public CMTApplicationConnection Connection
        {
            get { return _connection; }
            set 
            { 
                _connection = value;
                _objectsTree.Connection = value;
                _objectsList.Connection = value;
            }
        }

        protected int _dispose()
        {
            CMTApplicationContext.ConnectionForms.Remove(this);
            CMTApplicationContext.sRemoveConnection(this._connection.Configuration.ID);
            return -1;
        }
        protected int _refreshObjects()
        {
            return this._objectsTree.RefreshTree();
        }
        protected int _showObjectsTree()
        {
            this._objectsTree.Show(this.dockMain, DockState.DockLeft); 
            return -1;
        }
        protected int _showObjectsTable()
        {
            this._objectsList.Show(this.dockMain, DockState.Document); 
            return -1;
        }
        protected int _initialize()
        {
            this._showObjectsTree();
            this._showObjectsTable();

            this._objectsTree.OnObjectsTreeSelectionChanged += new OnObjectsTreeSelectionChangedDelegate(this._objectsList.OnObjectsTreeSelectionChangedHandler);
            this._objectsList.OnObjectsListSelectionChanged += new OnObjectsListSelectionChangedDelegate(this._objectsTree.OnObjectsListSelectionChangedHandler);
            this._objectsTree.OnFormExecuteAction += new OnFormExecuteActionDelegate(this._objectsList.OnFormExecuteActionHandler);

            return -1;
        }

        public frmMtServer()
        {
            InitializeComponent();
        }

        public int Initialize()
        {
            this._initialize();
            this._refreshObjects();
            return -1;
        }
        private void frmMtServer_FormClosed(object sender, FormClosedEventArgs e)
        {
            this._dispose();
        }

        private void frmMtServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            var R = MessageBox.Show("Вы хотите закрыть текущее соединение?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (R != System.Windows.Forms.DialogResult.Yes)
                e.Cancel = true;
        }
    }
}
