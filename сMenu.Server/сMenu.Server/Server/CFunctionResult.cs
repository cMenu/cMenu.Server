using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using cMenu.Common;

namespace cMenu.Communication.Server
{
    [Serializable]
    [DataContractFormat]
    public class CFunctionResult
    {
        #region PROTECTED FIELDS
        protected int _errorCode = CErrors.ERR_SUC;
        protected Dictionary<string, object> _inputParameters;
        protected string _functionID;
        protected object _content;
        protected string _message = "";
        protected EnFunctionResultType _type;
        #endregion

        #region PUBLIC FIELDS
        public int ErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }
        public Dictionary<string, object> InputParameters
        {
            get { return _inputParameters; }
            set { _inputParameters = value; }
        }
        public EnFunctionResultType ResultType
        {
            get { return _type; }
            set { _type = value; }
        }
        public string FunctionID
        {
            get { return _functionID; }
            set { _functionID = value; }
        }
        public object Content
        {
            get { return _content; }
            set { _content = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        #endregion

        #region STATIC FUNCTIONS
        #endregion
    }
}
