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
    class RenameDemo : IExternalEventHandler
    {
        public string newFaName { get; set; }
        public void Execute(UIApplication app)
        {

            var uidoc = app.ActiveUIDocument;
            var doc = uidoc.Document;

            try
            {
                //选择一个族实例
                var iselectionFilter = new myiSelectionFilter();
                Reference pickedElement = uidoc.Selection.PickObject(ObjectType.Element, iselectionFilter);
                FamilyInstance pickFamilyInstance = doc.GetElement(pickedElement.ElementId) as FamilyInstance;
                var pickFamilySymbol = pickFamilyInstance.Symbol;
                var pickFamily = pickFamilySymbol.Family;
                var oldFamliy = pickFamily.Name;

                //开启事务，重命名族
                Transaction transaction = new Transaction(doc);
                transaction.Start("族重命名");
                pickFamily.Name = pickFamily.Name.Replace(pickFamily.Name, newFaName);
                transaction.Commit();
                TaskDialog.Show("修改结果", "原名称是：" + oldFamliy + '\n' + "修改后的名称为：" + newFaName);
            }
            //取消选择异常
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {

            }
            catch (Autodesk.Revit.Exceptions.InvalidOperationException)
            {
                TaskDialog.Show("警告", "所选族的名称不可被修改");
            }
            catch (Autodesk.Revit.Exceptions.ArgumentException)
            {
                TaskDialog.Show("警告", "新名称在当前项目中不唯一！\n请重新输入");
            }
            catch (Exception e)
            {
                TaskDialog.Show("异常", "Error:" + e.Message);

            }

        }

        public string GetName()
        {
            return "RenameDemo";
        }

        public class myiSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element elem)
            {
                var familyInstance = elem as FamilyInstance;

                if (familyInstance.Name != null)
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
    }


}

