using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using cMenu.Communication.Server.Notifications;
using cMenu.Windows;
using cMenu.Windows.Helpers;
using cMenu.Windows.Controls;

namespace cMenu.UI.Notification
{
    public partial class frmHistory : Form
    {
        protected Hashtable _notifications;

        public Hashtable Notifications
        {
            get { return _notifications; }
            set { _notifications = value; }
        }

        protected int _loadNotifications()
        {
            this.listNotifications.Items.Clear();

            var Keys = new DateTime[this._notifications.Keys.Count];
            this._notifications.Keys.CopyTo(Keys, 0);

            for (int i = 0; i < Keys.Length; i++)
            {
                var Item = new ListViewItem();
                var N = (CNotificationRequest)this._notifications[Keys[i]];
                Item.Text = N.Name;
                Item.SubItems.Add(N.Source);
                Item.SubItems.Add(N.Type.ToString());
                Item.SubItems.Add(N.Date.ToString());
                Item.SubItems.Add(N.Header);
                Item.SubItems.Add((string)N.Content);
                this.listNotifications.Items.Add(Item);
            }
            
            return -1;
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

        public frmHistory()
        {
            InitializeComponent();            
        }

        public int Initialize()
        {
            this._loadNotifications();

            return -1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmHistory_Load(object sender, EventArgs e)
        {
            this._initUI();
        }
    }
}
