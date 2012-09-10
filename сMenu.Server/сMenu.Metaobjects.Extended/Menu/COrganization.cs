using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.Common;
using cMenu.Metaobjects;
using cMenu.Security;

using Newtonsoft.Json;

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
        protected CMetaobjectAttributeLocalized<DateTime> _workStartTimeAttribute = new CMetaobjectAttributeLocalized<DateTime>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_WORK_START };
        protected CMetaobjectAttributeLocalized<DateTime> _workEndTimeAttribute = new CMetaobjectAttributeLocalized<DateTime>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_WORK_END };
        protected CMetaobjectAttributeLocalized<string> _urlAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_URL };
        protected CMetaobjectAttributeLocalized<string> _coordsAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_MAP_COORDS };
        protected CMetaobjectAttributeLocalized<double> _avgMoneyAttribute = new CMetaobjectAttributeLocalized<double>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_AVG_MONEY };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> AddressAttribute
        {
            get { return _addressAttribute; }
            set { _addressAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> Phone1Attribute
        {
            get { return _phone1Attribute; }
            set { _phone1Attribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> Phone2Attribute
        {
            get { return _phone2Attribute; }
            set { _phone2Attribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> Phone3Attribute
        {
            get { return _phone3Attribute; }
            set { _phone3Attribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<DateTime> WorkStartTimeAttribute
        {
            get { return _workStartTimeAttribute; }
            set { _workStartTimeAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<DateTime> WorkEndTimeAttribute
        {
            get { return _workEndTimeAttribute; }
            set { _workEndTimeAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> UrlAttribute
        {
            get { return _urlAttribute; }
            set { _urlAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> CoordsAttribute
        {
            get { return _coordsAttribute; }
            set { _coordsAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<double> AvgMoneyAttribute
        {
            get { return _avgMoneyAttribute; }
            set { _avgMoneyAttribute = value; }
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
        public DateTime WorkStartTime
        {
            get { return _workStartTimeAttribute.DefaultValue; }
            set { _workStartTimeAttribute.DefaultValue = value; }
        }
        public DateTime WorkEndTime
        {
            get { return _workEndTimeAttribute.DefaultValue; }
            set { _workEndTimeAttribute.DefaultValue = value; }
        }
        public string Url
        {
            get { return _urlAttribute.DefaultValue; }
            set { _urlAttribute.DefaultValue = value; }
        }
        public string Coordinates
        {
            get { return _coordsAttribute.DefaultValue; }
            set { _coordsAttribute.DefaultValue = value; }
        }
        public double AverageMoney
        {
            get { return _avgMoneyAttribute.DefaultValue; }
            set { _avgMoneyAttribute.DefaultValue = value; }
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
            this._workStartTimeAttribute.Attributes = this._attributes;
            this._workEndTimeAttribute.Attributes = this._attributes;
            this._urlAttribute.Attributes = this._attributes;
            this._coordsAttribute.Attributes = this._attributes;
            this._avgMoneyAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EOrganization;
        }
        public COrganization(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._addressAttribute.Attributes = this._attributes;
            this._phone1Attribute.Attributes = this._attributes;
            this._phone2Attribute.Attributes = this._attributes;
            this._phone3Attribute.Attributes = this._attributes;
            this._workStartTimeAttribute.Attributes = this._attributes;
            this._workEndTimeAttribute.Attributes = this._attributes;
            this._urlAttribute.Attributes = this._attributes;
            this._coordsAttribute.Attributes = this._attributes;
            this._avgMoneyAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EOrganization;
            this.Address = "";
            this.PhoneNumber1 = "";
            this.PhoneNumber2 = "";
            this.PhoneNumber3 = "";
            this.WorkStartTime = DateTime.MinValue;
            this.WorkEndTime = DateTime.MaxValue;
            this.Url = "";
            this.Coordinates = "";
            this.AverageMoney = 0;
        }
        public COrganization(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
            this._addressAttribute.Attributes = this._attributes;
            this._phone1Attribute.Attributes = this._attributes;
            this._phone2Attribute.Attributes = this._attributes;
            this._phone3Attribute.Attributes = this._attributes;
            this._workStartTimeAttribute.Attributes = this._attributes;
            this._workEndTimeAttribute.Attributes = this._attributes;
            this._urlAttribute.Attributes = this._attributes;
            this._coordsAttribute.Attributes = this._attributes;
            this._avgMoneyAttribute.Attributes = this._attributes;
        }
        public COrganization(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._addressAttribute.Attributes = this._attributes;
            this._phone1Attribute.Attributes = this._attributes;
            this._phone2Attribute.Attributes = this._attributes;
            this._phone3Attribute.Attributes = this._attributes;
            this._workStartTimeAttribute.Attributes = this._attributes;
            this._workEndTimeAttribute.Attributes = this._attributes;
            this._urlAttribute.Attributes = this._attributes;
            this._coordsAttribute.Attributes = this._attributes;
            this._avgMoneyAttribute.Attributes = this._attributes;
        }
        #endregion
    }
}
