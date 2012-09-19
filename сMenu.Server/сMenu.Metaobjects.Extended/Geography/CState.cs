using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.Metaobjects;
using cMenu.DB;

namespace cMenu.Metaobjects.Extended.Geography
{
    public class CState : CMetaobjectExtented
    {
        #region CONSTRUCTORS
        public CState()
            : base()
        {
            this._class = EnMetaobjectClass.EState;
        }
        public CState(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._class = EnMetaobjectClass.EState;
        }
        public CState(Guid ID, IDatabaseProvider Provider) 
            : base(ID, Provider)
        {
            this._class = EnMetaobjectClass.EState;
        }
        public CState(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
            this._class = EnMetaobjectClass.EState;
        }
        #endregion
    }
}
