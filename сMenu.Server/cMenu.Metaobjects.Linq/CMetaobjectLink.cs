using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.DB;
using cMenu.Common;

namespace cMenu.Metaobjects.Linq
{
    public static class CMetaobjectLinkEx
    {
        #region STATIC FUNCTIONS
        public static List<CMetaobjectLink> GetExternalLinks(this Table<CMetaobjectLink> Table, decimal ObjectKey)
        {
            return CMetaobjectLink.sGetExternalLinks(ObjectKey, Table.Context);
        }
        public static List<CMetaobjectLink> GetInternalLinks(this Table<CMetaobjectLink> Table, decimal ObjectKey)
        {
            return CMetaobjectLink.sGetInternalLinks(ObjectKey, Table.Context);
        }
        #endregion
    }

    [Serializable]
    public class CMetaobjectLink
    {
        #region PROTECTED FIELDS
        protected decimal _sourceObjectKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected decimal _linkedObjectKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected decimal _linkValue = -1;
        protected EnMetaobjectLinkType _linkType = EnMetaobjectLinkType.ESimple;
        #endregion  

        #region PUBLIC FIELDS
        public decimal SourceObjectKey
        {
            get { return _sourceObjectKey; }
            set { _sourceObjectKey = value; }
        }
        public decimal LinkedObjectKey
        {
            get { return _linkedObjectKey; }
            set { _linkedObjectKey = value; }
        }       
        public EnMetaobjectLinkType LinkType
        {
            get { return _linkType; }
            set { _linkType = value; }
        }
        public decimal LinkValue
        {
            get { return _linkValue; }
            set { _linkValue = value; }
        }        
        #endregion   

        #region PUBLIC FUNCTIONS
        public int LinkGet(DataContext Context)
        {
            var Links = Context.GetTable<CMetaobjectLink>();
            var Query = from Link in Links
                        where
                            Link.SourceObjectKey == this._sourceObjectKey &&
                            Link.LinkedObjectKey == this._linkedObjectKey
                        select Link;

            if (Query.Count() == 0)
                return CErrors.ERR_DB_GET_OBJECT;

            var Result = Query.ToList()[0];

            this._sourceObjectKey = Result.SourceObjectKey;
            this._linkedObjectKey = Result.LinkedObjectKey;
            this._linkValue = Result.LinkValue;
            this._linkType = Result.LinkType;

            return CErrors.ERR_SUC;
        }
        public int LinkDelete(DataContext Context)
        {
            var Links = Context.GetTable<CMetaobjectLink>();
            var Query = from Link in Links
                        where
                            Link.SourceObjectKey == this._sourceObjectKey &&
                            Link.LinkedObjectKey == this._linkedObjectKey
                        select Link;

            Links.DeleteAllOnSubmit(Query);

            return CErrors.ERR_SUC;
        }
        public int LinkUpdate(DataContext Context)
        {
            this.SourceObjectKey = this.SourceObjectKey;

            return CErrors.ERR_SUC;
        }
        public int LinkInsert(DataContext Context)
        {
            var Links = Context.GetTable<CMetaobjectLink>();

            Links.InsertOnSubmit(this);

            return CErrors.ERR_SUC;
        }

        public CMetaobject GetSourceObject(DataContext Context)
        {
            CMetaobject R = new CMetaobject(this._sourceObjectKey, Context);
            return (R.ID == Guid.Empty ? null : R);
        }
        public CMetaobject GetLinkedObject(DataContext Context)
        {
            CMetaobject R = new CMetaobject(this._linkedObjectKey, Context);
            return (R.ID == Guid.Empty ? null : R);
        }
        #endregion        

        #region STATIC FUNCTIONS
        public static List<CMetaobjectLink> sGetExternalLinks(decimal ObjectKey, DataContext Context)
        {
            List<CMetaobjectLink> R = new List<CMetaobjectLink>();

            var Links = Context.GetTable<CMetaobjectLink>();
            var Result = from Link in Links
                         where Link.LinkedObjectKey == ObjectKey
                         select Link;
            R = Result.ToList();

            return R;
        }
        public static List<CMetaobjectLink> sGetInternalLinks(decimal ObjectKey, DataContext Context)
        {
            List<CMetaobjectLink> R = new List<CMetaobjectLink>();

            var Links = Context.GetTable<CMetaobjectLink>();
            var Result = from Link in Links
                         where Link.SourceObjectKey == ObjectKey
                         select Link;
            R = Result.ToList();

            return R;
        }
        public static List<CMetaobjectLink> sGetExternalLinksByClass(decimal ObjectKey, EnMetaobjectClass Class, DataContext Context)
        {
            List<CMetaobjectLink> R = new List<CMetaobjectLink>();

            var Links = Context.GetTable<CMetaobjectLink>();
            var Objects = Context.GetTable<CMetaobject>();
            var Result = from Link in Links 
                         join Metaobject in Objects 
                         on Link.LinkedObjectKey equals Metaobject.Key
                         where Link.LinkedObjectKey == ObjectKey && Metaobject.Class == Class
                         select Link;
            R = Result.ToList();

            return R;
        }
        public static List<CMetaobjectLink> sGetInternalLinksByClass(decimal ObjectKey, EnMetaobjectClass Class, DataContext Context)
        {
            List<CMetaobjectLink> R = new List<CMetaobjectLink>();

            var Links = Context.GetTable<CMetaobjectLink>();
            var Objects = Context.GetTable<CMetaobject>();
            var Result = from Link in Links
                         join Metaobject in Objects
                         on Link.SourceObjectKey equals Metaobject.Key
                         where Link.SourceObjectKey == ObjectKey && Metaobject.Class == Class
                         select Link;
            R = Result.ToList();

            return R;
        }
        #endregion
    }
}
