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

namespace cMenu.Metaobjects.Extended.Linq
{
    [Serializable]
    public class CMetaobjectExtented : CSecuredMetaobject
    {
        #region PROTECTED FIELDS
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<int> _likesNumberAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<int>() { AttributeID = CMetaobjectsExConsts.CONST_OBJ_ATTR_LIKES_NUMBER };
        protected List<cMenu.Metaobjects.Extended.Linq.CMediaResource> _mediaResources = new List<cMenu.Metaobjects.Extended.Linq.CMediaResource>();
        protected List<cMenu.Metaobjects.Linq.CMetaobjectComment> _comments = new List<cMenu.Metaobjects.Linq.CMetaobjectComment>();
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<int> LikesNumberAttribute
        {
            get { return _likesNumberAttribute; }
            set { _likesNumberAttribute = value; }
        }

        public int LikesNumber
        {
            get { return _likesNumberAttribute.DefaultValue; }
            set { _likesNumberAttribute.DefaultValue = value; }
        }

        public List<cMenu.Metaobjects.Extended.Linq.CMediaResource> MediaResources
        {
            get { return _mediaResources; }
            set { _mediaResources = value; }
        }
        public List<cMenu.Metaobjects.Linq.CMetaobjectComment> Comments
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
        public CMetaobjectExtented(DataContext Context)
            : base(Context)
        {
            this._likesNumberAttribute.Attributes = this._attributes;

            this.LikesNumber = 0;
        }
        public CMetaobjectExtented(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._likesNumberAttribute.Attributes = this._attributes;

            this.LikesNumber = 0;
        }
        public CMetaobjectExtented(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._likesNumberAttribute.Attributes = this._attributes;

            this.LikesNumber = 0;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public List<cMenu.Metaobjects.Extended.Linq.CMediaResource> GetMediaResources(DataContext Context)
        {
            var Links = cMenu.Metaobjects.Linq.CMetaobjectLink.sGetInternalLinksByClass(this._key, EnMetaobjectClass.EMediaResource, Context);
            foreach (cMenu.Metaobjects.Linq.CMetaobjectLink Link in Links)
            {
                cMenu.Metaobjects.Extended.Linq.CMediaResource Media = new cMenu.Metaobjects.Extended.Linq.CMediaResource(Link.LinkedObjectKey, Context);
                Media.Attributes.Context = Context;
                this._mediaResources.Add(Media);
            }

            return this._mediaResources;
        }
        public List<cMenu.Metaobjects.Linq.CMetaobjectComment> GetComments(DataContext Context)
        {
            this._comments = cMenu.Metaobjects.Linq.CMetaobjectComment.sGetCommentsByObject(this._key, Context);
            return this._comments;
        }

        public override int ObjectDeleteByID(DataContext Context)
        {
            var R = cMenu.Metaobjects.Linq.CMetaobjectComment.sDeleteCommentsByObject(this._key, Context);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            return base.ObjectDeleteByID(Context);
        }
        public override int ObjectDeleteByKey(DataContext Context)
        {
            var R = cMenu.Metaobjects.Linq.CMetaobjectComment.sDeleteCommentsByObject(this._key, Context);
            if (R != CErrors.ERR_SUC)
                return CErrors.ERR_DB_DELETE_OBJECT;

            return base.ObjectDeleteByKey(Context);
        }
        #endregion
    }
}
