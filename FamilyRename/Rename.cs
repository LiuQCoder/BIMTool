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
    public class Rename: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;

            
            var renameWindow1 = new RenameWindow();
            renameWindow1.Show();
            FamilyRm(doc, uidoc, renameWindow1.newname.Text);
            return Result.Succeeded;
        }

        public void FamilyRm(Document doc, UIDocument uidoc, string v)
        {
            try
            {
                //选择一个族实例
                var iselectionFilter = new myiSelectionFilter();
                Reference pickedElement = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, iselectionFilter);
                FamilyInstance pickFamilyInstance = doc.GetElement(pickedElement.ElementId) as FamilyInstance;
                var pickFamilySymbol = pickFamilyInstance.Symbol;
                var pickFamily = pickFamilySymbol.Family;


                //开启事务，重命名族
                Transaction transaction = new Transaction(doc);
                transaction.Start("族重命名");
                TaskDialog.Show("族名", "选择的族名称为" + pickFamily.Name);
                pickFamily.Name = pickFamily.Name.Replace(pickFamily.Name, v);
                transaction.Commit();
            }
            catch (Exception e)
            {

                TaskDialog.Show("异常", e.Message);
            }

        }

      
        public class myiSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element elem)
            {
                var familyInstance= elem as FamilyInstance;

                if (familyInstance.Name != null)
                {
                    return true;
                }
                else
                    return false;

            }

            public bool AllowReference(Reference reference, XYZ position)
            {
                return false;
            }
        }
    }
}

