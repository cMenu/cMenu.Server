using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using cMenu.Common;

namespace cMenu.IO
{
    public class CPath
    {
        #region Paths
        /// <summary>
        /// Получение пути к папке, где установлено приложение
        /// </summary>
        /// <returns>Путь к папке</returns>
        public static string sGetInstalledDir()
        {
            return (string)CRegistry.sGetConfigKey(CConsts.REG_KEY_INSTALL_DIR);
        }
        public static string sGetModulesInstalledDir()
        {
            return sGetInstalledDir() + "\\" + CConsts.REG_PATH_MODULES;
        }
        /// <summary>
        /// Получение путь к папке Application Data для приложения
        /// </summary>
        /// <returns></returns>
        public static string sGetAppDataDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + CConsts.FS_PATH_ROOT;
        }
        /// <summary>
        /// Получение пути к папке данных модуля
        /// </summary>
        /// <param name="ModuleGuid">Идентификатор модуля</param>
        /// <returns>Путь к папке</returns>
        public static string sGetModuleDataDir(Guid ModuleGuid)
        {
            string rPath = CPath.sGetAppDataDir() + "\\" + CConsts.FS_PATH_MODULES;
            rPath += "\\" + ModuleGuid.ToString();
            return rPath;
        }
        public static string sGetModuleInstallerDir(Guid ModuleGuid)
        {
            string rPath = CPath.sGetModulesInstalledDir();
            rPath += "\\" + ModuleGuid.ToString();
            return rPath;
        }
        /// <summary>
        /// Получени пути к папке настроек приложения
        /// </summary>
        /// <returns>Путь к папке</returns>
        public static string sGetAppConfigDir()
        {
            return CPath.sGetInstalledDir() + "\\" + CConsts.FS_PATH_APP_CONFIG;
        }
        public static string sGetModulesDataDir()
        {
            string rPath = CPath.sGetAppDataDir() + "\\" + CConsts.FS_PATH_MODULES;
            return rPath;
        }
        #endregion
    }
}
