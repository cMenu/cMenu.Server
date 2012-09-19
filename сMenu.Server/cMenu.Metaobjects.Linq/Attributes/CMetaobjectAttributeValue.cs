using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.DB;
using cMenu.IO;
using cMenu.Common;

namespace cMenu.Metaobjects.Linq
{
    public static class CMetaobjectAttributeValueEx
    {
        #region STATIC FUNCTIONS
        public static CMetaobjectAttributeValue GetAttributeValue(this Table<CMetaobjectAttributeValue> Table, decimal ObjectKey, string AttributeID, int AttributeLocale)
        {
            return CMetaobjectAttributeValue.sGetAttributeValue(ObjectKey, AttributeID, AttributeLocale, Table.Context);
        }
        public static List<CMetaobjectAttributeValue> GetAttributeValues(this Table<CMetaobjectAttributeValue> Table, decimal ObjectKey)
        {
            return CMetaobjectAttributeValue.sGetAttributeValues(ObjectKey, Table.Context);
        }
        public static List<CMetaobjectAttributeValue> GetAttributeValues(this Table<CMetaobjectAttributeValue> Table, decimal ObjectKey, string AttributeID)
        {
            return CMetaobjectAttributeValue.sGetAttributeValues(ObjectKey, AttributeID, Table.Context);
        }

        public static int DeleteAttributeValues(this Table<CMetaobjectAttributeValue> Table, decimal ObjectKey)
        {
            return CMetaobjectAttributeValue.sDeleteAttributeValues(ObjectKey, Table.Context);
        }
        public static int DeleteAttributeValues(this Table<CMetaobjectAttributeValue> Table, decimal ObjectKey, string AttributeID)
        {
            return CMetaobjectAttributeValue.sDeleteAttributeValues(ObjectKey, AttributeID, Table.Context);
        }
        #endregion
    }

    [Serializable]
    public class CMetaobjectAttributeValue
    {
        #region PROTECTED FIELDS
        protected decimal _objectKey = CDBConst.CONST_OBJECT_EMPTY_KEY;        
        protected int _attributeLocale = -1;
        protected string _attributeID = "";
        [Column(Name = CDBConst.CONST_TABLE_FIELD_OBJ_ATTR_VALUE)]
        protected byte[] _attributeValueData = new byte[0];
        #endregion

        #region PUBLIC FIELDS
        public decimal ObjectKey
        {
            get { return _objectKey; }
            set { _objectKey = value; }
        }
        public int AttributeLocale
        {
            get { return _attributeLocale; }
            set { _attributeLocale = value; }
        }
        public string AttributeID
        {
            get { return _attributeID; }
            set { _attributeID = value; }
        }        
        public object AttributeValue
        {
            get { return _getValue(); }
            set { _setValue(value); }
        }
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
            var AttributeValues = Context.GetTable<CMetaobjectAttributeValue>();
            var Query = from Value in AttributeValues
                        where Value.ObjectKey == this.ObjectKey &&
                              Value.AttributeID == this.AttributeID &&
                              Value.AttributeLocale == this.AttributeLocale
                        select Value;

            if (Query.Count() != 0)
                return CErrors.ERR_DB_INSERT_OBJECT;

            AttributeValues.InsertOnSubmit(this);
            return CErrors.ERR_SUC;
        }
        public int AttributeValueUpdate(DataContext Context)
        {
            var AttributeValues = Context.GetTable<CMetaobjectAttributeValue>();
            var Query = from Value in AttributeValues
                        where Value.ObjectKey == this.ObjectKey &&
                              Value.AttributeID == this.AttributeID &&
                              Value.AttributeLocale == this.AttributeLocale
                        select Value;

            if (Query.Count() == 0)
                return CErrors.ERR_DB_UPDATE_OBJECT;

            this.ObjectKey = this.ObjectKey;

            return CErrors.ERR_SUC;
        }
        public int AttributeValueDelete(DataContext Context)
        {
            var AttributeValues = Context.GetTable<CMetaobjectAttributeValue>();
            var Query = from Value in AttributeValues
                        where Value.ObjectKey == this.ObjectKey &&
                              Value.AttributeID == this.AttributeID &&
                              Value.AttributeLocale == this.AttributeLocale
                        select Value;

            AttributeValues.DeleteAllOnSubmit(Query);

            return CErrors.ERR_SUC;
        }
        public int AttributeValueGet(DataContext Context)
        {
            var AttributeValues = Context.GetTable<CMetaobjectAttributeValue>();
            var Query = from Value in AttributeValues
                        where Value.ObjectKey == this.ObjectKey &&
                              Value.AttributeID == this.AttributeID &&
                              Value.AttributeLocale == this.AttributeLocale
                        select Value;

            if (Query.Count() == 0)
                return CErrors.ERR_DB_GET_OBJECT;

            this.AttributeValue = Query.ToList()[0].AttributeValue;

            return CErrors.ERR_SUC;
        }
        #endregion

        #region STATIC FUNCTIONS
        public static CMetaobjectAttributeValue sGetAttributeValue(decimal ObjectKey, string AttributeID, int AttributeLocale, DataContext Context)
        {
            var AttributeValues = Context.GetTable<CMetaobjectAttributeValue>();
            var Query = from Value in AttributeValues
                        where Value.ObjectKey == ObjectKey &&
                              Value.AttributeID == AttributeID &&
                              Value.AttributeLocale == AttributeLocale
                        select Value;

            return (Query.Count() == 0 ? null : Query.ToList()[0]);
        }
        public static List<CMetaobjectAttributeValue> sGetAttributeValues(decimal ObjectKey, DataContext Context)
        {
            var AttributeValues = Context.GetTable<CMetaobjectAttributeValue>();
            var Query = from Value in AttributeValues
                        where Value.ObjectKey == ObjectKey
                        select Value;

            return Query.ToList();
        }
        public static List<CMetaobjectAttributeValue> sGetAttributeValues(decimal ObjectKey, string AttributeID, DataContext Context)
        {
            var AttributeValues = Context.GetTable<CMetaobjectAttributeValue>();
            var Query = from Value in AttributeValues
                        where Value.ObjectKey == ObjectKey &&
                              Value.AttributeID == AttributeID
                        select Value;

            return Query.ToList();
        }

        public static int sDeleteAttributeValues(decimal ObjectKey, DataContext Context)
        {
            var AttributeValues = Context.GetTable<CMetaobjectAttributeValue>();
            var Query = from Value in AttributeValues
                        where Value.ObjectKey == ObjectKey
                        select Value;

            AttributeValues.DeleteAllOnSubmit(Query);

            return CErrors.ERR_SUC;
        }
        public static int sDeleteAttributeValues(decimal ObjectKey, string AttributeID, DataContext Context)
        {
            var AttributeValues = Context.GetTable<CMetaobjectAttributeValue>();
            var Query = from Value in AttributeValues
                        where Value.ObjectKey == ObjectKey &&
                              Value.AttributeID == AttributeID
                        select Value;

            AttributeValues.DeleteAllOnSubmit(Query);

            return CErrors.ERR_SUC;
        }
        #endregion
    }
}
