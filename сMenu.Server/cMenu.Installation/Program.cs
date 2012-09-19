using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using cMenu.Common;
using cMenu.DB;
using cMenu.DB.Providers;
using cMenu.Metaobjects;
using cMenu.Metaobjects.Linq;
using cMenu.Metaobjects.Extended.Linq;
using cMenu.Metaobjects.Extended.Linq.Geography;
using cMenu.Metaobjects.Extended.Linq.Menu;
using cMenu.Security;
using cMenu.Security.Linq;
using cMenu.Security.Linq.PoliciesManagement;
using cMenu.Security.Linq.UsersManagement;

namespace cMenu.Installation
{
    class Program
    {
        ///  HOME
        /// static IDatabaseProvider Provider = new CMSSQLProvider() { ConnectionString = "Data Source=N_F_HOME\\SQLEXPRESS;Initial Catalog=DB_CMENU;User Id=sa;Password=Qwerty1;" };
        
        /// WORK
        static IDatabaseProvider Provider = new CMSSQLProvider() { ConnectionString = "Data Source=.\\;Initial Catalog=DB_CMENU;User Id=sa;Password=Qwerty1;" };
        static DataContext DatabaseContext = null;

        public static int InitializeDatabaseContext()
        {
            cMenu.Metaobjects.Linq.CMetaobject M = new cMenu.Metaobjects.Linq.CMetaobject();
            cMenu.Security.Linq.CSecuredMetaobject SM = new cMenu.Security.Linq.CSecuredMetaobject();
            cMenu.Metaobjects.Extended.Linq.CMetaobjectExtented ME = new cMenu.Metaobjects.Extended.Linq.CMetaobjectExtented();
 
            var Asm = Assembly.LoadWithPartialName("cMenu.Resources");
            var S = Asm.GetManifestResourceStream("cMenu.Resources.LinqMapping.xml");
            TextReader R = new StreamReader(S);
            var XML = R.ReadToEnd();
            XmlMappingSource mapping = XmlMappingSource.FromXml(XML);
            DatabaseContext = new DataContext("Data Source=.\\;Initial Catalog=DB_CMENU;User Id=sa;Password=Qwerty1;", mapping);

            return CErrors.ERR_SUC;
        }
        public static int CreateSystemFolders()
        {
            CFolder PublicRoot = new CFolder(DatabaseContext)
            {
                FullDescription = "Корень репозитория",
                ShortDescription = "Корень репозитория",
                Name = "Репозиторий",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_FAKE_ROOT_KEY,                
                Status = EnMetaobjectStatus.EEnabled,
                System = true                
            };
            var R = PublicRoot.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CFolder UsersFolder = new CFolder(DatabaseContext)
            {
                FullDescription = "Группы и пользователи",
                ShortDescription = "Группы и пользователи",
                Name = "Группы и пользователи",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_USERS_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_USERS_KEY,
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,                
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            R = UsersFolder.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CFolder UsersOnlyFolder = new CFolder(DatabaseContext)
            {
                FullDescription = "Пользователи",
                ShortDescription = "Пользователи",
                Name = "Пользователи",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_USERS_ONLY_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_USERS_ONLY_KEY,
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_USERS_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            R = UsersOnlyFolder.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CFolder GroupsOnlyFolder = new CFolder(DatabaseContext)
            {
                FullDescription = "Группы",
                ShortDescription = "Группы",
                Name = "Группы",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_GROUPS_ONLY_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_GROUPS_ONLY_KEY,
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_USERS_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            R = GroupsOnlyFolder.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CFolder PoliciesFolder = new CFolder(DatabaseContext)
            {
                FullDescription = "Папка, содержащая политики безопасности системы",
                ShortDescription = "Папка, содержащая политики безопасности системы",
                Name = "Политики безопасности",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            R = PoliciesFolder.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CFolder ApplicationsFolder = new CFolder(DatabaseContext)
            {
                FullDescription = "Папка, содержащая описания приложений",
                ShortDescription = "Папка, содержащая описания приложений",
                Name = "Приложения",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_APPLICATIONS_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_APPLICATIONS_KEY,
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            R = ApplicationsFolder.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CFolder CitiesFolder = new CFolder(DatabaseContext)
            {
                FullDescription = "Папка, содержащая иерархию регионов и городов",
                ShortDescription = "Папка, содержащая иерархию регионов и городов",
                Name = "Регионы",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_REGIONS_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_REGIONS_KEY,
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            R = CitiesFolder.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CFolder MenuFolder = new CFolder(DatabaseContext)
            {
                FullDescription = "Папка, содержащая иерархию меню заведений",
                ShortDescription = "Папка, содержащая иерархию меню заведений",
                Name = "Меню",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_MENUS_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_MENUS_KEY,
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            R = MenuFolder.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CFolder ServicesFolder = new CFolder(DatabaseContext)
            {
                FullDescription = "Папка, содержащая список товаров и услуг",
                ShortDescription = "Папка, содержащая список товаров и услуг",
                Name = "Товары и услуги",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_SERVICES_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_SERVICES_KEY,
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            R = ServicesFolder.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CFolder MediaFolder = new CFolder(DatabaseContext)
            {
                FullDescription = "Папка, содержащая список медиа ресурсов приложений",
                ShortDescription = "Папка, содержащая список медиа ресурсов приложений",
                Name = "Медиа",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_MEDIA_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_MEDIA_KEY,
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            R = MediaFolder.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CFolder DictionariesFolder = new CFolder(DatabaseContext)
            {
                FullDescription = "Папка, содержащая список справочников приложений",
                ShortDescription = "Папка, содержащая список справочников приложений",
                Name = "Справочники",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_DICT_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_DICT_KEY,
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            R = DictionariesFolder.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CFolder AdvertisementFolder = new CFolder(DatabaseContext)
            {
                FullDescription = "Папка, содержащая рекламу приложений",
                ShortDescription = "Папка, содержащая рекламу приложений",
                Name = "Реклама",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_ADVER_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_ADVER_KEY,
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            R = AdvertisementFolder.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;
            try
            { DatabaseContext.SubmitChanges(); }
            catch (Exception Exception)
            {
                throw new Exception(Exception.Message);
                return CErrors.ERR_DB_INSERT_OBJECT;
            }

            return CErrors.ERR_SUC;
        }
        public static int CreateGroupsUsers()
        {
            CSystemUserGroup AdminGroup = new CSystemUserGroup(DatabaseContext)
            {
                FullDescription = "Группа администраторов",
                ShortDescription = "Группа администраторов",
                Name = "Администраторы",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_ID),
                Key = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY,
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_USERS_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            var R = AdminGroup.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CSystemUserGroup ModeratorsGroup = new CSystemUserGroup(DatabaseContext)
            {
                FullDescription = "Группа модераторов",
                ShortDescription = "Группа модераторов",
                Name = "Модераторы",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_USER_GROUP_MODERATORS_ID),
                Key = CEmbeddedSecurityConsts.CONST_USER_GROUP_MODERATORS_KEY,
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_USERS_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            R = AdminGroup.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CSystemUserGroup UsersGroup = new CSystemUserGroup(DatabaseContext)
            {
                FullDescription = "Группа пользователей",
                ShortDescription = "Группа пользователей",
                Name = "Пользователи",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_ID),
                Key = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY,
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_USERS_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            R = AdminGroup.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CSystemUser AdminUser = new CSystemUser(DatabaseContext)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_USER_ADMINISTRATOR_ID),
                Key = CEmbeddedSecurityConsts.CONST_USER_ADMINISTRATOR_KEY,
                ShortDescription = "Главный администратор системы",
                Email = "admin@cmenu.ru",
                EnterpriseType = -1,
                FirstName = "Administrator",
                SecondName = "",
                Surname = "",
                Login = "Administrator",
                Name = "Администратор",
                MobilePhone = "89028087122",
                HomePhone = "",
                WorkPhone = "",
                Passhash = CSecurityHelper.sGeneratePasshash("Administrator", "q"),
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY,                
                Status = EnMetaobjectStatus.EEnabled,               
                System = true                
            };
            R = AdminUser.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            cMenu.Metaobjects.Linq.CMetaobjectLink Link = new Metaobjects.Linq.CMetaobjectLink()
            {
                LinkedObjectKey = AdminUser.Key,
                LinkType = EnMetaobjectLinkType.ESecurity,
                LinkValue = 1,
                SourceObjectKey = AdminGroup.Key
            };
            R = Link.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;
            try
            { DatabaseContext.SubmitChanges(); }
            catch (Exception Exception)
            {
                throw new Exception(Exception.Message);
                return CErrors.ERR_DB_INSERT_OBJECT;
            }            

            return CErrors.ERR_SUC;
        }
        public static int CreatePolicies()
        {
            CSystemPolicy Policy = new CSystemPolicy(DatabaseContext)
            {
                ShortDescription = "Редактирование списка пользователей",
                FullDescription = "Редактирование списка пользователей",
                Name = "Редактирование списка пользователей",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_USERS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_USERS_KEY,
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,                
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_USERS_ID)
            };
            var R = Policy.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            Policy = new CSystemPolicy(DatabaseContext)
            {
                ShortDescription = "Просмотр списка пользователей",
                FullDescription = "Просмотр списка пользователей",
                Name = "Просмотр списка пользователей",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_USERS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_USERS_KEY,
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_USERS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            Policy = new CSystemPolicy(DatabaseContext)
            {
                ShortDescription = "Редактирование списка сессий",
                FullDescription = "Редактирование списка сессий",
                Name = "Редактирование списка сессий",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_KEY,
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            Policy = new CSystemPolicy(DatabaseContext)
            {
                ShortDescription = "Просмотр списка сессий",
                FullDescription = "Просмотр списка сессий",
                Name = "Просмотр списка сессий",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_SESSIONS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_SESSIONS_KEY,
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_SESSIONS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            Policy = new CSystemPolicy(DatabaseContext)
            {
                ShortDescription = "Редактирование списка сессий",
                FullDescription = "Редактирование списка сессий",
                Name = "Редактирование списка объектов",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_OBJECTS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_OBJECTS_KEY,
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_OBJECTS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            Policy = new CSystemPolicy(DatabaseContext)
            {
                ShortDescription = "Просмотр списка объектов",
                FullDescription = "Просмотр списка объектов",
                Name = "Просмотр списка объектов",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_OBJECTS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_OBJECTS_KEY,
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_OBJECTS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            Policy = new CSystemPolicy(DatabaseContext)
            {
                ShortDescription = "Просмотр списка заказов",
                FullDescription = "Просмотр списка заказов",
                Name = "Просмотр списка заказов",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_ORDERS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_ORDERS_KEY,
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_ORDERS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            Policy = new CSystemPolicy(DatabaseContext)
            {
                ShortDescription = "Редактирование списка заказов",
                FullDescription = "Редактирование списка заказов",
                Name = "Редактирование списка заказов",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_ORDERS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_ORDERS_KEY,
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_ORDERS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            Policy = new CSystemPolicy(DatabaseContext)
            {
                ShortDescription = "Просмотр комментариев",
                FullDescription = "Просмотр комментариев",
                Name = "Просмотр комментариев",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_COMMENTS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_COMMENTS_KEY,
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_COMMENTS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            Policy = new CSystemPolicy(DatabaseContext)
            {
                ShortDescription = "Редактирование комментариев",
                FullDescription = "Редактирование комментариев",
                Name = "Редактирование комментариев",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_COMMENTS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_COMMENTS_KEY,
                ModificatonDate = DateTime.Now,                
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_COMMENTS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            try
            { DatabaseContext.SubmitChanges(); }
            catch (Exception Exception)
            {
                throw new Exception(Exception.Message);
                return CErrors.ERR_DB_INSERT_OBJECT;
            }            

            return CErrors.ERR_SUC;
        }
        public static int BindPoliciesToAdministrators()
        {
            cMenu.Metaobjects.Linq.CMetaobjectLink PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_USERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            var R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_USERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_SESSIONS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_OBJECTS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_OBJECTS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_ORDERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_ORDERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_COMMENTS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_COMMENTS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            try
            { DatabaseContext.SubmitChanges(); }
            catch (Exception Exception)
            {
                throw new Exception(Exception.Message);
                return CErrors.ERR_DB_INSERT_OBJECT;
            }

            return CErrors.ERR_SUC;
        }
        public static int BindPoliciesToUsers()
        {
            cMenu.Metaobjects.Linq.CMetaobjectLink PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY;
            var R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_SESSIONS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_OBJECTS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_ORDERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_USERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            try
            { DatabaseContext.SubmitChanges(); }
            catch (Exception Exception)
            {
                throw new Exception(Exception.Message);
                return CErrors.ERR_DB_INSERT_OBJECT;
            }

            return CErrors.ERR_SUC;
        }
        public static int BindPoliciesToModerators()
        {
            cMenu.Metaobjects.Linq.CMetaobjectLink PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_USERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_MODERATORS_KEY;
            var R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_USERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_MODERATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_MODERATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_SESSIONS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_MODERATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_OBJECTS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_MODERATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_OBJECTS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_MODERATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_ORDERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_MODERATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_ORDERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_MODERATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_COMMENTS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_MODERATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            PolicyLink = new cMenu.Metaobjects.Linq.CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_COMMENTS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_MODERATORS_KEY;
            R = PolicyLink.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            try
            { DatabaseContext.SubmitChanges(); }
            catch (Exception Exception)
            {
                throw new Exception(Exception.Message);
                return CErrors.ERR_DB_INSERT_OBJECT;
            }

            return CErrors.ERR_SUC;
        }

        public static int CreateTestObjects()
        {

            #region REGIONS
            CCountry Country = new CCountry(DatabaseContext)
            {
                FullDescription = "Россия",
                ShortDescription = "Россия",
                Name = "Россия",
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(Provider),
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_REGIONS_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = false
            };

            var R = Country.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CState State = new CState(DatabaseContext)
            {
                FullDescription = "Пермский край",
                ShortDescription = "Пермский край",
                Name = "Пермский край",
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(Provider),
                ModificatonDate = DateTime.Now,
                Parent = Country.Key,
                Status = EnMetaobjectStatus.EEnabled,
                System = false                
            };

            R = State.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CCity City = new CCity(DatabaseContext)
            {
                FullDescription = "Пермь",
                ShortDescription = "Пермь",
                Name = "Пермь",
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(Provider),
                ModificatonDate = DateTime.Now,
                Parent = State.Key,
                Status = EnMetaobjectStatus.EEnabled,
                System = false,
                Coordinates = ""
            };

            R = City.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            #endregion

            #region ORGANIZATIONS
            COrganizationNetwork OrganizationNetwork = new COrganizationNetwork(DatabaseContext)
            {
                FullDescription = "Сеть организаций",
                ShortDescription = "Сеть организаций",
                Name = "Сеть организаций",
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(Provider),
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_MENUS_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = false,
                Email = "Email",
                Url = ""
            };
            R = OrganizationNetwork.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            COrganization Organization = new COrganization(DatabaseContext)
            {
                FullDescription = "Организация",
                ShortDescription = "Организация",
                Name = "Организация",
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(Provider),
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_MENUS_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = false
            };
            R = Organization.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            cMenu.Metaobjects.Linq.CMetaobjectLink Link = new Metaobjects.Linq.CMetaobjectLink()
            {
                LinkedObjectKey = Organization.Key,
                LinkType = EnMetaobjectLinkType.ESimple,
                LinkValue = 1,
                SourceObjectKey = City.Key
            };
            R = Link.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            #endregion

            #region SERVICES
            CMenuService Service1 = new CMenuService(DatabaseContext)
            {
                FullDescription = "Товар 1",
                ShortDescription = "Товар 1",
                Name = "Товар 1",
                Composition = "Состав",
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(Provider),
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_SERVICES_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = false
            };
            R = Service1.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CMenuService Service2 = new CMenuService(DatabaseContext)
            {
                FullDescription = "Товар 2",
                ShortDescription = "Товар 2",
                Name = "Товар 2",
                Composition = "Состав",
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(Provider),
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_SERVICES_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = false
            };
            R = Service2.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CMenuServiceAmount Amount1 = new CMenuServiceAmount(DatabaseContext)
            {
                FullDescription = "Порция 1.1",
                ShortDescription = "ТПорция 1.1",
                Name = "Порция 1.1",
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(Provider),
                ModificatonDate = DateTime.Now,
                Parent = Service1.Key,
                Status = EnMetaobjectStatus.EEnabled,
                System = false,
                Price = 100,
                Amount = 200,
                Units = "мг",
                TimeAmount = new TimeSpan(0, 10, 0)
            };
            R = Amount1.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CMenuServiceAmount Amount2 = new CMenuServiceAmount(DatabaseContext)
            {
                FullDescription = "Порция 2.1",
                ShortDescription = "ТПорция 2.1",
                Name = "Порция 2.1",
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(Provider),
                ModificatonDate = DateTime.Now,
                Parent = Service2.Key,
                Status = EnMetaobjectStatus.EEnabled,
                System = false,
                Price = 200,
                Amount = 300,
                Units = "г",
                TimeAmount = new TimeSpan(0, 20, 0)
            };
            R = Amount2.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;
            #endregion

            #region MENU
            CMenu Menu = new CMenu(DatabaseContext)
            {
                FullDescription = "Меню",
                ShortDescription = "Меню",
                Name = "Меню",
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(Provider),
                ModificatonDate = DateTime.Now,
                Parent = Organization.Key,
                Status = EnMetaobjectStatus.EEnabled,
                System = false,
                Primary = true
            };
            R = Menu.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CCategory Category = new CCategory()
            {
                FullDescription = "Категория",
                ShortDescription = "Категория",
                Name = "Категория",
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(Provider),
                ModificatonDate = DateTime.Now,
                Parent = Menu.Key,
                Status = EnMetaobjectStatus.EEnabled,
                System = false
            };
            R = Category.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CMetaobjectShortcut Shortcut = new CMetaobjectShortcut(DatabaseContext)
            {
                FullDescription = "Ссылка на порцию 1.1",
                ShortDescription = "Ссылка на порцию 1.1",
                Name = "Ссылка на порцию 1.1",
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(Provider),
                ModificatonDate = DateTime.Now,
                Parent = Category.Key,
                Status = EnMetaobjectStatus.EEnabled,
                System = false,
                SourceObjectKey = Amount1.Key
            };
            R = Shortcut.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            CMetaobjectShortcut Shortcut2 = new CMetaobjectShortcut(DatabaseContext)
            {
                FullDescription = "Ссылка на порцию 1.2",
                ShortDescription = "Ссылка на порцию 1.2",
                Name = "Ссылка на порцию 1.2",
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(Provider),
                ModificatonDate = DateTime.Now,
                Parent = Category.Key,
                Status = EnMetaobjectStatus.EEnabled,
                System = false,
                SourceObjectKey = Amount2.Key
            };
            R = Shortcut2.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            #endregion

            #region TABLES
            COrganizationTable Table = new COrganizationTable(DatabaseContext)
            {
                FullDescription = "Стол",
                ShortDescription = "Стол",
                Name = "Стол",
                ID = Guid.NewGuid(),
                Key = CDatabaseSequence.sGetObjectKey(Provider),
                ModificatonDate = DateTime.Now,
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_SERVICES_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = false
            };
            R = Table.ObjectInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            Link = new Metaobjects.Linq.CMetaobjectLink()
            {
                LinkedObjectKey = Table.Key,
                LinkType = EnMetaobjectLinkType.ESimple,
                LinkValue = 1,
                SourceObjectKey = Organization.Key
            };
            R = Link.LinkInsert(DatabaseContext);
            if (R != CErrors.ERR_SUC)
                return R;

            #endregion

            try
            { DatabaseContext.SubmitChanges(); }
            catch (Exception Exception)
            {
                throw new Exception(Exception.Message);
                return CErrors.ERR_DB_INSERT_OBJECT;
            }

            return CErrors.ERR_SUC;
        }


        static void Main(string[] args)
        {
            try
            {
                var R = InitializeDatabaseContext();
                if (R != CErrors.ERR_SUC)
                    return;

                R = CreateSystemFolders();
                if (R != CErrors.ERR_SUC)
                    return;

                R = CreateGroupsUsers();
                if (R != CErrors.ERR_SUC)
                    return;

                R = CreatePolicies();
                if (R != CErrors.ERR_SUC)
                    return;

                R = BindPoliciesToAdministrators();
                if (R != CErrors.ERR_SUC)
                    return;

                R = BindPoliciesToUsers();
                if (R != CErrors.ERR_SUC)
                    return;

                R = BindPoliciesToModerators();
                if (R != CErrors.ERR_SUC)
                    return;

                R = CreateTestObjects();
                if (R != CErrors.ERR_SUC)
                    return;
            }
            catch (Exception Exception)
            { Console.WriteLine(Exception.Message); }            
        }
    }
}
