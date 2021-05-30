using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Gma.System.MouseKeyHook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FamilyRename
{

    class CadVisiable : IExternalEventHandler
    {

        public string ButtonName { get; set; }

        public string ButtonIS { get; set; }



        public void Execute(UIApplication app)
        {
            ButtonIS = "on";
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
                        ButtonIS = "23off";
                        //选择CAD图层
                        var refSeleCad = seleCad.PickObjects(ObjectType.PointOnElement, "选择CAD图层，按单机空格键或鼠标右键完成选择");
                        if (refSeleCad != null)
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
                                //开启事务 隐藏图层
                                Transaction transaction1 = new Transaction(doc, "隐藏CAD图层");
                                transaction1.Start();
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
                                TaskDialog.Show("错误", "选择为空，请重新选择");
                            }
                        }
                        else
                        {
                            ButtonIS = "on";
                            TaskDialog.Show("警告", "所选图元为空");

                        }
                        ButtonIS = "on";
                        break;

                    case "保留选定图层":
                        //1.通过subcategories获取所有图层
                        //2.移除已选择的图层
                        //3.关闭图层
                        //无法直接list.remove来移除选择Category ，使用ElementId的方式进行移除

                        ButtonIS = "13off";
                        //初始化一个已用列表，用来存放选择的CAD图层 tty
                        List<ElementId> SelCadCategories = new List<ElementId> { };

                        //选择需要保留的CAD图层
                        var refSeleCad1 = seleCad.PickObjects(ObjectType.PointOnElement, "选择CAD图层，按单机空格键或鼠标右键完成选择");

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
                            else
                            {
                                ButtonIS = "on";
                                TaskDialog.Show("警告", "所选图元为空");
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

                        ButtonIS = "on";

                        break;
                    case "图层全开":
                        ButtonIS = "12off";
                        //选择CAD图层
                        var refSeleCadOn = seleCad.PickObject(ObjectType.PointOnElement, "选择CAD图层");
                        var seleElementOn = doc.GetElement(refSeleCadOn);
                        var geomobjOn = seleElementOn.GetGeometryObjectFromReference(refSeleCadOn);
                        //获取选择的CAD Category
                        Category cadCategoryOn = null;
                        if (geomobjOn.GraphicsStyleId != ElementId.InvalidElementId)
                        {
                            var gs = doc.GetElement(geomobjOn.GraphicsStyleId) as GraphicsStyle;
                            if (gs != null)
                            {
                                cadCategoryOn = gs.GraphicsStyleCategory;

                            }
                        }
                        //获取所有CAD图层

                        Category CadlinkCategoryOn = cadCategoryOn.Parent;
                        var CadAllLayerOn = CadlinkCategoryOn.SubCategories;



                        //开启事务 隐藏图层
                        Transaction transaction3 = new Transaction(doc, "图层全开");
                        transaction3.Start();

                        //打开图层
                        foreach (var item in CadAllLayerOn)
                        {
                            Category cadlayer = item as Category;
                            cadlayer.set_Visible(doc.ActiveView, true);
                        }


                        transaction3.Commit();

                        ButtonIS = "on";
                        break;
                }
            }
            //用户取消操作异常
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                ButtonIS = "on";
            }
            //选择了非CAD图层异常
            catch (NullReferenceException)
            {
                ButtonIS = "on";
                TaskDialog.Show("警告", "未选任何CAD图层对象\n请重新选择");
            }

            //捕获其他异常
            catch (Exception e)
            {
                ButtonIS = "on";
                TaskDialog.Show("错误", e.ToString());
            }

        }



        public string GetName()
        {
            return "CadVisiable";
        }
    }
}
