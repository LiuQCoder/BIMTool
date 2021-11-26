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
    class pip : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var sourceSymboolName = "P-给水压力(J2)";
            var newpipeFitting = "111";
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
            // 新建过滤器
            var fittingCollector = new FilteredElementCollector(doc);
            //获取管道配件类型
            var pipeCollector = fittingCollector.OfCategory(BuiltInCategory.OST_PipeFitting).OfClass(typeof(FamilySymbol));

            var creatpipeTransaction = new Transaction(doc);
            creatpipeTransaction.Start("复制新建管件类型");
            foreach (var item in pipeCollector)
            {
                if (sourceSymboolName == item.Name.ToString())
                {
                   var  itemSy = item as FamilySymbol;
                    itemSy.Duplicate(newpipeFitting);

                }
            }
            creatpipeTransaction.Commit();
            return Result.Succeeded;

        }
    }
}
