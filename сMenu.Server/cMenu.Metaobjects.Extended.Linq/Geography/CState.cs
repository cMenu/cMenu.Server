using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects.Extended;
using cMenu.Metaobjects.Linq;
using cMenu.Security.Linq;

namespace cMenu.Metaobjects.Extended.Linq.Geography
{
    [Serializable]
    public class CState : CMetaobjectExtented
    {
        #region CONSTRUCTORS
        public CState()
            : base()
        {
            this._class = EnMetaobjectClass.EState;
        }
        public CState(DataContext Context)
            : base(Context)
        {
            this._class = EnMetaobjectClass.EState;
        }
        public CState(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._class = EnMetaobjectClass.EState;
        }
        public CState(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._class = EnMetaobjectClass.EState;
        }
        #endregion
    }
}
