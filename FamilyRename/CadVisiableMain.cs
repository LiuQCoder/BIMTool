using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace FamilyRename
{
    [Transaction(TransactionMode.Manual)]
    class CadVisiableMain : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var mainWin = new CadVisiableWPF();
            //将WPF窗口绑定到主窗口，随revit一同最小化
            System.Windows.Interop.WindowInteropHelper mainUI = new System.Windows.Interop.WindowInteropHelper(mainWin);
            mainUI.Owner = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            //任务栏是否显示窗口
            mainWin.ShowInTaskbar = false;
            mainWin.Show();
            return Result.Succeeded;
           
        }
    }

}
