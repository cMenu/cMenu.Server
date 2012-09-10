using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using iMenu.DB;
using iMenu.Common;
using iMenu.Metaobjects;
using iMenu.Security;

namespace iMenu.Metaobjects.Extended
{
    public class CMenu : CSecuredMetaobject
    {
        #region PUBLIC FIELDS
        public Image Image
        {
            get { return (Image)this._attributes[CMetaobjectsExConsts.CONST_MENU_ATTR_IMAGE]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_MENU_ATTR_IMAGE] = value; }
        }
        public bool Primary
        {
            get { return (bool)this._attributes[CMetaobjectsExConsts.CONST_MENU_ATTR_PRIMARY]; }
            set { this._attributes[CMetaobjectsExConsts.CONST_MENU_ATTR_PRIMARY] = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMenu(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._class = EnMetaobjectClass.EMenu;
            this.Image = null;
        }
        public CMenu(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider, false, false)
        {
        }
        public CMenu(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider, false, false)
        {
        }
        #endregion
    }
}
