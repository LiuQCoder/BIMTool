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

    class CadVisiable : IExternalEventHandler
    {

        public string ButtonName { get; set; }



        public void Execute(UIApplication app)
        {

            var uidoc = app.ActiveUIDocument;
            var doc = uidoc.Document;
            var seleCad = uidoc.Selection;
            //初始化一个已用列表，用来存放筛选出的CAD图层引用
            List<Autodesk.Revit.DB.Reference> refCadList = new List<Reference> { };
            try
            {
                switch (ButtonName)
                {

                    case "关闭选定图层":
                        //选择CAD图层
                        var refSeleCad = seleCad.PickObjects(ObjectType.PointOnElement, "选择CAD");
                        foreach (var item in refSeleCad)
                        {
                            if (item.GetType().Name == "Reference")
                            {
                                refCadList.Add(item);
                            }
                        }
                        //开启事务 隐藏图层
                        Transaction transaction1 = new Transaction(doc, "隐藏CAD图层");
                        transaction1.Start();
                        if (refCadList != null)
                        {
                            foreach (var item in refCadList)
                            {
                                var seleElement = doc.GetElement(item);
                                var geomobj = seleElement.GetGeometryObjectFromReference(item);
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

                                if (cadCategory != null)
                                {
                                    cadCategory.set_Visible(doc.ActiveView, false);
                                }

                            }
                            transaction1.Commit();
                        }
                        else
                        {
                            TaskDialog.Show("错误", "所选元素中包含非CAD图层部分");
                        }
                        break;

                    case "保留选定图层":
                        //1.通过subcategories获取所有图层
                        //2.移除已选择的图层
                        //3.关闭图层
                        //无法直接list.remove来移除选择Category ，使用ElementId的方式进行移除

                        //初始化一个已用列表，用来存放选择的CAD图层 tty
                        List<ElementId> SelCadCategories = new List<ElementId> { };

                        //选择需要保留的CAD图层
                        var refSeleCad1 = seleCad.PickObjects(ObjectType.PointOnElement, "选择CAD");
                        foreach (var item in refSeleCad1)
                        {
                            if (item.GetType().Name == "Reference")
                            {

                                var seleElement = doc.GetElement(item);
                                var geomobj = seleElement.GetGeometryObjectFromReference(item);
                                Category cadCategory = null;
                                //获取选择CAD图层的Category
                                if (geomobj.GraphicsStyleId != ElementId.InvalidElementId)
                                {
                                    var gs = doc.GetElement(geomobj.GraphicsStyleId) as GraphicsStyle;
                                    if (gs != null)
                                    {
                                        cadCategory = gs.GraphicsStyleCategory;
                                        SelCadCategories.Add(cadCategory.Id);
                                    }
                                }
                            }
                        }
                        //通过选择的第一个元素，获取图层所属的图层
                        Category CADlinkCategory = Category.GetCategory(doc, SelCadCategories.FirstOrDefault()).Parent;
                        //获取链接CAD的所有图层
                        var CadAllLayer = CADlinkCategory.SubCategories;
                        //初始化一个已用列表，用来存放CAD链接下的所有图层ID
                        List<ElementId> LayerList = new List<ElementId> { };
                        //开启事务 隐藏图层
                        Transaction transaction2 = new Transaction(doc, "保留选定CAD图层");
                        transaction2.Start();

                        //获取所有图层ID
                        foreach (var item in CadAllLayer)
                        {
                            Category cadlayer = item as Category;
                            LayerList.Add(cadlayer.Id);
                        }
                        //移除选择图层的ID
                        foreach (var item in SelCadCategories)
                        {
                            LayerList.Remove(item);
                        }

                       
                        //关闭未选中图层
                        foreach (var item in LayerList)
                        {
                            Category.GetCategory(doc, item).set_Visible(doc.ActiveView, false);
                        }
                        transaction2.Commit();
                        break;
                    case "图层全开":
                        break;
                }
            }
            //用户取消操作异常
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {

            }
            //选择了非CAD图层异常
            catch (NullReferenceException )
            {

                TaskDialog.Show("警告", "所选对象包含非CAD图层对象\n请重新选择");
            }

            //捕获其他异常
            catch (Exception e)
            {
                TaskDialog.Show("错误", e.ToString());
            }

        }



        public string GetName()
        {
            return "CadVisiable";
        }
    }
}
