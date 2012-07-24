using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Common
{
    public class CConsts
    {
        public const string REG_PATH_ROOT = "iMenu";
        public const string REG_PATH_MODULES = "Modules";
        public const string REG_PATH_APP_CONFIG = "AppConfig";
        public const string REG_KEY_INSTALL_DIR = "InstallDir";

        public const string FS_PATH_MODULES = "Modules";
        public const string FS_PATH_APP_CONFIG = "AppConfig";
        public const string FS_PATH_ROOT = "iMenu";
    }
    public class CErrors
    {
        public const int ERR_SUC = -1;
        public const int ERR_REG = -1;
        public const int ERR_REG_PATH = -2;
        public const int ERR_REG_KEY = -3;
        public const int ERR_FS_SERIALIZE = -4;
        public const int ERR_FS_PATH = -5;
        public const int ERR_FS_FILE = -6;
        public const int ERR_SVC = -7;
        public const int ERR_SVC_CONNECTION_FAILED = -8;
        public const int ERR_SVC_CONNECTION_ACCEPT = -9;
        public const int ERR_SVC_CANT_FIND_PROCESS = -10;

        public const int ERR_SRVR_BAD_RESULT_FORMAT_TYPE = -256;
        public const int ERR_SRVR_BAD_SERVER_APP_ID = -257;
        public const int ERR_SRVR_BAD_SERVER_CONFIG_FILE = -258;
        public const int ERR_SRVR_BAD_SERVER_APP_DISABLED = -259;
        public const int ERR_SRVR_BAD_SERVER_APP_NULL = -260;
        public const int ERR_SRVR_BAD_SERVER_APP_INTERNAL_ERROR = -261;
        public const int ERR_SRVR_BAD_APP_PARAMS_JSON = -262;

        public const int ERR_SRVR_USER_ALREADY_EXISTS = -11;
        public const int ERR_SRVR_USER_DOESNT_EXIST_OR_PASS_INVALID = -12;
        public const int ERR_SRVR_ALREADY_STARTED = -13;
        public const int ERR_SRVR_DOESNT_STARTED = -14;
        public const int ERR_SRVR_GROUP_CANT_ADD = -17;
        public const int ERR_SRVR_GROUP_CANT_UPDATE = -18;
        public const int ERR_SRVR_GROUP_CANT_DELETE = -19;
        public const int ERR_SRVR_TIMEOUT = -23;

        public const int ERR_SMS_LOAD_CFG = -15;

        public const int ERR_DB_ADD_JOURNAL = -16;

        public const int ERR_IL_VECTOR_UNABLE_TO_GET_TYPE = -20;
        public const int ERR_IL_UNABLE_TO_GET_PARAM = -21;
        public const int ERR_IL_UNKNOWN = -22;

    }    
}
