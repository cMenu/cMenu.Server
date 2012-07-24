using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using cMenu.Windows;
using cMenu.Windows.Controls;
using cMenu.Windows.Helpers;
using cMenu.Windows.VisualEffects;

namespace cMenu.Test.Controls.BitFlexCtrls
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            this.sAeroInitialize();
            this.sAeroSet(new CSystemUIHelper.Margins() { Bottom = 0, Left = 0, Right = 0, Top = this.panel1.Height });
            this.sAeroSetTransparency(this.panel1);        

        }
    }
}
