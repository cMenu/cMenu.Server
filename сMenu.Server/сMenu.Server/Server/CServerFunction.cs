using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cMenu.Common.Base;

namespace cMenu.Communication.Server
{
    public class CServerFunction : CBaseEntity, IServerFunction
    {
        #region PROTECTED FIELDS
        protected Dictionary<string, object> _paramaters;
        #endregion

        #region PROTECTED FUNCTIONS
        #endregion

        #region PUBLIC FUNCTIONS
        public virtual int Initialize(Dictionary<string, object> Parameters)
        {
            this._paramaters = Parameters;
            return -1;
        }
        public virtual CFunctionResult Execute(Dictionary<string, object> Parameters)
        {
            CFunctionResult Result = new CFunctionResult();
            Result.FunctionID = "VIRTUAL";
            return Result;
        }
        #endregion
    }
}
