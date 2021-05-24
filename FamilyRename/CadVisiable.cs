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
                                Transaction transaction = new Transaction(doc, "隐藏CAD图层");
                                transaction.Start();
                                if (cadCategory != null)
                                {
                                    cadCategory.set_Visible(doc.ActiveView, false);
                                }
                                transaction.Commit();
                            }
                        }
                        else
                        {
                            var nullException = new ArgumentNullException("refCadList");
                            throw nullException;
                        }
                        break;

                    case "保留选定图层":
                        break;
                    case "图层全开":
                        break;
                }
            }
            //用户取消操作异常
            catch (OperationCanceledException)
            {  
                
            }
            

        }



        public string GetName()
        {
            return "CadVisiable";
        }
    }
}
