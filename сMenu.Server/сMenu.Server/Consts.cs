using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Communication
{
    public class CCommunicationCConsts
    {
        public const string CONST_SERVER_DESC = "SERVER_DESC";
        public const string CONST_SERVER_NAME = "SERVER_NAME";
        public const string CONST_SERVER_ID = "SERVER_ID";
        public const string CONST_SERVER_ADDRESS = "SERVER_ADDRESS";
        public const string CONST_SERVER_CONNECTION_STRING = "SERVER_CONNECTION_STRING";
        public const string CONST_SERVER_DB_TYPE = "SERVER_DB_TYPE";

        public const string CONST_SERVER_APP_NAME = "SERVER_APP_NAME";
        public const string CONST_SERVER_APP_PATH = "SERVER_APP_PATH";
        public const string CONST_SERVER_APP_ID = "SERVER_APP_ID";
        public const string CONST_SERVER_APP_MODULE_NAME = "SERVER_APP_MODULE_NAME";
        public const string CONST_SERVER_APP_INTERNAL = "SERVER_APP_INTERNAL";
        public const string CONST_SERVER_APP_ENABLED = "SERVER_APP_ENABLED";

        public const string CONST_SERVER_APP_SERVER_PATH = "SERVER_APP_SERVER_PATH";

        public const string CONST_SERVER_PARAM_SERIALIZATION_TYPE = "SERVER_PARAM_SERIALIZATION_TYPE";
        public const string CONST_SERVER_PARAM_FUNCTION_ID = "SERVER_PARAM_FUNCTION_ID";

        public const string CONST_NOTIFICATION_ENABLED = "NOTIFICATION_ENABLED";
        public const string CONST_NOTIFICATION_TYPE = "NOTIFICATION_TYPE";
        public const string CONST_NOTIFICATION_ID = "NOTIFICATION_ID";
        public const string CONST_NOTIFICATION_AUDIO_FILE = "NOTIFICATION_AUDIO_FILE";
        public const string CONST_NOTIFICATION_PLAY_AUDIO = "NOTIFICATION_PLAY_AUDIO";
        public const string CONST_NOTIFICATION_AUTOSTART_APP = "NOTIFICATION_AUTOSTART_APP";
        public const string CONST_NOTIFICATION_AUTOSTART_SVC = "NOTIFICATION_AUTOSTART_SVC";
        public const string CONST_NOTIFICATION_NAME = "NOTIFICATION_NAME";
        public const string CONST_NOTIFICATION_DESC = "NOTIFICATION_DESC";
        public const string CONST_NOTIFICATION_ADDRESS = "NOTIFICATION_ADDRESS";
    }
    public class CServerConst
    {
        public const string CONST_SERVER_NAME = "SERVER_NAME";
        public const string CONST_SERVER_ADDRESS = "SERVER_ADDRESS";
        public const string CONST_SERVER_CONNECTION_STRING = "SERVER_CONNECTION_STRING";
        public const string CONST_SERVER_DB_TYPE = "SERVER_DB_TYPE";

        public const string CONST_SERVER_APP_NAME = "SERVER_APP_NAME";
        public const string CONST_SERVER_APP_PATH = "SERVER_APP_PATH";
        public const string CONST_SERVER_APP_ID = "SERVER_APP_ID";
    }
    public class CServerFunctionParams
    {
        public const string CONST_FUNC_PARAM_GET_RECURSIVE = "PARAM_GET_RECURSIVE";
        public const string CONST_FUNC_PARAM_GET_CHILDREN = "PARAM_GET_CHILDREN";
        public const string CONST_FUNC_PARAM_GET_COMMENTS = "PARAM_GET_COMMENTS";
        public const string CONST_FUNC_PARAM_GET_SOURCE_LINKS = "PARAM_GET_SOURCE_LINKS";
        public const string CONST_FUNC_PARAM_GET_DEST_LINKS = "PARAM_GET_DEST_LINKS";
        public const string CONST_FUNC_PARAM_GET_ATTRIBUTES = "PARAM_GET_ATTRIBUTES";
        public const string CONST_FUNC_PARAM_GET_MEDIA = "PARAM_GET_MEDIA";

        public const string CONST_FUNC_PARAM_OBJECT = "PARAM_OBJECT";
        public const string CONST_FUNC_PARAM_OBJECT_KEY = "PARAM_OBJECT_KEY";
        public const string CONST_FUNC_PARAM_OBJECT_ID = "PARAM_OBJECT_ID";                
        public const string CONST_FUNC_PARAM_FUNC_ID = "PARAM_FUNC_ID";
        public const string CONST_FUNC_PARAM_DEVICE_ID = "PARAM_DEVICE_ID";
        public const string CONST_FUNC_PARAM_LOGIN = "PARAM_LOGIN";
        public const string CONST_FUNC_PARAM_PASSWORD = "PARAM_PASSWORD";
        public const string CONST_FUNC_PARAM_DEVICE_TYPE = "PARAM_DEVICE_TYPE";
        public const string CONST_FUNC_PARAM_DEVICE_MAC = "PARAM_DEVICE_MAC";
        public const string CONST_FUNC_PARAM_SESSION_DEADLINE = "PARAM_SESSION_DEADLINE";
        public const string CONST_FUNC_PARAM_DEVICE_NAME = "PARAM_DEVICE_NAME";
        public const string CONST_FUNC_PARAM_AUTH_TOKEN = "PARAM_AUTH_TOKEN";
        public const string CONST_FUNC_PARAM_LOCALE = "PARAM_LOCALE";
    }
    public class CServerFunctionID
    {
        public const string CONST_FUNC_ID_MAKE_ORDER = "41D08F39-26D8-47CF-85C7-90490358DAFB";
        public const string CONST_FUNC_ID_GET_ORDER = "03D8CEB0-546D-48F5-AC74-9DB564D1193F";
        public const string CONST_FUNC_ID_GET_SERVICE = "86B28E7D-BE88-4327-9A0F-87DE203B1C70";
        public const string CONST_FUNC_ID_GET_MEDIA = "28BF6574-B02A-4ECA-8AAA-862A933A38C5";
        public const string CONST_FUNC_ID_GET_CATEGORY = "DB42217C-67F3-4B9F-9D0F-E336BB962535";
        public const string CONST_FUNC_ID_GET_MENU = "AB54C3EB-8591-46F3-BD00-24436CB98D8D";
        public const string CONST_FUNC_ID_GET_ADV = "5A282C27-46AC-477E-8DE4-FCDC63ECF477";
        public const string CONST_FUNC_ID_AUTHENTICATE = "A6BF0560-E95B-490D-AFAB-58286E276BF3";
        public const string CONST_FUNC_CALL_OFICIANT = "2D284851-D822-4A4D-BA38-1E012273F809";

        #region USERS & USER GROUPS
        public const string CONST_FUNC_ID_USER_ADD = "4EDEED9C-8151-4F35-8879-134893A797F0";
        public const string CONST_FUNC_ID_USER_EDIT = "9D7C06A9-AB8F-48B7-BCBE-9D802FB626D9";
        public const string CONST_FUNC_ID_USER_DELETE = "B9A35351-7EAA-4E65-9B8F-B6E210B18269";
        public const string CONST_FUNC_ID_USER_GET = "1BD79771-F5A0-4C9A-8461-365F9CAFA21E";
        public const string CONST_FUNC_ID_USER_GROUP_ADD = "9F67CA69-35C2-4225-A34F-147E4E6A256E";
        public const string CONST_FUNC_ID_USER_GROUP_EDIT = "7AD62C55-6D41-4EE1-80C7-2663D3ABF832";
        public const string CONST_FUNC_ID_USER_GROUP_DELETE = "A1109DC2-31CD-4486-8768-8E577517E586";
        public const string CONST_FUNC_ID_USER_GROUP_GET = "F53FD181-E487-4D9F-B1BE-7B029127BAAA";
        #endregion

        #region OBJECTS
        public const string CONST_FUNC_ID_OBJ_ADD = "1CC56249-8356-453F-A391-9D111D9E9EC1";
        public const string CONST_FUNC_ID_OBJ_DELETE = "06B7FC25-593D-4DF1-AB8D-7771EE6ADCAD";
        public const string CONST_FUNC_ID_OBJ_EDIT = "EDB9AE40-45D0-4C9C-B056-E1A4E6AB9A1C";
        public const string CONST_FUNC_ID_OBJ_GET = "2FD90E42-4E0C-429D-A948-BE033DED0BEE";
        public const string CONST_FUNC_ID_OBJ_GET_LINKED = "E0197366-5088-44E5-8361-E08998B95D10";
        #endregion

        #region DEVICES
        public const string CONST_FUNC_ID_DEVICE_ADD = "10160E43-247B-4140-84D6-A9C9B3812D99";
        public const string CONST_FUNC_ID_DEVICE_EDIT = "2EBEBEA7-EB1C-48F3-A8E4-3B67F084C672";
        public const string CONST_FUNC_ID_DEVICE_DELETE = "3C8E0281-EBFA-45EA-A091-F772C162513B";
        public const string CONST_FUNC_ID_DEVICE_GET = "F16BCCAC-981F-4BE6-B827-6ACF46D425CF";
        #endregion

        #region POLICIES
        public const string CONST_FUNC_ID_POLICY_GET = "CB3C8757-8352-4D06-BE90-7865E3082101";
        #endregion

        #region SESSIONS
        public const string CONST_FUNC_ID_SESSION_GET = "5FA64EFC-99AD-42FE-9A89-26EA30EE43C7";
        public const string CONST_FUNC_ID_SESSION_ADD = "6E7442EA-3A60-44F4-98C4-06D235B0E073";
        public const string CONST_FUNC_ID_SESSION_EDIT = "393F7A96-922A-40C6-A54C-6B562FCCC4CA";
        public const string CONST_FUNC_ID_SESSION_DELETE = "1C79E9E8-E373-48CA-BB19-C83A9D1374DE";
        #endregion
    }
}
