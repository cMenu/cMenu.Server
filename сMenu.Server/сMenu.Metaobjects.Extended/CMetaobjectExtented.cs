using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.DB;
using cMenu.Metaobjects;
using cMenu.Security;

using Newtonsoft.Json;

namespace cMenu.Metaobjects.Extended
{
    [Serializable]
    public class CMetaobjectExtented : CSecuredMetaobject
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<int> _likesNumberAttribute = new CMetaobjectAttributeLocalized<int>() { AttributeID = CMetaobjectsExConsts.CONST_OBJ_ATTR_LIKES_NUMBER };
        protected List<CMediaResource> _mediaResources = new List<CMediaResource>();
        protected List<CMetaobjectComment> _comments = new List<CMetaobjectComment>();
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<int> LikesNumberAttribute
        {
            get { return _likesNumberAttribute; }
            set { _likesNumberAttribute = value; }
        }

        public int LikesNumber
        {
            get { return _likesNumberAttribute.DefaultValue; }
            set { _likesNumberAttribute.DefaultValue = value; }
        }

        public List<CMediaResource> MediaResources
        {
            get { return _mediaResources; }
            set { _mediaResources = value; }
        }
        public List<CMetaobjectComment> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CMetaobjectExtented() 
            : base()
        {
            this._likesNumberAttribute.Attributes = this._attributes;
        }
        public CMetaobjectExtented(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._likesNumberAttribute.Attributes = this._attributes;

            this.LikesNumber = 0;
        }
        public CMetaobjectExtented(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
            this._likesNumberAttribute.Attributes = this._attributes;

            this.LikesNumber = 0;
        }
        public CMetaobjectExtented(Guid ID, IDatabaseProvider Provider)
            : base(ID, Provider)
        {
            this._likesNumberAttribute.Attributes = this._attributes;

            this.LikesNumber = 0;
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
        public List<CMetaobjectComment> GetComments(IDatabaseProvider Provider)
        {
            this._comments = CMetaobjectComment.sGetCommentsByObject(this._key, Provider);
            return this._comments;
        }

        public override int ObjectDeleteByID(IDatabaseProvider Provider)
        {
            var R = CMetaobjectComment.sDeleteCommentsByObject(this._key, Provider);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            return base.ObjectDeleteByID(Provider);
        }
        public override int ObjectDeleteByKey(IDatabaseProvider Provider)
        {
            var R = CMetaobjectComment.sDeleteCommentsByObject(this._key, Provider);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            return base.ObjectDeleteByKey(Provider);
        }
        #endregion
    }
}
