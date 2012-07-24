using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

using cMenu.IO;
using cMenu.UI.ManagementTools.WPF.Core;

namespace cMenu.UI.ManagementTools.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void OnStartupHandler(object sender, StartupEventArgs e)
        {
            CMTApplicationContext.ApplicationConfiguration = (CMTApplicationConfiguration)CSerialize.sDeserializeXMLFile("Configuration.xml", typeof(CMTApplicationConfiguration));
            if (CMTApplicationContext.ApplicationConfiguration == null)
            {
                CMTApplicationContext.ApplicationConfiguration = new CMTApplicationConfiguration();
                CMTApplicationContext.ApplicationConfiguration.SerializeXMLFile("Configuration.xml", typeof(CMTApplicationConfiguration));
            }
        }
    }
}
