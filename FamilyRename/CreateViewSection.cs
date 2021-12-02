using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;

namespace FamilyRename
{
    [Transaction(TransactionMode.Manual)]
    class CreateViewSection : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
            var selection = uidoc.Selection;


            #region 获取viewfamilyID
            //获取剖面的viewfamilyID



            List<ViewFamilyType> viewFType = new List<ViewFamilyType> { };
            var viewFamilyID = new ElementId(-1);
            var viewFamilyTypeCot = new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType));
            foreach (var item in viewFamilyTypeCot)
            {
                var viewfamilyType = item as ViewFamilyType;
                if (viewfamilyType != null && viewfamilyType.ViewFamily == ViewFamily.Section)
                {
                    viewFType.Add(viewfamilyType);

                }
            }

            #endregion

            //创建窗口
            var cvsWpf = new CreatViewSectionWPF(viewFType);
            cvsWpf.Left = 400;
            cvsWpf.Top = 300;
            cvsWpf.Show();
            viewFamilyID = cvsWpf.SelectViewType.Id;


            //创建剖面

            //CrViewSection(doc, viewFamilyID, boundingbox);



            #region 创建范围框
            //创建sectionBox来存储范围框：BoundingBoxXYZ
            BoundingBoxXYZ sectionBox;

            //定义范围框的长高为3m
            double length = UnitUtils.Convert(3, DisplayUnitType.DUT_METERS, DisplayUnitType.DUT_DECIMAL_FEET);
            double hight = UnitUtils.Convert(3, DisplayUnitType.DUT_METERS, DisplayUnitType.DUT_DECIMAL_FEET);



            try
            {
             

                //选择一个MEP管线
                var iselectionFilter = new mepiSelectionFilter();
                Reference pickedElement = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, iselectionFilter);
                var mepPipe = doc.GetElement(pickedElement.ElementId) as MEPCurve;
                var mepCurve = mepPipe.Location as LocationCurve;
                //通过选择两个点来确定剖面生成的位置、深度和方向
                //第一个点来确定原点
                //第二个点来确定方向和深度
                XYZ firstPoint = selection.PickPoint("请选择第一个点");
                XYZ mepPoint1 = mepCurve.Curve.Project(firstPoint).XYZPoint;
                XYZ secPoint = selection.PickPoint("请选择第二个点");
                XYZ mepPoint2 = mepCurve.Curve.Project(secPoint).XYZPoint;


                //求选择两点的距离即剖面的深度
                var zHight = FindDistance(mepPoint1, mepPoint2);

                //设置sectionBOX
                var sectionTran = new Transaction(doc);
                sectionTran.Start("设置sectionBox");
                sectionBox = new BoundingBoxXYZ();
                sectionBox.Enabled = true;
                //设置范围框的大小，有两个对角点来生成范围框,设置为3m*3m
                XYZ maxPoint = new XYZ(length, length, 0);

                XYZ minPoint = new XYZ(-length, -length, -zHight);
                sectionBox.Max = maxPoint;
                sectionBox.Min = minPoint;
                #endregion
                sectionTran.Commit();



                #region 将范围框转换成模型坐标系

                var transForm = GetpipeTransform(mepPipe, mepPoint1, mepPoint2);
                sectionBox.Transform = transForm;




                #endregion



                var transaction1 = new Transaction(doc);
                transaction1.Start("创建剖面");

                #region 创建剖面:CreateSection(doc,viewfamilyID,BoundingBox)
                //创建剖面:CreateSection(doc,viewfamilyID,BoundingBox)

                var section1 = ViewSection.CreateSection(doc, viewFamilyID, sectionBox);

                if (null == section1)
                {
                    message = "无法创建此剖面";
                    return Autodesk.Revit.UI.Result.Failed;
                    
                }
                else
                {
                    var viewScale = uidoc.ActiveView.LookupParameter("比例值 1:").AsInteger();
                    var sectScale = section1.LookupParameter("当比例粗略度超过下列值时隐藏").Set(viewScale);

                }
                transaction1.Commit();
                return Result.Succeeded;
            }


            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Autodesk.Revit.UI.Result.Failed;


            }
            catch (Exception e)
            {
                TaskDialog.Show("警告", e.Message.ToString());
                return Autodesk.Revit.UI.Result.Failed;

            }
            #endregion


        }

        public static void CrViewSection(Document doc, ElementId viewFamilyID, BoundingBoxXYZ boundingbox)
        {
            
        }





        /// <summary>
        /// 获取mepcurve的transform
        /// </summary>
        /// <param name="mepPipe"></param>
        /// <returns></returns>
        public Transform GetpipeTransform(MEPCurve mepPipe, XYZ Point1, XYZ Point2)
        {
            Transform transform = null;
            var locationCurve = mepPipe.Location as LocationCurve;
            var line = locationCurve.Curve as Line;
            transform = Transform.Identity;

            ;
            transform.Origin = Point2;

            var basisZ = FindDirection(Point1, Point2);
            var basisX = FindRightDirection(basisZ);
            var basisY = FindUpDirection(basisZ);
            transform.set_Basis(0, basisX);
            transform.set_Basis(1, basisY);
            transform.set_Basis(2, basisZ);
            return transform;

        }




        #region 继承ISelectionFilyter接口，创建选择过滤器
        //继承ISelectionFilyter接口，创建选择过滤器

        public class mepiSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element elem)
            {
                List<string> cateList = new List<string>() { "管道", "电缆桥架", "风管" };

                if (cateList.IndexOf(elem.Category.Name) != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public bool AllowReference(Reference reference, XYZ position)
            {
                throw new NotImplementedException();
            }

        }
        #endregion
        /// <summary>
        /// 求取两点之间的中点
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>返回中点</returns>
        public static XYZ FindMidPoint(XYZ first, XYZ second)
        {
            double x = (first.X + second.X) / 2;
            double y = (first.Y + second.Y) / 2;
            double z = (first.Z + second.Z) / 2;
            Autodesk.Revit.DB.XYZ midPoint = new Autodesk.Revit.DB.XYZ(x, y, z);
            return midPoint;
        }
        /// <summary>
        /// 求取给定两点的向量
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>返回向量点</returns>
        public XYZ FindDirection(XYZ first, XYZ second)
        {
            double x = second.X - first.X;
            double y = second.Y - first.Y;
            double z = second.Z - first.Z;
            double distance = FindDistance(first, second);
            Autodesk.Revit.DB.XYZ direction = new XYZ(x / distance, y / distance, z / distance);
            return direction;
        }
        /// <summary>
        /// 求取两点间的距离
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>获得两点间距离</returns>
        public double FindDistance(XYZ first, XYZ second)
        {
            double x = first.X - second.X;
            double y = first.Y - second.Y;
            double z = first.Z - second.Z;
            return Math.Sqrt(x * x + y * y + z * z);
        }
        /// <summary>
        /// 求X轴
        /// </summary>
        /// <param name="basisZ"></param>
        /// <returns></returns>
        public XYZ FindRightDirection(XYZ viewDirection)
        {
            double x = -viewDirection.Y;
            double y = viewDirection.X;
            double z = viewDirection.Z;
            XYZ direction = new XYZ(x, y, z);
            return direction;
        }

        /// <summary>
        /// 求Y轴
        /// </summary>
        /// <param name="basisZ"></param>
        /// <returns></returns>
        public XYZ FindUpDirection(XYZ basisZ)
        {
            XYZ direction = new XYZ(0, 0, 1);
            return direction;
        }

    }
}

