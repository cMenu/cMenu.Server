using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.DB;
using cMenu.Common;
using cMenu.Common.Base;

namespace cMenu.Rds.Linq
{
    [Serializable]
    [Table(Name = CDBConst.CONST_TABLE_RDS_ATTRIBUTES)]
    public class CRdsAttribute
    {
        #region PROTECTED FIELDS
        protected List<CRdsAttributeLink> _links = new List<CRdsAttributeLink>();

        protected Guid _id = Guid.Empty;
        protected string _name = "";
        protected decimal _key;
        protected decimal _dictionaryKey;
        protected bool _isHidden;
        protected DbType _type;
        #endregion

        #region PUBLIC FIELDS
        public List<CRdsAttributeLink> Links
        {
            get { return _links; }
            set { _links = value; }
        }

        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_ATTR_ID, DbType = "nvarchar(max)")]
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_ATTR_NAME)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_ATTR_KEY, IsPrimaryKey = true)]
        public decimal Key
        {
            get { return _key; }
            set { _key = value; }
        }
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_ATTR_DICT_KEY)]
        public decimal DictionaryKey
        {
            get { return _dictionaryKey; }
            set { _dictionaryKey = value; }
        }
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_ATTR_IS_HIDDEN, DbType = "int")]
        public bool IsHidden
        {
            get { return _isHidden; }
            set { _isHidden = value; }
        }
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_ATTR_TYPE)]
        public DbType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CRdsAttribute()
        {

        }
        public CRdsAttribute(decimal Key, DataContext Context)
        {
            this._key = Key;
            this.AttributeGetByKey(Context);

        }
        public CRdsAttribute(Guid ID, DataContext Context)
        {
            this._id = ID;
            this.AttributeGetByID(Context);

        }
        #endregion

        #region PUBLIC FUNCTIONS
        public List<CRdsAttributeLink> GetLinks(DataContext Context)
        {
            this._links = CRdsAttributeLink.sGetLinksByAttribute(this._key, Context);
            return this._links;
        }

        public int AttributeDeleteByKey(DataContext Context)
        {
            var Attributes = Context.GetTable<CRdsAttribute>();
            var Query = from Attr in Attributes
                        where Attr.Key == this._key
                        select Attr;

            Attributes.DeleteAllOnSubmit(Query);

            var RR = CRdsAttributeLink.sDeleteLinksByAttribute(this._key, Context);
            if (RR != -1)
                return RR;

            RR = CRdsAttributeValue.sAttributeValuesDeleteByAttribute(this._key, Context);
            if (RR != -1)
                return RR;

            RR = CRdsAttributeLink.sDeleteLinksByLinkedAttribute(this._key, Context);

            return RR;
        }
        public int AttributeDeleteByID(DataContext Context)
        {
            var Attributes = Context.GetTable<CRdsAttribute>();
            var Query = from Attr in Attributes
                        where Attr.ID == this._id
                        select Attr;

            Attributes.DeleteAllOnSubmit(Query);

            var RR = CRdsAttributeLink.sDeleteLinksByAttribute(this._key, Context);
            if (RR != -1)
                return RR;

            RR = CRdsAttributeValue.sAttributeValuesDeleteByAttribute(this._key, Context);
            if (RR != -1)
                return RR;

            RR = CRdsAttributeLink.sDeleteLinksByLinkedAttribute(this._key, Context);

            return RR;
        }
        public int AttributeUpdateByKey(DataContext Context)
        {
            var Attributes = Context.GetTable<CRdsAttribute>();
            var Query = from Attr in Attributes
                        where Attr.Key == this._key
                        select Attr;
            if (Query.Count() == 0)
            {
                Attributes.InsertOnSubmit(this);
                return -2;
            }

            this.Key = this.Key;

            return -1;
        }
        public int AttributeInsert(DataContext Context)
        {
            var Attributes = Context.GetTable<CRdsAttribute>();
            Attributes.InsertOnSubmit(this);

            return -1;
        }
        public int AttributeGetByKey(DataContext Context)
        {
            var Attributes = Context.GetTable<CRdsAttribute>();
            var Query = from Attr in Attributes
                        where Attr.Key == this._key
                        select Attr;
            if (Query.Count() == 0)
                return -2;

            var Result = Query.ToList()[0];

            this._dictionaryKey = Result.DictionaryKey;
            this._id = Result.ID;
            this._isHidden = Result.IsHidden;
            this._key = Result.Key;
            this._name = Result.Name;
            this._type = Result.Type;

            return -1;
        }
        public int AttributeGetByID(DataContext Context)
        {
            var Attributes = Context.GetTable<CRdsAttribute>();
            var Query = from Attr in Attributes
                        where Attr.ID == this._id
                        select Attr;
            if (Query.Count() == 0)
                return -2;

            var Result = Query.ToList()[0];

            this._dictionaryKey = Result.DictionaryKey;
            this._id = Result.ID;
            this._isHidden = Result.IsHidden;
            this._key = Result.Key;
            this._name = Result.Name;
            this._type = Result.Type;

            return -1;
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CRdsAttribute> sAttributesGetByDictionary(decimal DictionaryKey, DataContext Context)
        {
            var Attributes = Context.GetTable<CRdsAttribute>();
            var Query = from Attr in Attributes
                        where Attr.DictionaryKey == DictionaryKey
                        select Attr;

            return Query.ToList();
        }
        public static int sAttributesDeleteByDictionary(decimal DictionaryKey, DataContext Context)
        {
            var Attributes = Context.GetTable<CRdsAttribute>();
            var Query = from Attr in Attributes
                        where Attr.DictionaryKey == DictionaryKey
                        select Attr;

            Attributes.DeleteAllOnSubmit(Query);
            return -1;
        }

        public static CRdsAttribute sAttributeFindById(Guid ID, List<CRdsAttribute> Attributes)
        {
            foreach (CRdsAttribute Attr in Attributes)
                if (Attr.ID == ID)
                    return Attr;
            return null;
        }
        public static CRdsAttribute sAttributeFindByKey(decimal Key, List<CRdsAttribute> Attributes)
        {
            foreach (CRdsAttribute Attr in Attributes)
                if (Attr.Key == Key)
                    return Attr;
            return null;
        }
        public static CRdsAttribute sAttributeFindByName(string Name, List<CRdsAttribute> Attributes)
        {
            foreach (CRdsAttribute Attr in Attributes)
                if (Attr.Name == Name)
                    return Attr;
            return null;
        }
        #endregion
    }
}
