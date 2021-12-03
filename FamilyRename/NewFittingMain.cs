﻿using Autodesk.Revit.Attributes;
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
    class NewFittingMain : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            
            var cableFamily = new List<string> { "槽式", "梯级", "托盘" };
            var newfitingwpf = new NewFittingWPF(cableFamily);
            //将WPF窗口绑定到主窗口，随revit一同最小化
            System.Windows.Interop.WindowInteropHelper mainUI = new System.Windows.Interop.WindowInteropHelper(newfitingwpf);
            mainUI.Owner = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            newfitingwpf.ShowInTaskbar = false;
            newfitingwpf.Show();


            return Result.Succeeded;
        }

       
    }
}