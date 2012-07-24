using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.Metaobjects;
using cMenu.Metaobjects.Extended;
using cMenu.Metaobjects.Extended.Advertisement;
using cMenu.Metaobjects.Extended.Menu;
using cMenu.Metaobjects.Extended.Order;
using cMenu.Metaobjects.Extended.Helpers;
using cMenu.Security;
using cMenu.Security.DevicesManagement;
using cMenu.Security.MetaobjectsManagement;
using cMenu.Security.PoliciesManagement;
using cMenu.Security.UsersManagement;

namespace cMenu.UI.ManagementTools.WPF.Core.Helpers
{
    public class CMTSecurityHelper
    {
        #region STATIC FUNCTIONS
        public static bool sAllowCreateFolder(CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (TargetMetaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(TargetMetaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;

            return false;
        }
        public static bool sAllowCreateAdvertisement(CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (TargetMetaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(TargetMetaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;

            if (TargetMetaobject.ID != Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_ADVER_ID))
                return false;

            return true;
        }
        public static bool sAllowCreateMediaResource(CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (TargetMetaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(TargetMetaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;

            if (TargetMetaobject.ID != Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_MEDIA_ID))
                return false;

            return true;
        }
        public static bool sAllowCreateShortcut(CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (TargetMetaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(TargetMetaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;

            if (TargetMetaobject.Class != EnMetaobjectClass.ECategory)
                return false;

            return true;
        }
        public static bool sAllowCreateOrganization(CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (TargetMetaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(TargetMetaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;

            if (TargetMetaobject.ID != Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_MENUS_ID))
                return false;

            return true;
        }
        public static bool sAllowCreateCategory(CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (TargetMetaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(TargetMetaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;

            if (TargetMetaobject.Class != EnMetaobjectClass.EMenu)
                return false;

            return true;
        }
        public static bool sAllowCreateMenu(CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (TargetMetaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(TargetMetaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;

            if (TargetMetaobject.Class != EnMetaobjectClass.EOrganization)
                return false;

            return true;
        }
        public static bool sAllowCreateService(CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (TargetMetaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(TargetMetaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;

            if (TargetMetaobject.ID != Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_SERVICES_ID))
                return false;

            return true;
        }
        public static bool sAllowCreateServiceAmount(CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (TargetMetaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(TargetMetaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;

            if (TargetMetaobject.Class != EnMetaobjectClass.EMenuService)
                return false;

            return true;
        }
        public static bool sAllowCreateUserGroup(CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (TargetMetaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(TargetMetaobject);
            if ((!User.PolicyAllowEditObjectsList() || !User.PolicyAllowEditUserList()) && Rights <= 1)
                return false;

            if (TargetMetaobject.ID != Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_USERS_ID))
                return false;

            return true;
        }
        public static bool sAllowCreateUser(CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (TargetMetaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(TargetMetaobject);
            if ((!User.PolicyAllowEditObjectsList() || !User.PolicyAllowEditUserList()) && Rights <= 1)
                return false;

            if (TargetMetaobject.Class != EnMetaobjectClass.ESystemUserGroup)
                return false;

            return true;
        }
        public static bool sAllowCreatePolicy(CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (TargetMetaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(TargetMetaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;

            if (TargetMetaobject.ID != Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_ID))
                return false;

            return true;
        }
        public static bool sAllowCreateDevice(CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (TargetMetaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(TargetMetaobject);
            if ((!User.PolicyAllowEditObjectsList() || !User.PolicyAllowEditDevicesList()) && Rights <= 1)
                return false;

            if (TargetMetaobject.ID != Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_DEVICES_ID))
                return false;

            return true;
        }

        public static bool sAllowViewProperties(CMetaobject Metaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (Metaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(Metaobject);
            if (Rights == 0)
                return false;
            
            return true;
        }
        public static bool sAllowEditObject(CMetaobject Metaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (Metaobject == null)
                return false;

            if (Metaobject.System)
                return false;

            var Rights = User.GetRightsForMetaobject(Metaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;
            
            return true;
        }
        public static bool sAllowRemoveObject(CMetaobject Metaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (Metaobject == null)
                return false;

            if (Metaobject.System)
                return false;

            var Rights = User.GetRightsForMetaobject(Metaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;

            return true;
        }
        public static bool sAllowLinkObject(CMetaobject Metaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (Metaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(Metaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;

            return true;
        }
        public static bool sAllowMoveObject(CMetaobject Metaobject, CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (Metaobject == null && TargetMetaobject == null)
                return false;

            if (Metaobject.System)
                return false;

            var SourceRights = User.GetRightsForMetaobject(Metaobject);
            var TargetRights = User.GetRightsForMetaobject(TargetMetaobject);

            if (!User.PolicyAllowEditObjectsList() && (SourceRights <= 1 || TargetRights <= 1))
                return false;

            CMetaobject Parent = CMetaobjectHelper.sFindObjectByKey(TargetMetaobject.Key, Metaobject);
            if (Parent != null)
                return false;            
          
            /// Repository Rules
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_ID) || TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_SHADOW_ROOT_ID))
                return false;
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_SERVICES_ID) && Metaobject.Class != EnMetaobjectClass.EMenuService)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EMenuService && Metaobject.Class != EnMetaobjectClass.EMenuServiceAmount)
                return false;
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_ADVER_ID) && Metaobject.Class != EnMetaobjectClass.EAdvertisement)
                return false;
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_MEDIA_ID) && Metaobject.Class != EnMetaobjectClass.EMediaResource)
                return false;
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_MENUS_ID) && Metaobject.Class != EnMetaobjectClass.EOrganization)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EOrganization && Metaobject.Class != EnMetaobjectClass.EMenu)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EMenu && Metaobject.Class != EnMetaobjectClass.ECategory)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.ECategory && Metaobject.Class != EnMetaobjectClass.EShortcut && Metaobject.Class != EnMetaobjectClass.EMenuService)
                return false;
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_DEVICES_ID) && Metaobject.Class != EnMetaobjectClass.EClientDevice)
                return false;
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_ID) && Metaobject.Class != EnMetaobjectClass.ESystemPolicy)
                return false;
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_USERS_ID) && Metaobject.Class != EnMetaobjectClass.ESystemUserGroup)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.ESystemUserGroup && Metaobject.Class != EnMetaobjectClass.ESystemUser)
                return false;

            /// Object Rules
            if (TargetMetaobject.Class == EnMetaobjectClass.ESystemPolicy)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EAdvertisement)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EClientDevice)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EMediaResource)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EMenuServiceAmount)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EShortcut)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.ESystemUser)
                return false;

            return true;
        }
        public static bool sAllowCreateObject(CMetaobject ParentMetaobject, CSystemUser User)        
        {
            if (User == null)
                return false;

            var Rights = User.GetRightsForMetaobject(ParentMetaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;

            /// Object Rules
            if (ParentMetaobject.Class == EnMetaobjectClass.ESystemPolicy)
                return false;
            if (ParentMetaobject.Class == EnMetaobjectClass.EAdvertisement)
                return false;
            if (ParentMetaobject.Class == EnMetaobjectClass.EClientDevice)
                return false;
            if (ParentMetaobject.Class == EnMetaobjectClass.EMediaResource)
                return false;
            if (ParentMetaobject.Class == EnMetaobjectClass.EMenuServiceAmount)
                return false;
            if (ParentMetaobject.Class == EnMetaobjectClass.EShortcut)
                return false;
            if (ParentMetaobject.Class == EnMetaobjectClass.ESystemUser)
                return false;

            return true;
        }
        public static bool sAllowCutObject(CMetaobject Metaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (Metaobject == null)
                return false;

            if (Metaobject.System)
                return false;

            var Rights = User.GetRightsForMetaobject(Metaobject);
            if (!User.PolicyAllowEditObjectsList() && Rights <= 1)
                return false;

            return true;
        }
        public static bool sAllowCopyObject(CMetaobject Metaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (Metaobject == null)
                return false;

            var Rights = User.GetRightsForMetaobject(Metaobject);
            if (Rights < 0)
                return false;

            return true;
        }
        public static bool sAllowPasteObject(CMetaobject Metaobject, CMetaobject TargetMetaobject, CSystemUser User)
        {
            if (User == null)
                return false;

            if (Metaobject == null || TargetMetaobject == null)
                return false;

            if (Metaobject.System)
                return false;

            var SourceRights = User.GetRightsForMetaobject(Metaobject);
            var TargetRights = User.GetRightsForMetaobject(TargetMetaobject);

            if (!User.PolicyAllowEditObjectsList() && (SourceRights <= 1 || TargetRights <= 1))
                return false;

            /// Repository Rules
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_SERVICES_ID) && Metaobject.Class != EnMetaobjectClass.EMenuService)
                return false;            
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_ADVER_ID) && Metaobject.Class != EnMetaobjectClass.EAdvertisement)
                return false;
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_MEDIA_ID) && Metaobject.Class != EnMetaobjectClass.EMediaResource)
                return false;
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_MENUS_ID) && Metaobject.Class != EnMetaobjectClass.EOrganization)
                return false;            
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_DEVICES_ID) && Metaobject.Class != EnMetaobjectClass.EClientDevice)
                return false;
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_ID) && Metaobject.Class != EnMetaobjectClass.ESystemPolicy)
                return false;
            if (TargetMetaobject.ID == Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_USERS_ID) && Metaobject.Class != EnMetaobjectClass.ESystemUserGroup)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.ESystemUserGroup && Metaobject.Class != EnMetaobjectClass.ESystemUser)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EOrganization && Metaobject.Class != EnMetaobjectClass.EMenu)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EMenu && Metaobject.Class != EnMetaobjectClass.ECategory)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.ECategory && Metaobject.Class != EnMetaobjectClass.EShortcut && Metaobject.Class != EnMetaobjectClass.EMenuService)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EMenuService && Metaobject.Class != EnMetaobjectClass.EMenuServiceAmount)
                return false;

            /// Object Rules
            if (TargetMetaobject.Class == EnMetaobjectClass.ESystemPolicy)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EAdvertisement)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EClientDevice)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EMediaResource)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EMenuServiceAmount)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.EShortcut)
                return false;
            if (TargetMetaobject.Class == EnMetaobjectClass.ESystemUser)
                return false;

            return true;
        }
        #endregion
    }
}
