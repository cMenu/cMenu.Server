using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.Security;
using cMenu.Security.UsersManagement;

namespace cMenu.UI.ManagementTools.WPF.Core
{
    public class CMTApplicationConnection
    {
        #region PROTECTED FIELDS
        protected CMTApplicationConnectionConfiguration _configuration;
        protected IDatabaseProvider _provider;
        protected bool _isConnectionSecurity = false;
        protected CSystemUser _user;
        #endregion

        #region PUBLIC FIELDS
        public CMTApplicationConnectionConfiguration Configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }
        public IDatabaseProvider Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }        
        public CSystemUser User
        {
            get { return _user; }
            set { _user = value; }
        }
        public bool IsConnectionSecurity
        {
            get { return _isConnectionSecurity; }
            set { _isConnectionSecurity = value; }
        }
        #endregion
    }
}
