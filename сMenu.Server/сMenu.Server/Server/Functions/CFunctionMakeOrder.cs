using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using cMenu.DB;
using cMenu.IO;
using cMenu.Common.Base;
using cMenu.Metadata;
using cMenu.Metadata.Attributes;
using cMenu.Security;
using cMenu.Security.UsersManagement;
using cMenu.Communication.Server.Environment;
using cMenu.Communication.Server.Configuration;
using cMenu.Communication.Server.Notifications;
using cMenu.Communication.Client;
using cMenu.Metaobjects;
using cMenu.Metaobjects.Extended;
using cMenu.Metaobjects.Extended.Menu;
using cMenu.Metaobjects.Extended.Order;
using cMenu.Globalization;

namespace cMenu.Communication.Server.Functions
{
    [CMetaFunctionAttribute(CServerFunctionID.CONST_FUNC_ID_MAKE_ORDER, true, true)]
    public class CFunctionMakeOrder : CServerFunction
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
        /// PARAM_ORDER - заказ
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

            if (!Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_OBJECT))
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_INPUT_ORDER_NULL", ClientCulture));           
            if (!CFunctionExecutionEnvironment.CurrentUser.PolicyAllowEditOrdersList())
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_UNABLE_TO_EDIT_ORDERS", ClientCulture));

            string OrderJSON = (string)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_OBJECT];
            CMenuServiceOrder Order = (CMenuServiceOrder)CSerialize.sDeserializeJSONStream(OrderJSON.ToDataStream(), typeof(CMenuServiceOrder));
            if (Order == null)
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_BAD_ORDER", ClientCulture));

            Order.Key = CDatabaseSequence.sGetObjectKey(CFunctionExecutionEnvironment.sGetCurrentProvider());
            Order.OrderInsert(CFunctionExecutionEnvironment.sGetCurrentProvider());            

            foreach (CNotificationConfiguration Config in CFunctionExecutionEnvironment.ApplicationServerConfiguration.Notifications)
            {
                if (Config.Type == EnNotificationApplicationType.EUserInterfaceNotification)
                {
                    CNotificationRequest Request = new CNotificationRequest();
                    Request.Date = DateTime.Now;
                    Request.Header = CGlobalizationHelper.sGetStringResource("MSG_MAKE_ORDER", ClientCulture);
                    Request.ID = Guid.NewGuid();
                    Request.Name = CGlobalizationHelper.sGetStringResource("MSG_MAKE_ORDER", ClientCulture);
                    Request.Source = CFunctionExecutionEnvironment.ApplicationServerConfiguration.Name;
                    Request.Content = CGlobalizationHelper.sGetStringResource("MSG_DEVICE_NAME", ClientCulture);

                    CCommunicationClient.sRequestNotificationAsync(Request, Config.Address);
                }
                if (Config.Type == EnNotificationApplicationType.ELocalNetworkPrinter)
                {
                    /// Отправка на принтер
                }
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
