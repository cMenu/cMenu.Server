using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.Common;
using cMenu.Metaobjects;
using cMenu.Security;

namespace cMenu.Metaobjects.Extended.Menu
{
    [Serializable]
    public class COrganization : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<string> _addressAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_ADDRESS };
        protected CMetaobjectAttributeLocalized<string> _phone1Attribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_PHONE_1 };
        protected CMetaobjectAttributeLocalized<string> _phone2Attribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_PHONE_2 };
        protected CMetaobjectAttributeLocalized<string> _phone3Attribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_PHONE_3 };
        #endregion

        #region PUBLIC FIELDS
        public CMetaobjectAttributeLocalized<string> AddressAttribute
        {
            get { return _addressAttribute; }
            set { _addressAttribute = value; }
        }
        public CMetaobjectAttributeLocalized<string> Phone1Attribute
        {
            get { return _phone1Attribute; }
            set { _phone1Attribute = value; }
        }
        public CMetaobjectAttributeLocalized<string> Phone2Attribute
        {
            get { return _phone2Attribute; }
            set { _phone2Attribute = value; }
        }
        public CMetaobjectAttributeLocalized<string> Phone3Attribute
        {
            get { return _phone3Attribute; }
            set { _phone3Attribute = value; }
        }

        public string Address
        {
            get { return _addressAttribute.DefaultValue; }
            set { _addressAttribute.DefaultValue = value; }
        }
        public string PhoneNumber1
        {
            get { return _phone1Attribute.DefaultValue; }
            set { _phone1Attribute.DefaultValue = value; }
        }
        public string PhoneNumber2
        {
            get { return _phone2Attribute.DefaultValue; }
            set { _phone2Attribute.DefaultValue = value; }
        }
        public string PhoneNumber3
        {
            get { return _phone3Attribute.DefaultValue; }
            set { _phone3Attribute.DefaultValue = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public COrganization()
            : base()
        {
            this._addressAttribute.Attributes = this._attributes;
            this._phone1Attribute.Attributes = this._attributes;
            this._phone2Attribute.Attributes = this._attributes;
            this._phone3Attribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EOrganization;
            this.Address = "";
            this.PhoneNumber1 = "";
            this.PhoneNumber2 = "";
            this.PhoneNumber3 = "";
        }
        public COrganization(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._addressAttribute.Attributes = this._attributes;
            this._phone1Attribute.Attributes = this._attributes;
            this._phone2Attribute.Attributes = this._attributes;
            this._phone3Attribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EOrganization;
            this.Address = "";
            this.PhoneNumber1 = "";
            this.PhoneNumber2 = "";
            this.PhoneNumber3 = "";
        }
        public COrganization(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
            this._addressAttribute.Attributes = this._attributes;
            this._phone1Attribute.Attributes = this._attributes;
            this._phone2Attribute.Attributes = this._attributes;
            this._phone3Attribute.Attributes = this._attributes;
        }
        public COrganization(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._addressAttribute.Attributes = this._attributes;
            this._phone1Attribute.Attributes = this._attributes;
            this._phone2Attribute.Attributes = this._attributes;
            this._phone3Attribute.Attributes = this._attributes;
        }
        #endregion
    }
}
