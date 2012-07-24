using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using cMenu.Common.Base;
using cMenu.Common.ErrorHandling;
using cMenu.Metadata;
using cMenu.Metadata.Attributes;
using cMenu.Security;
using cMenu.Security.UsersManagement;
using cMenu.Security.DevicesManagement;
using cMenu.Communication.Server.Environment;
using cMenu.Metaobjects;
using cMenu.Metaobjects.Extended;
using cMenu.Metaobjects.Extended.Menu;
using cMenu.Metaobjects.Extended.Order;
using cMenu.Globalization;

namespace cMenu.Communication.Server.Functions
{
    [CMetaFunctionAttribute(CServerFunctionID.CONST_FUNC_ID_GET_ORDER, true, true)]
    public class CFunctionGetOrderData : CServerFunction
    {
        #region PROTECTED FIELDS
        #endregion

        #region PROTECTED FUNCTIONS
        protected CFunctionResult _compileResult(EnFunctionResultType Type, Dictionary<string, object> Parameters, string Message)
        {
            CFunctionResult R = new CFunctionResult
            {
                ResultType = Type,
                InputParameters = Parameters,
                Message = Message
            };
            return R;
        }
        #endregion

        #region PUBLIC FUNCTIONS
        /// <summary>
        /// Параметры функции
        /// PARAM_OBJECT_ID - идентификатор объекта
        /// PARAM_OBJECT_KEY - ключ объекта
        /// PARAM_LOCALE - локаль клиентского устройства
        /// </summary>
        /// <param name="Parameters">Параметры</param>
        /// <returns></returns>
        public override CFunctionResult Execute(Dictionary<string, object> Parameters)
        {
            CFunctionResult Result = new CFunctionResult();
            CultureInfo ClientCulture = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_LOCALE) ? CultureInfo.GetCultureInfo((int)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_LOCALE]) : CultureInfo.CurrentCulture);

            CFunctionExecutionEnvironment.CurrentUser.GetPolicies(CFunctionExecutionEnvironment.sGetCurrentProvider());
            CFunctionExecutionEnvironment.CurrentUser.GetSecurityRecords(CFunctionExecutionEnvironment.sGetCurrentProvider());

            decimal ObjectKey = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_OBJECT_KEY) ? (decimal)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_OBJECT_KEY] : -1);
            string ObjectID = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_OBJECT_ID) ? (string)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_OBJECT_ID] : "");

            if (ObjectID == "" && ObjectKey == -1)
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_KEY_ID_NULL", ClientCulture));
            if (!CFunctionExecutionEnvironment.CurrentUser.PolicyAllowViewOrdersList())
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_UNABLE_TO_VIEW_ORDERS", ClientCulture));

            CMenuServiceOrder Order = new CMenuServiceOrder();
            if (ObjectKey != -1)
            {
                Order.Key = ObjectKey;
                Order.OrderGetByKey(CFunctionExecutionEnvironment.sGetCurrentProvider());
                if (Order.ID == Guid.Empty)
                    return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_ORDER_NULL", ClientCulture));
            }
            if (ObjectID != "")
            {
                Order.ID = Guid.Parse(ObjectID);
                Order.OrderGetByID(CFunctionExecutionEnvironment.sGetCurrentProvider());
                if (Order.Key == -1)
                    return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_ORDER_NULL", ClientCulture));
            }

            Result.ErrorCode = -1;
            Result.ResultType = EnFunctionResultType.ESuccess;
            Result.InputParameters = Parameters;
            Result.Message = CGlobalizationHelper.sGetStringResource("ERROR_MSG_SUCCESS", ClientCulture);
            Result.Content = Order;

            return Result;
        }
        #endregion
    }
}
