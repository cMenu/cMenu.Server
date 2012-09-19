using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.Common.Base;
using cMenu.DB;

namespace cMenu.Metaobjects
{
    [Serializable]
    public class CMetaobjectComment : CBaseEntity
    {
        #region PROTECTED FIELDS
        protected decimal _parent = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected decimal _objectKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected decimal _key = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected string _author = "";
        protected string _authorEmail = "";
        protected DateTime _date = DateTime.Now;
        protected string _comment = "";
        protected List<CMetaobjectComment> _chidlren = new List<CMetaobjectComment>();
        #endregion

        #region PUBLIC FIELDS
        public decimal Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
        public decimal ObjectKey
        {
            get { return _objectKey; }
            set { _objectKey = value; }
        }
        public decimal Key
        {
            get { return _key; }
            set { _key = value; }
        }
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }
        public string AuthorEmail
        {
            get { return _authorEmail; }
            set { _authorEmail = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
        public List<CMetaobjectComment> Children
        {
            get { return _chidlren; }
            set { _chidlren = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMetaobjectComment()
            : base()
        { 

        }
        public CMetaobjectComment(Guid ID, IDatabaseProvider Provider)
            : base(ID)
        { 
            this.ID = ID;
            this.CommentGetByID(Provider);
        }
        public CMetaobjectComment(decimal Key, IDatabaseProvider Provider)
        {
            this.Key = Key;
            this.CommentGetByKey(Provider);
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public List<CMetaobjectComment> GetChildren(IDatabaseProvider Provider, bool Recursive = false)
        {
            List<CMetaobjectComment> R = new List<CMetaobjectComment>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY, this._key);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_ID + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_DT + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR_EMAIL;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_COMMENT + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT;
            SQL += " FROM " + CDBConst.CONST_TABLE_COMMENTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT + " = @p " + CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Comment = new CMetaobjectComment();
                Comment.Key = T.Rows[0][0].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
                Comment.ID = Guid.Parse(T.Rows[0][1].PostProcessDatabaseValue<string>(""));
                Comment.ObjectKey = T.Rows[0][2].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
                Comment.Date = T.Rows[0][3].PostProcessDatabaseValue<DateTime>(DateTime.Now);
                Comment.Author = T.Rows[0][4].PostProcessDatabaseValue<string>("");
                Comment.AuthorEmail = T.Rows[0][5].PostProcessDatabaseValue<string>("");
                Comment.Comment = T.Rows[0][6].PostProcessDatabaseValue<string>("");
                Comment.Parent = T.Rows[0][7].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
                if (Recursive)
                    Comment.GetChildren(Provider, Recursive);

                R.Add(Comment);
            }

            return R; 
        }

        public int CommentGetByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY, this._key);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_ID + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_DT + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR_EMAIL;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_COMMENT + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT;
            SQL += " FROM " + CDBConst.CONST_TABLE_COMMENTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY + " = @p " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return CErrors.ERR_DB_GET_OBJECT;

            this._key = T.Rows[0][0].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
            this._id = Guid.Parse(T.Rows[0][1].PostProcessDatabaseValue<string>(""));
            this._objectKey = T.Rows[0][2].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
            this._date = T.Rows[0][3].PostProcessDatabaseValue<DateTime>(DateTime.Now);
            this._author = T.Rows[0][4].PostProcessDatabaseValue<string>("");
            this._authorEmail = T.Rows[0][5].PostProcessDatabaseValue<string>("");
            this._comment = T.Rows[0][6].PostProcessDatabaseValue<string>("");
            this._parent = T.Rows[0][7].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);

            return CErrors.ERR_SUC; 
        }
        public int CommentGetByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_ID, this._id.ToString().ToUpper());

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_ID + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_DT + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR_EMAIL;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_COMMENT + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT;
            SQL += " FROM " + CDBConst.CONST_TABLE_COMMENTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_COMMENTS_ID + " = @p " + CDBConst.CONST_TABLE_FIELD_COMMENTS_ID;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return CErrors.ERR_DB_GET_OBJECT;

            this._key = T.Rows[0][0].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
            this._id = Guid.Parse(T.Rows[0][1].PostProcessDatabaseValue<string>(""));
            this._objectKey = T.Rows[0][2].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
            this._date = T.Rows[0][3].PostProcessDatabaseValue<DateTime>(DateTime.Now);
            this._author = T.Rows[0][4].PostProcessDatabaseValue<string>("");
            this._authorEmail = T.Rows[0][5].PostProcessDatabaseValue<string>("");
            this._comment = T.Rows[0][6].PostProcessDatabaseValue<string>("");
            this._parent = T.Rows[0][7].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);

            return CErrors.ERR_SUC;
        }
        public int CommentDeleteByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY, this._key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_COMMENTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY + " = @p " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);
            if (T) 
                sDeleteCommentsByParent(this.Key, Provider);

            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_DELETE_OBJECT);
        }
        public int CommentDeleteByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_ID, this._id.ToString().ToUpper());

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_COMMENTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_COMMENTS_ID + " = @p " + CDBConst.CONST_TABLE_FIELD_COMMENTS_ID;

            var T = Provider.QueryExecute(SQL, false, Params);
            if (T) 
                sDeleteCommentsByParent(this.Key, Provider);

            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_DELETE_OBJECT);
        }
        public int CommentInsert(IDatabaseProvider Provider)
        {
            this._key = CDatabaseSequence.sGetObjectKey(Provider);

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_ID, this._id.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY, this._objectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_DT, this._date);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR, this._author);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR_EMAIL, this._authorEmail);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_COMMENT, this._comment);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT, this._parent);

            var SQL = "INSERT INTO " + CDBConst.CONST_TABLE_COMMENTS;
            SQL += "(" + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_ID + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_DT + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR_EMAIL;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_COMMENT + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT + ")";
            SQL += " VALUES ";
            SQL += "(@p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_ID + ", @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY;
            SQL += ", @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_DT + ", @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR + ", @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR_EMAIL;
            SQL += ", @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_COMMENT + ", @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT + ")";

            var T = Provider.QueryExecute(SQL, false, Params);
            if (!T)
                return CErrors.ERR_DB_INSERT_OBJECT;

            foreach (CMetaobjectComment Comment in this._chidlren)
            {
                var R = Comment.CommentInsert(Provider);
                if (R != CErrors.ERR_SUC)
                    return CErrors.ERR_DB_INSERT_OBJECT;
            }

            return CErrors.ERR_SUC;
        }
        public int CommentUpdateByKey(IDatabaseProvider Provider)
        {
            this._key = CDatabaseSequence.sGetObjectKey(Provider);

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_ID, this._id.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY, this._objectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_DT, this._date);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR, this._author);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR_EMAIL, this._authorEmail);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_COMMENT, this._comment);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_COMMENTS + " SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_COMMENTS_DT + " = @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_DT + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR + " = @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR_EMAIL + " = @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR_EMAIL + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_COMMENTS_COMMENT + " = @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_COMMENT + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT + " = @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);
            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_UPDATE_OBJECT);
        }
        public int CommentUpdateByID(IDatabaseProvider Provider)
        {
            this._key = CDatabaseSequence.sGetObjectKey(Provider);

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_ID, this._id.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY, this._objectKey);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_DT, this._date);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR, this._author);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR_EMAIL, this._authorEmail);
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_COMMENT, this._comment);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_COMMENTS + " SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_COMMENTS_DT + " = @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_DT + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR + " = @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR_EMAIL + " = @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR_EMAIL + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_COMMENTS_COMMENT + " = @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_COMMENT + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT + " = @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_COMMENTS_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_COMMENTS_ID;

            var T = Provider.QueryExecute(SQL, false, Params);
            return (T ? CErrors.ERR_SUC: CErrors.ERR_DB_UPDATE_OBJECT);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CMetaobjectComment> sGetCommentsByObject(decimal Key, IDatabaseProvider Provider)
        {
            List<CMetaobjectComment> R = new List<CMetaobjectComment>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY, Key);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_ID + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_DT + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_AUTHOR_EMAIL;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_COMMENT + ", " + CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT;
            SQL += " FROM " + CDBConst.CONST_TABLE_COMMENTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY + " = @p " + CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return R;

            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Comment = new CMetaobjectComment();
                Comment.Key = T.Rows[0][0].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
                Comment.ID = Guid.Parse(T.Rows[0][1].PostProcessDatabaseValue<string>(""));
                Comment.ObjectKey = T.Rows[0][2].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
                Comment.Date = T.Rows[0][3].PostProcessDatabaseValue<DateTime>(DateTime.Now);
                Comment.Author = T.Rows[0][4].PostProcessDatabaseValue<string>("");
                Comment.AuthorEmail = T.Rows[0][5].PostProcessDatabaseValue<string>("");
                Comment.Comment = T.Rows[0][6].PostProcessDatabaseValue<string>("");
                Comment.Parent = T.Rows[0][7].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);

                R.Add(Comment);
            }            

            return R; 
        }

        public static int sDeleteCommentsByObject(decimal Key, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY, Key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_COMMENTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY + " = @p " + CDBConst.CONST_TABLE_FIELD_COMMENTS_OBJ_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);
            return (T ? CErrors.ERR_SUC : CErrors.ERR_DB_DELETE_OBJECT);
        }
        public static int sDeleteCommentsByParent(decimal Key, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY, Key);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY + " FROM " + CDBConst.CONST_TABLE_COMMENTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT + " = @p " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
                return -1;

            for (int i = 0; i < T.Rows.Count; i++)
                CMetaobjectComment.sDeleteCommentsByParent(T.Rows.PostProcessDatabaseValue<decimal>(), Provider);

            SQL = "DELETE FROM " + CDBConst.CONST_TABLE_COMMENTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT + " = @p " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY;

            var R  = Provider.QueryExecute(SQL, false, Params);
            return (R ? CErrors.ERR_SUC : CErrors.ERR_DB_DELETE_OBJECT);
        }
        #endregion
    }   
}
