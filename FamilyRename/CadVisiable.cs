using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyRename
{
    [Transaction(TransactionMode.Manual)]
    class CadVisiable : IExternalCommand

    {
        private Hashtable m_categoriesWithName; // 作为获得所有的Category及其名称的哈希表


        public void CategoryHash(UIDocument uidoc)

        {
            // 初始化哈希表

            m_categoriesWithName = new Hashtable();

            // 将类名填入表中
            foreach (Category category in uidoc.Document.Settings.Categories)
            {
                if (category.get_AllowsVisibilityControl(uidoc.Document.ActiveView))
                {

                    m_categoriesWithName.Add(category.Name, category);
                }
            }
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
            var seleCad = uidoc.Selection;
            //选择CAD图层
            var refSeleCad = seleCad.PickObjects(ObjectType.PointOnElement,"选择CAD");
            //初始化一个已用列表，用来存放筛选出的CAD图层引用
            List<Autodesk.Revit.DB.Reference> refCadList = new List<Reference> { };
            try
            {
                foreach (var item in refSeleCad)
                {
                    if (item.GetType().Name == "Reference")
                    {
                        refCadList.Add(item);
                    }
                }
                if (refCadList != null)
                {
                    foreach (var item in refCadList)
                    {
                        var seleElement = doc.GetElement(item);
                        var geomobj = seleElement.GetGeometryObjectFromReference(item);


                        CategoryHash(uidoc);
                        Category cadCategory = null;
                        //获取选择CAD图层的Category
                        if (geomobj.GraphicsStyleId != ElementId.InvalidElementId)
                        {
                            var gs = doc.GetElement(geomobj.GraphicsStyleId) as GraphicsStyle;
                            if (gs != null)

                            {
                                cadCategory = gs.GraphicsStyleCategory;
                            }

                        }

                        Transaction transaction = new Transaction(doc, "隐藏CAD图层");
                        transaction.Start();
                        if (cadCategory != null)
                        {

                            cadCategory.set_Visible(doc.ActiveView, false);

                        }
                        transaction.Commit();

                        
                    }
                }
                

               
                
                return Result.Succeeded;
            }
            catch (Exception)
            {

                throw;
            }

           

       
        
        
        }

       


    }
}
