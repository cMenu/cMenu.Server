using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects;
using cMenu.Metaobjects.Linq;

using Newtonsoft.Json;

namespace cMenu.Security.Linq.PoliciesManagement
{
    [Serializable]
    public class CSystemPolicy : CSecuredMetaobject
    {
        #region PROTECTED FIELDS
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<EnSystemPolicyType> _typeAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<EnSystemPolicyType>() { AttributeID = CSecurityConsts.CONST_POLICY_ATTR_TYPE };
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<Guid> _idAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<Guid>() { AttributeID = CSecurityConsts.CONST_POLICY_ATTR_ID };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<EnSystemPolicyType> TypeAttribute
        {
            get { return _typeAttribute; }
            set { _typeAttribute = value; }
        }
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<Guid> PolicyIDAttribute
        {
            get { return _idAttribute; }
            set { _idAttribute = value; }
        }

        public EnSystemPolicyType Type
        {
            get { return _typeAttribute.DefaultValue; }
            set { _typeAttribute.DefaultValue = value; }
        }
        public Guid PolicyID
        {
            get { return _idAttribute.DefaultValue; }
            set { _idAttribute.DefaultValue = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CSystemPolicy()
            : base()
        {
            this._typeAttribute.Attributes = this._attributes;
            this._idAttribute.Attributes = this._attributes;
        }
        public CSystemPolicy(DataContext Context)
            : base(Context)
        {
            this._typeAttribute.Attributes = this._attributes;
            this._idAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.ESystemPolicy;
            this.Type = EnSystemPolicyType.ESystemDefined;
            this.PolicyID = Guid.Empty;
        }
        public CSystemPolicy(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._typeAttribute.Attributes = this._attributes;
            this._idAttribute.Attributes = this._attributes;
        }
        public CSystemPolicy(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._typeAttribute.Attributes = this._attributes;
            this._idAttribute.Attributes = this._attributes;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public List<CSystemPolicy> sGetAllPolicies(DataContext Context)
        {
            var Policies = Context.GetTable<cMenu.Metaobjects.Linq.CMetaobject>();
            var Query = from Policy in Policies
                        where Policy.Class == EnMetaobjectClass.ESystemPolicy
                        select (CSystemPolicy)Policy;

            return Query.ToList();
        }
        #endregion
    }
}
