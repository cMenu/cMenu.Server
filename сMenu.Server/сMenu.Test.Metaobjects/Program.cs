﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

using cMenu.Common;
using cMenu.Common.Base;
using cMenu.DB;
using cMenu.DB.Providers;
using cMenu.IO;
using cMenu.Security.Linq;
using cMenu.Security.Linq.UsersManagement;
using cMenu.Metaobjects.Linq;
using cMenu.Security.Linq;
using cMenu.Metaobjects.Extended.Linq;
using cMenu.Metaobjects.Extended.Linq.Order;

namespace cMenu.Test.Metaobjects
{
    class Program
    {
        ///  HOME
        /// static IDatabaseProvider Provider = new CMSSQLProvider() { ConnectionString = "Data Source=N_F_HOME\\SQLEXPRESS;Initial Catalog=DB_CMENU;User Id=sa;Password=Qwerty1;" };
        /// WORK
        static IDatabaseProvider Provider = new CMSSQLProvider() { ConnectionString = "Data Source=.\\;Initial Catalog=DB_CMENU;User Id=sa;Password=Qwerty1;" };

        #region OLD
        /*public static int CreateSecurityObjects()
        {
            CFolder SecurityRoot = new CFolder(Provider)
            {
                ShortDescription = "Корень репозитория безопасности",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_SHADOW_ROOT_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_SHADOW_ROOT_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Репозиторий безопаности",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_FAKE_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true                
            };
            CFolder PolicyFolder = new CFolder(Provider)
            {
                ShortDescription = "Папка, содержащая политики безопасности системы",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Политики безопасности",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_SHADOW_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            CFolder UsersFolder = new CFolder(Provider)
            {
                ShortDescription = "Папка, группы и пользователей",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_USERS_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_USERS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Группы и пользователи",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_SHADOW_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };

            var R = SecurityRoot.ObjectInsert(Provider);
            R = PolicyFolder.ObjectInsert(Provider);
            R = UsersFolder.ObjectInsert(Provider);

            CSystemUserGroup AdminGroup = new CSystemUserGroup(Provider)
            {
                ShortDescription = "Группа администраторов",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_ID),
                Key = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Администраторы",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_USERS_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            CSystemUserGroup UsersGroup = new CSystemUserGroup(Provider)
            {
                ShortDescription = "Группа пользователей",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_ID),
                Key = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Пользователи",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_USERS_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            CSystemUser AdminUser = new CSystemUser(Provider)
            {
                ShortDescription = "Главный администратор системы",
                Email = "N_F@pisem.net",
                EnterpriseType = -1,
                FirstName = "Валентин",
                HomePhone = "",
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_USER_ADMINISTRATOR_ID),
                Key = CEmbeddedSecurityConsts.CONST_USER_ADMINISTRATOR_KEY,
                Login = "Admin",
                MobilePhone = "",
                ModificatonDate = DateTime.Now,
                Name = "Администратор",
                Parent = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY,
                Passhash = CSecurityHelper.sGeneratePasshash("Admin", "q"),
                SecondName = "Сергеевич",
                Status = EnMetaobjectStatus.EEnabled,
                Surname = "Никонов",
                System = true,
                WorkPhone = ""                
            };
            CSystemUser SimpleUser = new CSystemUser(Provider)
            {
                ShortDescription = "Тестовый пользователь",
                Email = "N_F@pisem.net",
                EnterpriseType = -1,
                FirstName = "Василий",
                HomePhone = "",
                ID = Guid.NewGuid(),
                Login = "User",
                MobilePhone = "",
                ModificatonDate = DateTime.Now,
                Name = "Пользователь",
                Parent = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY,
                Passhash = CSecurityHelper.sGeneratePasshash("User", "q"),
                SecondName = "Васильевич",
                Status = EnMetaobjectStatus.EEnabled,
                Surname = "Пупкин",
                System = true,
                WorkPhone = ""
            };

            R = AdminGroup.ObjectInsert(Provider);
            R = UsersGroup.ObjectInsert(Provider);
            R = AdminUser.ObjectInsert(Provider);
            R = SimpleUser.ObjectInsert(Provider);

            return -1;
        }
        public static int CreatePublicObjects()
        {
            CFolder PublicRoot = new CFolder(Provider)
            {
                ShortDescription = "Корень репозитория",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Репозиторий",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_FAKE_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            CFolder ServicesFolder = new CFolder(Provider)
            {
                ShortDescription = "Папка, содержащая список услуг и товаров заведения",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_SERVICES_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_SERVICES_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Услуги и товары",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            CFolder DevicesFolder = new CFolder(Provider)
            {
                ShortDescription = "Папка, содержащая устройства",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_DEVICES_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_DEVICES_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Устройства",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            CFolder MenusFolder = new CFolder(Provider)
            {
                ShortDescription = "Папка, содержащая меню заведения",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_MENUS_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_MENUS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Меню",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            CFolder MediaFolder = new CFolder(Provider)
            {
                ShortDescription = "Папка, содержащая медиа контент",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_MEDIA_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_MEDIA_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Медиа",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            CFolder AdvFolder = new CFolder(Provider)
            {
                ShortDescription = "Папка, содержащая рекламу",
                ID = Guid.Parse(CEmbeddedObjectsConsts.CONST_FOLDER_ADVER_ID),
                Key = CEmbeddedObjectsConsts.CONST_FOLDER_ADVER_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Реклама",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };

            var R = PublicRoot.ObjectInsert(Provider);
            R = ServicesFolder.ObjectInsert(Provider);
            R = DevicesFolder.ObjectInsert(Provider);
            R = MenusFolder.ObjectInsert(Provider);
            R = MediaFolder.ObjectInsert(Provider);
            R = AdvFolder.ObjectInsert(Provider);

            return -1;
        }
        public static int CreatePolicies()
        {
            CSystemPolicy Policy = new CSystemPolicy(Provider)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_USERS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_USERS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Редактирование списка пользователей",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_USERS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            var R = Policy.ObjectInsert(Provider);

            Policy = new CSystemPolicy(Provider)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_USERS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_USERS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Просмотр списка пользователей",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_USERS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(Provider);

            Policy = new CSystemPolicy(Provider)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_DEVICES_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_DEVICES_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Редактирование списка устройств",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_DEVICES_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(Provider);

            Policy = new CSystemPolicy(Provider)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_DEVICES_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_DEVICES_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Просмотр списка устройств",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_DEVICES_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(Provider);

            Policy = new CSystemPolicy(Provider)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_BIND_DEVICES_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_BIND_DEVICES_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Привязка (аутентификация) устройств",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_BIND_DEVICES_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(Provider);

            Policy = new CSystemPolicy(Provider)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Редактирование списка сессий",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(Provider);

            Policy = new CSystemPolicy(Provider)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Редактирование списка сессий",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(Provider);

            Policy = new CSystemPolicy(Provider)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_SESSIONS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_SESSIONS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Просмотр списка сессий",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_SESSIONS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(Provider);

            Policy = new CSystemPolicy(Provider)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_OBJECTS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_OBJECTS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Редактирование списка объектов",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_OBJECTS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(Provider);

            Policy = new CSystemPolicy(Provider)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_OBJECTS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_OBJECTS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Просмотр списка объектов",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_OBJECTS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(Provider);

            Policy = new CSystemPolicy(Provider)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_ORDERS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_ORDERS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Просмотр списка заказов",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_ORDERS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(Provider);

            Policy = new CSystemPolicy(Provider)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_ORDERS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_ORDERS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Редактирование списка заказов",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_ORDERS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(Provider);

            Policy = new CSystemPolicy(Provider)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_COMMENTS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_COMMENTS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Просмотр комментариев",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_VIEW_COMMENTS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(Provider);

            Policy = new CSystemPolicy(Provider)
            {
                ID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_COMMENTS_ID),
                Key = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_COMMENTS_KEY,
                ModificatonDate = DateTime.Now,
                Name = "Редактирование комментариев",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_POLICIES_KEY,
                PolicyID = Guid.Parse(CEmbeddedSecurityConsts.CONST_POLICY_EDIT_COMMENTS_ID),
                Status = EnMetaobjectStatus.EEnabled,
                System = true,
                Type = EnSystemPolicyType.ESystemDefined
            };
            R = Policy.ObjectInsert(Provider);

            return -1;
        }
        public static int BindPoliciesToAdmins()
        {
            CMetaobjectLink PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_USERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_USERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_DEVICES_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_DEVICES_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_BIND_DEVICES_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_SESSIONS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_OBJECTS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_OBJECTS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_ORDERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_ORDERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_COMMENTS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_COMMENTS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY;
            PolicyLink.LinkInsert(Provider);


            return -1;
        }
        public static int BindPoliciesToUsers()
        {
            CMetaobjectLink PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_DEVICES_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_DEVICES_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_BIND_DEVICES_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_SESSIONS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_SESSIONS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_OBJECTS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_VIEW_ORDERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_USERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY;
            PolicyLink.LinkInsert(Provider);

            PolicyLink = new CMetaobjectLink();
            PolicyLink.LinkedObjectKey = CEmbeddedSecurityConsts.CONST_POLICY_EDIT_USERS_KEY;
            PolicyLink.LinkType = EnMetaobjectLinkType.ESecurity;
            PolicyLink.LinkValue = 1;
            PolicyLink.SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY;
            PolicyLink.LinkInsert(Provider);


            return -1;
        }
        public static int BindObjectRights()
        {
            CMetaobjectSecurityRecord R = new CMetaobjectSecurityRecord()
            {
                MetaobjectKey = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Rights = 1,
                UserKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY
            };
            R.RecordInsert(Provider);

            R = new CMetaobjectSecurityRecord()
            {
                MetaobjectKey = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Rights = 1,
                UserKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY
            };
            R.RecordInsert(Provider);

            /// 
            R = new CMetaobjectSecurityRecord()
            {
                MetaobjectKey = CEmbeddedObjectsConsts.CONST_FOLDER_SERVICES_KEY,
                Rights = 1,
                UserKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY
            };
            R.RecordInsert(Provider);

            R = new CMetaobjectSecurityRecord()
            {
                MetaobjectKey = CEmbeddedObjectsConsts.CONST_FOLDER_SERVICES_KEY,
                Rights = 1,
                UserKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY
            };
            R.RecordInsert(Provider);

            ///
            R = new CMetaobjectSecurityRecord()
            {
                MetaobjectKey = CEmbeddedObjectsConsts.CONST_FOLDER_DEVICES_KEY,
                Rights = 1,
                UserKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY
            };
            R.RecordInsert(Provider);

            R = new CMetaobjectSecurityRecord()
            {
                MetaobjectKey = CEmbeddedObjectsConsts.CONST_FOLDER_DEVICES_KEY,
                Rights = 1,
                UserKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY
            };
            R.RecordInsert(Provider);

            ///
            R = new CMetaobjectSecurityRecord()
            {
                MetaobjectKey = CEmbeddedObjectsConsts.CONST_FOLDER_MENUS_KEY,
                Rights = 1,
                UserKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY
            };
            R.RecordInsert(Provider);

            R = new CMetaobjectSecurityRecord()
            {
                MetaobjectKey = CEmbeddedObjectsConsts.CONST_FOLDER_MENUS_KEY,
                Rights = 1,
                UserKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY
            };
            R.RecordInsert(Provider);

            ///
            R = new CMetaobjectSecurityRecord()
            {
                MetaobjectKey = CEmbeddedObjectsConsts.CONST_FOLDER_MEDIA_KEY,
                Rights = 1,
                UserKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY
            };
            R.RecordInsert(Provider);

            R = new CMetaobjectSecurityRecord()
            {
                MetaobjectKey = CEmbeddedObjectsConsts.CONST_FOLDER_MEDIA_KEY,
                Rights = 1,
                UserKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY
            };
            R.RecordInsert(Provider);

            ///
            R = new CMetaobjectSecurityRecord()
            {
                MetaobjectKey = CEmbeddedObjectsConsts.CONST_FOLDER_ADVER_KEY,
                Rights = 1,
                UserKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY
            };
            R.RecordInsert(Provider);

            R = new CMetaobjectSecurityRecord()
            {
                MetaobjectKey = CEmbeddedObjectsConsts.CONST_FOLDER_ADVER_KEY,
                Rights = 1,
                UserKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY
            };
            R.RecordInsert(Provider);

            return -1;
        }
        public static int PutUsersIntoGroups()
        {
            CMetaobjectLink L = new CMetaobjectLink()
            {
                LinkedObjectKey = 423,
                LinkType = EnMetaobjectLinkType.ESecurity,
                LinkValue = 1,
                SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_USERS_KEY
            };
            L.LinkInsert(Provider);

            L = new CMetaobjectLink()
            {
                LinkedObjectKey = 
                CEmbeddedSecurityConsts.CONST_USER_ADMINISTRATOR_KEY,
                LinkType = EnMetaobjectLinkType.ESecurity,
                LinkValue = 1,
                SourceObjectKey = CEmbeddedSecurityConsts.CONST_USER_GROUP_ADMINISTRATORS_KEY
            };
            L.LinkInsert(Provider);

            return -1;
        }
        public static int TestObjects1()
        {
            CFolder F = new CFolder(Provider)
            {
                ShortDescription = "Тестовый объект 2",
                ID = Guid.NewGuid(),
                ModificatonDate = DateTime.Now,
                Name = "Папка 2",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_PUBLIC_ROOT_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                System = false
            };
            F.ObjectInsert(Provider);

            CMetaobjectSecurityRecord R = new CMetaobjectSecurityRecord();
            R.MetaobjectKey = F.Key;
            R.Rights = 3;

            R.UserKey = CEmbeddedSecurityConsts.CONST_USER_ADMINISTRATOR_KEY;
            R.RecordInsert(Provider);
            R.UserKey = 423;
            R.RecordInsert(Provider);
            return -1;
        }*/
        /*public static int TestObjects2()
        {
            CMenuService Service = new CMenuService(Provider)
            {
                Composition = "Состав",
                Description = "Тестовый товар 1",
                ID = Guid.NewGuid(),
                ModificatonDate = DateTime.Now,
                Name = "Товар 1",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_SERVICES_KEY,
                Rating = 0,
                Status = EnMetaobjectStatus.EEnabled                
            };
            var R = Service.ObjectInsert(Provider);
            var Groups = CSystemUserGroup.sGetAllGroups(Provider);
            foreach (CSystemUserGroup G in Groups)
            {
                CMetaobjectSecurityRecord Rec = new CMetaobjectSecurityRecord()
                {
                    MetaobjectKey = Service.Key,
                    Rights = 3,
                    UserKey = G.Key
                };
                Rec.RecordInsert(Provider);
            }

            CMenuServiceAmount Amount = new CMenuServiceAmount(Provider)
            {
                Description = "Объем",
                ID = Guid.NewGuid(),
                ModificatonDate = DateTime.Now,
                Name = "Объем",
                Parent = Service.Key,
                Status = EnMetaobjectStatus.EEnabled
            };
            R = Amount.ObjectInsert(Provider);
            foreach (CSystemUserGroup G in Groups)
            {
                CMetaobjectSecurityRecord Rec = new CMetaobjectSecurityRecord()
                {
                    MetaobjectKey = Amount.Key,
                    Rights = 3,
                    UserKey = G.Key
                };
                Rec.RecordInsert(Provider);
            }

            Service = new CMenuService(Provider)
            {
                Composition = "Состав",
                Description = "Тестовый товар 2",
                ID = Guid.NewGuid(),
                ModificatonDate = DateTime.Now,
                Name = "Товар 2",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_SERVICES_KEY,
                Rating = 0,
                Status = EnMetaobjectStatus.EEnabled
            };
            R = Service.ObjectInsert(Provider);
            foreach (CSystemUserGroup G in Groups)
            {
                CMetaobjectSecurityRecord Rec = new CMetaobjectSecurityRecord()
                {
                    MetaobjectKey = Service.Key,
                    Rights = 3,
                    UserKey = G.Key
                };
                Rec.RecordInsert(Provider);
            }

            Amount = new CMenuServiceAmount(Provider)
            {
                Description = "Объем",
                ID = Guid.NewGuid(),
                ModificatonDate = DateTime.Now,
                Name = "Объем",
                Parent = Service.Key,
                Status = EnMetaobjectStatus.EEnabled
            };
            R = Amount.ObjectInsert(Provider);
            foreach (CSystemUserGroup G in Groups)
            {
                CMetaobjectSecurityRecord Rec = new CMetaobjectSecurityRecord()
                {
                    MetaobjectKey = Amount.Key,
                    Rights = 3,
                    UserKey = G.Key
                };
                Rec.RecordInsert(Provider);
            }

            var Groups = CSystemUserGroup.sGetAllGroups(Provider);
            COrganization Org = new COrganization(Provider)
            {
                ShortDescription = "Org",
                ID = Guid.NewGuid(),
                Name = "Организация",
                Parent = CEmbeddedObjectsConsts.CONST_FOLDER_MENUS_KEY,
                Status = EnMetaobjectStatus.EEnabled,
                ModificatonDate = DateTime.Now
            };
            Org.ObjectInsert(Provider);
            foreach (CSystemUserGroup G in Groups)
            {
                CMetaobjectSecurityRecord Rec = new CMetaobjectSecurityRecord()
                {
                    MetaobjectKey = Org.Key,
                    Rights = 3,
                    UserKey = G.Key
                };
                Rec.RecordInsert(Provider);
            }

            CCategory Cat = new CCategory(Provider)
            {
                ShortDescription = "Категория",
                ID = Guid.NewGuid(),
                Name = "Категория",
                Parent = Org.Key,
                Status = EnMetaobjectStatus.EEnabled,
                ModificatonDate = DateTime.Now
            };
            Cat.ObjectInsert(Provider);
            foreach (CSystemUserGroup G in Groups)
            {
                CMetaobjectSecurityRecord Rec = new CMetaobjectSecurityRecord()
                {
                    MetaobjectKey = Cat.Key,
                    Rights = 3,
                    UserKey = G.Key
                };
                Rec.RecordInsert(Provider);
            }

            CMetaobjectShortcut Shortcut = new CMetaobjectShortcut(Provider)
            {
                ShortDescription = "Ссылка на товар 2",
                ID = Guid.NewGuid(),
                Name = "Ссылка на товар 2",
                Parent = Cat.Key,
                Status = EnMetaobjectStatus.EEnabled,
                ModificatonDate = DateTime.Now,
                SourceObjectKey = 453
            };
            Shortcut.ObjectInsert(Provider);
            foreach (CSystemUserGroup G in Groups)
            {
                CMetaobjectSecurityRecord Rec = new CMetaobjectSecurityRecord()
                {
                    MetaobjectKey = Shortcut.Key,
                    Rights = 3,
                    UserKey = G.Key
                };
                Rec.RecordInsert(Provider);
            }

            return -1;
        }*/

        /*public static int TestExceptions()
        {
            try
            {
                Provider.TestConnection();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            return -1;
        }*/
        /*public static int TestRDS()
        {
            CRdsDictionary Dict = new CRdsDictionary(Provider)
            {
                Description = "DEST",
                ID = Guid.NewGuid(),
                ModificatonDate = DateTime.Now,
                Name = "Dict",
                Parent = -2,
                Status = EnMetaobjectStatus.EEnabled,
                System = false                
            };
            var R = Dict.ObjectInsert(Provider);

            CRdsAttribute NameAttr = new CRdsAttribute()
            {
                DictionaryKey = Dict.Key,
                ID = Guid.NewGuid(),
                IsHidden = false,
                Name = "NAME",
                Type = System.Data.DbType.String
            };
            R = NameAttr.AttributeInsert(Provider);

            CRdsElement Element = new CRdsElement(Provider)
            {
                DictionaryKey = Dict.Key,
                Parent = -2,
                Provider = Provider
            };
            Element[NameAttr.Key, -1] = "Name!";
            R = Element.ElementInsert(Provider);

            CRdsDictionary Dict = new CRdsDictionary(3, Provider);
            Dict.GetElements(Provider);
            Dict.GetAttributes(Provider);
            var Attr = Dict.DictionaryAttributes.FindByName("NAME");
            var Value = Dict.DictionaryElements[0][Attr.Key, -1];
            Dict.DictionaryElements[0][Attr.Key, -1] = "111";
            Dict.DictionaryElements[0][Attr.Key, -2] = "111333";
            Dict.DictionaryElements[0].ElementUpdate(Provider);

            return -1;
        }*/
        /*public static int TestGEO()
        {
            CCountry Ctr = new CCountry(Provider)
            {
                FullDescription = "Full",
                ID = Guid.NewGuid(),
                LikesNumber = 100500,
                ModificatonDate = DateTime.Now,
                Name = "Russia",
                Parent = -2,
                ShortDescription = "Short",
                Status = EnMetaobjectStatus.EEnabled,
                System = true
            };
            var R = Ctr.ObjectInsert(Provider);

            Ctr = new CCountry(Ctr.Key, Provider);
            return -1;
        }*/
        #endregion

        public static int TestMetaobjects()
        {
            CMetaobjectExtented C = new CMetaobjectExtented();

            var Asm = Assembly.LoadWithPartialName("cMenu.Resources");
            var S = Asm.GetManifestResourceStream("cMenu.Resources.LinqMapping.xml");
            TextReader R = new StreamReader(S);
            var XML = R.ReadToEnd();
            XmlMappingSource mapping = XmlMappingSource.FromXml(XML);
            DataContext DB = new DataContext("Data Source=.\\;Initial Catalog=DB_CMENU;User Id=sa;Password=Qwerty1;", mapping);

            CMetaobject Root = new CSecuredMetaobject(DB)
            {
                FullDescription = "Root",
                ID = Guid.NewGuid(),
                Key = -2,
                ModificatonDate = DateTime.Now,
                Name = "Root",
                Parent = -3,
                ShortDescription = "Root",
                Status = cMenu.Metaobjects.EnMetaobjectStatus.EEnabled,
                System = true
            };

            (Root as CMetaobject).ObjectInsert(DB);

            DB.SubmitChanges();

            return -1;
        }
        public static int TestGetMetaobjects()
        {
            CMetaobjectExtented C = new CMetaobjectExtented();

            var Asm = Assembly.LoadWithPartialName("cMenu.Resources");
            var S = Asm.GetManifestResourceStream("cMenu.Resources.LinqMapping.xml");
            TextReader R = new StreamReader(S);
            var XML = R.ReadToEnd();
            XmlMappingSource mapping = XmlMappingSource.FromXml(XML);
            DataContext DB = new DataContext("Data Source=.\\;Initial Catalog=DB_CMENU;User Id=sa;Password=Qwerty1;", mapping);

            CSystemUser User = new CSystemUser(5, DB);

            return -1;
        }

        static void Main(string[] args)
        {
            TestGetMetaobjects();
        }
    }
}