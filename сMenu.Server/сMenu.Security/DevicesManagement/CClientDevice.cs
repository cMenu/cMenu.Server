using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.Common;
using cMenu.Metaobjects;
using cMenu.Security;

namespace cMenu.Security.DevicesManagement
{
    [Serializable]
    public class CClientDevice : CSecuredMetaobject
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<EnClientDeviceType> _typeAttribute = new CMetaobjectAttributeLocalized<EnClientDeviceType>() { AttributeID = CSecurityConsts.CONST_DEVICE_ATTR_TYPE };
        protected CMetaobjectAttributeLocalized<string> _macAddressAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CSecurityConsts.CONST_DEVICE_ATTR_MAC };
        protected List<CClientDeviceSession> _sessions = new List<CClientDeviceSession>();
        #endregion

        #region PUBLIC FIELDS
        public CMetaobjectAttributeLocalized<EnClientDeviceType> TypeAttribute
        {
            get { return _typeAttribute; }
            set { _typeAttribute = value; }
        }
        public CMetaobjectAttributeLocalized<string> MacAddressAttribute
        {
            get { return _macAddressAttribute; }
            set { _macAddressAttribute = value; }
        }
        
        public EnClientDeviceType Type
        {
            get { return _typeAttribute.DefaultValue; ; }
            set { _typeAttribute.DefaultValue = value; }
        }
        public string MACAddress
        {
            get { return _macAddressAttribute.DefaultValue; }
            set { _macAddressAttribute.DefaultValue = value; }
        }
        public List<CClientDeviceSession> Sessions
        {
            get { return _sessions; }
            set { _sessions = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CClientDevice()
            : base()
        {
            this._macAddressAttribute.Attributes = this._attributes;
            this._typeAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EClientDevice;
            this.MACAddress = "";
            this.Type = EnClientDeviceType.EInternal;
        }
        public CClientDevice(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._macAddressAttribute.Attributes = this._attributes;
            this._typeAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.EClientDevice;
            this.MACAddress = "";
            this.Type = EnClientDeviceType.EInternal;
        }
        public CClientDevice(int Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
            this._macAddressAttribute.Attributes = this._attributes;
            this._typeAttribute.Attributes = this._attributes;
        }
        public CClientDevice(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._macAddressAttribute.Attributes = this._attributes;
            this._typeAttribute.Attributes = this._attributes;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public override int ObjectDeleteByID(IDatabaseProvider Provider)
        {
            CClientDeviceSession.sDeleteSessionsByUser(this._key, Provider);
            return base.ObjectDeleteByID(Provider);
        }
        public override int ObjectDeleteByKey(IDatabaseProvider Provider)
        {
            CClientDeviceSession.sDeleteSessionsByUser(this._key, Provider);
            return base.ObjectDeleteByKey(Provider);
        }
        public override int ObjectGetByID(IDatabaseProvider Provider)
        {
            base.ObjectGetByID(Provider);
            return CErrors.ERR_SUC;
        }
        public override int ObjectGetByKey(IDatabaseProvider Provider)
        {
            base.ObjectGetByKey(Provider);
            return CErrors.ERR_SUC;
        }
        public override int ObjectInsert(IDatabaseProvider Provider)
        {
            base.ObjectInsert(Provider);
            return CErrors.ERR_SUC;
        }
        public override int ObjectUpdate(IDatabaseProvider Provider)
        {
            base.ObjectUpdate(Provider);
            return CErrors.ERR_SUC;
        }

        public List<CClientDeviceSession> GetSessions(IDatabaseProvider Provider)
        {
            this._sessions = CClientDeviceSession.sGetSessionsByUser(this._key, Provider);
            return this._sessions;
        }
        #endregion

        #region STATIC FUNCTIONS
        public static List<CClientDevice> sGetAllDevices(IDatabaseProvider Provider)
        {
            List<CClientDevice> R = new List<CClientDevice>();

            Hashtable Params = new Hashtable();
            Params.Add(CDBConst.CONST_TABLE_FIELD_OBJ_CLASS, EnMetaobjectClass.EClientDevice);

            var SQL = "SELECT " + CDBConst.CONST_TABLE_FIELD_OBJ_KEY + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_ID + ", ";
            SQL += CDBConst.CONST_TABLE_FIELD_OBJ_PARENT + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_SYSTEM + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_MOD + ", " + CDBConst.CONST_TABLE_FIELD_OBJ_STATUS;
            SQL += " FROM " + CDBConst.CONST_TABLE_OBJECTS + " WHERE " + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS + " = @p" + CDBConst.CONST_TABLE_FIELD_OBJ_CLASS;

            var T = Provider.QueryGetData(SQL, false, Params);
            if (T == null)
                return R;
            for (int i = 0; i < T.Rows.Count; i++)
            {
                var Device = new CClientDevice(Provider);
                Device.Key = T.Rows[i][0].CheckDBNULLValue<decimal>();
                Device.ID = Guid.Parse(T.Rows[i][1].CheckDBNULLValue<string>());
                Device.Parent = T.Rows[i][2].CheckDBNULLValue<decimal>();
                Device.Class = T.Rows[i][3].CheckDBNULLValue<EnMetaobjectClass>();
                Device.System = (T.Rows[i][4].CheckDBNULLValue<int>() == 1);
                Device.ModificatonDate = T.Rows[0][5].CheckDBNULLValue<DateTime>();
                Device.Status = T.Rows[0][6].CheckDBNULLValue<EnMetaobjectStatus>();
                Device.Attributes.ObjectKey = Device.Key;
                R.Add(Device);
            }

            return R;
        }
        #endregion
    }
}
