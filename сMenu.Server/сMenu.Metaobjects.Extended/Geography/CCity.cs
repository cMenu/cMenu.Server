using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common;
using cMenu.Metaobjects;
using cMenu.DB;

using Newtonsoft.Json;

namespace cMenu.Metaobjects.Extended.Geography
{
    public class CCity : CMetaobjectExtented
    {
        #region PROTECTED FIELDS
        protected CMetaobjectAttributeLocalized<string> _coordsAttribute = new CMetaobjectAttributeLocalized<string>() { AttributeID = CMetaobjectsExConsts.CONST_CTY_ATTR_COORDS };
        #endregion

        #region PUBLIC FIELDS
        [JsonIgnore]
        public CMetaobjectAttributeLocalized<string> CoordsAttribute
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
        public CCity(IDatabaseProvider Provider)
            : base(Provider)
        {
            this._coordsAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.ECity;
            this.Coordinates = "";
        }
        public CCity(Guid ID, IDatabaseProvider Provider) 
            : base(ID, Provider)
        {
            this._coordsAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.ECity;
            this.Coordinates = "";
        }
        public CCity(decimal Key, IDatabaseProvider Provider)
            : base(Key, Provider)
        {
            this._coordsAttribute.Attributes = this._attributes;

            this._class = EnMetaobjectClass.ECity;
            this.Coordinates = "";
        }
        #endregion
    }
}
