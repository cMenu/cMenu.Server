using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.Common;
using cMenu.Common.Base;
using cMenu.DB;
using cMenu.IO;

namespace cMenu.Rds.Linq
{
    [Serializable]
    [Table(Name = CDBConst.CONST_TABLE_RDS_ELEM_DATA)]
    public class CRdsAttributeValue
    {
        #region PROTECTED FIELDS
        protected decimal _elementVersion = -1;
        protected decimal _attributeKey = -1;
        protected int _attributeLocale = -1;
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_VALUE)]
        protected byte[] _attributeValueData = new byte[0];
        #endregion

        #region PUBLIC FIELDS
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION)]
        public decimal ElementVersion
        {
            get { return _elementVersion; }
            set { _elementVersion = value; }
        }
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY)]
        public decimal AttributeKey
        {
            get { return _attributeKey; }
            set { _attributeKey = value; }
        }
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DATA_LANGUAGE)]
        public int AttributeLocale
        {
            get { return _attributeLocale; }
            set { _attributeLocale = value; }
        }
        public object AttributeValue
        {
            get { return _getValue(); }
            set { _setValue(value); }
        }
        #endregion

        #region CONSTRUCTORS
        #endregion

        #region PROTECTED FUNCTIONS
        protected object _getValue()
        {
            object R = null;

            var Stream = this._attributeValueData.ToDataStream();
            R = CSerialize.sDeserializeBinaryStream(Stream);

            return R;
        }
        protected byte[] _setValue(object Value)
        {
            var Stream = Value.SerializeBinaryStream();
            this._attributeValueData = Stream.ToDataByteArray();

            return this._attributeValueData;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int AttributeValueInsert(DataContext Context)
        {
            var AttributeValues = Context.GetTable<CRdsAttributeValue>();
            var Query = from Value in AttributeValues
                        where Value.AttributeKey == this.AttributeKey &&
                              Value.ElementVersion == this.ElementVersion &&
                              Value.AttributeLocale == this.AttributeLocale
                        select Value;

            if (Query.Count() != 0)
                return -2;

            AttributeValues.InsertOnSubmit(this);
            return -1;
        }
        public int AttributeValueUpdate(DataContext Context)
        {
            var AttributeValues = Context.GetTable<CRdsAttributeValue>();
            var Query = from Value in AttributeValues
                        where Value.AttributeKey == this.AttributeKey &&
                              Value.ElementVersion == this.ElementVersion &&
                              Value.AttributeLocale == this.AttributeLocale
                        select Value;

            if (Query.Count() != 0)
                return -1;

            AttributeValues.InsertOnSubmit(this);
            return -2;
        }
        public int AttributeValueDelete(DataContext Context)
        {
            var AttributeValues = Context.GetTable<CRdsAttributeValue>();
            var Query = from Value in AttributeValues
                        where Value.ElementVersion == this.ElementVersion &&
                              Value.AttributeKey == this.AttributeKey &&
                              Value.AttributeLocale == this.AttributeLocale
                        select Value;

            AttributeValues.DeleteAllOnSubmit(Query);

            return -1;
        }
        public int AttributeValueGet(DataContext Context)
        {
            var AttributeValues = Context.GetTable<CRdsAttributeValue>();
            var Query = from Value in AttributeValues
                        where Value.ElementVersion == this.ElementVersion &&
                              Value.AttributeKey == this.AttributeKey &&
                              Value.AttributeLocale == this.AttributeLocale
                        select Value;

            if (Query.Count() == 0)
                return -2;

            this.AttributeValue = Query.ToList()[0].AttributeValue;

            return -1;
        }
        #endregion

        #region STATIC FUNCTIONS
        public static int sAttributeValuesDeleteByAttribute(decimal AttributeKey, DataContext Context)
        {
            var Values = Context.GetTable<CRdsAttributeValue>();
            var Query = from Value in Values
                        where Value.AttributeKey == AttributeKey
                        select Value;
            Values.DeleteAllOnSubmit(Query);

            return -1;
        }
        public static int sAttributeValuesDeleteByElement(decimal ElementVersion, DataContext Context)
        {
            var Values = Context.GetTable<CRdsAttributeValue>();
            var Query = from Value in Values
                        where Value.ElementVersion == ElementVersion
                        select Value;
            Values.DeleteAllOnSubmit(Query);

            return -1;
        }
        public static int sAttributeValuesDelete(decimal ElementVersion, decimal AttributeKey, DataContext Context)
        {
            var Values = Context.GetTable<CRdsAttributeValue>();
            var Query = from Value in Values
                        where 
                        Value.ElementVersion == ElementVersion &&
                        Value.AttributeKey == AttributeKey
                        select Value;
            Values.DeleteAllOnSubmit(Query);

            return -1;
        }

        public static List<CRdsAttributeValue> sGetAttributeValues(decimal ElementVersion, decimal AttributeKey, DataContext Context)
        {
            var Values = Context.GetTable<CRdsAttributeValue>();
            var Query = from Value in Values
                        where 
                        Value.ElementVersion == ElementVersion &&
                        Value.AttributeKey == AttributeKey
                        select Value;

            return Query.ToList();
        }
        public static List<CRdsAttributeValue> sGetAttributeValuesByElement(decimal ElementVersion, DataContext Context)
        {
            var Values = Context.GetTable<CRdsAttributeValue>();
            var Query = from Value in Values
                        where Value.ElementVersion == ElementVersion
                        orderby Value.AttributeKey
                        select Value;

            return Query.ToList();
        }
        public static CRdsAttributeValue sGetAttributeValue(decimal ElementVersion, decimal AttributeKey, int AttributeLocale, DataContext Context)
        {
            CRdsAttributeValue R = new CRdsAttributeValue();
            R.ElementVersion = ElementVersion;
            R.AttributeKey = AttributeKey;
            R.AttributeLocale = AttributeLocale;
            var RR = R.AttributeValueGet(Context);

            return (RR == -1 ? R : null);
        }
        #endregion

    }
}
