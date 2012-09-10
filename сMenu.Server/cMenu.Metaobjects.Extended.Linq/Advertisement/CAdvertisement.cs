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

namespace cMenu.Metaobjects.Extended.Linq.Advertisement
{
    [Serializable]
    public class CAdvertisement : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        #endregion

        #region PUBLIC FIELDS
        #endregion

        #region CONSTRUCTORS
        public CAdvertisement()
            : base()
        {
            this._class = EnMetaobjectClass.EAdvertisement;
        }
        public CAdvertisement(DataContext Context)
            : base(Context)
        {
            this._class = EnMetaobjectClass.EAdvertisement;
        }
        public CAdvertisement(decimal Key, DataContext Context)
            : base(Key, Context)
        {
        }
        public CAdvertisement(Guid ID, DataContext Context)
            : base(ID, Context)
        {
        }
        #endregion
    }
}
