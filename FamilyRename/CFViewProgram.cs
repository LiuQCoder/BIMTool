using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;



namespace FamilyRename
{
    class CFViewProgram : IExternalEventHandler
    {
        public string buttonName { get; set; }
        public void Execute(UIApplication app)
        {

            var uidoc = app.ActiveUIDocument;
            var doc = uidoc.Document;
            var actView = doc.ActiveView;

            Transaction transaction = new Transaction(doc);
            transaction.Start("可见性设置");

            try
            {
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


                    case "轴网剖面":

                        List<Autodesk.Revit.DB.Category> grseCategoryList = new List<Category> { Category.GetCategory(doc, BuiltInCategory.OST_Grids), Category.GetCategory(doc, BuiltInCategory.OST_Elev) };

                        FilteredElementCollector elemCollector = new FilteredElementCollector(doc);
                        elemCollector.OfCategory(BuiltInCategory.OST_Viewers);
                        Element sectionBox = null;
                        List<ElementId> sectionBoxIds = new List<ElementId>();
                        //找到当前视图中可以隐藏的剖面框
                        foreach (Element e in elemCollector)
                        {
                            if (e.CanBeHidden(actView))
                            {
                                sectionBox = e;
                                sectionBoxIds.Add(sectionBox.Id);
                                continue;
                            }
                        }

                        //判断当前视图中剖面框是否被隐藏
                        if (sectionBox.IsHidden(actView))
                        {
                            //取消隐藏
                            actView.UnhideElements(sectionBoxIds);
                            foreach (Category item in grseCategoryList)
                            {
                                item.set_Visible(actView, true);
                            }
                        }
                        else
                        {
                            //隐藏
                            actView.HideElements(sectionBoxIds);
                            foreach (Category item in grseCategoryList)
                            {
                                item.set_Visible(actView, false) ;
                            }
                        }

                        

                        break;

                    case "净高分析视图":
                        var category = new FilteredElementCollector(doc, actView.Id);

                        List<Category> nethighView = new List<Category> { Category.GetCategory(doc, BuiltInCategory.OST_DetailComponents), Category.GetCategory(doc, BuiltInCategory.OST_TextNotes) };

                        VisiAbleSet(actView, nethighView);

                        break;

                }
                transaction.Commit();
            }
            catch (Exception e)
            {

                TaskDialog.Show("错误", "当前视图已添加视图样板或其他 \n无法修改可见性 \n" + e.Message.ToString());
            }



        }
        public string GetName()
        {
            return "CFViewProgram";
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
