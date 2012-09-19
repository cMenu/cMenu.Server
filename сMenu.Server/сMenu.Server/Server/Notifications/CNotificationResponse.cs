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
    public class CNotificationResponse : CBaseEntity
    {
        #region PROTECTED FIELDS
        protected int _errorCode = -1;
        protected object _content = null;
        protected string _message = "";
        #endregion

        #region PUBLIC FIELDS
        public int ErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }
        public object Content
        {
            get { return _content; }
            set { _content = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        #endregion
    }
}
