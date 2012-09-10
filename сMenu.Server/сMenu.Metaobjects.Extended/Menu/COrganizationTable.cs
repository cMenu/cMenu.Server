using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects;

namespace cMenu.Metaobjects.Extended.Menu
{
    public class COrganizationTable : CMenuService
    {
        #region CONSTRUCTORS
        public COrganizationTable()
            : base()
        {
            this._class = EnMetaobjectClass.EOrganizationTable;
        }
        public COrganizationTable(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._class = EnMetaobjectClass.EOrganizationTable;
        }
        public COrganizationTable(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._class = EnMetaobjectClass.EOrganizationTable;
        }
        public COrganizationTable(decimal Key, IDatabaseProvider Provider) 
            : base(Key, Provider)
        {
            this._class = EnMetaobjectClass.EOrganizationTable;
        }
        #endregion
    }
}
