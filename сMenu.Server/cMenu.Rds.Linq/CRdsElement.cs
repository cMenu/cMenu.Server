using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.IO;
using cMenu.DB;
using cMenu.Common;
using cMenu.Common.Base;
using cMenu.Rds;

namespace cMenu.Rds.Linq
{
    [Serializable]
    [Table(Name = CDBConst.CONST_TABLE_RDS_ELEM)]
    public class CRdsElement
    {
        #region PROTECTED FIELDS
        protected DataContext _context;
        protected CRdsElementValues _values = new CRdsElementValues();
        protected List<CRdsElement> _children = new List<CRdsElement>();

        protected decimal _parent = CRDSConst.CONST_RDS_ROOT_PARENT;
        protected decimal _key = -1;
        protected decimal _version = -1;
        protected decimal _dictionaryKey = -1;
        #endregion

        #region PUBLIC FIELDS
        public DataContext Context
        {
            get { return _context; }
            set
            {
                _context = value;
                _values.Context = value;
            }
        }
        public CRdsElementValues Values
        {
            get { return _values; }
            set { _values = value; }
        }
        public List<CRdsElement> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_ELEM_PARENT)]
        public decimal Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_ELEM_DICT_KEY)]
        public decimal DictionaryKey
        {
            get { return _dictionaryKey; }
            set { _dictionaryKey = value; }
        }
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_ELEM_VERSION, IsPrimaryKey = true)]
        public decimal Version
        {
            get { return _version; }
            set
            {
                _version = value;
                _values.ElementVersion = value;
            }
        }
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_ELEM_KEY)]
        public decimal Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public CRdsAttributeValues this[decimal AttributeKey]
        {
            get { return _getValue(AttributeKey); }
            set { _setValue(AttributeKey, value); }
        }
        public object this[decimal AttributeKey, int LanguageCode]
        {
            get { return _getValue(AttributeKey, LanguageCode); }
            set { _setValue(AttributeKey, LanguageCode, value); }
        }
        #endregion

        #region PROTECTED FUNCTIONS
        protected CRdsAttributeValues _getValue(decimal AttributeKey)
        {
            return this._values[AttributeKey];
        }
        protected object _getValue(decimal AttributeKey, int LanguageCode)
        {
            return this._values[AttributeKey, LanguageCode];
        }
        protected int _setValue(decimal AttributeKey, CRdsAttributeValues Value)
        {
            this._values[AttributeKey] = Value;
            return -1;
        }
        protected int _setValue(decimal AttributeKey, int LanguageCode, object Value)
        {
            this._values[AttributeKey, LanguageCode] = Value;
            return -1;
        }
        #endregion

        #region CONSTRUCTORS
        public CRdsElement(DataContext Context)
        {
            this.Context = Context;
        }
        public CRdsElement(decimal ElementVersion, DataContext Context)
        {
            this.Version = ElementVersion;
            this.Context = Context;

            this.ElementGet(Context);
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int ElementGet(DataContext Context)
        {
            var Elements = Context.GetTable<CRdsElement>();
            var Query = from Element in Elements
                        where Element.Version == this._version
                        select Element;

            if (Query.Count() == 0)
                return -2;

            var Result = Query.ToList()[0];

            this._key = Result.Key;
            this._version = Result.Version;
            this._dictionaryKey = Result.DictionaryKey;
            this._parent = Result.Parent;
            this.Version = this.Version;

            return -1;
        }
        public int ElementInsert(DataContext Context)
        {
            var Elements = Context.GetTable<CRdsElement>();
            Elements.InsertOnSubmit(this);
            this.Version = this.Version;
            this._values.AttributeValuesInsert(Context);

            return -1;
        }
        public int ElementUpdate(DataContext Context)
        {
            this.Version = this.Version;
            return -1;
        }
        public int ElementDelete(DataContext Context)
        {
            var Elements = Context.GetTable<CRdsElement>();
            var Query = from Element in Elements
                        where Element.Version == this._version
                        select Element;

            Elements.DeleteAllOnSubmit(Query);

            var RR = this._values.AttributeValuesDelete(Context);
            if (RR != -1)
                return RR;

            return -1;
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CRdsElement> sElementsGetByDictionary(decimal DictionaryKey, DataContext Context)
        {
            var Elements = Context.GetTable<CRdsElement>();
            var Query = from Element in Elements
                        where Element.DictionaryKey == DictionaryKey
                        select Element;

            return Query.ToList();
        }
        public static int sElementsDeleteByDictionary(decimal DictionaryKey, DataContext Context)
        {
            var Elements = Context.GetTable<CRdsElement>();
            var Query = from Element in Elements
                        where Element.DictionaryKey == DictionaryKey
                        select Element;

            Elements.DeleteAllOnSubmit(Query);

            return -1;
        }
        public static int sElementsChildrenDelete(decimal ElementKey, decimal DictionaryKey, DataContext Context)
        {
            var Elements = Context.GetTable<CRdsElement>();
            var Query = from Element in Elements
                        where 
                        Element.DictionaryKey == DictionaryKey &&
                        Element.Parent == ElementKey
                        select Element;

            Elements.DeleteAllOnSubmit(Query);

            return -1;
        }

        public static CRdsElement sFindElementByKey(List<CRdsElement> Elements, decimal ElementKey, bool Recursive = true)
        {
            CRdsElement TempElement = null;
            foreach (CRdsElement Element in Elements)
            {
                if (Element.Key == ElementKey)
                    return Element;
                if (Element.Children.Count > 0 && Recursive)
                {
                    TempElement = CRdsElement.sFindElementByKey(Element.Children, ElementKey, Recursive);
                    if (TempElement != null)
                        return TempElement;
                }
            }
            return null;
        }
        #endregion
    }
}
