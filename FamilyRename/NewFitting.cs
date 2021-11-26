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
    class NewFitting : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            var selectCabName = "梯级";
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
            // 新建过滤器
            var fittingCollector = new FilteredElementCollector(doc);
            //获取桥架配件类型
            var cableCollector = fittingCollector.OfCategory(BuiltInCategory.OST_CableTrayFitting).OfClass(typeof(FamilySymbol));
            //获取桥架配件族ID
            var cbFamNameList = new List<string>();
            var cbFamList = new List<ElementId>();
            var a = "";
            foreach (FamilySymbol item in cableCollector)
            {
                cbFamList.Add(item.Family.Id);
            }
            //族去重
            var cbFamList2 = cbFamList.Distinct().ToList();

            foreach (var item in cbFamList2)
            {
                var famname = doc.GetElement(item) as Family; 
                cbFamNameList.Add( famname.Name.ToString());

            }
            #region 筛选族
            var resultFamily = new List<Family>();
            int cbFNListNum = 0;
            foreach (var item in cbFamNameList)
            {
                if (item.Contains(selectCabName))
                {
                    resultFamily.Add(doc.GetElement(cbFamList2[cbFNListNum]) as Family);
                    cbFNListNum = cbFNListNum + 1;
                }
                else
                {
                    cbFNListNum = cbFNListNum + 1;
                }
            }
            #endregion
           
            #region 复制新建类型
            var creatTransaction = new Transaction(doc);
            creatTransaction.Start("复制新建类型");
            CreetFitting( resultFamily, "2222",doc);
            creatTransaction.Commit();
            #endregion







            return Result.Succeeded;
        }

        public void CreetFitting(List<Family> famlist, string newname,Document doc )
        {
            foreach (var item in famlist)
            {
                var familySymSet = item.GetFamilySymbolIds();
                var familySym = doc.GetElement(familySymSet.First()) as FamilySymbol;
                familySym.Duplicate(newname);
            }
        }
    }
}