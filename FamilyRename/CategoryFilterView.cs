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
    class CategoryFilterView : IExternalCommand
    {
        public string buttonName { get; set; }
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var app = commandData.Application;
            var uidoc = app.ActiveUIDocument;
            var doc = uidoc.Document;
            var actView = doc.ActiveView;

            Transaction transaction = new Transaction(doc);
            transaction.Start("123");

            buttonName = "净高分析";

            switch (buttonName)
            {
                case "风管及相关":
                    List<Autodesk.Revit.DB.Category> ductCategoryList = new List<Category> { Category.GetCategory(doc, BuiltInCategory.OST_DuctFitting), Category.GetCategory(doc, BuiltInCategory.OST_DuctCurves), Category.GetCategory(doc, BuiltInCategory.OST_DuctInsulations), Category.GetCategory(doc, BuiltInCategory.OST_DuctAccessory), Category.GetCategory(doc, BuiltInCategory.OST_DuctTerminal) };

                    VisiAbleSet(actView, ductCategoryList);
                    break;

                case "管道及相关":
                    List<Autodesk.Revit.DB.Category> pipCategoryList = new List<Category> { Category.GetCategory(doc, BuiltInCategory.OST_PipeCurves), Category.GetCategory(doc, BuiltInCategory.OST_PipeFitting), Category.GetCategory(doc, BuiltInCategory.OST_PipeAccessory), Category.GetCategory(doc, BuiltInCategory.OST_PipeInsulations), Category.GetCategory(doc, BuiltInCategory.OST_Sprinklers) };

                    VisiAbleSet(actView, pipCategoryList);
                    break;

                case "电缆桥架及配件":
                    List<Autodesk.Revit.DB.Category> cableCategoryList = new List<Category> { Category.GetCategory(doc, BuiltInCategory.OST_CableTray), Category.GetCategory(doc, BuiltInCategory.OST_CableTrayFitting) };

                    VisiAbleSet(actView, cableCategoryList);
                    break;

                case "风管与管道隔热层":
                    List<Autodesk.Revit.DB.Category> insuCategoryList = new List<Category> { Category.GetCategory(doc, BuiltInCategory.OST_PipeInsulations), Category.GetCategory(doc, BuiltInCategory.OST_DuctInsulations) };

                    VisiAbleSet(actView, insuCategoryList);
                    break;

                /*case "CAD链接":
                    var cadlinkFilter = new FilteredElementCollector(doc).OfClass(typeof(ImportInstance));
                    List<string> visablList = new List<string> { };

                    foreach (var item in cadlinkFilter)
                    {
                        var cadlink = item as ImportInstance;
                        if (cadlink.GetVisibility() == null)
                        {
                            TaskDialog.Show("123", cadlink.OwnerViewId.ToString() + "\n" + actView.Id.ToString());
                            cadlink.Category.set_Visible(actView, false);
                        }

                    }

                    TaskDialog.Show("123", visablList.FirstOrDefault());
                    break;*/
                case "轴网剖面":
                    List<Autodesk.Revit.DB.Category> grseCategoryList = new List<Category> { Category.GetCategory(doc, BuiltInCategory.OST_Grids), Category.GetCategory(doc, BuiltInCategory.OST_Viewers) };

                    VisiAbleSet(actView, grseCategoryList);
                    break;

                case "净高分析":
                    var category = new FilteredElementCollector(doc, actView.Id);

                    List<Category> nethighView = new List<Category> { Category.GetCategory(doc, BuiltInCategory.OST_DetailComponents), Category.GetCategory(doc, BuiltInCategory.OST_TextNotes) };

                    VisiAbleSet(actView, nethighView);



                    break;

            }


            transaction.Commit();

            return Result.Succeeded;



        }

        private void VisiAbleSet(View actView, List<Category> categoryList)
        {
            List<bool> boolList = new List<bool> { };
            foreach (var item in categoryList)
            {
                boolList.Add(item.get_Visible(actView));

            }

            if (boolList.Contains(false) && boolList.Contains(true))
            {
                foreach (Category item in categoryList)
                {
                    item.set_Visible(actView, true);
                }

            }
            else if (boolList.Contains(false))
            {
                foreach (Category item in categoryList)
                {
                    item.set_Visible(actView, true);
                }

            }
            else
            {
                foreach (Category item in categoryList)
                {
                    item.set_Visible(actView, false);
                }
            }

        }



    }
}
