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
                                //开启事务 隐藏图层
                                Transaction transaction1 = new Transaction(doc, "隐藏CAD图层");
                                transaction1.Start();
                                if (cadCategory != null)
                                {
                                    cadCategory.set_Visible(doc.ActiveView, false);
                                }
                                transaction1.Commit();
                            }
                        }
                        else
                        {
                            TaskDialog.Show("错误", "所选元素中包含非CAD图层部分");
                        }
                        break;

                    case "保留选定图层":
                        //初始化一个已用列表，用来存放选择的CAD图层 tty
                        List<Category> SelCadCategories = new List<Category> { };

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
                                        SelCadCategories.Add(cadCategory);
                                    }
                                }
                            }
                        }
                        //获取图层所属的图层
                        Category CADlinkCategory = SelCadCategories.FirstOrDefault().Parent;
                        //获取链接CAD的所有图层
                        var CadAllLayer = CADlinkCategory.SubCategories;
                        //初始化一个已用列表，用来存放CAD链接下的所有图层
                        List<Category> LayerList = new List<Category> { };
                        //开启事务 隐藏图层
                        Transaction transaction2 = new Transaction(doc, "保留选定CAD图层");
                        transaction2.Start();
                        foreach (var layer in CadAllLayer)
                        {
                            Category layerCategory = layer as Category;
                            if (SelCadCategories.Contains(layerCategory))
                            {

                            }
                            else
                            {
                                
                                if (layerCategory != null)
                                {
                                   layerCategory.set_Visible(doc.ActiveView, false);
                                }
                               
                            }
                            

                        }
                        transaction2.Commit();
                        break;
                    case "图层全开":
                        break;
                }
            }
            //用户取消操作异常
            catch (OperationCanceledException)
            {

            }
            //捕获其他异常
            catch (Exception e)
            {
                TaskDialog.Show("错误", e.Message.ToString());
            }

        }



        public string GetName()
        {
            return "CadVisiable";
        }
    }
}
