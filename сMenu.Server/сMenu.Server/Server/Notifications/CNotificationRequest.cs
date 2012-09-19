using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using cMenu.Common;
using cMenu.Common.Base;

namespace cMenu.Communication.Server.Notifications
{
    [DataContractFormat]
    public class CNotificationRequest : CBaseEntity
    {
        #region PROTECTED FIELDSы
        protected string _source = "";
        protected EnNotificationApplicationType _type = EnNotificationApplicationType.EUnknown;
        protected DateTime _date = DateTime.Now;
        protected string _header = "";
        protected object _content = null;        
        #endregion

        #region PUBLIC FIELDS
        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }
        public EnNotificationApplicationType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public object Content
        {
            get { return _content; }
            set { _content = value; }
        }
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion
    }
}
