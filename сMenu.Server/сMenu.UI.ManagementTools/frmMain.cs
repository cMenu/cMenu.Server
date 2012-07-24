using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using cMenu.IO;
using cMenu.UI.ManagementTools.Core;
using cMenu.UI.ManagementTools.Forms.Common;
using WeifenLuo.WinFormsUI.Docking;

namespace cMenu.UI.ManagementTools
{
    public partial class frmMain : Form
    {
        #region PROTECTED FUNCTIONS
        protected int _initialize()
        {
            CMTApplicationContext.Configuration = (CMTApplicationConfiguration)CSerialize.sDeserializeXMLFile("Configuration.xml", typeof(CMTApplicationConfiguration));
            if (CMTApplicationContext.Configuration == null)
            {
                CMTApplicationContext.Configuration = new CMTApplicationConfiguration();
                CMTApplicationContext.Configuration.SerializeXMLFile("Configuration.xml", typeof(CMTApplicationConfiguration));
            }

            return -1;
        }
        protected int _editConnections()
        {
            var frm = new frmMtConnectionManager();            
            frm.Connections = CMTApplicationContext.Configuration.Connections;
            frm.Initialize();
            frm.ShowDialog();
            CMTApplicationContext.Configuration.Connections = frm.Connections;
            CMTApplicationContext.Configuration.SerializeXMLFile("Configuration.xml", typeof(CMTApplicationConfiguration));

            return -1;
        }
        protected int _connectionOpen()
        {
            var frm = new frmMtConnectionOpen() { Connections = CMTApplicationContext.Configuration.Connections };
            frm.Initialize();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var Connection = frm.Connection;
                if (CMTApplicationContext.sGetConnection(Connection.Configuration.ID) != null)
                {
                    var frmServer = CMTApplicationContext.ConnectionForms.Where(F => F.Connection.Configuration.ID == Connection.Configuration.ID).ToList()[0];
                    frmServer.Activate();
                    return -3;
                }

                CMTApplicationContext.sSetActiveConnection(Connection.Configuration.ID, Connection);
                var frmChild = new frmMtServer();
                frmChild.Text = "Сервер iMenu [" + Connection.Configuration.Name + "]";
                frmChild.HideOnClose = false;
                frmChild.Connection = Connection;
                frmChild.Initialize();                
                frmChild.Show(this.dockMain, DockState.Document);                
                CMTApplicationContext.ConnectionForms.Add(frmChild);
            }
            return -1;
        }
        private void _closeActiveConnection()
        {
            if (this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
        }
        #endregion

        public frmMain()
        {
            InitializeComponent();
            this._initialize();
        }

        #region EVENT HANDLERS
        private void btnEditConnections_Click(object sender, EventArgs e)
        {
            this._editConnections();
        }
        private void mItemServerConnectEdit_Click(object sender, EventArgs e)
        {
            this._editConnections();
        }
        private void btnConnectServer_Click(object sender, EventArgs e)
        {
            this._connectionOpen();
        }
        private void mItemServerConnect_Click(object sender, EventArgs e)
        {
            this._connectionOpen();
        }
        private void btnCloseConnection_Click(object sender, EventArgs e)
        {
            this._closeActiveConnection();
        }
        private void frmMain_Shown(object sender, EventArgs e)
        {
            this._connectionOpen();
        }
        private void mItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
