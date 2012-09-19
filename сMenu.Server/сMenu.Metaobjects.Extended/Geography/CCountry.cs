using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.Metaobjects;
using cMenu.DB;

namespace cMenu.Metaobjects.Extended.Geography
{
    public class CCountry : CMetaobjectExtented
    {
        #region CONSTRUCTORS
        public CCountry()
            : base()
        {
            this._class = EnMetaobjectClass.ECountry;
        }
        public CCountry(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._class = EnMetaobjectClass.ECountry;
        }
        public CCountry(Guid ID, IDatabaseProvider Provider) 
            : base(ID, Provider)
        {
            this._class = EnMetaobjectClass.ECountry;
        }
        public CCountry(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
            this._class = EnMetaobjectClass.ECountry;
        }
        #endregion
    }
}
