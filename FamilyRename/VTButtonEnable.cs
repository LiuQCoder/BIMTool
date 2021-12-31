using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyRename
{   
   [Transaction(TransactionMode.Manual)]
    class VTButtonEnable : IExternalCommandAvailability
    {   
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            UIDocument uidoc = applicationData.ActiveUIDocument;
            Document doc = uidoc.Document;
            var activeView = doc.ActiveView;
            if (activeView.ViewType.ToString() == "ThreeD")
            {
                return true;
            }
            return false;
        }
    }
}
