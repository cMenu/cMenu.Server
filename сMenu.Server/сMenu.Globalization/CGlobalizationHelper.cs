using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Globalization;

using cMenu.Globalization.Properties;

namespace cMenu.Globalization
{
    public class CGlobalizationHelper
    {
        public static string sGetStringResource(string ID, CultureInfo Culture)
        {
            var R = Resources.ResourceManager.GetString(ID, Culture);
            return R;
        }
        public static object sGetBinaryResource(string ID, CultureInfo Culture)
        {
            var R = Resources.ResourceManager.GetObject(ID, Culture);
            return R;
        }
    }
}
