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
using cMenu.Globalization;

namespace cMenu.Communication.Server.Functions
{
    [CMetaFunctionAttribute(CServerFunctionID.CONST_FUNC_ID_GET_MENU, true, true)]
    public class CFunctionGetCurrentMenuData : CServerFunction
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

            bool GetChildren = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_GET_CHILDREN) ? (bool)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_GET_CHILDREN] : false);
            bool GetRecursive = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_GET_RECURSIVE) ? (bool)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_GET_RECURSIVE] : false);
            bool GetSrcLinks = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_GET_SOURCE_LINKS) ? (bool)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_GET_SOURCE_LINKS] : false);
            bool GetDestLinks = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_GET_DEST_LINKS) ? (bool)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_GET_DEST_LINKS] : false);
            bool GetAttributes = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_GET_ATTRIBUTES) ? (bool)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_GET_ATTRIBUTES] : false);
            bool GetMedia = (Parameters.ContainsKey(CServerFunctionParams.CONST_FUNC_PARAM_GET_MEDIA) ? (bool)Parameters[CServerFunctionParams.CONST_FUNC_PARAM_GET_MEDIA] : false);

            if (!CFunctionExecutionEnvironment.CurrentUser.PolicyAllowViewObjectsList())
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_UNABLE_TO_VIEW_OBJECT", ClientCulture));

            List<CMetaobject> Menus = CMetaobject.sGetObjectsByClass(EnMetaobjectClass.EMenu, CFunctionExecutionEnvironment.sGetCurrentProvider());
            if (Menus.Count == 0)
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_NO_MENUS", ClientCulture));

            var PrimaryMenus = Menus.Where(M => (M as CMenu).Primary).ToList();
            if (PrimaryMenus.Count == 0)
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_NO_PRIMARY_MENU", ClientCulture));

            var PrimaryMenu = Menus[0] as CMenu;
            var Rights = CFunctionExecutionEnvironment.CurrentUser.GetRightsForMetaobject(PrimaryMenu);

            if (Rights == 0||
                PrimaryMenu.Status == EnMetaobjectStatus.EBanned ||
                PrimaryMenu.Status == EnMetaobjectStatus.EDisabled)
                return this._compileResult(EnFunctionResultType.EError, Parameters, CGlobalizationHelper.sGetStringResource("ERROR_MSG_USER_HASNT_ACCESS_TO_OBJECT", ClientCulture));

            PrimaryMenu.ObjectGetByKey(CFunctionExecutionEnvironment.sGetCurrentProvider());
            if (GetChildren)
            {
                PrimaryMenu.GetChildren(CFunctionExecutionEnvironment.sGetCurrentProvider(), GetRecursive);
                PrimaryMenu.Children = CMetaobjectHelper.sFilterObjectsByUser(PrimaryMenu.Children, CFunctionExecutionEnvironment.CurrentUser);
            }
            if (GetSrcLinks)
            {
                PrimaryMenu.GetExternalLinks(CFunctionExecutionEnvironment.sGetCurrentProvider());
                PrimaryMenu.ExternalLinks = CMetaobjectHelper.sFilterSourceLinksByUser(PrimaryMenu.InternalLinks, CFunctionExecutionEnvironment.CurrentUser);
            }
            if (GetDestLinks)
            {
                PrimaryMenu.GetInternalLinks(CFunctionExecutionEnvironment.sGetCurrentProvider());
                PrimaryMenu.ExternalLinks = CMetaobjectHelper.sFilterDestinationLinksByUser(PrimaryMenu.InternalLinks, CFunctionExecutionEnvironment.CurrentUser);
            }
            if (GetAttributes)
                PrimaryMenu.Attributes.AttributesGet(CFunctionExecutionEnvironment.sGetCurrentProvider());
            if (GetMedia)
            {
                PrimaryMenu.GetMediaResources(CFunctionExecutionEnvironment.sGetCurrentProvider());
                PrimaryMenu.MediaResources = CMetaobjectHelper.sFilterMediaByUser(PrimaryMenu.MediaResources, CFunctionExecutionEnvironment.CurrentUser);
            }

            Result.ErrorCode = -1;
            Result.ResultType = EnFunctionResultType.ESuccess;
            Result.InputParameters = Parameters;
            Result.Message = CGlobalizationHelper.sGetStringResource("ERROR_MSG_SUCCESS", ClientCulture);
            Result.Content = PrimaryMenu;

            return Result;
        }
        #endregion
    }
}
