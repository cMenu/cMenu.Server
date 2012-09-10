using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.Common;
using cMenu.Common.Base;
using cMenu.DB;

namespace cMenu.Rds.Linq
{
    [Serializable]
    [Table(Name = CDBConst.CONST_TABLE_RDS_LINKS)]
    public class CRdsAttributeLink
    {
        #region PROTECTED FIELDS
        protected Guid _id = Guid.Empty;
        protected string _name;
        protected decimal _key;
        protected decimal _attributeKey;
        protected decimal _linkedAttributeKey;
        #endregion

        #region PUBLIC FIELDS
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_LINK_ID, DbType = "nvarchar(max)")]
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_LINK_NAME)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_LINK_KEY, IsPrimaryKey = true)]
        public decimal Key
        {
            get { return _key; }
            set { _key = value; }
        }
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_LINK_ATTR_KEY)]
        public decimal AttributeKey
        {
            get { return _attributeKey; }
            set { _attributeKey = value; }
        }
        [Column(Name = CDBConst.CONST_TABLE_FIELD_RDS_LINK_LINKED_ATTR_KEY)]
        public decimal LinkedAttributeKey
        {
            get { return _linkedAttributeKey; }
            set { _linkedAttributeKey = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CRdsAttributeLink()
        {

        }
        public CRdsAttributeLink(decimal Key, DataContext Context)
        {
            this._key = Key;
            this.LinkGetByKey(Context);
        }
        public CRdsAttributeLink(Guid ID, DataContext Context)
        {
            this._id = ID;
            this.LinkGetByID(Context);
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int LinkInsert(DataContext Context)
        {
            var Links = Context.GetTable<CRdsAttributeLink>();
            Links.InsertOnSubmit(this);

            return -1;
        }
        public int LinkUpdate(DataContext Context)
        {
            this.Key = this._key;
            
            return -1;
        }
        public int LinkDeleteByKey(DataContext Context)
        {
            var Links = Context.GetTable<CRdsAttributeLink>();
            var Query = from Link in Links
                        where Link.Key == this._key
                        select Link;
            Links.DeleteAllOnSubmit(Query);

            return -1;
        }
        public int LinkDeleteByID(DataContext Context)
        {
            var Links = Context.GetTable<CRdsAttributeLink>();
            var Query = from Link in Links
                        where Link.ID == this._id
                        select Link;
            Links.DeleteAllOnSubmit(Query);

            return -1;
        }
        public int LinkGetByKey(DataContext Context)
        {
            var Links = Context.GetTable<CRdsAttributeLink>();
            var Query = from Link in Links
                        where Link.Key == this._key
                        select Link;

            if (Query.Count() == 0)
                return -2;
            var Result = Query.ToList()[0];

            this._key = Result.Key;
            this._id = Result.ID;
            this._name = Result.Name;
            this._attributeKey = Result.AttributeKey;
            this._linkedAttributeKey = Result.LinkedAttributeKey;

            return -1;
        }
        public int LinkGetByID(DataContext Context)
        {
            var Links = Context.GetTable<CRdsAttributeLink>();
            var Query = from Link in Links
                        where Link.ID == this._id
                        select Link;

            if (Query.Count() == 0)
                return -2;
            var Result = Query.ToList()[0];

            this._key = Result.Key;
            this._id = Result.ID;
            this._name = Result.Name;
            this._attributeKey = Result.AttributeKey;
            this._linkedAttributeKey = Result.LinkedAttributeKey;

            return -1;
        }

        public bool LinkExists(DataContext Context)
        {
            var Links = Context.GetTable<CRdsAttributeLink>();
            var Query = from Link in Links
                        where Link.AttributeKey == this._attributeKey && Link.LinkedAttributeKey == this._linkedAttributeKey
                        select Link;

            return (Query.Count() > 0);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CRdsAttributeLink> sGetLinksByAttribute(decimal AttributeKey, DataContext Context)
        {
            var Links = Context.GetTable<CRdsAttributeLink>();
            var Query = from Link in Links
                        where Link.AttributeKey == AttributeKey
                        select Link;

            return Query.ToList();
        }

        public static int sDeleteLinksByAttribute(decimal AttributeKey, DataContext Context)
        {
            var Links = Context.GetTable<CRdsAttributeLink>();
            var Query = from Link in Links
                        where Link.AttributeKey == AttributeKey
                        select Link;

            Links.DeleteAllOnSubmit(Query);

            return -1;
        }
        public static int sDeleteLinksByLinkedAttribute(decimal LinkedAttributeKey, DataContext Context)
        {
            var Links = Context.GetTable<CRdsAttributeLink>();
            var Query = from Link in Links
                        where Link.LinkedAttributeKey == LinkedAttributeKey
                        select Link;

            Links.DeleteAllOnSubmit(Query);

            return -1;
        }
        #endregion
    }
}
