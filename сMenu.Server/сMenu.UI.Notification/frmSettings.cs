using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.IO;

using cMenu.IO;
using cMenu.Windows;
using cMenu.Windows.Helpers;
using cMenu.Windows.Controls;
using cMenu.UI.Notification.Common;
using cMenu.Communication;
using cMenu.Communication.Server;
using cMenu.Communication.Server.Notifications;
using cMenu.Communication.Server.Configuration;

namespace cMenu.UI.Notification
{
    public partial class frmSettings : Form
    {
        protected bool _needAutostart = false;
        protected bool _needRestart = false;
        protected bool _programAction = false;
        protected bool _changed = false;

        public bool NeedRestart
        {
            get { return _needRestart; }
            set { _needRestart = value; }
        }
        public bool NeedAutostart
        {
            get { return _needAutostart; }
            set { _needAutostart = value; }
        }

        protected int _initUI()
        {
            if (CSystemUIHelper.sIsCompositionEnabled())
            {
                CSystemUIHelper.Margins M = new CSystemUIHelper.Margins() { Bottom = 0, Left = 0, Right = 0, Top = 0 };
                this.sAeroInitialize();
                this.sAeroSet(M);
            }
            return -1;
        }
        protected int _initControlButtons()
        {
            this.btnApply.Enabled = this._changed;
            return -1;
        }
        protected int _loadSettingsToUI()
        {
            this._programAction = true;
                this.editAddr.Text = CNotificationServerEnvironment.Configuration.Address;
                this.editDesc.Text = CNotificationServerEnvironment.Configuration.Description;
                this.editName.Text = CNotificationServerEnvironment.Configuration.Name;
                this.chAudio.Checked = CNotificationServerEnvironment.Configuration.PlayAudio;
                if (this.chAudio.Checked)
                    this.editAudio.Text = CNotificationServerEnvironment.Configuration.AudioFile;
                this.chAutostart.Checked = CNotificationServerEnvironment.Configuration.AutostartApplication;
                this.chStartService.Checked = CNotificationServerEnvironment.Configuration.AutostartService;
            this._programAction = false;
            return -1;
        }
        protected string _saveSettingsFromUI()
        {
            if (this.editName.Text.Trim() == "")
            {
                this.editName.Focus();
                return "Не указано наименование сервиса";
            }
            if (this.editAddr.Text.Trim() == "")
            {
                this.editAddr.Focus();
                return "Не указан адрес сервиса";
            }
            if (this.chAudio.Checked && !File.Exists(this.editAudio.Text.Trim()))
            {
                this.editAudio.Focus();
                return "Выбранный аудио файл не существует";
            }

            CNotificationServerEnvironment.Configuration.Name = this.editName.Text.Trim();
            if (!this._needRestart)
                this._needRestart = (CNotificationServerEnvironment.Configuration.Address.ToUpper() != this.editAddr.Text.Trim().ToUpper());
            CNotificationServerEnvironment.Configuration.Address = this.editAddr.Text.Trim();
            CNotificationServerEnvironment.Configuration.Description = this.editDesc.Text.Trim();
            CNotificationServerEnvironment.Configuration.PlayAudio = this.chAudio.Checked;
            CNotificationServerEnvironment.Configuration.AutostartApplication = this.chAutostart.Checked;
            CNotificationServerEnvironment.Configuration.AutostartService = this.chStartService.Checked;
            if (this.chAudio.Checked)
                CNotificationServerEnvironment.Configuration.AudioFile = this.editAudio.Text.Trim();

            CNotificationServerEnvironment.Configuration.SerializeXMLFile("Configuration.xml", typeof(CNotificationServerConfiguration));

            this._needAutostart = this.chAutostart.Checked;
            return "";
        }
        protected string _selectAudioFile()
        {
            var Dlg = new OpenFileDialog();
            Dlg.Title = "Выбрать аудио файл";
            Dlg.Multiselect = false;
            Dlg.Filter = "Аудио файлы (*.mp3)|*.mp3";
            if (Dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return Dlg.FileName;
            else
                return "";
        }

        public frmSettings()
        {
            InitializeComponent();
            this._loadSettingsToUI();
            this._initControlButtons();
        }

        private void editName_TextChanged(object sender, EventArgs e)
        {
            if (this._programAction)
                return;

            this._changed = true;
            this._initControlButtons();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this._changed)
            {
                var R = MessageBox.Show("Настройки приложения были изменены. Вы хотите сохранить новые значения?", "Настройки приложения", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (R == System.Windows.Forms.DialogResult.Yes)
                {
                    var Message = this._saveSettingsFromUI();
                    if (Message == "")
                        this.Close();
                    else
                        MessageBox.Show(Message, "Настройки приложения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (R == System.Windows.Forms.DialogResult.No)
                {
                    this.Close();
                    return;
                }
                if (R == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
            }
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            var Message = this._saveSettingsFromUI();
            if (Message != "")
                MessageBox.Show(Message, "Настройки приложения", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                this.btnApply.Enabled = false;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            var Message = this._saveSettingsFromUI();
            if (Message == "")
                this.Close();
            else
                MessageBox.Show(Message, "Настройки приложения", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void chAudio_CheckedChanged(object sender, EventArgs e)
        {
            this.editAudio.Enabled = (sender as CheckBox).Checked;
            this.btnAudio.Enabled = (sender as CheckBox).Checked;

            if (this._programAction)
                return;
            this._changed = true;
            this._initControlButtons();
        }
        private void btnAudio_Click(object sender, EventArgs e)
        {
            var R = this._selectAudioFile();
            if (R == "")
                return;
            this.editAudio.Text = R;
        }
        private void chaAutostart_CheckedChanged(object sender, EventArgs e)
        {
            if (this._programAction)
                return;
            this._changed = true;
            this._initControlButtons();
        }
        private void chStartService_CheckedChanged(object sender, EventArgs e)
        {
            if (this._programAction)
                return;
            this._changed = true;
            this._initControlButtons();
        }
        private void frmSettings_Load(object sender, EventArgs e)
        {
            this._initUI();
        }
    }
}
