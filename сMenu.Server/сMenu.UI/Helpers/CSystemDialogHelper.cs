using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace cMenu.Windows.Helpers
{
    public class CSystemDialogHelper
    {
        public static int sShowOpenFileDialog(string Title, string Filter, bool Multiselection)
        {
            var Version = CSystemHelper.sGetOSVersion();
            if (Version == EnOSVersion.ESeven)
            {
                CommonOpenFileDialog Dlg = new CommonOpenFileDialog() 
                { 
                    Title = Title
                };
                Dlg.ShowDialog();
            }
            return -1;
        }
    }
}
