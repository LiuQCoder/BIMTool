using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyRename
{
    [Transaction(TransactionMode.Manual)]
    class ViewsectionTran : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;

            Reference select = uidoc.Selection.PickObject(ObjectType.Element, "选择一个线型图元");
            Element selectElement = doc.GetElement(select.ElementId) as Element;
            //获取当前视图的范围框
            var curve = selectElement.Location as LocationCurve;
            var line = curve.Curve as Line;
            var linedir = line.Direction;
            //求旋转的弧度
            var ang = Math.Atan2(linedir.Y, linedir.X);
            //旋转剖面框
            var actView = doc.ActiveView as View3D;
            var currentViewSect = actView.GetSectionBox();
            var tran = new Transaction(doc, "旋转剖面框");
            tran.Start();
            XYZ or = new XYZ(0, 0, 0);
            XYZ ax = new XYZ(0, 0, 1);
            var ro = Transform.CreateRotationAtPoint(ax, ang, or);
            currentViewSect.Transform = currentViewSect.Transform.Multiply(ro);
            actView.SetSectionBox(currentViewSect);
            tran.Commit();


            return Result.Succeeded;
        }
    }
}
