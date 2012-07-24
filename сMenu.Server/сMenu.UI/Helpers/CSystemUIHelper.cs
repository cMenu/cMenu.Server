using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.Windows.Helpers
{
    public class CSystemUIHelper
    {
        #region WINAPI
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TOOLWINDOW = 0x00000080;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern void DwmExtendFrameIntoClientArea(System.IntPtr hWnd, ref Margins pMargins);

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern void DwmIsCompositionEnabled(ref bool isEnabled);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr window, int index, int value);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr window, int index);

        public struct Margins
        {
            public int Left, Right, Top, Bottom;
        }
        #endregion
        
        public static bool sIsCompositionEnabled()
        {
            var Version = CSystemHelper.sGetOSVersion();
            if (Version == EnOSVersion.EOld || Version == EnOSVersion.EUnknown)
                return false;
            var Enabled = false;
            CSystemUIHelper.DwmIsCompositionEnabled(ref Enabled);
            return Enabled;
        }
    }
}
