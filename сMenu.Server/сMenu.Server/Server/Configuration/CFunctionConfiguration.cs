using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using iMenu.Common.ErrorHandling;
using iMenu.Data.Configuration;


namespace iMenu.Communication.Server.Configuration
{
    [Serializable]
    [XmlRoot("FunctionConfiguration")]
    public class CFunctionConfiguration : CConfiguration
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
                    CErrorHelper.sThrowException("ERROR_MSG_BAD_CONFIG", ex);
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
                    CErrorHelper.sThrowException("ERROR_MSG_BAD_CONFIG", ex);
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
                    CErrorHelper.sThrowException("ERROR_MSG_BAD_CONFIG", ex);
                    return Guid.Empty;
                }
            }
            set { this._setValue(CCommunicationCConsts.CONST_SERVER_APP_NAME, value.ToString()); }
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

        public static CFunctionConfiguration sFindByID(List<CFunctionConfiguration> Functions, Guid ID)
        {
            if (Functions == null)
                return null;

            foreach (CFunctionConfiguration Function in Functions)
                if (Function.ID == ID)
                    return Function;
            return null;
        }
    }
}
