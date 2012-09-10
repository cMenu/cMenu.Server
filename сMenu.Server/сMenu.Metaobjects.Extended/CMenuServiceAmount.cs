using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using iMenu.DB;
using iMenu.Common;
using iMenu.Metaobjects;
using iMenu.Security;

namespace iMenu.Metaobjects.Extended
{
    public class CMenuServiceAmount : CSecuredMetaobject
    {
        #region PUBLIC FIELDS
        public double Price
        {
            get { return (double)this._attributes[CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_PRICE]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_PRICE] = value; }
        }
        public string Units
        {
            get { return (string)this._attributes[CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_UNITS]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_UNITS] = value; }
        }
        public double Amount
        {
            get { return (double)this._attributes[CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_AMOUNT]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_MENU_SERVICE_ATTR_AMOUNT] = value; }
        }
        public EnMenuElementStatus Status
        {
            get { return (EnMenuElementStatus)this._attributes[CMetaobjectsExConsts.CONST_MENU_ATTR_STATUS]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_MENU_ATTR_STATUS] = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMenuServiceAmount(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._class = EnMetaobjectClass.EMenuServiceAmount;
            this.Status = EnMenuElementStatus.EEnabled;
        }
        public CMenuServiceAmount(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider, false, false)
        {
        }
        public CMenuServiceAmount(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider, false, false)
        {
        }
        #endregion
    }
}
