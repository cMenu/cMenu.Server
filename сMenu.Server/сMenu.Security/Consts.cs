using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Security
{
    public class CSecurityConsts
    {
        #region POLICIES
        public const string CONST_POLICY_ATTR_TYPE = "POLICY_ATTR_TYPE";
        public const string CONST_POLICY_ATTR_DESC = "POLICY_ATTR_DESC";
        public const string CONST_POLICY_ATTR_ID = "POLICY_ATTR_ID";
        #endregion

        #region USERS
        public const string CONST_USER_ATTR_MAIL = "USER_ATTR_MAIL";
        public const string CONST_USER_ATTR_LOGIN = "USER_ATTR_LOGIN";
        public const string CONST_USER_ATTR_PASSHASH = "USER_ATTR_PASSHASH";
        public const string CONST_USER_ATTR_PHOTO = "USER_ATTR_PHOTO";
        public const string CONST_USER_ATTR_SYS_TYPE = "USER_ATTR_SYS_TYPE";
        public const string CONST_USER_ATTR_ENT_TYPE = "USER_ATTR_ENT_TYPE";
        public const string CONST_USER_ATTR_DESC = "USER_ATTR_DESC";
        public const string CONST_USER_ATTR_HOME_PHONE = "USER_ATTR_HOME_PHONE";
        public const string CONST_USER_ATTR_MOB_PHONE = "USER_ATTR_MOB_PHONE";
        public const string CONST_USER_ATTR_WORK_PHONE = "USER_ATTR_WORK_PHONE";
        public const string CONST_USER_ATTR_NAME = "USER_ATTR_NAME";
        public const string CONST_USER_ATTR_SEC_NAME = "USER_ATTR_SEC_NAME";
        public const string CONST_USER_ATTR_SUR_NAME = "USER_ATTR_SUR_NAME";
        public const string CONST_USER_ATTR_ADDR = "USER_ATTR_ADDR";
        #endregion

        #region WEB
        public const string CONST_WEB_APP_ATTR_ADDR = "WEB_APP_ATTR_ADDR";
        public const string CONST_WEB_APP_ATTR_ALLOW_ORDERS = "WEB_APP_ATTR_ALLOW_ORDERS";
        public const string CONST_WEB_APP_ATTR_GROUPS_FOLDER = "WEB_APP_ATTR_GROUPS_FOLDER";
        public const string CONST_WEB_APP_ATTR_USERS_FOLDER = "WEB_APP_ATTR_USERS_FOLDER";
        public const string CONST_WEB_APP_ATTR_SERVICES_FOLDER = "WEB_APP_ATTR_SERVICES_FOLDER";
        public const string CONST_WEB_APP_ATTR_MEDIA_FOLDER = "WEB_APP_ATTR_MEDIA_FOLDER";
        public const string CONST_WEB_APP_ATTR_DICT_FOLDER = "WEB_APP_ATTR_DICT_FOLDER";
        public const string CONST_WEB_APP_ATTR_ADVERT_FOLDER = "WEB_APP_ATTR_ADVERT_FOLDER";
        #endregion

        #region SESSIONS
        public const string CONST_SESSION_TABLE_ID = "SESSION_TABLE_ID";
        #endregion
    }
    public class CEmbeddedSecurityConsts
    {
        #region USER GROUPS
        public const string CONST_USER_GROUP_USERS_ID = "F037DA3F-CFAD-4C64-85C3-48E5C03C2368";
        public const string CONST_USER_GROUP_MODERATORS_ID = "D3EAB6AB-8B3F-4085-BAA4-F023215BCD3A";
        public const string CONST_USER_GROUP_ADMINISTRATORS_ID = "94AF3ECE-984A-4260-846D-2ACA67E9D613";

        public const int CONST_USER_GROUP_USERS_KEY = -20;
        public const int CONST_USER_GROUP_MODERATORS_KEY = -22;
        public const int CONST_USER_GROUP_ADMINISTRATORS_KEY = -21;
        #endregion

        #region USERS
        public const string CONST_USER_ADMINISTRATOR_ID = "24E82399-1EEA-42DE-A4B4-35CB1D42EDE8";
        public const int CONST_USER_ADMINISTRATOR_KEY = -11;
        #endregion

        #region POLICIES
        public const string CONST_POLICY_EDIT_USERS_ID = "7C584CCE-ECA7-409B-B56E-0C0F450F0E40";
        public const string CONST_POLICY_VIEW_USERS_ID = "BA261142-8901-4C69-9BDB-3A0770C57D7E";
        public const string CONST_POLICY_EDIT_SESSIONS_ID = "9ECC7CC5-0E41-4ABC-82B0-E00930D84E31";
        public const string CONST_POLICY_VIEW_SESSIONS_ID = "19E2F6F8-B071-4A44-8A92-32EF2C3CC7E0";
        public const string CONST_POLICY_EDIT_OBJECTS_ID = "F643BEE8-6988-4223-8FBE-4B2C5429ACBE";
        public const string CONST_POLICY_VIEW_OBJECTS_ID = "4E029D35-1F5A-4FF6-972A-DDC3EF1F16BD";
        public const string CONST_POLICY_VIEW_ORDERS_ID = "85E1C7A0-2442-47CC-B4D5-54C26094BBDC";
        public const string CONST_POLICY_EDIT_ORDERS_ID = "ED120B11-26B1-437A-89BE-E7A2C53669F8";
        public const string CONST_POLICY_VIEW_COMMENTS_ID = "869EF3E9-D319-43E3-A385-DFEA11F69F6E";
        public const string CONST_POLICY_EDIT_COMMENTS_ID = "FDF2BF05-0BF5-4764-A01C-58C412D8CC21";

        public const int CONST_POLICY_EDIT_USERS_KEY = -31;
        public const int CONST_POLICY_VIEW_USERS_KEY = -32;
        public const int CONST_POLICY_EDIT_SESSIONS_KEY = -36;
        public const int CONST_POLICY_VIEW_SESSIONS_KEY = -37;
        public const int CONST_POLICY_EDIT_OBJECTS_KEY = -38;
        public const int CONST_POLICY_VIEW_OBJECTS_KEY = -39;
        public const int CONST_POLICY_VIEW_ORDERS_KEY = -40;
        public const int CONST_POLICY_EDIT_ORDERS_KEY = -41;
        public const int CONST_POLICY_VIEW_COMMENTS_KEY = -42;
        public const int CONST_POLICY_EDIT_COMMENTS_KEY = -43;
        #endregion
    }
}
