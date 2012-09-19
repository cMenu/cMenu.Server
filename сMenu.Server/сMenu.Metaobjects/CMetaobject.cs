using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using cMenu.Common;
using cMenu.Common.Base;
using cMenu.DB;
using cMenu.IO;
using cMenu.Metadata;
using cMenu.Metadata.Attributes;

using Newtonsoft.Json;

namespace cMenu.Metaobjects
{
    [Serializable]
    public class CMetaobject
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<string> _nameAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectConsts.CONST_OBJ_ATTR_NAME };
        protected CMetaobjectAttributeLocalized<string> _shortDescAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectConsts.CONST_OBJ_ATTR_SHORT_DESC };
        protected CMetaobjectAttributeLocalized<string> _fullDescAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectConsts.CONST_OBJ_ATTR_FULL_DESC };

        protected Guid _id = Guid.Empty;
        protected decimal _key = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected decimal _parent = -3;
        protected bool _system = false;
        protected EnMetaobjectClass _class;
        protected DateTime _modificationDate = DateTime.Now;
        protected EnMetaobjectStatus _status = EnMetaobjectStatus.EEnabled;

        protected CMetaobjectAttributes _attributes = new CMetaobjectAttributes();        
        protected List<CMetaobject> _children = new List<CMetaobject>();
        protected List<CMetaobjectLink> _externalLinks = new List<CMetaobjectLink>();
        protected List<CMetaobjectLink> _internalLinks = new List<CMetaobjectLink>();        
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> NameAttribute
        {
            get { return _nameAttribute; }
            set { _nameAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> ShortDescriptionAttribute
        {
            get { return _shortDescAttribute; }
            set { _shortDescAttribute = value; }
        }
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> FullDescriptionAttribute
        {
            get { return _fullDescAttribute; }
            set { _fullDescAttribute = value; }
        }

        public string Name
        {
            get { return _nameAttribute.DefaultValue; }
            set { _nameAttribute.DefaultValue = value; }
        }
        public string ShortDescription
        {
            get { return _shortDescAttribute.DefaultValue; }
            set { _shortDescAttribute.DefaultValue = value; }
        }
        public string FullDescription
        {
            get { return _fullDescAttribute.DefaultValue; }
            set { _fullDescAttribute.DefaultValue = value; }
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
            this._shortDescAttribute.Attributes = this._attributes;
            this._fullDescAttribute.Attributes = this._attributes;

            this.Status = EnMetaobjectStatus.EEnabled;
        }
        public CMetaobject(IDatabaseProvider Provider)
        {
            this._attributes.Provider = Provider;
            this._nameAttribute.Attributes = this._attributes;
            this._shortDescAttribute.Attributes = this._attributes;
            this._fullDescAttribute.Attributes = this._attributes;

            this.Status = EnMetaobjectStatus.EEnabled;
            this.Name = "";
            this.ShortDescription = "";
            this.FullDescription = "";
        }
        public CMetaobject(decimal Key, IDatabaseProvider Provider)
        {
            this._nameAttribute.Attributes = this._attributes;
            this._shortDescAttribute.Attributes = this._attributes;
            this._fullDescAttribute.Attributes = this._attributes;

            this._attributes.Provider = Provider;
            this._key = Key;

            this.ObjectGetByKey(Provider);
        }
        public CMetaobject(Guid ID, IDatabaseProvider Provider)
        {
            this._nameAttribute.Attributes = this._attributes;
            this._shortDescAttribute.Attributes = this._attributes;
            this._fullDescAttribute.Attributes = this._attributes;

            this._attributes.Provider = Provider;           
            this._id = ID;

            this.ObjectGetByID(Provider);
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public virtual int ObjectDeleteByKey(IDatabaseProvider Provider)
        {
            if (this.IsSystem(Provider))
                return CErrors.ERR_DB_DELETE_OBJECT;

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._key);

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            var T = Provider.QueryExecute(SQL, false, Params);
            if (!T)
                return CErrors.ERR_DB_DELETE_OBJECT;

            SQL = "DELETE FROM " + CDBConst.CONST_TABLE_OBJECTS_ATTR + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            T = Provider.QueryExecute(SQL, false, Params);
            if (!T)
                return CErrors.ERR_DB_DELETE_OBJECT;

            var R = CMetaobjectLink.sDeleteExternalLinks(this._key, Provider);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            R = CMetaobjectLink.sDeleteInternalLinks(this._key, Provider);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            this._attributes.ObjectKey = this._key;
            R = this._attributes.AttributesDelete(Provider);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            foreach (CMetaobject Child in this._children)
            {
                R = Child.ObjectDeleteByKey(Provider);
                if (R != CErrors.ERR_SUC)
                    return CErrors.ERR_DB_DELETE_OBJECT;
            }

            return CErrors.ERR_SUC;
        }
        public virtual int ObjectDeleteByID(IDatabaseProvider Provider)
        {
            if (this.IsSystem(Provider))
                return CErrors.ERR_DB_DELETE_OBJECT;

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ID, this._id.ToString().ToUpper());

            var SQL = "DELETE FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE UPPER(" + CDBConst.CONST_TABLE_FIELD_OBJ_ID + ") = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ID;
            var T = Provider.QueryExecute(SQL, false, Params);
            if (!T)
                return CErrors.ERR_DB_DELETE_OBJECT;

            Params.Clear();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._key);
            SQL = "DELETE FROM " + CDBConst.CONST_TABLE_OBJECTS_ATTR + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;
            T = Provider.QueryExecute(SQL, false, Params);
            if (!T)
                return CErrors.ERR_DB_DELETE_OBJECT;

            var R = CMetaobjectLink.sDeleteExternalLinks(this._key, Provider);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            R = CMetaobjectLink.sDeleteInternalLinks(this._key, Provider);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            this._attributes.ObjectKey = this._key;
            R = this._attributes.AttributesDelete(Provider);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            foreach (CMetaobject Child in this._children)
            {
                R = Child.ObjectDeleteByID(Provider);
                if (R != CErrors.ERR_SUC)
                    return CErrors.ERR_DB_DELETE_OBJECT;
            }

            return CErrors.ERR_SUC;
        }
        public virtual int ObjectUpdate(IDatabaseProvider Provider)
        {
            if (this.IsSystem(Provider))
                  return CErrors.ERR_DB_UPDATE_OBJECT;

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
            if (!R)
                return CErrors.ERR_DB_INSERT_OBJECT;

            this._attributes.ObjectKey = this._key;
            var RR = this._attributes.AttributesUpdate(Provider);

            return ((RR == CErrors.ERR_SUC) ? CErrors.ERR_SUC : CErrors.ERR_DB_UPDATE_OBJECT);
        }
        public virtual int ObjectInsert(IDatabaseProvider Provider)
        {
            if (this.Exists(Provider))
                return CErrors.ERR_DB_INSERT_OBJECT;

            if (this._key == CDBConst.CONST_OBJECT_EMPTY_KEY)
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
                return CErrors.ERR_DB_INSERT_OBJECT;

            this._attributes.ObjectKey = this._key;
            var RR = this._attributes.AttributesInsert(Provider);            

            return (RR == CErrors.ERR_SUC ? CErrors.ERR_SUC : CErrors.ERR_DB_INSERT_OBJECT);
        }
        public virtual int ObjectGetByKey(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, this._key);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ID  + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_MOD + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_STATUS;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
            {
                this._key = CDBConst.CONST_OBJECT_EMPTY_KEY;
                this._id = Guid.Empty;
                this._parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY;
                return CErrors.ERR_DB_GET_OBJECT;
            }
            else
            {
                this._key = T.Rows[0][0].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
                this._id = Guid.Parse(T.Rows[0][1].PostProcessDatabaseValue<string>(""));
                this._parent = T.Rows[0][2].PostProcessDatabaseValue<decimal>(-3);
                this._class = T.Rows[0][3].PostProcessDatabaseValue<EnMetaobjectClass>();
                this._system = ((T.Rows[0][4].PostProcessDatabaseValue<int>(0)) == 1);
                this._modificationDate = T.Rows[0][5].PostProcessDatabaseValue<DateTime>(DateTime.Now);
                this._status = T.Rows[0][6].PostProcessDatabaseValue<EnMetaobjectStatus>(EnMetaobjectStatus.EEnabled);
                this._attributes.ObjectKey = this._key;
            }

            return CErrors.ERR_SUC;
        }
        public virtual int ObjectGetByID(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ID, this._id.ToString().ToUpper());

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ID  + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_MOD + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_STATUS;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE UPPER(" + CDBConst.CONST_TABLE_FIELD_OBJ_ID + ") = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ID;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null || T.Rows.Count == 0)
            {
                this._key = CDBConst.CONST_OBJECT_EMPTY_KEY;
                this._id = Guid.Empty;
                this._parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY;
                return CErrors.ERR_DB_GET_OBJECT;
            }
            else
            {
                this._key = T.Rows[0][0].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
                this._id = Guid.Parse(T.Rows[0][1].PostProcessDatabaseValue<string>(""));
                this._parent = T.Rows[0][2].PostProcessDatabaseValue<decimal>(-3);
                this._class = T.Rows[0][3].PostProcessDatabaseValue<EnMetaobjectClass>();
                this._system = ((T.Rows[0][4].PostProcessDatabaseValue<int>(0)) == 1);
                this._modificationDate = T.Rows[0][5].PostProcessDatabaseValue<DateTime>(DateTime.Now);
                this._status = T.Rows[0][6].PostProcessDatabaseValue<EnMetaobjectStatus>(EnMetaobjectStatus.EEnabled);
                this._attributes.ObjectKey = this._key;
            }

            return CErrors.ERR_SUC;
        }

        public List<CMetaobject> GetChildren(IDatabaseProvider Provider, bool Recursive = true)
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
                    Object.Key = T.Rows[i][0].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
                    Object.ID = Guid.Parse(T.Rows[i][1].PostProcessDatabaseValue<string>(""));
                    Object.Parent = T.Rows[i][2].PostProcessDatabaseValue<decimal>(-3);
                    Object.Class = T.Rows[i][3].PostProcessDatabaseValue<EnMetaobjectClass>();
                    Object.System = ((T.Rows[i][4].PostProcessDatabaseValue<int>(0)) == 1);
                    Object.ModificatonDate = T.Rows[i][5].PostProcessDatabaseValue<DateTime>(DateTime.Now);
                    Object.Status = T.Rows[i][6].PostProcessDatabaseValue<EnMetaobjectStatus>();
                    Object.Attributes.ObjectKey = Object.Key;

                    R.Add(Object);
                    if (Recursive)
                        Object.GetChildren(Provider, Recursive);
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

        public bool Exists(IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ID, this._id);            

            var SQL = "SELECT Count(*) FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ID;
            DataTable T = Provider.QueryGetData(SQL, false, Params);

            int R = (T != null ? T.Rows[0][0].PostProcessDatabaseValue<int>() : 1);
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

            int R = (T != null ? T.Rows[0][0].PostProcessDatabaseValue<int>() : 1);

            return (R > 0);
        }
        #endregion

        #region STATIC FUNCTIONS
        public static CMetaobject sGetObjectByKey(decimal Key, IDatabaseProvider Provider)
        {
            CMetaobject R = new CMetaobject(Provider);
            R.Key = Key;
            R.ObjectGetByKey(Provider);
            return (R.ID == Guid.Empty ? null : R);
        }
        public static CMetaobject sGetObjectByID(Guid ID, IDatabaseProvider Provider)
        {
            CMetaobject R = new CMetaobject(Provider);
            R.ID = ID;
            R.ObjectGetByID(Provider);
            return (R.Key == CDBConst.CONST_OBJECT_EMPTY_KEY ? null : R);
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
                Object.Key = T.Rows[i][0].PostProcessDatabaseValue<decimal>(CDBConst.CONST_OBJECT_EMPTY_KEY);
                Object.ID = Guid.Parse(T.Rows[i][1].PostProcessDatabaseValue<string>(""));
                Object.Parent = T.Rows[i][2].PostProcessDatabaseValue<decimal>(-3);
                Object.Class = T.Rows[i][3].PostProcessDatabaseValue<EnMetaobjectClass>();
                Object.System = (T.Rows[i][4].PostProcessDatabaseValue<int>(0) == 1);
                Object.ModificatonDate = T.Rows[0][5].PostProcessDatabaseValue<DateTime>(DateTime.Now);
                Object.Status = T.Rows[0][6].PostProcessDatabaseValue<EnMetaobjectStatus>();
                Object.Attributes.ObjectKey = Object.Key;
                R.Add(Object);
            }

            return R;
        }
        public static bool sExistsByID(decimal Key, Guid ID, IDatabaseProvider Provider)
        {
            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_ID, ID.ToString().ToUpper());
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_KEY, Key);

            var SQL = "SELECT Count(*) FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_ID + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_ID;
            SQL += " AND " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + " <> @p" + CDBConst.CONST_TABLE_FIELD_OBJ_KEY;

            DataTable T = Provider.QueryGetData(SQL, false, Params);

            int R = (T != null ? T.Rows[0][0].PostProcessDatabaseValue<int>() : 1);
            return (R > 0);
        }
        #endregion
    }
}
