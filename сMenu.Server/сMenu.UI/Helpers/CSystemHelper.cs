using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Windows.Helpers
{
    public class CSystemHelper
    {
        public static EnOSVersion sGetOSVersion()
        {
            var Version = Environment.OSVersion;
            if (Version.Version.Major < 6)
                return EnOSVersion.EOld;
            if (Version.Version.MajorRevision == 0)
                return EnOSVersion.EVista;
            if (Version.Version.MajorRevision == 1)
                return EnOSVersion.ESeven;

            return EnOSVersion.EUnknown;
        }
        public static EnPlatformType sGetPlatformType()
        {
            return (Environment.Is64BitOperatingSystem ? EnPlatformType.Ex64 : EnPlatformType.Ex86);
        }
        public static EnPlatformType sGetPlatformByCurrentProcess()
        {
            return (Environment.Is64BitProcess ? EnPlatformType.Ex64 : EnPlatformType.Ex86);
        }        
    }
}
