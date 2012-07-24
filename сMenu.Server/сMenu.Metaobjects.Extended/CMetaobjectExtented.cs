using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects;
using cMenu.Security;

namespace cMenu.Metaobjects.Extended
{
    [Serializable]
    public class CMetaobjectExtented : CSecuredMetaobject
    {
        #region PROTECTED FIELDS
        protected List<CMediaResource> _mediaResources = new List<CMediaResource>();
        #endregion

        #region PUBLIC FIELDS
        public List<CMediaResource> MediaResources
        {
            get { return _mediaResources; }
            set { _mediaResources = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMetaobjectExtented() 
            : base()
        { 
        }
        public CMetaobjectExtented(IDatabaseProvider Provider)
            : base(Provider)
        {
        }
        public CMetaobjectExtented(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
        }
        public CMetaobjectExtented(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public List<CMediaResource> GetMediaResources(IDatabaseProvider Provider)
        {
            var Links = CMetaobjectLink.sGetInternalLinksByClass(this._key, EnMetaobjectClass.EMediaResource, Provider);
            foreach (CMetaobjectLink Link in Links)
            {
                CMediaResource Media = new CMediaResource(Link.LinkedObjectKey, Provider);
                this._mediaResources.Add(Media);
            }

            return this._mediaResources;
        }
        #endregion
    }
}
