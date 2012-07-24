using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

using cMenu.Common;

namespace cMenu.IO
{
    public class CRegistry
    {
        public static int sDeleteFolder(string Path)
        {
            RegistryKey PathType = Microsoft.Win32.Registry.LocalMachine;
            PathType.DeleteSubKey(Path);

            return CErrors.ERR_SUC;
        }
        public static int sDeleteKey(string Path)
        {
            RegistryKey PathType = Microsoft.Win32.Registry.LocalMachine;
            PathType.DeleteValue(Path);

            return CErrors.ERR_SUC;
        }
        public static int sCreateFolder(string RootPath, string FolderName)
        {
            RegistryKey PathType = Microsoft.Win32.Registry.LocalMachine;
            RegistryKey RKey = PathType.CreateSubKey(RootPath + "\\" + FolderName, RegistryKeyPermissionCheck.ReadWriteSubTree);

            return CErrors.ERR_SUC;
        }
        public static int sCreateKey(string RootPath, string KeyName)
        {
            RegistryKey PathType = Microsoft.Win32.Registry.LocalMachine;
            RegistryKey RKey = PathType.CreateSubKey(RootPath + "\\" + KeyName);

            return CErrors.ERR_SUC;
        }
        public static bool sPathExists(string Path)
        {
            try
            {
                RegistryKey PathType = null;
                PathType = Microsoft.Win32.Registry.LocalMachine;
                RegistryKey RKey = PathType.OpenSubKey(Path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Получение пути в реестре по идентификатору модуля
        /// </summary>
        /// <param name="ModuleGuid">Идентификатор модуля</param>
        /// <returns>Путь в реестре</returns>
        public static string sGetModulePath(Guid ModuleGuid)
        {
            string Path = sGetiMenuPath(EnRegistryLocalPathType.EnModulesPath);
            try
            {
                RegistryKey RKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\" + CConsts.REG_PATH_ROOT + "\\" + CConsts.REG_PATH_MODULES);
                string[] SubKeys = RKey.GetSubKeyNames();
                foreach (string Key in SubKeys)
                {
                    if (Key == ModuleGuid.ToString())
                    {
                        return Path + "\\" + Key;
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return "";
        }
        /// <summary>
        /// Получение пути в реестре относительно корня
        /// </summary>
        /// <param name="Type">Тип пути</param>
        /// <param name="LocalType">Тип ветки реестра</param>
        /// <returns>Путь в реестре</returns>
        public static string sGetiMenuPath(EnRegistryLocalPathType LocalType = EnRegistryLocalPathType.EnRootPath)
        {
            RegistryKey PathType = null;
            try
            {
                PathType = Microsoft.Win32.Registry.LocalMachine;
                switch (LocalType)
                {
                    case EnRegistryLocalPathType.EnApplicationConfig: return "SOFTWARE\\" + CConsts.REG_PATH_ROOT + "\\" + CConsts.REG_PATH_APP_CONFIG;
                    case EnRegistryLocalPathType.EnModulesPath: return "SOFTWARE\\" + CConsts.REG_PATH_ROOT + "\\" + CConsts.REG_PATH_MODULES;
                    case EnRegistryLocalPathType.EnRootPath: return "SOFTWARE\\" + CConsts.REG_PATH_ROOT;
                    default: return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// Получение значения ключа
        /// </summary>
        /// <param name="KeyName">Путь до папки ключа</param>
        /// <param name="KeyValue">Наименование ключа</param>
        /// <returns>Значение ключа</returns>
        public static object sGetKeyValue(string KeyName)
        {
            try
            {
                return Microsoft.Win32.Registry.LocalMachine.GetValue(KeyName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Занесение значения в ключ
        /// </summary>
        /// <param name="KeyName">Путь до папки ключа</param>
        /// <param name="KeyValue">Наименование ключа</param>
        /// <param name="Value">Значение</param>
        /// <returns>Номер ошибки</returns>
        public static int sSetKeyValue(string KeyName, object Value)
        {
            try
            {
                Microsoft.Win32.Registry.LocalMachine.SetValue(KeyName, Value);
            }
            catch (Exception ex)
            {
                return CErrors.ERR_REG_KEY;
            }

            return CErrors.ERR_SUC;
        }
        /// <summary>
        /// Получение значения ключа настроек приложения
        /// </summary>
        /// <param name="KeyValue">Наименование ключа</param>
        /// <param name="Type">Тип ветки в реестре</param>
        /// <returns>Значение ключа</returns>
        public static object sGetConfigKey(string KeyValue)
        {
            string ConfigPath = CRegistry.sGetiMenuPath(EnRegistryLocalPathType.EnApplicationConfig);
            if (ConfigPath.Trim() == "")
                return null;
            RegistryKey Key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(ConfigPath);
            return Key.GetValue(KeyValue);
        }
        /// <summary>
        /// Занесение значения ключа настроек приложения
        /// </summary>
        /// <param name="KeyValue">Наименование ключа</param>
        /// <param name="Value">Тип ветки в реестре</param>
        /// <param name="Type">Значение ключа</param>
        /// <returns>Номер ошибки</returns>
        public static int sSetConfigKey(string KeyValue, object Value)
        {
            string ConfigPath = CRegistry.sGetiMenuPath(EnRegistryLocalPathType.EnApplicationConfig);
            if (ConfigPath.Trim() == "")
                return CErrors.ERR_REG_PATH;
            RegistryKey Key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(ConfigPath, true);
            Key.SetValue(KeyValue, Value);
            return CErrors.ERR_SUC;
        }
        /// <summary>
        /// Получение значения ключа настроек модуля
        /// </summary>
        /// <param name="ModuleGuid">Идентификатор модуля</param>
        /// <param name="KeyValue">Наименование ключа</param>
        /// <returns>Значение ключа</returns>
        public static object sGetModuleKey(Guid ModuleGuid, string KeyValue)
        {
            string ModulePath = CRegistry.sGetModulePath(ModuleGuid);
            if (ModulePath.Trim() == "")
                return null;
            RegistryKey Key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(ModulePath);
            return Key.GetValue(KeyValue);
        }
        /// <summary>
        /// Установка значения ключа настроек модуля
        /// </summary>
        /// <param name="ModuleGuid">Идентификатор модуля</param>
        /// <param name="KeyValue">Наименовение ключа</param>
        /// <param name="Value">Значение ключа</param>
        /// <returns>Номер ошибки</returns>
        public static int sSetModuleKey(Guid ModuleGuid, string KeyValue, object Value)
        {
            string ModulePath = CRegistry.sGetModulePath(ModuleGuid);
            if (ModulePath.Trim() == "")
                return CErrors.ERR_REG_PATH;

            RegistryKey Key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(ModulePath, true);
            Key.SetValue(KeyValue, Value);
            return CErrors.ERR_SUC;
        }
        public static List<string> sGetChildFolders(string Path, bool Absolute = true)
        {
            List<string> R = new List<string>();
            RegistryKey PathType = Microsoft.Win32.Registry.LocalMachine;
            if (!Absolute)
            {
                string iMenuPath = sGetiMenuPath(EnRegistryLocalPathType.EnRootPath);
                Path = iMenuPath + "\\" + Path;
            }

            RegistryKey FolderKey = PathType.OpenSubKey(Path);
            var ChArr = FolderKey.GetSubKeyNames();
            foreach (string F in ChArr)
                R.Add(F);

            return R;
        }
    }
}
