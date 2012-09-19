using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Metaobjects
{
    [Serializable]
    public enum EnMetaobjectStatus
    {
        EEnabled = 1,
        EDisabled = 2,
        EUnknown = 3,
        EBanned = 4,
        EVip = 5
    }
    [Serializable]
    public enum EnMetaobjectClass
    {
        EFolder = 0,
        ERDSDictionary = 1,
        ESystemPolicy = 2,
        ESystemUser = 3,
        ESystemUserGroup = 4,
        EOrganization = 5,
        EMenu = 6,
        ECategory = 7,
        EMenuService = 8,
        EMenuServiceAmount = 9,
        EMediaResource = 10,
        EClientDevice = 11,
        EAdvertisement = 12,
        EShortcut = 13,
        ECountry = 14,
        EState = 15,
        ECity = 16,
        EWebApplication = 17,
        EOrganizationNetwork = 18,
        EOrganizationTable = 19,
        ESecuredObject = 20,
        EExtendedObject = 21
    }
    [Serializable]
    public enum EnMetaobjectLinkType
    {
        ESimple = 0,
        ERecomendation = 1,
        ESecurity = 2
    }
}
