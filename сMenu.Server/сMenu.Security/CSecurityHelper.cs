using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

using cMenu.IO;
using cMenu.Metaobjects;

namespace cMenu.Security
{
    public class CSecurityHelper
    {
        #region STATIC FUNCTIONS
        public static string sGeneratePasshash(string Login, string Password)
        {
            var P = new CSecurityPassword((Login + "." + Password).ToCharArray());
            var S = P.Salt;
            var H = P.Hash;
            return S + "|" + H;
        }
        public static bool sVerifyPasshash(string Login, string Password, string Hash)
        {
            var R = false;
            var S = Hash.Split('|')[0];
            var H = Hash.Split('|')[1];

            var P = new CSecurityPassword(S, H);
            R = P.Verify((Login + "." + Password).ToCharArray());

            return R;
        }
        #endregion
    }
}
