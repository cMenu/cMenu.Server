using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using cMenu.DB;
using cMenu.Common;
using cMenu.IO;
using cMenu.Common.Base;
using cMenu.Metadata;
using cMenu.Metadata.Attributes;

namespace cMenu.Metaobjects
{
    [Serializable]
    public class CMetaobject
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<string> _nameAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectConsts.CONST_OBJ_ATTR_NAME };
        protected CMetaobjectAttributeLocalized<string> _descAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectConsts.CONST_OBJ_ATTR_DESC };

        protected Guid _id;
        protected decimal _key = decimal.MaxValue;
        protected decimal _parent;
        protected bool _system = false;
        protected EnMetaobjectClass _class;
        protected DateTime _modificationDate = DateTime.Now;
        protected EnMetaobjectStatus _status = EnMetaobjectStatus.EEnabled;

        protected CMetaobjectAttributes _attributes = new CMetaobjectAttributes();
        protected List<CMetaobjectComment> _comments = new List<CMetaobjectComment>();
        protected List<CMetaobject> _children = new List<CMetaobject>();
        protected List<CMetaobjectLink> _externalLinks = new List<CMetaobjectLink>();
        protected List<CMetaobjectLink> _internalLinks = new List<CMetaobjectLink>();        
        #endregion

        #region PUBLIC FIELDS
        public CMetaobjectAttributeLocalized<string> NameAttribute
        {
            get { return _nameAttribute; }
            set { _nameAttribute = value; }
        }
        public CMetaobjectAttributeLocalized<string> DescriptionAttribute
        {
            get { return _descAttribute; }
            set { _descAttribute = value; }
        }

        public string Name
        {
            get { return _nameAttribute.DefaultValue; }
            set { _nameAttribute.DefaultValue = value; }
        }
        public string Description
        {
            get { return _descAttribute.DefaultValue; }
            set { _descAttribute.DefaultValue = value; }
        }
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public decimal Key
        {
            get { return _key; }
            set { _key = value; }
        }
        public decimal Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
        public bool System
        {
            get { return _system; }
            set { _system = value; }
        }
        public DateTime ModificatonDate
        {
            get { return _modificationDate; }
            set { _modificationDate = value; }
        }
        public EnMetaobjectClass Class
        {
            get { return _class; }
            set { _class = value; }
        }
        public EnMetaobjectStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public List<CMetaobjectComment> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
        public List<CMetaobject> Children
        {
            get { return _children; }
            set { _children = value; }
        }
        public List<CMetaobjectLink> ExternalLinks
        {
            get { return _externalLinks; }
            set { _externalLinks = value; }
        }
        public List<CMetaobjectLink> InternalLinks
        {
            get { return _internalLinks; }
            set { _internalLinks = value; }
        }
        public CMetaobjectAttributes Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMetaobject()
        {
            this._nameAttribute.Attributes = this._attributes;
            this._descAttribute.Attributes = this._attributes;

            this.Status = EnMetaobjectStatus.EEnabled;            
        }
        public CMetaobject(IDatabaseProvider Provider)
        {
            this._attributes.Provider = Provider;
            this._nameAttribute.Attributes = this._attributes;
            this._descAttribute.Attributes = this._attributes;            
            this.Status = EnMetaobjectStatus.EEnabled;
        }
        public CMetaobject(decimal Key, IDatabaseProvider Provider)
        {
            this._nameAttribute.Attributes = this._attributes;
            this._descAttribute.Attributes = this._attributes;

            this._attributes.Provider = Provider;
            this._key = Key;
            this.ObjectGetByKey(Provider);
        }
        public CMetaobject(Guid ID, IDatabaseProvider Provider)
        {
            this._nameAttribute.Attributes = this._attributes;
            this._descAttribute.Attributes = this._attributes;

            this._attributes.Provider = Provider;           
            this._id = ID;
            this.ObjectGetByID(Provider);
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public virtual int ObjectDeleteByKey(IDatabaseProvider Provider)
        {
            if (this.IsSystem(Provider))
                return -2;

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            var T = Provider.QueryExecute(SQL, false, Params);
            SQL = "DELETE FROM " + CDBConst.CONST_TABLE_OBJECTS_ATTR + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            T = Provider.QueryExecute(SQL, false, Params);

            CMetaobjectLink.sDeleteExternalLinks(this._key, Provider);
            CMetaobjectLink.sDeleteInternalLinks(this._key, Provider);
            CMetaobjectComment.sDeleteCommentsByObject(this._key, Provider);

            this._attributes.ObjectKey = this._key;
            this._attributes.AttributesDelete(Provider);            

            foreach (CMetaobject Child in this._children)
                Child.ObjectDeleteByKey(Provider);
            return (T ? -1 : -2);
        }
        public virtual int ObjectDeleteByID(IDatabaseProvider Provider)
        {
            if (this.IsSystem(Provider))
                return -2;

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ID, this._id.ToString().ToUpper());

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE UPPER(" + CDBConst.CONST_TABLE_FIELD_OBJ_ID + ") = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ID;
            var T = Provider.QueryExecute(SQL, false, Params);

            Params.Clear();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._key);
            SQL = "DELETE FROM " + CDBConst.CONST_TABLE_OBJECTS_ATTR + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            T = Provider.QueryExecute(SQL, false, Params);

            CMetaobjectLink.sDeleteExternalLinks(this._key, Provider);
            CMetaobjectLink.sDeleteInternalLinks(this._key, Provider);
            CMetaobjectComment.sDeleteCommentsByObject(this._key, Provider);

            this._attributes.ObjectKey = this._key;
            this._attributes.AttributesDelete(Provider);

            foreach (CMetaobject Child in this._children)
                Child.ObjectDeleteByID(Provider);
            return (T ? -1 : -2);
        }
        public virtual int ObjectUpdate(IDatabaseProvider Provider)
        {
            if (this.IsSystem(Provider))
                  return -3;

            this._modificationDate = DateTime.Now;

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_CLASS, this._class);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ID, this._id.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_PARENT, this._parent);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM, (this._system ? 1 : 0));
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_MOD, DateTime.Now);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_STATUS, this._status);

            var SQL = "UPDATE " + CDBConst.CONST_TABLE_OBJECTS + " SET ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ID + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_MOD + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_MOD + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_STATUS + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_STATUS;
            SQL += " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;

            var R = Provider.QueryExecute(SQL, false, Params);

            this._attributes.ObjectKey = this._key;
            this._attributes.AttributesUpdate(Provider);

            return (R ? -1 : -3);
        }
        public virtual int ObjectInsert(IDatabaseProvider Provider)
        {
            if (this.Exists(Provider))
                return -2;

            if (this._key == decimal.MaxValue)
                this._key = CDatabaseSequence.sGetObjectKey(Provider);

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._key);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_CLASS, this._class);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ID, this._id.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_PARENT, this._parent);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM, (this._system ? 1 : 0));
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_MOD, this._modificationDate);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_STATUS, this._status);

            var SQL = "INSERT INTO " + CDBConst.CONST_TABLE_OBJECTS;
            SQL += " (" + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS  + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ID;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY +  ", " + CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM;
            SQL += ", " + CDBConst.CONST_TABLE_FIELD_OBJ_MOD + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_STATUS + ")";
            SQL += " VALUES ";
            SQL += " (@p" + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS  + ", @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ID;
            SQL += ", @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", @p" + CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + ", @p" + CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM;
            SQL += ", @p" + CDBConst.CONST_TABLE_FIELD_OBJ_MOD + ", @p" + CDBConst.CONST_TABLE_FIELD_OBJ_STATUS + ")";

            var R = Provider.QueryExecute(SQL, false, Params);
            if (!R)
                return -3;

            this._attributes.ObjectKey = this._key;
            this._attributes.AttributesInsert(Provider);            

            return (R ? -1 : -3);
        }
        public virtual int ObjectGetByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._key);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ID  + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_MOD + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_STATUS;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null)
            {
                this._key = -1;
                this._id = Guid.Empty;
                this._parent = -3;
            }
            else
            {
                this._key = T.Rows[0][0].CheckDBNULLValue<decimal>();
                this._id = Guid.Parse(T.Rows[0][1].CheckDBNULLValue<string>());
                this._parent = T.Rows[0][2].CheckDBNULLValue<decimal>();
                this._class = T.Rows[0][3].CheckDBNULLValue<EnMetaobjectClass>();
                this._system = ((T.Rows[0][4].CheckDBNULLValue<int>()) == 1);
                this._modificationDate = T.Rows[0][5].CheckDBNULLValue<DateTime>();
                this._status = T.Rows[0][6].CheckDBNULLValue<EnMetaobjectStatus>();
                this._attributes.ObjectKey = this._key;
            }

            return -1;
        }
        public virtual int ObjectGetByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ID, this._id.ToString().ToUpper());

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ID  + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_MOD + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_STATUS;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE UPPER(" + CDBConst.CONST_TABLE_FIELD_OBJ_ID + ") = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ID;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null)
            {
                this._key = -1;
                this._id = Guid.Empty;
                this._parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY;
            }
            else
            {
                this._key = T.Rows[0][0].CheckDBNULLValue<decimal>();
                this._id = Guid.Parse(T.Rows[0][1].CheckDBNULLValue<string>());
                this._parent = T.Rows[0][2].CheckDBNULLValue<decimal>();
                this._class = T.Rows[0][3].CheckDBNULLValue<EnMetaobjectClass>();
                this._system = ((T.Rows[0][4].CheckDBNULLValue<int>()) == 1);
                this._modificationDate = T.Rows[0][5].CheckDBNULLValue<DateTime>();
                this._status = T.Rows[0][6].CheckDBNULLValue<EnMetaobjectStatus>();
                this._attributes.ObjectKey = this._key;
            }

            return -1;
        }

        public List<CMetaobject> GetChildren(IDatabaseProvider Provider, bool GetRecursive = true)
        {
            List<CMetaobject> R = new List<CMetaobject>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._key);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ID + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_MOD + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_STATUS;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T != null)
                for (int i = 0; i < T.Rows.Count; i++)
                {
                    var Object = new CMetaobject(Provider);
                    Object.Key = T.Rows[i][0].CheckDBNULLValue<decimal>();
                    Object.ID = Guid.Parse(T.Rows[i][1].CheckDBNULLValue<string>());
                    Object.Parent = T.Rows[i][2].CheckDBNULLValue<decimal>();
                    Object.Class = T.Rows[i][3].CheckDBNULLValue<EnMetaobjectClass>();
                    Object.System = ((T.Rows[i][4].CheckDBNULLValue<int>()) == 1);
                    Object.ModificatonDate = T.Rows[i][5].CheckDBNULLValue<DateTime>();
                    Object.Status = T.Rows[i][6].CheckDBNULLValue<EnMetaobjectStatus>();
                    Object.Attributes.ObjectKey = Object.Key;

                    R.Add(Object);
                    if (GetRecursive)
                        Object.GetChildren(Provider);
                }
            this._children = R;
            return R;
        }
        public List<CMetaobjectLink> GetExternalLinks(IDatabaseProvider Provider)
        {
            this._externalLinks = CMetaobjectLink.sGetExternalLinks(this._key, Provider);
            return this._externalLinks;
        }
        public List<CMetaobjectLink> GetInternalLinks(IDatabaseProvider Provider)
        {
            this._internalLinks = CMetaobjectLink.sGetInternalLinks(this._key, Provider);
            return this._internalLinks;
        }
        public List<CMetaobjectComment> GetComments(IDatabaseProvider Provider)
        {
            this._comments = CMetaobjectComment.sGetCommentsByObject(this._key, Provider);
            return this._comments;
        }        

        public bool Exists(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ID, this._id);            

            var SQL = "SELECT Count(*) FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ID;
            DataTable T = Provider.QueryGetData(SQL, false, Params);

            int R = (T != null ? T.Rows[0][0].CheckDBNULLValue<int>() : 1);
            return (R > 0);
        }
        public bool IsSystem(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ID, this._id);
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._key);

            var SQL = "SELECT Count(*) FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE (" + CDBConst.CONST_TABLE_FIELD_OBJ_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ID;
            SQL += " OR " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ")";
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM + " = 1";
            DataTable T = Provider.QueryGetData(SQL, false, Params);

            int R = (T != null ? T.Rows[0][0].CheckDBNULLValue<int>() : 1);

            return (R > 0);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static CMetaobject sGetObjectByKey(decimal Key, IDatabaseProvider Provider)
        {
            CMetaobject R = new CMetaobject(Provider);
            R.Key = Key;
            R.ObjectGetByKey(Provider);
            return R;
        }
        public static CMetaobject sGetObjectByID(Guid ID, IDatabaseProvider Provider)
        {
            CMetaobject R = new CMetaobject(Provider);
            R.ID = ID;
            R.ObjectGetByID(Provider);
            return R;
        }
        public static List<CMetaobject> sGetObjectsByClass(EnMetaobjectClass Class, IDatabaseProvider Provider)
        {
            List<CMetaobject> R = new List<CMetaobject>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_CLASS, Class);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ID + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_MOD + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_STATUS;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null)
                return R;
            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Object = new CMetaobject(Provider);
                Object.Key = T.Rows[i][0].CheckDBNULLValue<decimal>();
                Object.ID = Guid.Parse(T.Rows[i][1].CheckDBNULLValue<string>());
                Object.Parent = T.Rows[i][2].CheckDBNULLValue<decimal>();
                Object.Class = T.Rows[i][3].CheckDBNULLValue<EnMetaobjectClass>();
                Object.System = (T.Rows[i][4].CheckDBNULLValue<int>() == 1);
                Object.ModificatonDate = T.Rows[0][5].CheckDBNULLValue<DateTime>();
                Object.Status = T.Rows[0][6].CheckDBNULLValue<EnMetaobjectStatus>();
                Object.Attributes.ObjectKey = Object.Key;
                R.Add(Object);
            }

            return R;
        }
        public static bool sExists(decimal Key, Guid ID, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ID, ID.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, Key);

            var SQL = "SELECT Count(*) FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE UPPER(" + CDBConst.CONST_TABLE_FIELD_OBJ_ID + ") = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ID;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " <> @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;

            DataTable T = Provider.QueryGetData(SQL, false, Params);

            int R = (T != null ? T.Rows[0][0].CheckDBNULLValue<int>() : 1);
            return (R > 0);
        }
        #endregion
    }
}
