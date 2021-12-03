using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyRename
{
    [Transaction(TransactionMode.Manual)]
    class RenameMain : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var mainWin = new RenameWPF();
            //将WPF窗口绑定到主窗口，随revit一同最小化
            System.Windows.Interop.WindowInteropHelper mainUI = new System.Windows.Interop.WindowInteropHelper(mainWin);
            mainUI.Owner = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            mainWin.ShowInTaskbar = false;
            mainWin.Show();
            return Result.Succeeded;
        }
    }
}
