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

namespace cMenu.Metaobjects.Extended.Linq
{
    [Serializable]
    public class CFolder : CMetaobjectExtented
    {
        #region CONSTRUCTORS
        public CFolder()
            : base()
        {
            this._class = EnMetaobjectClass.EFolder;
        }
        public CFolder(DataContext Context)
            : base(Context)
        {
            this._class = EnMetaobjectClass.EFolder;
        }
        public CFolder(decimal Key, DataContext Context)
            : base(Key, Context)
        {
        }
        public CFolder(Guid ID, DataContext Context)
            : base(ID, Context)
        {
        }
        #endregion
    }
}
