using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

using cMenu.Web.Server.Tablet.Common;
using cMenu.Communication.Server;
using cMenu.Metaobjects;
using cMenu.Metaobjects.Linq;
using cMenu.Metaobjects.Extended.Linq;
using cMenu.Metaobjects.Extended.Linq.Order;
using cMenu.Security;
using cMenu.Security.Linq.UsersManagement;
using cMenu.Globalization;
using cMenu.IO;
using cMenu.Common;
using cMenu.DB;

namespace cMenu.Web.Server.Tablet.Controllers
{
    public class OrderController : Controller
    {
        #region PROTECTED FUNCTIONS
        protected CFunctionResult _getOrderInformation(string UserIdentity, string Passhash, Guid SessionID, decimal OrderKey)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("UserIdentity", UserIdentity);
            InputParameters.Add("Passhash", Passhash);
            InputParameters.Add("SessionID", SessionID.ToString().ToUpper());
            InputParameters.Add("OrderKey", OrderKey);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "GetOrderInformation",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            if (OrderKey == null)
            {
                R = CServerHelper.sCompileFunctionResult("GetOrderInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_KEY_NULL", CultureInfo.CurrentCulture), null);
                return R;
            }

            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "GetOrderInformation";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            CSystemUser User = (CSystemUser)(UserResult.Content as object[])[0];
            CSystemUserSession Session = (CSystemUserSession)(UserResult.Content as object[])[1];

            CMenuServiceOrder Order = new CMenuServiceOrder();
            Order.Key = OrderKey;
            Order.OrderGetByKey(CServerEnvironment.DataContext);

            if (Order.ID == Guid.Empty)
            {
                R = CServerHelper.sCompileFunctionResult("GetOrderInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
                return R;
            }

            if (Session.Key != Order.SessionKey)
            {
                R = CServerHelper.sCompileFunctionResult("GetOrderInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SESSION_KEYS_NOT_EQUALS", CultureInfo.CurrentCulture), null);
                return R;
            }

            Order.GetAmounts(CServerEnvironment.DataContext);
            R.Content = Order;

            return R;
        }
        protected CFunctionResult _getOrderInformation(string UserIdentity, string Passhash, Guid SessionID, Guid OrderID)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("UserIdentity", UserIdentity);
            InputParameters.Add("Passhash", Passhash);
            InputParameters.Add("SessionID", SessionID.ToString().ToUpper());
            InputParameters.Add("OrderID", OrderID);

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "GetOrderInformation",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            if (OrderID == null)
            {
                R = CServerHelper.sCompileFunctionResult("GetOrderInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_ID_NULL", CultureInfo.CurrentCulture), null);
                return R;
            }

            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "GetOrderInformation";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            CSystemUser User = (CSystemUser)(UserResult.Content as object[])[0];
            CSystemUserSession Session = (CSystemUserSession)(UserResult.Content as object[])[1];

            CMenuServiceOrder Order = new CMenuServiceOrder();
            Order.ID = OrderID;
            Order.OrderGetByID(CServerEnvironment.DataContext);

            if (Order.Key == CDBConst.CONST_OBJECT_EMPTY_KEY)
            {
                R = CServerHelper.sCompileFunctionResult("GetOrderInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_OBJECT_UNABLE_TO_FIND", CultureInfo.CurrentCulture), null);
                return R;
            }

            if (Session.Key != Order.SessionKey)
            {
                R = CServerHelper.sCompileFunctionResult("GetOrderInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SESSION_KEYS_NOT_EQUALS", CultureInfo.CurrentCulture), null);
                return R;
            }

            Order.GetAmounts(CServerEnvironment.DataContext);
            R.Content = Order;

            return R;
        }
        protected CFunctionResult _makeOrder(string UserIdentity, string Passhash, Guid SessionID, string OrderJSON)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("UserIdentity", UserIdentity);
            InputParameters.Add("Passhash", Passhash);
            InputParameters.Add("SessionID", SessionID.ToString().ToUpper());

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "MakeOrder",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            if (OrderJSON == null)
            {
                R = CServerHelper.sCompileFunctionResult("MakeOrder", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_INPUT_OBJECT_BAD_FORMAT", CultureInfo.CurrentCulture), null);
                return R;
            }

            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "MakeOrder";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            CSystemUser User = (CSystemUser)(UserResult.Content as object[])[0];
            CSystemUserSession Session = (CSystemUserSession)(UserResult.Content as object[])[1];

            CMenuServiceOrder NewOrder = OrderJSON.ToDataStream().DeserializeJSONStream<CMenuServiceOrder>(typeof(CMenuServiceOrder));
            if (NewOrder == null)
            {
                R = CServerHelper.sCompileFunctionResult("MakeOrder", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_INPUT_OBJECT_BAD_FORMAT", CultureInfo.CurrentCulture), null);
                return R;
            }

            NewOrder.SessionKey = Session.Key;
            NewOrder.Key = CDatabaseSequence.sGetObjectKey(CServerEnvironment.DatabaseProvider);
            NewOrder.ID = Guid.NewGuid();
            NewOrder.Date = DateTime.Now;

            var RR = NewOrder.OrderInsert(CServerEnvironment.DataContext);
            if (RR != CErrors.ERR_SUC)
            {
                R = CServerHelper.sCompileFunctionResult("MakeOrder", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_ORDER_MAKE_UNABLE", CultureInfo.CurrentCulture), null);
                return R;
            }

            try
            { CServerEnvironment.DataContext.SubmitChanges(); }
            catch (Exception Exception)
            {
                R = CServerHelper.sCompileFunctionResult("MakeOrder", Communication.EnFunctionResultType.EError, InputParameters, Exception.Message, null);
                return R;
            }

            R.Content = NewOrder;

            return R;
        }
        protected CFunctionResult _callOficiant(string UserIdentity, string Passhash, Guid SessionID)
        {
            Dictionary<string, object> InputParameters = new Dictionary<string, object>();
            InputParameters.Add("UserIdentity", UserIdentity);
            InputParameters.Add("Passhash", Passhash);
            InputParameters.Add("SessionID", SessionID.ToString().ToUpper());

            CFunctionResult R = new CFunctionResult()
            {
                FunctionID = "CallOficiant",
                InputParameters = InputParameters,
                ResultType = Communication.EnFunctionResultType.ESuccess
            };

            CFunctionResult UserResult = CServerHelper.sCheckUser(UserIdentity, Passhash, SessionID);
            if (UserResult.ResultType != Communication.EnFunctionResultType.ESuccess)
            {
                UserResult.FunctionID = "CallOficiant";
                UserResult.InputParameters = InputParameters;
                return UserResult;
            }

            CSystemUser User = (CSystemUser)(UserResult.Content as object[])[0];
            CSystemUserSession Session = (CSystemUserSession)(UserResult.Content as object[])[1];            

            return R;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        public string GetOrderInformationByKey(string UserIdentity, string Passhash, Guid? SessionID, decimal? OrderKey)
        {
            try
            {
                var R = this._getOrderInformation(UserIdentity, Passhash, (Guid)SessionID, (decimal)OrderKey);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Passhash", Passhash);
                InputParameters.Add("SessionID", (SessionID == null ? "" : SessionID.ToString().ToUpper()));
                InputParameters.Add("OrderKey", OrderKey);

                var R = CServerHelper.sCompileFunctionResult("GetOrderInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }
        public string GetOrderInformationByID(string UserIdentity, string Passhash, Guid? SessionID, Guid? OrderID)
        {
            try
            {
                var R = this._getOrderInformation(UserIdentity, Passhash, (Guid)SessionID, (Guid)OrderID);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Passhash", Passhash);
                InputParameters.Add("SessionID", (SessionID == null ? "" : SessionID.ToString().ToUpper()));
                InputParameters.Add("OrderID", (OrderID == null ? "" : OrderID.ToString().ToUpper()));

                var R = CServerHelper.sCompileFunctionResult("GetOrderInformation", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }
        public string MakeOrder(string UserIdentity, string Passhash, Guid? SessionID, string OrderJSON)
        {
            try
            {
                var R = this._makeOrder(UserIdentity, Passhash, (Guid)SessionID, OrderJSON);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Passhash", Passhash);
                InputParameters.Add("SessionID", (SessionID == null ? "" : SessionID.ToString().ToUpper()));
                InputParameters.Add("OrderJSON", OrderJSON);

                var R = CServerHelper.sCompileFunctionResult("MakeOrder", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }
        public string CallOficiant(string UserIdentity, string Passhash, Guid? SessionID)
        {
            try
            {
                var R = this._callOficiant(UserIdentity, Passhash, (Guid)SessionID);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }
            catch (Exception)
            {
                Dictionary<string, object> InputParameters = new Dictionary<string, object>();
                InputParameters.Add("UserIdentity", UserIdentity);
                InputParameters.Add("Passhash", Passhash);
                InputParameters.Add("SessionID", (SessionID == null ? "" : SessionID.ToString().ToUpper()));

                var R = CServerHelper.sCompileFunctionResult("CallOficiant", Communication.EnFunctionResultType.EError, InputParameters, CGlobalizationHelper.sGetStringResource("ERROR_SERVER_UNKNOWN_ERROR", CultureInfo.CurrentCulture), null);
                var JSON = CSerialize.sSerializeJSONStream(R).ToDataString();
                return JSON;
            }            
        }
        #endregion

        public string Index()
        {
            return "";
        }
    }
}
