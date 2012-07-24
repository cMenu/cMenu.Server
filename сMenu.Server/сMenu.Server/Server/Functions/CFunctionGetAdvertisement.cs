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
using cMenu.Metaobjects.Extended.Helpers;
using cMenu.Metaobjects.Extended.Menu;
using cMenu.Metaobjects.Extended.Advertisement;
using cMenu.Globalization;

namespace cMenu.Communication.Server.Functions
{
    [CMetaFunctionAttribute(CServerFunctionID.CONST_FUNC_ID_GET_ADV, true, true)]
    public class CFunctionGetAdvertisement : CServerFunction
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
        /// PARAM_GET_CHILDREN - получать ли данные о дочерних объектах
        /// PARAM_GET_RECURSIVE - получать ли данные о дочерних объектах рекурсивно
        /// PARAM_GET_SOURCE_LINKS - получать ли ссылки, в которых объект является источником
        /// PARAM_GET_DEST_LINKS - получать ли ссылки, в которых объект является связанным
        /// PARAM_GET_ATTRIBUTES - получать ли атрибуты объекта
        /// PARAM_GET_MEDIA - получать ли медиа ресурсы, связанные с объектом
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
            bool GetChildren = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_GET_CHILDREN) ? (bool)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_GET_CHILDREN] : false);
            bool GetRecursive = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_GET_RECURSIVE) ? (bool)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_GET_RECURSIVE] : false);
            bool GetSrcLinks = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_GET_SOURCE_LINKS) ? (bool)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_GET_SOURCE_LINKS] : false);
            bool GetDestLinks = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_GET_DEST_LINKS) ? (bool)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_GET_DEST_LINKS] : false);
            bool GetAttributes = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_GET_ATTRIBUTES) ? (bool)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_GET_ATTRIBUTES] : false);
            bool GetMedia = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_GET_MEDIA) ? (bool)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_GET_MEDIA] : false);

            if (ObjectID == "" && ObjectKey == -1)
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_KEY_ID_NULL", ClientCulture));
            if (!CFunctionExecutionEnvironment.CurrentUser.PolicyAllowViewObjectsList())
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_UNABLE_TO_VIEW_OBJECT", ClientCulture));

            CAdvertisement Advertisement = new CAdvertisement(CFunctionExecutionEnvironment.sGetCurrentProvider());
            if (ObjectKey != -1)
            {
                Advertisement.Key = ObjectKey;
                Advertisement.ObjectGetByKey(CFunctionExecutionEnvironment.sGetCurrentProvider());
                if (Advertisement.ID == Guid.Empty)
                    return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_ADV_NULL", ClientCulture));
            }
            if (ObjectID != "")
            {
                Advertisement.ID = Guid.Parse(ObjectID);
                Advertisement.ObjectGetByID(CFunctionExecutionEnvironment.sGetCurrentProvider());
                if (Advertisement.Key == -1)
                    return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_ADV_NULL", ClientCulture));
            }

            var Rights = CFunctionExecutionEnvironment.CurrentUser.GetRightsForMetaobject(Advertisement);
            if (Rights == 0 ||
                Advertisement.Status == EnMetaobjectStatus.EBanned ||
                Advertisement.Status == EnMetaobjectStatus.EDisabled)
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_HASNT_ACCESS_TO_OBJECT", ClientCulture));

            Advertisement.ObjectGetByKey(CFunctionExecutionEnvironment.sGetCurrentProvider());
            if (GetChildren)
            {
                Advertisement.GetChildren(CFunctionExecutionEnvironment.sGetCurrentProvider(), GetRecursive);
                Advertisement.Children = CMetaobjectHelper.sFilterObjectsByUser(Advertisement.Children, CFunctionExecutionEnvironment.CurrentUser);
            }
            if (GetSrcLinks)
            {
                Advertisement.GetExternalLinks(CFunctionExecutionEnvironment.sGetCurrentProvider());
                Advertisement.ExternalLinks = CMetaobjectHelper.sFilterSourceLinksByUser(Advertisement.InternalLinks, CFunctionExecutionEnvironment.CurrentUser);
            }
            if (GetDestLinks)
            {
                Advertisement.GetInternalLinks(CFunctionExecutionEnvironment.sGetCurrentProvider());
                Advertisement.ExternalLinks = CMetaobjectHelper.sFilterDestinationLinksByUser(Advertisement.InternalLinks, CFunctionExecutionEnvironment.CurrentUser);
            }
            if (GetAttributes)
                Advertisement.Attributes.AttributesGet(CFunctionExecutionEnvironment.sGetCurrentProvider());
            if (GetMedia)
            {
                Advertisement.GetMediaResources(CFunctionExecutionEnvironment.sGetCurrentProvider());
                Advertisement.MediaResources = CMetaobjectHelper.sFilterMediaByUser(Advertisement.MediaResources, CFunctionExecutionEnvironment.CurrentUser);
            }

            Result.ErrorCode = -1;
            Result.ResultType = EnFunctionResultType.ESuccess;
            Result.InputParameters = Parameters;
            Result.Message = CGlobalizationHelper.sGetStringResource("ERROR_MSG_SUCCESS", ClientCulture);
            Result.Content = Advertisement;

            return Result;
        }
        #endregion
    }
}
