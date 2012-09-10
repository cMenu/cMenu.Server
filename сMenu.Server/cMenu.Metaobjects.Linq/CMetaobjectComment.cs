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
    public static class CMetaobjectCommentEx
    {
        #region STATIC FUNCTIONS
        public static List<CMetaobjectComment> GetCommentsByObject(this Table<CMetaobjectComment> Table, decimal ObjectKey)
        {
            return CMetaobjectComment.sGetCommentsByObject(ObjectKey, Table.Context);
        }
        public static int DeleteCommentsByObject(this Table<CMetaobjectComment> Table, decimal ObjectKey)
        {
            return CMetaobjectComment.sDeleteCommentsByObject(ObjectKey, Table.Context);
        }
        public static int DeleteCommentsByParent(this Table<CMetaobjectComment> Table, decimal ParentKey)
        {
            return CMetaobjectComment.sDeleteCommentsByParent(ParentKey, Table.Context);
        }
        #endregion
    }

    [Serializable]
    public class CMetaobjectComment
    {
        #region PROTECTED FIELDS
        protected decimal _key = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected Guid _id = Guid.Empty;        
        protected decimal _parent = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected decimal _objectKey = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected string _author = "";
        protected string _authorEmail = "";
        protected DateTime _date = DateTime.Now;
        protected string _comment = "";
        protected List<CMetaobjectComment> _chidlren = new List<CMetaobjectComment>();
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

        #region PUBLIC FUNCTIONS
        public List<CMetaobjectComment> GetChildren(DataContext Context, bool Recursive = false)
        {
            var Comments = Context.GetTable<CMetaobjectComment>();
            var Result = from Comment in Comments
                         where Comment.Parent == this.Key
                         select Comment;

            if (Recursive)
                foreach (CMetaobjectComment Comment in Result)
                    Comment.GetChildren(Context, Recursive);

            this._chidlren = Result.ToList();

            return this._chidlren;
        }

        public int CommentGetByKey(DataContext Context)
        {            
            var Comments = Context.GetTable<CMetaobjectComment>();
            var Query = from Comment in Comments
                        where Comment.Key == this._key
                        select Comment;

            if (Query.Count() == 0)
                return CErrors.ERR_DB_GET_OBJECT;

            var Result = Query.ToList()[0];

            this._key = Result.Key;
            this._id = Result.ID;
            this._objectKey = Result.ObjectKey;
            this._date = Result.Date;
            this._author = Result.Author;
            this._authorEmail = Result.AuthorEmail;
            this._comment = Result.Comment;
            this._parent = Result.Parent;

            return CErrors.ERR_SUC;
        }
        public int CommentGetByID(DataContext Context)
        {
            var Comments = Context.GetTable<CMetaobjectComment>();
            var Query = from Comment in Comments
                        where Comment.ID == this._id
                        select Comment;

            if (Query.Count() == 0)
                return CErrors.ERR_DB_GET_OBJECT;

            var Result = Query.ToList()[0];

            this._key = Result.Key;
            this._id = Result.ID;
            this._objectKey = Result.ObjectKey;
            this._date = Result.Date;
            this._author = Result.Author;
            this._authorEmail = Result.AuthorEmail;
            this._comment = Result.Comment;
            this._parent = Result.Parent;

            return CErrors.ERR_SUC;
        }
        public int CommentDeleteByKey(DataContext Context)
        {
            var Comments = Context.GetTable<CMetaobjectComment>();
            var Query = from Comment in Comments
                        where Comment.Key == this._key
                        select Comment;

            Comments.DeleteAllOnSubmit(Query);

            return CErrors.ERR_SUC;
        }
        public int CommentDeleteByID(DataContext Context)
        {
            var Comments = Context.GetTable<CMetaobjectComment>();
            var Query = from Comment in Comments
                        where Comment.ID == this._id
                        select Comment;

            Comments.DeleteAllOnSubmit(Query);

            return CErrors.ERR_SUC;
        }
        public int CommentInsert(DataContext Context)
        {
            var Comments = Context.GetTable<CMetaobjectComment>();
            Comments.InsertOnSubmit(this);

            return CErrors.ERR_SUC;
        }
        public int CommentUpdate(DataContext Context)
        {
            this.Key = this.Key;

            return CErrors.ERR_SUC;
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CMetaobjectComment> sGetCommentsByObject(decimal ObjectKey, DataContext Context)
        {
            var Comments = Context.GetTable<CMetaobjectComment>();
            var Result = from Comment in Comments
                         where Comment.ObjectKey == ObjectKey
                         select Comment;

            return Result.ToList();
        }

        public static int sDeleteCommentsByObject(decimal ObjectKey, DataContext Context)
        {
            var Comments = Context.GetTable<CMetaobjectComment>();
            var Result = from Comment in Comments
                         where Comment.ObjectKey == ObjectKey
                         select Comment;

            Comments.DeleteAllOnSubmit(Result);

            return CErrors.ERR_SUC;
        }
        public static int sDeleteCommentsByParent(decimal ParentKey, DataContext Context)
        {
            var Comments = Context.GetTable<CMetaobjectComment>();
            var Result = from Comment in Comments
                         where Comment.Parent == ParentKey
                         select Comment;

            Comments.DeleteAllOnSubmit(Result);

            return CErrors.ERR_SUC;
        }
        #endregion
    }
}
