using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using cMenu.Security;
using cMenu.Security.UsersManagement;
using cMenu.UI.ManagementTools.Core;

namespace cMenu.UI.ManagementTools.Forms.Common
{
    public partial class frmMtConnectionOpen : Form
    {
        protected List<CMTApplicationConnectionConfiguration> _connections;
        protected CMTApplicationConnection _connection;

        public List<CMTApplicationConnectionConfiguration> Connections
        {
            get { return _connections; }
            set { _connections = value; }
        }
        public CMTApplicationConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        protected int _inititalize()
        {
            this.cmbConnections.Items.Clear();
            foreach (CMTApplicationConnectionConfiguration Conn in this._connections)
            {
                this.cmbConnections.Items.Add(string.Format("{0} ({1})", Conn.Name, Conn.ID.ToString().ToUpper()));
            }

            return -1;
        }
        protected int _initControls()
        {
            var SelectedConnection = this._connections[this.cmbConnections.SelectedIndex];
            this.grCred.Enabled = (SelectedConnection.SystemUser == "" || SelectedConnection.SystemPassword == "");
            return -1;
        }
        protected int _openConnection()
        {
            if (this.cmbConnections.SelectedIndex == -1)
            {
                MessageBox.Show("Не выбрано подключение", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -2;
            }

            var SelectedConnection = this._connections[this.cmbConnections.SelectedIndex];
            this._connection = new CMTApplicationConnection() { Configuration = SelectedConnection };
            this._connection.Provider = CMTApplicationContext.sGetProvider(this._connection);
            this._connection.SecurityConnection = this.chSecuredConn.Checked;
            var Login = "";
            var Password = "";

            if (this.grCred.Enabled)
            {
                Login = this.editUser.Text.Trim();
                Password = this.editPass.Text.Trim();
                if (Login == "")
                {
                    MessageBox.Show("Для подключения к серверу необходимо указать имя пользователя", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.editUser.Focus();
                    return -2;
                }
                if (Password == "")
                {
                    MessageBox.Show("Для подключения к серверу необходимо указать пароль", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.editPass.Focus();
                    return -2;
                }

            }
            else
            {
                Login = this._connection.Configuration.SystemUser;
                Password = this._connection.Configuration.SystemPassword;
            }
            if (!this._connection.Provider.TestConnection())
            {
                MessageBox.Show("Невозможно связаться с сервером. Возможно, неверно указана информация о пользователе сервера БД или сервер БД остановлен", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -2;
            }

            var User = CSystemUser.sGetUserByLoginPasshash(this._connection.Provider, Login, Password, CSecurityHelper.sGeneratePasshash(Login, Password));
            if (User == null)
            {
                MessageBox.Show("Указаны неверные данные для аутентификации", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.editUser.Focus();
                return -2;
            }
            if (this.chSecuredConn.Checked && User.ID.ToString().ToUpper() != CEmbeddedSecurityConsts.CONST_USER_ADMINISTRATOR_ID)
            {
                MessageBox.Show("Пользователь с указанным логином не имеет права на просмотр репозитория безопасности", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.editUser.Focus();
                return -2;
            }

            this._connection.CurrentUser = User;
            return -1;
        }

        public frmMtConnectionOpen()
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
        private void cmbConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._initControls();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            var R = this._openConnection();
            if (R == -2)
                return;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
