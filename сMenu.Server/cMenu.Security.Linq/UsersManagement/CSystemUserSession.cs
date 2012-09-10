using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.DB;
using cMenu.IO;
using cMenu.Common;

namespace cMenu.Security.Linq.UsersManagement
{
    [Serializable]
    public class CSystemUserSession
    {
        #region PROTECTED FIELDS
        protected EnSessionStatus _status = EnSessionStatus.EEnabled;
        protected EnSessionType _type = EnSessionType.EWeb;
        protected decimal _key = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected Guid _id = Guid.Empty;
        protected decimal _userKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected DateTime _deadLine = DateTime.MaxValue;
        protected byte[] _variables = new byte[0];
        #endregion        

        #region PUBLIC FIELDS
        public decimal Key
        {
            get { return _key; }
            set { _key = value; }
        }
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public decimal UserKey
        {
            get { return _userKey; }
            set { _userKey = value; }
        }
        public EnSessionStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public EnSessionType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public DateTime DeadLine
        {
            get { return _deadLine; }
            set { _deadLine = value; }
        }
        public byte[] Variables
        {
            get { return _variables; }
            set { _variables = value; }
        }
        #endregion  

        #region PUBLIC FUNCTIONS
        public int SessionGetByKey(decimal Key, DataContext Context)
        {
            var Sessions = Context.GetTable<CSystemUserSession>();
            var Query = from Session in Sessions
                        where Session.Key == Key
                        select Session;

            if (Query.Count() == 0)
                return CErrors.ERR_DB_GET_OBJECT;
            var Object = Query.ToList()[0];

            this.Key = Object.Key;
            this.ID = Object.ID;
            this.UserKey = Object.UserKey;
            this.Status = Object.Status;
            this.Type = Object.Type;
            this.DeadLine = Object.DeadLine;
            this.Variables = Object.Variables;

            return CErrors.ERR_SUC;
        }
        public int SessionGetByID(Guid ID, DataContext Context)
        {
            var Sessions = Context.GetTable<CSystemUserSession>();
            var Query = from Session in Sessions
                        where Session.ID == ID
                        select Session;

            if (Query.Count() == 0)
                return CErrors.ERR_DB_DELETE_OBJECT;
            var Object = Query.ToList()[0];

            this.Key = Object.Key;
            this.ID = Object.ID;
            this.UserKey = Object.UserKey;
            this.Status = Object.Status;
            this.Type = Object.Type;
            this.DeadLine = Object.DeadLine;
            this.Variables = Object.Variables;

            return CErrors.ERR_SUC;
        }
        public int SessionUpdate(DataContext Context)
        {
            this.Key = this._key;

            return CErrors.ERR_SUC;
        }
        public int SessionDeleteByKey(DataContext Context)
        {
            var Sessions = Context.GetTable<CSystemUserSession>();
            var Query = from Session in Sessions
                        where Session.Key == this._key
                        select Session;

            Sessions.DeleteAllOnSubmit(Query);

            return CErrors.ERR_SUC;
        }
        public int SessionDeleteByID(DataContext Context)
        {
            var Sessions = Context.GetTable<CSystemUserSession>();
            var Query = from Session in Sessions
                        where Session.ID == ID
                        select Session;

            Sessions.DeleteAllOnSubmit(Query);

            return CErrors.ERR_SUC;
        }
        public int SessionInsert(DataContext Context)
        {
            this.Key = this._key;

            var Sessions = Context.GetTable<CSystemUserSession>();
            Sessions.InsertOnSubmit(this);

            return CErrors.ERR_SUC;
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CSystemUserSession> sGetSessionsByUser(decimal UserKey, DataContext Context)
        {
            var Sessions = Context.GetTable<CSystemUserSession>();
            var Query = from Session in Sessions
                        where Session.UserKey == UserKey
                        select Session;
            return Query.ToList();
        }
        public static int sDeleteSessionsByUser(decimal UserKey, DataContext Context)
        {
            var Sessions = Context.GetTable<CSystemUserSession>();
            var Query = from Session in Sessions
                        where Session.UserKey == UserKey
                        select Session;
            Sessions.DeleteAllOnSubmit(Query);
            return CErrors.ERR_SUC;
        }
        #endregion
    }
}