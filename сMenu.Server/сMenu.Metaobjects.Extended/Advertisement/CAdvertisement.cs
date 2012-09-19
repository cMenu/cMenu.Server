using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.Common;
using cMenu.Metaobjects;
using cMenu.Security;

namespace cMenu.Metaobjects.Extended.Advertisement
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
        public CAdvertisement(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._class = EnMetaobjectClass.EAdvertisement;
        }
        public CAdvertisement(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
        }
        public CAdvertisement(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
        }
        #endregion

        #region PUBLIC FUNCTIONS
        #endregion
    }
}
