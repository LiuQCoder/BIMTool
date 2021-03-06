using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using System.Collections;

namespace FamilyRename
{
    class CFViewProgram : IExternalEventHandler
    {
        public string buttonName { get; set; }

      
        private Hashtable m_categoriesWithName; // all categories with its name
       

        public void VisibilityCtrl(UIDocument document)

        {
           // 初始化哈希表
            
            m_categoriesWithName = new Hashtable();

            // 将类名填入表中
            foreach (Category category in document.Document.Settings.Categories)
            {
                if (category.get_AllowsVisibilityControl(document.Document.ActiveView))
                {
                  
                    m_categoriesWithName.Add(category.Name, category);
                }
            }
        }



        public void Execute(UIApplication app)
        {

            var uidoc = app.ActiveUIDocument;
            var doc = uidoc.Document;
            var actView = doc.ActiveView;
            VisibilityCtrl(uidoc);

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

                        

                        var cat1 = m_categoriesWithName["轴网"] as Category;
                        var cat2 = m_categoriesWithName["剖面"] as Category;
                        List<Autodesk.Revit.DB.Category> grseCategoryList = new List<Category> {cat1,cat2};

                        VisiAbleSet(actView,grseCategoryList);


                       


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

                    actView.SetCategoryHidden(item.Id, false);
                    // 另一种可见性设置方法cat.set_Visible(m_document.ActiveView, visible);
                }

            }
            else if (boolList.Contains(false))
            {
                foreach (Category item in categoryList)
                {
                    actView.SetCategoryHidden(item.Id, false);
                }

            }
            else
            {
                foreach (Category item in categoryList)
                {
                    actView.SetCategoryHidden(item.Id, true);
                }
            }

        }
    }






}
