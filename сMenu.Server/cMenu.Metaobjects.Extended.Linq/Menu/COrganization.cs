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

using Newtonsoft.Json;

namespace cMenu.Metaobjects.Extended.Linq.Menu
{
    [Serializable]
    public class COrganization : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _addressAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_ADDRESS };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _phone1Attribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_PHONE_1 };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _phone2Attribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_PHONE_2 };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _phone3Attribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_PHONE_3 };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<DateTime> _workStartTimeAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<DateTime>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_WORK_START };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<DateTime> _workEndTimeAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<DateTime>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_WORK_END };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _urlAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_URL };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _coordsAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_MAP_COORDS };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<double> _avgMoneyAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<double>() { AttributeID = CMetaobjectsExConsts.CONST_ORG_ATTR_AVG_MONEY };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> AddressAttribute
        {
            get { return _addressAttribute; }
            set { _addressAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> Phone1Attribute
        {
            get { return _phone1Attribute; }
            set { _phone1Attribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> Phone2Attribute
        {
            get { return _phone2Attribute; }
            set { _phone2Attribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> Phone3Attribute
        {
            get { return _phone3Attribute; }
            set { _phone3Attribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<DateTime> WorkStartTimeAttribute
        {
            get { return _workStartTimeAttribute; }
            set { _workStartTimeAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<DateTime> WorkEndTimeAttribute
        {
            get { return _workEndTimeAttribute; }
            set { _workEndTimeAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> UrlAttribute
        {
            get { return _urlAttribute; }
            set { _urlAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> CoordsAttribute
        {
            get { return _coordsAttribute; }
            set { _coordsAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<double> AvgMoneyAttribute
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
        public COrganization(DataContext Context)
            : base(Context)
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
        public COrganization(decimal Key, DataContext Context)
            : base(Key, Context)
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
        public COrganization(Guid ID, DataContext Context)
            : base(ID, Context)
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
