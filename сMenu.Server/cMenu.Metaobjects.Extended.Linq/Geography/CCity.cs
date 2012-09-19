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

namespace cMenu.Metaobjects.Extended.Linq.Geography
{
    [Serializable]
    public class CCity : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> _coordsAttribute = new cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_CTY_ATTR_COORDS };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public cMenu.Metaobjects.Linq.CMetaobjectAttributeLocalized<string> CoordsAttribute
        {
            get { return _coordsAttribute; }
            set { _coordsAttribute = value; }
        }

        public string Coordinates
        {
            get { return _coordsAttribute.DefaultValue; }
            set { _coordsAttribute.DefaultValue = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CCity()
            : base()
        {
            this._coordsAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.ECity;
        }
        public CCity(DataContext Context)
            : base(Context)
        {
            this._coordsAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.ECity;
            this.Coordinates = "";
        }
        public CCity(Guid ID, DataContext Context)
            : base(ID, Context)
        {
            this._coordsAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.ECity;
            this.Coordinates = "";
        }
        public CCity(decimal Key, DataContext Context)
            : base(Key, Context)
        {
            this._coordsAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.ECity;
            this.Coordinates = "";
        }
        #endregion
    }
}
