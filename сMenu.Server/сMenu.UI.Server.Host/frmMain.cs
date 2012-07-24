using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using cMenu.IO;
using cMenu.Common;
using cMenu.Communication;
using cMenu.Communication.Server;
using cMenu.Communication.Server.Environment;
using cMenu.Communication.Server.Configuration;
using cMenu.Windows;
using cMenu.Windows.Controls;
using cMenu.Windows.Helpers;
using cMenu.UI.Server.Host.Common;
using cMenu.UI.Server.Host.Properties;

namespace cMenu.UI.Server.Host
{
    public partial class frmMain : Form
    {
        #region PROTECTED FIELDS
        protected bool _programClosing = false;
        #endregion

        #region EVENT_HANDLERS
        void CommunicationServer_OnCommunicationServerStatusChanged(object Sender, EnCommunicationServerStatus Status)
        {
            this._initServerStatus();
        }
        #endregion

        #region DATA FORMATTING
        protected string _formatDBType(EnServerDB Type)
        {
            var R = "";
            switch (Type)
            {
                case EnServerDB.EMsSQL: R = "MS SQL Server"; break;
                case EnServerDB.EMySQL: R = "MySQL Server"; break;
                case EnServerDB.EOralce: R = "Oracle Server"; break;
                case EnServerDB.ESQLLite: R = "SQLLite Server"; break;
            }
            return R;
        }
        protected string _formatStatus(EnCommunicationServerStatus Status)
        {
            string R = "";
            switch (Status)
            {
                case EnCommunicationServerStatus.EDisabled: R = "Остановлен"; break;
                case EnCommunicationServerStatus.EEnabled: R = "Запущен"; break;
                case EnCommunicationServerStatus.EReady: R = "Готов"; break;
                case EnCommunicationServerStatus.ESuspended: R = "Приостановлен"; break;
            }
            return R;
        }
        #endregion

        #region SERVER MANAGEMENT
        protected int _startServer()
        {
            CApplicationServerEnvironment.sStartServer();
            this.iconTray.Icon = Resources.ICO_SERVER_RUNNING;
            CApplicationServerEnvironment.CommunicationServer.OnCommunicationServerStatusChanged += new OnCommunicationServerStatusChangedDelegate(CommunicationServer_OnCommunicationServerStatusChanged);
            this._initServerStatus();
            this._initControlButtons();
            return -1;
        }       
        protected int _stopServer()
        {
            CApplicationServerEnvironment.sStopServer();
            this.iconTray.Icon = Resources.ICO_SERVER_STOPPED;
            CApplicationServerEnvironment.CommunicationServer.OnCommunicationServerStatusChanged -= new OnCommunicationServerStatusChangedDelegate(CommunicationServer_OnCommunicationServerStatusChanged);
            this._initControlButtons();
            return -1;
        }
        protected int _restartServer()
        {
            CApplicationServerEnvironment.sRestartServer();            
            CApplicationServerEnvironment.CommunicationServer.OnCommunicationServerStatusChanged -= new OnCommunicationServerStatusChangedDelegate(CommunicationServer_OnCommunicationServerStatusChanged);
            CApplicationServerEnvironment.CommunicationServer.OnCommunicationServerStatusChanged += new OnCommunicationServerStatusChangedDelegate(CommunicationServer_OnCommunicationServerStatusChanged);

            this.iconTray.Icon = Resources.ICO_SERVER_RUNNING;
            this._initServerStatus();
            this._initControlButtons();
            return -1;
        }
        #endregion

        #region UI MANAGEMENT
        protected int _hideForm()
        {
            this.sHideFull();
            return -1;
        }
        protected int _showForm()
        {
            this.sShowFull();
            return -1;
        }
        protected int _initServerStatus()
        {
            this.lblStatusValue.Text = this._formatStatus(CApplicationServerEnvironment.CommunicationServer.Status);
            this.Text = "Сервер приложений iMenu 0.1a (" + this.lblStatusValue.Text + ")";
            this.iconTray.Text = this.Text; 
            switch (CApplicationServerEnvironment.CommunicationServer.Status)
            {
                case EnCommunicationServerStatus.EDisabled: this.imgStatus.Image = Resources.ICO_STOPPED; break;
                case EnCommunicationServerStatus.EEnabled: this.imgStatus.Image = Resources.ICO_RUNNING; break;
                case EnCommunicationServerStatus.EReady: this.imgStatus.Image = Resources.ICO_READY; break;
                case EnCommunicationServerStatus.ESuspended: this.imgStatus.Image = Resources.ICO_READY; break;
            }
            /// this.imgStatus.Left = this.pnlStatus.Width - this.imgStatus.Width - 2;

            return -1;
        }
        protected int _initUIValues()
        {
            this.lblAddrValue.Text = CApplicationServerEnvironment.Configuration.Address;
            this.lblIDValue.Text = CApplicationServerEnvironment.Configuration.ID.ToString().ToUpper();
            this.lblNameValue.Text = CApplicationServerEnvironment.Configuration.Name;
            this.lblDbTypeValue.Text = this._formatDBType(CApplicationServerEnvironment.Configuration.DatabaseType);

            this.lblAddrValue.Left = this.lblAddr.Left + this.lblAddr.Width;
            this.lblIDValue.Left = this.lblID.Left + this.lblID.Width;
            this.lblNameValue.Left = this.lblName.Left + this.lblName.Width;
            this.lblDbTypeValue.Left = this.lblDbType.Left + this.lblDbType.Width;            

            return -1;
        }
        protected int _initControlButtons()
        {
            this.btnStart.Enabled = (CApplicationServerEnvironment.CommunicationServer.Status != EnCommunicationServerStatus.EEnabled);
            this.btnStop.Enabled = (CApplicationServerEnvironment.CommunicationServer.Status == EnCommunicationServerStatus.EEnabled);
            this.btnRestart.Enabled = (CApplicationServerEnvironment.CommunicationServer.Status == EnCommunicationServerStatus.EEnabled);
            return -1;
        }
        protected int _initPopItems()
        {
            this.itemRun.Enabled = (CApplicationServerEnvironment.CommunicationServer.Status != EnCommunicationServerStatus.EEnabled);
            this.itemStop.Enabled = (CApplicationServerEnvironment.CommunicationServer.Status == EnCommunicationServerStatus.EEnabled);
            this.itemRestart.Enabled = (CApplicationServerEnvironment.CommunicationServer.Status == EnCommunicationServerStatus.EEnabled);
            return -1;
        }
        protected int _initUI(bool OnResize = false)
        {
            if (CSystemUIHelper.sIsCompositionEnabled())
            {
                CSystemUIHelper.Margins M = new CSystemUIHelper.Margins() { Bottom = -10, Left = 0, Right = 0, Top = this.pnlStatus.Height + this.pnlContent.Height + 10};                
                this.sAeroSet(M);
                if (OnResize)
                    return -1;
                this.sAeroInitialize();
                this.sAeroSetTransparency(this.pnlStatus);
                this.sAeroSetTransparency(this.pnlContent);
                this.sAeroSetTransparency(this.tabMain);
            }
            else
            {
                if (OnResize)
                    return -1;
            }
            return -1;
        }
        protected int _initialize()
        {

            CApplicationServerEnvironment.Configuration = (CApplicationServerConfiguration)CSerialize.sDeserializeXMLFile("Configuration.xml", typeof(CApplicationServerConfiguration));
            if (CApplicationServerEnvironment.Configuration == null)
            {
                CApplicationServerEnvironment.Configuration = new CApplicationServerConfiguration();
                CApplicationServerEnvironment.Configuration.Address = "http://127.0.0.1/ApplicationServer";
                CApplicationServerEnvironment.Configuration.Description = "";
                CApplicationServerEnvironment.Configuration.ID = Guid.NewGuid().ToString().ToUpper();
                CApplicationServerEnvironment.Configuration.Name = "";
                CApplicationServerEnvironment.Configuration.ConnectionString = "Data Source=.\\;Initial Catalog=DB_IMENU;User Id=sa;Password=Qwerty1;";
                CApplicationServerEnvironment.Configuration.DatabaseType = cMenu.Common.EnServerDB.EMsSQL;
                CApplicationServerEnvironment.Configuration.Functions = new List<CServerFunctionConfiguration>();

                CServerFunctionConfiguration FunctionConfiguration = new CServerFunctionConfiguration()
                {
                    Enabled = true,
                    ID = Guid.Parse(CServerFunctionID.CONST_FUNC_ID_AUTHENTICATE),
                    Internal = true,
                    ModuleFileName = "",
                    Name = "Authentication",
                    Path = ""
                };
                CApplicationServerEnvironment.Configuration.Functions.Add(FunctionConfiguration);

                FunctionConfiguration = new CServerFunctionConfiguration()
                {
                    Enabled = true,
                    ID = Guid.Parse(CServerFunctionID.CONST_FUNC_CALL_OFICIANT),
                    Internal = true,
                    ModuleFileName = "",
                    Name = "Call Oficiant",
                    Path = ""
                };
                CApplicationServerEnvironment.Configuration.Functions.Add(FunctionConfiguration);
                CApplicationServerEnvironment.Configuration.SerializeXMLFile("Configuration.xml", typeof(CApplicationServerConfiguration));                
            }            
            CFunctionExecutionEnvironment.ApplicationServerConfiguration = CApplicationServerEnvironment.Configuration;

            this._initUIValues();
            this._initFunctions();

            return -1;
        }
        protected int _initFunctions()
        {
            this.listFuncs.Items.Clear();

            foreach (CServerFunctionConfiguration Function in CApplicationServerEnvironment.Configuration.Functions)
            {
                var Item = new ListViewItem(Function.Name);
                this.listFuncs.Items.Add(Item);
                Item.SubItems.Add(Function.ID.ToString().ToUpper());
                Item.SubItems.Add(Function.Enabled ? "Включена" : "Выключена");
                Item.SubItems.Add(Function.Internal ? "Внутренняя" : "Внешняя");
                Item.SubItems.Add(Function.Path);
                Item.SubItems.Add(Function.ModuleFileName);
            }
            return -1;
        }
        #endregion

        public frmMain()
        {
            InitializeComponent();

            this.Visible = false;
            this._initialize();
            this._startServer();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {            
        }
        private void itemRun_Click(object sender, EventArgs e)
        {
            this._startServer();
        }
        private void itemStop_Click(object sender, EventArgs e)
        {
            this._stopServer();
        }
        private void itemRestart_Click(object sender, EventArgs e)
        {
            this._restartServer();
        }
        private void itemExit_Click(object sender, EventArgs e)
        {
            this._programClosing = true;
            this._stopServer();
            this.Close();
        }
        private void popMain_Opening(object sender, CancelEventArgs e)
        {
            this._initPopItems();
        }
        private void frmMain_Shown(object sender, EventArgs e)
        {
            this._initUI();
            this._hideForm();
        }
        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            this._initUI(true);
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            this.itemRun_Click(null, null);
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            this.itemStop_Click(null, null);
        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.itemRestart_Click(null, null);
        }
        private void iconTray_DoubleClick(object sender, EventArgs e)
        {
            this._showForm();
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this._programClosing)
            {
                this._hideForm();
                e.Cancel = true;
            }
        }
    }
}
