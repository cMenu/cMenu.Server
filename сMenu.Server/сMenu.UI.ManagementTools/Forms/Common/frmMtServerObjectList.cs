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
using cMenu.UI.ManagementTools.Forms.Editors;
using WeifenLuo.WinFormsUI.Docking;

namespace cMenu.UI.ManagementTools.Forms.Common
{
    public partial class frmMtServerObjectList : DockContent
    {
        #region PROTECTED FIELDS
        protected CMTApplicationConnection _connection;
        protected CMetaobject _rootObject;
        #endregion

        #region PUBLIC EVENTS
        public event OnObjectsListSelectionChangedDelegate OnObjectsListSelectionChanged;
        #endregion

        #region PUBLIC FIELDS
        public CMTApplicationConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }
        #endregion

        protected string _getClassName(EnMetaobjectClass Class)
        {
            var R = "";
            switch (Class)
            {
                case EnMetaobjectClass.EFolder: R = "Папка"; break;
                case EnMetaobjectClass.ESystemUserGroup: R = "Группа пользователей"; break;
                case EnMetaobjectClass.ESystemUser: R = "Пользователь"; break;
                case EnMetaobjectClass.ESystemPolicy: R = "Политика безопасности"; break;
                case EnMetaobjectClass.EClientDevice: R = "Клиентское устройство"; break;
            }
            return R;
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
        protected int _loadObjects()
        {
            this.listObjects.Items.Clear();
            foreach (CMetaobject Child in this._rootObject.Children)
            {
                ListViewItem Item = new ListViewItem(Child.Name);
                Item.Tag = Child;
                Item.ImageIndex = this._getImageIndex(Child);
                Item.SubItems.Add(Child.ModificatonDate.ToString());
                Item.SubItems.Add(this._getClassName(Child.Class));

                this.listObjects.Items.Add(Item);
            }

            this._adjustColumns();
            return -1;
        }
        protected int _adjustColumns()
        {
            foreach (ColumnHeader Column in this.listObjects.Columns)
                Column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            return -1;
        }
        protected int _selectObject()
        {
            if (this.listObjects.SelectedItems.Count == 0)
                return -2;
            var SelectedObject = (CMetaobject)this.listObjects.SelectedItems[0].Tag;

            if (this.OnObjectsListSelectionChanged != null)
                this.OnObjectsListSelectionChanged(this, SelectedObject);
            return -1;
        }
        protected int _showProperties()
        {
            if (this.listObjects.SelectedItems.Count == 0)
                return -2;
            var Object = (CMetaobject)this.listObjects.SelectedItems[0].Tag;
            Object.GetExternalLinks(this._connection.Provider);
            Object.GetInternalLinks(this._connection.Provider);

            var Records = this._connection.CurrentUser.GetSecurityRecords(this._connection.Provider);
            var Record = Records.Where(R => R.MetaobjectKey == Object.Key).ToList();
            var Rights = (Record.Count == 0 ? 1 : Record[0].Rights);

            var frm = new frmMtObjectProperties();
            frm.Object = Object;
            frm.Provider = this._connection.Provider;
            frm.Initialize();
            frm.AllowEdit = (Rights >= 2);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK && frm.AllowEdit)
            {                
                this.listObjects.SelectedItems[0].Tag = frm.Object;
                this.listObjects.SelectedItems[0].Text = frm.Object.Name;
                frm.Object.ObjectUpdate(this._connection.Provider);
            }
            return -1;
        }
        protected int _showProperties(CMetaobject Object)
        {
            Object.GetExternalLinks(this._connection.Provider);
            Object.GetInternalLinks(this._connection.Provider);

            var Records = this._connection.CurrentUser.GetSecurityRecords(this._connection.Provider);
            var Record = Records.Where(R => R.MetaobjectKey == Object.Key).ToList();
            var Rights = (Record.Count == 0 ? 1 : Record[0].Rights);

            var frm = new frmMtObjectProperties();
            frm.Object = Object;
            frm.Provider = this._connection.Provider;
            frm.Initialize();
            frm.AllowEdit = (Rights >= 2);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK && frm.AllowEdit)
            {
                frm.Object.ObjectUpdate(this._connection.Provider);
                this._loadObjects();
            }
            return -1;
        }

        public frmMtServerObjectList()
        {
            InitializeComponent();
        }

        public void OnObjectsTreeSelectionChangedHandler(object Sender, object SelectedObject)
        {
            this._rootObject = (CMetaobject)SelectedObject;
            this._loadObjects();
        }
        public void OnFormExecuteActionHandler(object Sender, Hashtable Parameters, string Action)
        {
            switch (Action)
            {
                case CMTApplicationActionsConsts.CONST_ACTION_SHOW_PROPERTIES:
                    this._showProperties((CMetaobject)Parameters[CMTApplicationActionsParametersConsts.CONST_ACTION_PARAM_METAOBJECT]);
                    break;
            }
        }
        private void listObjects_DoubleClick(object sender, EventArgs e)
        {
            this._selectObject();
        }
        private void mItemObjectProp_Click(object sender, EventArgs e)
        {
            this._showProperties();
        }
    }
}
