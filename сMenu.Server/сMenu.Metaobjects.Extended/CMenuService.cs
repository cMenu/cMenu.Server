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
    public class CMenuService : CSecuredMetaobject
    {
        #region PUBLIC FIELDS
        public string Composition
        {
            get { return (string)this._attributes[CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_COMPOS]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_COMPOS] = value; }
        }
        public double Rating
        {
            get { return (double)this._attributes[CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_RATING]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_RATING] = value; }
        }
        public EnMenuElementStatus Status
        {
            get { return (EnMenuElementStatus)this._attributes[CMetaobjectsExConsts.CONST_MENU_ATTR_STATUS]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_MENU_ATTR_STATUS] = value; }
        }
        public Image Image
        {
            get { return (Image)this._attributes[CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_IMAGE]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_IMAGE] = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMenuService(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._class = EnMetaobjectClass.EMenuService;
            this.Status = EnMenuElementStatus.EEnabled;
        }
        public CMenuService(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider, false, false)
        {
        }
        public CMenuService(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider, false, false)
        {
        }
        #endregion
    }
}
