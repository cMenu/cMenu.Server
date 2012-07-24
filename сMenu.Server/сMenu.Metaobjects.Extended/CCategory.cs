using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using iMenu.DB;
using iMenu.Common;
using iMenu.Metaobjects;
using iMenu.Security;

namespace iMenu.Metaobjects.Extended
{
    public class CCategory : CSecuredMetaobject
    {
        #region PUBLIC FIELDS
        public EnMenuElementStatus Status
        {
            get { return (EnMenuElementStatus)this._attributes[CMetaobjectsExConsts.CONST_MENU_ATTR_STATUS]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_MENU_ATTR_STATUS] = value; }
        }
        public Image Image
        {
            get { return (Image)this._attributes[CMetaobjectsExConsts.CONST_CAT_ATTR_IMAGE]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_CAT_ATTR_IMAGE] = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CCategory(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._class = EnMetaobjectClass.ECategory;
            this.Status = EnMenuElementStatus.EEnabled;
        }
        public CCategory(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider, false, false)
        {
        }
        public CCategory(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider, false, false)
        {
        }
        #endregion
    }
}
