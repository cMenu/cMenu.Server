using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.UI.ManagementTools.WPF.Core.MVVM
{
    public class CMVVMCachedNotificationObject : CMVVMNotificationObject
    {
        #region PROTECTED FIELDS
        protected CMVVMModelCache _cache = new CMVVMModelCache();
        #endregion

        #region PUBLIC FIELDS
        public CMVVMModelCache Cache
        {
            get { return _cache; }
            set { _cache = value; }
        }
        #endregion

        #region PUBLIC FIELDS
        #endregion
    }
}
