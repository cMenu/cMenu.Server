using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using cMenu.DB;
using cMenu.Metaobjects;
using cMenu.Metadata.Attributes;

namespace cMenu.Security.PoliciesManagement
{
    [Serializable]
    public class CSystemPolicy : CSecuredMetaobject
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<EnSystemPolicyType> _typeAttribute = new CMetaobjectAttributeLocalized<EnSystemPolicyType>() { AttributeID = CSecurityConsts.CONST_POLICY_ATTR_TYPE };
        protected CMetaobjectAttributeLocalized<Guid> _idAttribute = new CMetaobjectAttributeLocalized<Guid>() { AttributeID = CSecurityConsts.CONST_POLICY_ATTR_ID };
        #endregion

        #region PUBLIC FIELDS
        public CMetaobjectAttributeLocalized<EnSystemPolicyType> TypeAttribute
        {
            get { return _typeAttribute; }
            set { _typeAttribute = value; }
        }
        public CMetaobjectAttributeLocalized<Guid> PolicyIDAttribute
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
        public CSystemPolicy(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._typeAttribute.Attributes = this._attributes;
            this._idAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.ESystemPolicy;
            this.Type = EnSystemPolicyType.ESystemDefined;
            this.PolicyID = Guid.Empty;
        }
        public CSystemPolicy(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
            this._typeAttribute.Attributes = this._attributes;
            this._idAttribute.Attributes = this._attributes;
        }
        public CSystemPolicy(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._typeAttribute.Attributes = this._attributes;
            this._idAttribute.Attributes = this._attributes;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public List<CSystemPolicy> sGetAllPolicies(IDatabaseProvider Provider)
        {
            List<CSystemPolicy> R = new List<CSystemPolicy>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_CLASS, EnMetaobjectClass.ESystemUser);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ID + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_MOD + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_STATUS;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null)
                return R;
            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Policy = new CSystemPolicy(Provider);
                Policy.Key = T.Rows[i][0].CheckDBNULLValue<decimal>();
                Policy.ID = Guid.Parse(T.Rows[i][1].CheckDBNULLValue<string>());
                Policy.Parent = T.Rows[i][2].CheckDBNULLValue<decimal>();
                Policy.Class = T.Rows[i][3].CheckDBNULLValue<EnMetaobjectClass>();
                Policy.System = ((T.Rows[i][4]).CheckDBNULLValue<int>() == 1);
                Policy.ModificatonDate = T.Rows[0][5].CheckDBNULLValue<DateTime>();
                Policy.Status = T.Rows[0][6].CheckDBNULLValue<EnMetaobjectStatus>();
                Policy.Attributes.ObjectKey = Policy.Key;
                R.Add(Policy);
            }

            return R;
        }        
        #endregion
    }
}
