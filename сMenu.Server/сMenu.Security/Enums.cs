using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Security
{
    [Serializable]
    public enum EnSystemPolicyType
    {
        ESystemDefined = 0,
        EUserDefined = 1
    }
    [Serializable]
    public enum EnSystemUserType
    {
        EUser = 0,
        ELocalAdministrator = 1,
        ESystemAdministrator = 2
    }
    [Serializable]
    public enum EnMetaobjectSecurityRights
    {
        ENone = 0,
        ERead = 1,
        EEdit = 2
    }
    [Serializable]
    public enum EnSessionStatus
    {
        EEnabled = 0,
        EDisabled = 1,
        ESuspended = 2,
        EClosed = 3
    }
    [Serializable]
    public enum EnSessionType
    {
        ETablet = 0,
        EWeb = 1
    }
}
