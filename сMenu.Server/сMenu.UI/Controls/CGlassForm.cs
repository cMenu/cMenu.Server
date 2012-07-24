using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Dialogs.Controls;

using cMenu.Windows.Helpers;

namespace cMenu.Windows.Controls
{
    public static class CGlassFormExt
    {
        public static int sAeroInitialize(this Form Form)
        {
            return CGlassForm.sInitializeAero(Form);
        }
        public static int sAeroSet(this Form Form, CSystemUIHelper.Margins Margins)
        {
            return CGlassForm.sSetAero(Form, Margins);
        }
        public static int sAeroSetTransparency(this Form Form, Control Control)
        {
            return CGlassForm.sSetAeroGlassTransparency(Form, Control);
        }
        public static int sHideFull(this Form Form)
        {
            return CGlassForm.sHideFull(Form);
        }
        public static int sShowFull(this Form Form)
        {
            return CGlassForm.sShowFull(Form);
        }
    }

    public class CGlassForm : Microsoft.WindowsAPICodePack.Shell.GlassForm
    {
        #region CONSTRUCTORS
        public CGlassForm()
        {
            Color BackColor = Color.FromArgb(255, 221, 220, 220);
            this.TransparencyKey = BackColor;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public int SetAero(CSystemUIHelper.Margins Margins)
        {
            CSystemUIHelper.DwmExtendFrameIntoClientArea(this.Handle, ref Margins);
            return -1;
        }
        public int SetAeroGlassTransparency(Control Control)
        {
            Control.BackColor = this.TransparencyKey;
            return -1;
        }
        public int HideFull()
        {
            this.Visible = false;
            this.ShowInTaskbar = false;
            CSystemUIHelper.SetWindowLong(Handle, CSystemUIHelper.GWL_EXSTYLE, CSystemUIHelper.GetWindowLong(Handle, CSystemUIHelper.GWL_EXSTYLE) | CSystemUIHelper.WS_EX_TOOLWINDOW);
            return -1;
        }
        public int ShowFull()
        {
            this.Visible = true;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            return -1;
        }
        #endregion

        #region STATIC FUNCTIONS
        public static int sInitializeAero(Form Form)
        {
            Color BackColor = Color.FromArgb(255, 221, 220, 220);
            Form.TransparencyKey = BackColor;
            return -1;
        }
        public static int sSetAero(Form Form, CSystemUIHelper.Margins Margins)
        {
            CSystemUIHelper.DwmExtendFrameIntoClientArea(Form.Handle, ref Margins);
            return -1;
        }
        public static int sSetAeroGlassTransparency(Form Form, Control Control)
        {
            Control.BackColor = Form.TransparencyKey;
            return -1;
        }
        public static int sHideFull(Form Form)
        {
            Form.WindowState = FormWindowState.Minimized;
            Form.Visible = false;
            Form.ShowInTaskbar = false;
            CSystemUIHelper.SetWindowLong(Form.Handle, CSystemUIHelper.GWL_EXSTYLE, CSystemUIHelper.GetWindowLong(Form.Handle, CSystemUIHelper.GWL_EXSTYLE) | CSystemUIHelper.WS_EX_TOOLWINDOW);
            return -1;
        }
        public static int sShowFull(Form Form)
        {
            Form.Visible = true;
            Form.ShowInTaskbar = true;
            Form.WindowState = FormWindowState.Normal;
            return -1;
        }
        #endregion
    }
}
