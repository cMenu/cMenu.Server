using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Common
{
    public class CConsts
    {
    }

    public class CErrors
    {
        public const int ERR_SUC = -1;

        public const int ERR_DB_DELETE_OBJECT = -2;
        public const int ERR_DB_UPDATE_OBJECT = -3;
        public const int ERR_DB_INSERT_OBJECT = -5;
        public const int ERR_DB_GET_OBJECT = -6;

        public const int ERR_SRVR_INIT_ENV = -7;

        public const int ERR_FS_SERIALIZE = -4;
    }    
}