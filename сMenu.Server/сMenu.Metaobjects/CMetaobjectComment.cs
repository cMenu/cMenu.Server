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
        protected decimal _parent = -1;
        protected decimal _objectKey = -1;
        protected decimal _key = -1;
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
        public CMetaobjectComment(Guid ID, IDatabaseProvider Provider, bool GetChildren = false, bool GetRecursive = false)
            : base(ID)
        { 
            this.ID = ID;
            this.CommentGetByID(Provider);
        }
        public CMetaobjectComment(decimal Key, IDatabaseProvider Provider, bool GetChildren = false, bool GetRecursive = false)
        {
            this.Key = Key;
            this.CommentGetByKey(Provider);
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public List<CMetaobjectComment> GetChildren(IDatabaseProvider Provider, bool GetRecursive = false)
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
                Comment.Key = T.Rows[0][0].CheckDBNULLValue<decimal>();
                Comment.ID = Guid.Parse(T.Rows[0][1].CheckDBNULLValue<string>());
                Comment.ObjectKey = T.Rows[0][2].CheckDBNULLValue<decimal>();
                Comment.Date = T.Rows[0][3].CheckDBNULLValue<DateTime>();
                Comment.Author = T.Rows[0][4].CheckDBNULLValue<string>();
                Comment.AuthorEmail = T.Rows[0][5].CheckDBNULLValue<string>();
                Comment.Comment = T.Rows[0][6].CheckDBNULLValue<string>();
                Comment.Parent = T.Rows[0][7].CheckDBNULLValue<decimal>();
                if (GetRecursive)
                    Comment.GetChildren(Provider, GetRecursive);

                R.Add(Comment);
            }

            return R; 
        }
        public int CommentGetByKey(IDatabaseProvider Provider, bool GetChildren = false, bool GetRecursive = false)
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
                return -2;

            this._key = T.Rows[0][0].CheckDBNULLValue<decimal>();
            this._id = Guid.Parse(T.Rows[0][1].CheckDBNULLValue<string>());
            this._objectKey = T.Rows[0][2].CheckDBNULLValue<decimal>();
            this._date = T.Rows[0][3].CheckDBNULLValue<DateTime>();
            this._author = T.Rows[0][4].CheckDBNULLValue<string>();
            this._authorEmail = T.Rows[0][5].CheckDBNULLValue<string>();
            this._comment = T.Rows[0][6].CheckDBNULLValue<string>();
            this._parent = T.Rows[0][7].CheckDBNULLValue<decimal>();
            if (GetChildren)
                this.GetChildren(Provider, GetRecursive);
            return -1; 
        }
        public int CommentGetByID(IDatabaseProvider Provider, bool GetChildren = false, bool GetRecursive = false)
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
                return -2;

            this._key = T.Rows[0][0].CheckDBNULLValue<decimal>();
            this._id = Guid.Parse(T.Rows[0][1].CheckDBNULLValue<string>());
            this._objectKey = T.Rows[0][2].CheckDBNULLValue<decimal>();
            this._date = T.Rows[0][3].CheckDBNULLValue<DateTime>();
            this._author = T.Rows[0][4].CheckDBNULLValue<string>();
            this._authorEmail = T.Rows[0][5].CheckDBNULLValue<string>();
            this._comment = T.Rows[0][6].CheckDBNULLValue<string>();
            this._parent = T.Rows[0][7].CheckDBNULLValue<decimal>();
            if (GetChildren)
                this.GetChildren(Provider, GetRecursive);

            return -1;
        }
        public int CommentDeleteByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY, this._key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_COMMENTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY + " = @p " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);
            if (T) sDeleteCommentsByParent(this.Key, Provider);

            return (T ? -1 : -2);
        }
        public int CommentDeleteByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_ID, this._id.ToString().ToUpper());

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_COMMENTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_COMMENTS_ID + " = @p " + CDBConst.CONST_TABLE_FIELD_COMMENTS_ID;

            var T = Provider.QueryExecute(SQL, false, Params);
            if (T) sDeleteCommentsByParent(this.Key, Provider);

            return (T ? -1 : -2);
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
            foreach (CMetaobjectComment Comment in this._chidlren)
                Comment.CommentInsert(Provider);

            return (T ? -1 : -2);
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
            return (T ? -1 : -2);
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
            return (T ? -1 : -2);
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
                Comment.Key = T.Rows[0][0].CheckDBNULLValue<decimal>();
                Comment.ID = Guid.Parse(T.Rows[0][1].CheckDBNULLValue<string>());
                Comment.ObjectKey = T.Rows[0][2].CheckDBNULLValue<decimal>();
                Comment.Date = T.Rows[0][3].CheckDBNULLValue<DateTime>();
                Comment.Author = T.Rows[0][4].CheckDBNULLValue<string>();
                Comment.AuthorEmail = T.Rows[0][5].CheckDBNULLValue<string>();
                Comment.Comment = T.Rows[0][6].CheckDBNULLValue<string>();
                Comment.Parent = T.Rows[0][7].CheckDBNULLValue<decimal>();

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
            return (T ? -1 : -2);
        }
        public static int sDeleteCommentsByParent(decimal Key, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY, Key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_COMMENTS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_COMMENTS_PARENT + " = @p " + CDBConst.CONST_TABLE_FIELD_COMMENTS_KEY;

            var T = Provider.QueryExecute(SQL, false, Params);
            return (T ? -1 : -2);
        }
        #endregion
    }   
}
