using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using cMenu.Data.Configuration;

namespace cMenu.Communication.Server.Configuration
{
    [Serializable]
    [XmlRoot("FunctionConfiguration")]
    public class CServerFunctionConfiguration : CConfiguration
    {
        [XmlIgnore]
        public bool Enabled
        {
            get
            {
                try
                {
                    var R = bool.Parse(this._getValue(CCommunicationCConsts.CONST_SERVER_APP_ENABLED));
                    return R;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            set
            {
                this._setValue(CCommunicationCConsts.CONST_SERVER_APP_ENABLED, value.ToString());
            }
        }
        [XmlIgnore]
        public bool Internal
        {
            get
            {
                try
                {
                    var R = bool.Parse(this._getValue(CCommunicationCConsts.CONST_SERVER_APP_INTERNAL));
                    return R;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            set
            {
                this._setValue(CCommunicationCConsts.CONST_SERVER_APP_INTERNAL, value.ToString());
            }
        }
        [XmlIgnore]
        public Guid ID
        {
            get
            {
                try
                {
                    var R = Guid.Parse(this._getValue(CCommunicationCConsts.CONST_SERVER_APP_ID));
                    return R;
                }
                catch (Exception ex)
                {
                    return Guid.Empty;
                }
            }
            set { this._setValue(CCommunicationCConsts.CONST_SERVER_APP_ID, value.ToString()); }
        }
        [XmlIgnore]
        public string Name
        {
            get { return this._getValue(CCommunicationCConsts.CONST_SERVER_APP_NAME); }
            set { this._setValue(CCommunicationCConsts.CONST_SERVER_APP_NAME, value); }
        }
        [XmlIgnore]
        public string Path
        {
            get { return this._getValue(CCommunicationCConsts.CONST_SERVER_APP_PATH); }
            set { this._setValue(CCommunicationCConsts.CONST_SERVER_APP_PATH, value); }
        }
        [XmlIgnore]
        public string ModuleFileName
        {
            get { return this._getValue(CCommunicationCConsts.CONST_SERVER_APP_MODULE_NAME); }
            set { this._setValue(CCommunicationCConsts.CONST_SERVER_APP_MODULE_NAME, value); }
        }

        public static CServerFunctionConfiguration sFindByID(List<CServerFunctionConfiguration> Functions, Guid ID)
        {
            if (Functions == null)
                return null;

            foreach (CServerFunctionConfiguration Function in Functions)
                if (Function.ID == ID)
                    return Function;
            return null;
        }
    }
}
