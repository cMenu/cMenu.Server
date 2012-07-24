using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using iMenu.UI.ManagementTools.Core;

namespace iMenu.UI.ManagementTools.Forms.Common
{
    public partial class frmMtConnectionEditor : Form
    {
        protected EnEditorFormMode _mode = EnEditorFormMode.EEdit;
        protected CMTApplicationConnectionConfiguration _configuration;

        public EnEditorFormMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        public CMTApplicationConnectionConfiguration Configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }

        protected int _initialize()
        {
            if (this._mode == EnEditorFormMode.EEdit)
            {
                this.editName.Text = this._configuration.Name;
                this.editID.Text = this._configuration.ID.ToString().ToUpper();
                this.editUser.Text = this._configuration.UserName;
                this.editPass.Text = this._configuration.Password;
                this.editAddr.Text = this._configuration.ServerAddress;
                this.editDBName.Text = this._configuration.DatabaseName;
                this.cmbServerDBType.SelectedIndex = (int)this.Configuration.DatabaseType;

                if (this.editUser.Text != "" || this.editPass.Text != "")
                    this.chSaveCred.Checked = true;
            }
            return -1;
        }
        protected string _check()
        {
            Guid TmpID = Guid.Empty;

            if (this.editName.Text.Trim() == "")
            {
                this.editName.Focus();
                return "Не указано имя подключения";
            }
            if (this.editID.Text.Trim() == "" || !Guid.TryParse(this.editID.Text.Trim(), out TmpID))
            {
                this.editID.Focus();
                return "Указан неверный идентификатор";
            }
            if (this.editAddr.Text.Trim() == "")
            {
                this.editAddr.Focus();
                return "Не указан адрес сервера БД";
            }
            if (this.editDBName.Text.Trim() == "")
            {
                this.editDBName.Focus();
                return "Не указано наименование БД";
            }
            if (this.cmbServerDBType.SelectedIndex == -1)
            {
                this.cmbServerDBType.Focus();
                return "Не выбран тип БД";
            }

            this._configuration = new CMTApplicationConnectionConfiguration()
            {
                DatabaseName = this.editDBName.Text.Trim(),
                DatabaseType = (iMenu.Common.EnServerDB)this.cmbServerDBType.SelectedIndex,
                ID = Guid.Parse(this.editID.Text.Trim()),
                Name = this.editName.Text.Trim(),
                ServerAddress = this.editAddr.Text.Trim()
            };
            if (this.chSaveCred.Checked)
            {
                this._configuration.UserName = this.editUser.Text.Trim();
                this._configuration.Password = this.editPass.Text.Trim();
            }

            return "";
        }
        protected string _generateID()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        public frmMtConnectionEditor()
        {
            InitializeComponent();
        }

        public int Initialize()
        {
            return this._initialize();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            var R = this._check();
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
            this.editID.Text = this._generateID();
        }
    }
}
