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
    public class CCountry : CMetaobjectExtented
    {
        #region CONSTRUCTORS
        public CCountry()
            : base()
        {
            this._class = EnMetaobjectClass.ECountry;
        }
        public CCountry(DataContext Context)
            : base(Context)
        {
            this._class = EnMetaobjectClass.ECountry;
        }
        public CCountry(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._class = EnMetaobjectClass.ECountry;
        }
        public CCountry(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._class = EnMetaobjectClass.ECountry;
        }
        #endregion
    }
}
