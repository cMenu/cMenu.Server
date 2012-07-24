using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using cMenu.UI.ManagementTools.Core;

namespace cMenu.UI.ManagementTools.Forms.Common
{
    public partial class frmMtConnectionManager : Form
    {
        protected List<CMTApplicationConnectionConfiguration> _connections;

        public List<CMTApplicationConnectionConfiguration> Connections
        {
            get { return _connections; }
            set { _connections = value; }
        }

        protected int _initControlButtons()
        {
            this.btnConnEdit.Enabled = this.listConnections.SelectedItems.Count != 0;
            this.btnConnDel.Enabled = this.listConnections.SelectedItems.Count != 0;
            return -1;
        }
        protected int _loadConnections()
        {
            this.listConnections.Items.Clear();
            foreach (CMTApplicationConnectionConfiguration Conn in this._connections)
            {
                ListViewItem Item = new ListViewItem(Conn.Name);
                Item.SubItems.Add(Conn.ID.ToString().ToUpper());
                Item.SubItems.Add(Conn.DatabaseType.ToString());
                Item.SubItems.Add(Conn.ServerAddress);
                Item.SubItems.Add(Conn.DatabaseName);
                Item.Tag = Conn;

                this.listConnections.Items.Add(Item);
            }

            return -1;
        }
        protected int _initialize()
        {
            this._loadConnections();
            return -1;
        }
        protected int _connectionAdd()
        {
            var frm = new frmMtConnectionEditor();
            frm.Mode = EnEditorFormMode.ECreate;
            frm.Initialize();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var Config = frm.Configuration;
                this._connections.Add(Config);

                ListViewItem Item = new ListViewItem(Config.Name);
                Item.SubItems.Add(Config.ID.ToString().ToUpper());
                Item.SubItems.Add(Config.DatabaseType.ToString());
                Item.SubItems.Add(Config.ServerAddress);
                Item.SubItems.Add(Config.DatabaseName);
                Item.Tag = Config;

                this.listConnections.Items.Add(Item);
            }
            return -1;
        }
        protected int _connectionEdit()
        {
            if (this.listConnections.SelectedItems.Count == 0)
                return -2;
            var SelectedItem = (CMTApplicationConnectionConfiguration)this.listConnections.SelectedItems[0].Tag;

            var frm = new frmMtConnectionEditor()
            {
                Configuration = SelectedItem
            };
            frm.Mode = EnEditorFormMode.EEdit;
            frm.Initialize();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {                
                var Config = frm.Configuration;
                this.listConnections.SelectedItems[0].Tag = Config;
                this._connections[this.listConnections.SelectedItems[0].Index] = Config;
            }
            return -1;
        }
        protected int _connectionDelete()
        {
            var R = MessageBox.Show("Вы хотите удалить выбранный элемент?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (R == System.Windows.Forms.DialogResult.Yes)
            {
                var Item = this.listConnections.SelectedItems[0];
                this._connections.RemoveAt(Item.Index);
                Item.Remove();
            }

            return -1;
        }

        public frmMtConnectionManager()
        {
            InitializeComponent();
        }

        public int Initialize()
        {
            this._initialize();
            return -1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnConnAdd_Click(object sender, EventArgs e)
        {
            this._connectionAdd();
        }
        private void btnConnEdit_Click(object sender, EventArgs e)
        {
            this._connectionEdit();
        }
        private void listConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._initControlButtons();
        }
        private void btnConnDel_Click(object sender, EventArgs e)
        {
            this._connectionDelete();
        }
        private void listConnections_DoubleClick(object sender, EventArgs e)
        {
            this._connectionEdit();
        }
    }
}
