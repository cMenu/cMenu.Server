using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.DB;
using cMenu.Common;
using cMenu.Metaobjects;
using cMenu.Security;

namespace cMenu.Metaobjects.Extended
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
        public CFolder(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._class = EnMetaobjectClass.EFolder;
        }
        public CFolder(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
        }
        public CFolder(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
        }
        #endregion
    }
}
