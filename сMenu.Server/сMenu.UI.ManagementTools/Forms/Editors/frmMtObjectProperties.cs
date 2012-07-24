using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using cMenu.Metaobjects;
using cMenu.DB;

namespace cMenu.UI.ManagementTools.Forms.Editors
{
    public partial class frmMtObjectProperties : Form
    {
        protected CMetaobject _object;
        protected IDatabaseProvider _provider;
        protected bool _allowEdit = true;

        public CMetaobject Object
        {
            get { return _object; }
            set 
            { 
                _object = value;
                this.Text = this.Text + " - " + this._object.Name;
            }
        }
        public IDatabaseProvider Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }
        public bool AllowEdit
        {
            get { return _allowEdit; }
            set { _allowEdit = value; }
        }

        protected string _checkResults()
        {
            if (!this._allowEdit)
                return "";

            Guid TmpID = Guid.Empty;

            if (this.editName.Text.Trim() == "")
            {
                this.editName.Focus();
                return "Не указано имя объекта";
            }
            if (!Guid.TryParse(this.editID.Text.Trim(), out TmpID))
            {
                this.editID.Focus();
                return "Неверно указан идентификатор объекта";
            }
            if (CMetaobject.sExists(this._object.Key, TmpID, this._provider))
            {
                this.editID.Focus();
                return "Объект с указанным идентификатором уже существует";
            }

            this._object.Name = this.editName.Text.Trim();
            this._object.ID = Guid.Parse(this.editID.Text.Trim());
            this._object.Description = this.editDesc.Text.Trim();

            return "";
        }
        protected int _inititalize()
        {
            this.editName.Text = this._object.Name;
            this.editID.Text = this._object.ID.ToString().ToUpper();
            this.editKey.Text = this._object.Key.ToString();
            this.editDesc.Text = this._object.Description;

            this._loadInnerLinks();
            this._loadOuterLinks();

            this.editDesc.Enabled = this._allowEdit;
            this.editName.Enabled = this._allowEdit;
            this.editID.Width = (this._allowEdit ? this.editID.Width : this.editName.Width);
            this.btnNewID.Visible = this._allowEdit;

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
        protected int _generateID()
        {
            this.editID.Text = Guid.NewGuid().ToString().ToUpper();
            return -1;
        }
        protected int _loadInnerLinks()
        {
            this.treeInnerLinks.Nodes.Clear();
            foreach (CMetaobjectLink Link in this._object.ExternalLinks)
            {
                var Object = Link.GetLinkedObject(this._provider);
                TreeNode Node = this.treeInnerLinks.Nodes.Add(Object.Name);
                Node.ImageIndex = this._getImageIndex(Object);
                Node.SelectedImageIndex = this._getImageIndex(Object);
            }
            return -1;
        }
        protected int _loadOuterLinks()
        {
            this.treeOuterLinks.Nodes.Clear();
            foreach (CMetaobjectLink Link in this._object.InternalLinks)
            {
                var Object = Link.GetSourceObject(this._provider);
                TreeNode Node = this.treeOuterLinks.Nodes.Add(Object.Name);
                Node.ImageIndex = this._getImageIndex(Object);
                Node.SelectedImageIndex = this._getImageIndex(Object);
            }
            return -1;
        }

        public frmMtObjectProperties()
        {
            InitializeComponent();
        }

        public int Initialize()
        {
            return this._inititalize();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            var R = this._checkResults();
            if (R != "")
            {
                MessageBox.Show(R, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        private void btnNewID_Click(object sender, EventArgs e)
        {
            this._generateID();
        }
    }
}
