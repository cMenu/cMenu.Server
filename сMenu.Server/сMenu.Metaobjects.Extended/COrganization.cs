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
    public class COrganization : CSecuredMetaobject
    {
        #region PUBLIC FIELDS
        public string Address
        {
            get { return (string)this._attributes[CMetaobjectsExConsts.CONST_ORG_ATTR_ADDRESS]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_ORG_ATTR_ADDRESS] = value; }
        }
        public string PhoneNumber1
        {
            get { return (string)this._attributes[CMetaobjectsExConsts.CONST_ORG_ATTR_PHONE_1]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_ORG_ATTR_PHONE_1] = value; }
        }
        public string PhoneNumber2
        {
            get { return (string)this._attributes[CMetaobjectsExConsts.CONST_ORG_ATTR_PHONE_2]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_ORG_ATTR_PHONE_2] = value; }
        }
        public string PhoneNumber3
        {
            get { return (string)this._attributes[CMetaobjectsExConsts.CONST_ORG_ATTR_PHONE_3]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_ORG_ATTR_PHONE_3] = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public COrganization(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._class = EnMetaobjectClass.EOrganization;
            this.Address = "";
            this.PhoneNumber1 = "";
            this.PhoneNumber2 = "";
            this.PhoneNumber3 = "";
        }
        public COrganization(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider, false, false)
        {
        }
        public COrganization(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider, false, false)
        {
        }
        #endregion
    }
}
