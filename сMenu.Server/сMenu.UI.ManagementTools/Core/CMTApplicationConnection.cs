using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.Security;
using cMenu.Security.UsersManagement;

namespace cMenu.UI.ManagementTools.Core
{
    public class CMTApplicationConnection
    {
        #region PROTECTED FIELDS
        protected CMTApplicationConnectionConfiguration _configuration;
        protected IDatabaseProvider _provider;
        protected bool _securityConnection = false;
        protected CSystemUser _currentUser;
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
        public bool SecurityConnection
        {
            get { return _securityConnection; }
            set { _securityConnection = value; }
        }
        public CSystemUser CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }
        #endregion
    }
}
