using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.DB;
using cMenu.Common;
using cMenu.IO;
using cMenu.Common.Base;
using cMenu.Metadata;
using cMenu.Metadata.Attributes;

using Newtonsoft.Json;

namespace cMenu.Metaobjects.Linq
{
    [Serializable]
    public class CMetaobject
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<string> _nameAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectConsts.CONST_OBJ_ATTR_NAME };
        protected CMetaobjectAttributeLocalized<string> _shortDescAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectConsts.CONST_OBJ_ATTR_SHORT_DESC };
        protected CMetaobjectAttributeLocalized<string> _fullDescAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectConsts.CONST_OBJ_ATTR_FULL_DESC };
        
        protected decimal _key = CDBConst.CONST_OBJECT_EMPTY_KEY;
        protected Guid _id = Guid.Empty;
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
            set 
            { 
                _key = value;
                _attributes.ObjectKey = value;
            }
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
        }
        public CMetaobject(DataContext Context)
        {
            this._attributes.Context = Context;

            this._nameAttribute.Attributes = this._attributes;
            this._shortDescAttribute.Attributes = this._attributes;
            this._fullDescAttribute.Attributes = this._attributes;

            this.Status = EnMetaobjectStatus.EEnabled;
            this.Name = "";
            this.ShortDescription = "";
            this.FullDescription = "";
        }
        public CMetaobject(decimal Key, DataContext Context)
        {
            this._attributes.Context = Context;

            this._nameAttribute.Attributes = this._attributes;
            this._shortDescAttribute.Attributes = this._attributes;
            this._fullDescAttribute.Attributes = this._attributes;

            this._key = Key;

            this.ObjectGetByKey(Context);
        }
        public CMetaobject(Guid ID, DataContext Context)
        {
            this._attributes.Context = Context;

            this._nameAttribute.Attributes = this._attributes;
            this._shortDescAttribute.Attributes = this._attributes;
            this._fullDescAttribute.Attributes = this._attributes;

            this.ID = ID;

            this.ObjectGetByID(Context);
        }
        #endregion

        #region PUBLIC FUNCTIONS

        public virtual int ObjectDeleteByKey(DataContext Context)
        {
            if (this.System)
                return CErrors.ERR_DB_DELETE_OBJECT;

            var Objects = Context.GetTable<CMetaobject>();
            var Query = from Obj in Objects
                        where Obj.Key == this._key
                        select Obj;

            Objects.DeleteAllOnSubmit(Query);
            this.Attributes.AttributesDelete(Context);

            return CErrors.ERR_SUC;
        }
        public virtual int ObjectDeleteByID(DataContext Context)
        {
            if (this.System)
                return CErrors.ERR_DB_DELETE_OBJECT;

            var Objects = Context.GetTable<CMetaobject>();
            var Query = from Obj in Objects
                        where Obj.ID == this.ID
                        select Obj;

            Objects.DeleteAllOnSubmit(Query);
            this.Attributes.AttributesDelete(Context);

            return CErrors.ERR_SUC;
        }
        public virtual int ObjectUpdate(DataContext Context)
        {
            if (this.System)
                  return CErrors.ERR_DB_UPDATE_OBJECT;

            this.ModificatonDate = DateTime.Now;
            this.Attributes.AttributesUpdate(Context);
            return CErrors.ERR_SUC;
        }
        public virtual int ObjectInsert(DataContext Context)
        {
            this.Key = this._key;

            var Objects = Context.GetTable<CMetaobject>();            
            Objects.InsertOnSubmit(this);
            this.Attributes.AttributesInsert(Context);
            return CErrors.ERR_SUC;
        }
        public virtual int ObjectGetByKey(DataContext Context)
        {
            var Objects = Context.GetTable<CMetaobject>();
            var Query = from Obj in Objects
                        where Obj.Key == this._key
                        select Obj;

            if (Query.Count() == 0)
                return CErrors.ERR_DB_GET_OBJECT;
            var Object = Query.ToList()[0];

            this.Key = Object.Key;
            this.ID = Object.ID;
            this.Parent = Object.Parent;
            this.Class = Object.Class;
            this.System = Object.System;
            this.ModificatonDate = Object.ModificatonDate;
            this.Status = Object.Status;

            this._attributes.Context = Context; ;
            this._attributes.ObjectKey = this._key;

            return CErrors.ERR_SUC;
        }
        public virtual int ObjectGetByID(DataContext Context)
        {
            var Objects = Context.GetTable<CMetaobject>();
            var Query = from Obj in Objects
                        where Obj.ID == this.ID
                        select Obj;

            if (Query.Count() == 0)
                return CErrors.ERR_DB_GET_OBJECT;

            var Object = Query.ToList()[0];

            this.Key= Object.Key;
            this.ID = Object.ID;
            this.Parent = Object.Parent;
            this.Class = Object.Class;
            this.System = Object.System;
            this.ModificatonDate = Object.ModificatonDate;
            this.Status = Object.Status;

            this._attributes.ObjectKey = this._key;

            return CErrors.ERR_SUC;
        }

        public List<CMetaobject> GetChildren(DataContext Context, bool Recursive = true)
        {
            List<CMetaobject> R = new List<CMetaobject>();

            var Objects = Context.GetTable<CMetaobject>();
            var Query = from Obj in Objects
                        where Obj.Parent == this._key
                        select Obj;

            if (Query.Count() == 0)
                return R;

            foreach (CMetaobject Object in Query)
                if (Recursive)
                    Object.GetChildren(Context, Recursive);

            R = Query.ToList();
            this._children = R;

            return R;
        }
        public List<CMetaobjectLink> GetExternalLinks(DataContext Context)
        {
            this._externalLinks = CMetaobjectLink.sGetExternalLinks(this._key, Context);
            return this._externalLinks;
        }
        public List<CMetaobjectLink> GetInternalLinks(DataContext Context)
        {
            this._internalLinks = CMetaobjectLink.sGetInternalLinks(this._key, Context);
            return this._internalLinks;
        }        
        #endregion

        #region STATIC FUNCTIONS
        public static CMetaobject sGetObjectByKey(decimal Key, DataContext Context)
        {
            CMetaobject R = new CMetaobject(Context);
            R.Key = Key;
            R.ObjectGetByKey(Context);
            return (R.ID == Guid.Empty ? null : R);
        }
        public static CMetaobject sGetObjectByID(Guid ID, DataContext Context)
        {
            CMetaobject R = new CMetaobject(Context);
            R.ID = ID;
            R.ObjectGetByID(Context);
            return (R.Key == CDBConst.CONST_OBJECT_EMPTY_KEY ? null : R);
        }
        public static List<CMetaobject> sGetObjectsByClass(EnMetaobjectClass Class, DataContext Context)
        {
            List<CMetaobject> R = new List<CMetaobject>();

            var Objects = Context.GetTable<CMetaobject>();
            var Query = from Obj in Objects
                        where Obj.Class == Class
                        select Obj;

            foreach (CMetaobject Object in Query)
                Object.Attributes.ObjectKey = Object.Key;

            return R;
        }
        #endregion
    }
}
