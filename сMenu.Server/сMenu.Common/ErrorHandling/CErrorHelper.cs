using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Globalization;

namespace cMenu.Common.ErrorHandling
{
    public class CErrorHelper
    {
        public static int sThrowException(string ResourceMessageID, Exception InnerException)
        {
            /// var Message = Resources.ResourceManager.GetString(ResourceMessageID, Resources.Culture);
            /// var Exception = new Exception(Message, InnerException);
            /// sLogException(Exception);
            /// throw Exception;
            return -1;
        }
        public static int sLogException(Exception Exception)
        {
            return -1;
        }
    }
}
