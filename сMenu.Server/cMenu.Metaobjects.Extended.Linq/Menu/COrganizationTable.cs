using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects.Extended;
using cMenu.Metaobjects.Linq;
using cMenu.Security.Linq;

using Newtonsoft.Json;

namespace cMenu.Metaobjects.Extended.Linq.Menu
{
    [Serializable]
    public class COrganizationTable : CMetaobjectExtented
    {
        #region CONSTRUCTORS
        public COrganizationTable()
            : base()
        {
            this._class = EnMetaobjectClass.EOrganizationTable;
        }
        public COrganizationTable(DataContext Context)
            : base(Context)
        {
            this._class = EnMetaobjectClass.EOrganizationTable;
        }
        public COrganizationTable(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._class = EnMetaobjectClass.EOrganizationTable;
        }
        public COrganizationTable(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._class = EnMetaobjectClass.EOrganizationTable;
        }
        #endregion
    }
}
