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
            return Resources.ResourceManager.GetString(ID, Culture);
        }
        public static object sGetBinaryResource(string ID, CultureInfo Culture)
        {
            return Resources.ResourceManager.GetObject(ID, Culture);
        }
    }
}
