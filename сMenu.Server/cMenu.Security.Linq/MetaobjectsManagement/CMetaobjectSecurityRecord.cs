using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.DB;
using cMenu.Common;
using cMenu.Metaobjects.Linq;

namespace cMenu.Security.Linq.MetaobjectsManagement
{
    [Serializable]
    public class CMetaobjectSecurityRecord
    {
        #region PROTECTED FIELDS
        protected decimal _userKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected decimal _metaobjectKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected int _rights = 3;
        #endregion

        #region PUBLIC FIELDS
        public decimal UserKey
        {
            get { return _userKey; }
            set { _userKey = value; }
        }
        public decimal MetaobjectKey
        {
            get { return _metaobjectKey; }
            set { _metaobjectKey = value; }
        }
        public int Rights
        {
            get { return _rights; }
            set { _rights = value; }
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CMetaobjectSecurityRecord> sGetRecordsByMetaobject(decimal MetaobjectKey, DataContext Context)
        {
            var Records = Context.GetTable<CMetaobjectSecurityRecord>();
            var Query = from Record in Records
                        where Record.MetaobjectKey == MetaobjectKey
                        select Record;

            return Query.ToList();
        }
        public static List<CMetaobjectSecurityRecord> sGetRecordsByUser(decimal UserKey, DataContext Context)
        {
            var Records = Context.GetTable<CMetaobjectSecurityRecord>();
            var Query = from Record in Records
                        where Record.UserKey == UserKey
                        select Record;

            return Query.ToList();
        }

        public static int sDeleteRecordsByMetaobject(decimal MetaobjectKey, DataContext Context)
        {
            var Records = Context.GetTable<CMetaobjectSecurityRecord>();
            var Query = from Record in Records
                        where Record.MetaobjectKey == MetaobjectKey
                        select Record;
            Records.DeleteAllOnSubmit(Query);

            return CErrors.ERR_DB_DELETE_OBJECT;
        }
        public static int sDeleteRecordsByUser(decimal UserKey, DataContext Context)
        {
            var Records = Context.GetTable<CMetaobjectSecurityRecord>();
            var Query = from Record in Records
                        where Record.UserKey == UserKey
                        select Record;
            Records.DeleteAllOnSubmit(Query);

            return CErrors.ERR_DB_DELETE_OBJECT;
        }
        #endregion
    }
}
