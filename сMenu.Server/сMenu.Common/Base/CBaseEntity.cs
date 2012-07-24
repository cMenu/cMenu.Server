using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace cMenu.Common.Base
{
    [Serializable]
    public class CBaseEntity
    {
        #region PROTECTED FIELDS
        protected Guid _id = Guid.Empty;
        protected string _name;
        #endregion

        #region PUBLIC FIELDS
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public CBaseEntity()
        {
            this._id = Guid.NewGuid();
            this._name = "";
        }
        public CBaseEntity(Guid ID)
        {
            this._id = ID;
        }
        public CBaseEntity(Guid ID, string Name)
        {
            this._id = ID;
            this._name = Name;
        }
        #endregion
    }
}
