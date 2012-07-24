using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using cMenu.UI.Notification.Common;
using cMenu.IO;
using cMenu.Communication;
using cMenu.Communication.Server;
using cMenu.Communication.Server.Notifications;
using cMenu.Communication.Server.Configuration;
using iMenu.Ext.Media;
using cMenu.Windows.Helpers;
using cMenu.Windows.Controls;

namespace cMenu.UI.Notification
{
    public partial class frmMain : Form
    {
        #region EVENT HANDLERS
        protected void OnNotificationArrivedHandler(CNotificationRequest Notification)
        {
            CNotificationServerEnvironment.Notifications.Add(DateTime.Now, Notification);
            this.iconTray.ShowBalloonTip(7200000, Notification.Header, (string)Notification.Content, ToolTipIcon.Info);
            if (CNotificationServerEnvironment.Configuration.PlayAudio)
            {
                iMenu.Ext.Media.MediaPlayer Player = new MediaPlayer();
                Player.Play(CNotificationServerEnvironment.Configuration.AudioFile);
            }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected int _setAutoStart(bool Autostart)
        {
            string Path = Application.ExecutablePath;
            string KeyPath = "";
            RegistryKey Key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            try
            {
                foreach (string SubKey in Key.GetValueNames())
                {
                    if (SubKey.Trim().ToUpper() == "cMenu.UINOTIFICATION")
                    {
                        if (Autostart)
                        {
                            KeyPath = (string)Key.GetValue(SubKey);
                            if (KeyPath == Path)
                                return -1;
                            else
                            {
                                Key.SetValue(SubKey, "\"" + Path + "\"");
                                return -1;
                            }
                        }
                        else
                            Key.DeleteValue(SubKey);
                    }
                }
                if (Autostart)
                    Key.SetValue("cMenu.UINOTIFICATION", "\"" + Path + "\"");
            }
            catch (Exception Ex)
            { return -2; }           

            return -1;
        }
        protected int _showHistory()
        {
            var frm = new frmHistory();
            frm.Notifications = CNotificationServerEnvironment.Notifications;
            frm.Initialize();
            frm.ShowDialog();

            return -1;
            
        }
        protected int _setupService()
        {
            var frm = new frmSettings();
            frm.ShowDialog();
            if (frm.NeedRestart)
            {
                var R = MessageBox.Show("В процессе настройки приложения был изменен адрес сервиса. Произвести перезапуск сервиса?", "Настройки приложения", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (R == System.Windows.Forms.DialogResult.Yes)
                    this.перезапуститьToolStripMenuItem_Click_1(null, null);
            }
            this._setAutoStart(frm.NeedAutostart);

            return -1;
        }
        protected int _initialize()
        {
            CNotificationServerEnvironment.Configuration = (CNotificationServerConfiguration)CSerialize.sDeserializeXMLFile("Configuration.xml", typeof(CNotificationServerConfiguration));
            if (CNotificationServerEnvironment.Configuration == null)
            {
                CNotificationServerEnvironment.Configuration = new CNotificationServerConfiguration();
                CNotificationServerEnvironment.Configuration.Address = "http://127.0.0.1/Notification_1";
                CNotificationServerEnvironment.Configuration.ApplicationType = Communication.EnNotificationApplicationType.EUserInterfaceNotification;
                CNotificationServerEnvironment.Configuration.Description = "";
                CNotificationServerEnvironment.Configuration.ID = Guid.NewGuid().ToString().ToUpper();
                CNotificationServerEnvironment.Configuration.Name = "";
                CNotificationServerEnvironment.Configuration.PlayAudio = false;
                CNotificationServerEnvironment.Configuration.AudioFile = "";
                CNotificationServerEnvironment.Configuration.AutostartService = false;
                CNotificationServerEnvironment.Configuration.AutostartApplication = false;

                CNotificationServerEnvironment.Configuration.SerializeXMLFile("Configuration.xml", typeof(CNotificationServerConfiguration));
            }

            if (CNotificationServerEnvironment.Configuration.AutostartService)
            {
                this.запуститьToolStripMenuItem_Click(null, null);
                this.iconTray.Icon = cMenu.UI.Notification.Properties.Resources.ICO_SERVICE_RUNNING;
            }            
            else
                this.iconTray.Icon = cMenu.UI.Notification.Properties.Resources.ICO_SERVICE_STOPPED;
            return -1;
        }
        protected int _initPopItems()
        {
            this.runItem.Enabled = (CNotificationServerEnvironment.Server == null || CNotificationServerEnvironment.Server.Status != EnCommunicationServerStatus.EEnabled);
            this.stopItem.Enabled = (CNotificationServerEnvironment.Server != null && CNotificationServerEnvironment.Server.Status == EnCommunicationServerStatus.EEnabled);
            this.restartItem.Enabled = (CNotificationServerEnvironment.Server != null && CNotificationServerEnvironment.Server.Status == EnCommunicationServerStatus.EEnabled);
            return -1;
        }
        #endregion

        public frmMain()
        {
            InitializeComponent();

            this.Visible = false;
            this._initialize();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CNotificationServerEnvironment.sStopServer();
            this.Close();
        }
        private void запуститьToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            var R = CNotificationServerEnvironment.sStartServer();
            if (R == -1)
            {
                this.iconTray.Icon = cMenu.UI.Notification.Properties.Resources.ICO_SERVICE_RUNNING;
                CUINotificationServer.OnNotificationArrived += this.OnNotificationArrivedHandler;
            }
        }
        private void остановитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.iconTray.Icon = cMenu.UI.Notification.Properties.Resources.ICO_SERVICE_STOPPED;
            CNotificationServerEnvironment.sStopServer();
            CUINotificationServer.OnNotificationArrived -= this.OnNotificationArrivedHandler;
        }
        private void popMain_Opening(object sender, CancelEventArgs e)
        {
            this._initPopItems();
        }
        private void settingsItem_Click(object sender, EventArgs e)
        {
            this._setupService();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.sHideFull();
        }
        private void историяУведомленийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._showHistory();
        }
        private void перезапуститьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.iconTray.Icon = cMenu.UI.Notification.Properties.Resources.ICO_SERVICE_STOPPED;
            CNotificationServerEnvironment.sRestartServer();
            CUINotificationServer.OnNotificationArrived -= this.OnNotificationArrivedHandler;
            CUINotificationServer.OnNotificationArrived += this.OnNotificationArrivedHandler;
            this.iconTray.Icon = cMenu.UI.Notification.Properties.Resources.ICO_SERVICE_RUNNING;
        }
    }
}
