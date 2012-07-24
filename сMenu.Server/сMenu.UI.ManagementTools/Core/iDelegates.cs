using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cMenu.UI.ManagementTools.Core
{
    public delegate void OnObjectsTreeSelectionChangedDelegate(object Sender, object SelectedObject);
    public delegate void OnObjectsListSelectionChangedDelegate(object Sender, object SelectedObject);
    public delegate void OnFormExecuteActionDelegate(object Sender, Hashtable Parameters, string Action);
}
