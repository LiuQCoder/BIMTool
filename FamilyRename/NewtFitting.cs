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
    
    class NewtFitting : IExternalEventHandler
    {   

        public string CableFamilySelect { set; get; }
        public string CableNewName { set; get; }
        public string SourceSymboolName { set; get; }
        public string NewpipeFitting { set; get; }

        public bool PipeChecked { set; get; }
            
         
        public void Execute(UIApplication app)
        {
            try
            {
                if (PipeChecked)
                {
                    //生成管件
                    var sourceSymboolName = SourceSymboolName;
                    var newpipeFitting = NewpipeFitting;
                    CreatpipFit(app, sourceSymboolName, newpipeFitting);
                }
                else
                {
                    //生成桥架配件
                    var selectCabName = CableFamilySelect;
                    var cableNewName = CableNewName;
                    CreatCableFit(app, selectCabName, cableNewName);
                }
            }



            catch (Autodesk.Revit.Exceptions.ArgumentException)
            {

                TaskDialog.Show("警告", "新建类型名称在项目中已存在！");
            }
            catch (Exception e)
            {
                TaskDialog.Show("异常", "Error:" + e.Message);

            }


        }



       
        /// <summary>
        /// 生成桥架配件
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="selectCabName">桥架类型</param>
        /// <param name="cableNewName">新建配件名称</param>
        public void CreatCableFit(UIApplication app, string selectCabName, string cableNewName)
        {
            var uidoc = app.ActiveUIDocument;
            var doc = uidoc.Document;
            // 新建过滤器
            var fittingCollector = new FilteredElementCollector(doc);
            //获取桥架配件类型
            var cableCollector = fittingCollector.OfCategory(BuiltInCategory.OST_CableTrayFitting).OfClass(typeof(FamilySymbol));
            //获取桥架配件族ID
            var cbFamNameList = new List<string>();
            var cbFamList = new List<ElementId>();

            foreach (FamilySymbol item in cableCollector)
            {
                cbFamList.Add(item.Family.Id);
            }
            //族去重
            var cbFamList2 = cbFamList.Distinct().ToList();

            foreach (var item in cbFamList2)
            {
                var famname = doc.GetElement(item) as Family;
                cbFamNameList.Add(famname.Name.ToString());

            }
            #region 筛选族
            var resultFamily = new List<Family>();
            int cbFNListNum = 0;
            foreach (var item in cbFamNameList)
            {
                if (item.Contains(selectCabName))
                {
                    resultFamily.Add(doc.GetElement(cbFamList2[cbFNListNum]) as Family);
                    cbFNListNum += 1;
                }
                else
                {
                    cbFNListNum += 1;
                }
            }
            #endregion

            #region 复制新建类型
            var creatTransaction = new Transaction(doc);
            creatTransaction.Start("复制新建类型");
            CreetFitting(resultFamily, cableNewName, doc);
            creatTransaction.Commit();
            #endregion
        }

        /// <summary>
        /// 复制桥架配件
        /// </summary>
        /// <param name="famlist"></param>
        /// <param name="newname"></param>
        /// <param name="doc"></param>
        public void CreetFitting(List<Family> famlist, string newname, Document doc)
        {
            foreach (var item in famlist)
            {
                var familySymSet = item.GetFamilySymbolIds();
                var familySym = doc.GetElement(familySymSet.First()) as FamilySymbol;
                familySym.Duplicate(newname);
            }
        }

        /// <summary>
        /// 生成管件
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="sourceSymboolName">复制源名称</param>
        /// <param name="newpipeFitting">新建名称</param>
        public void CreatpipFit(UIApplication app, string sourceSymboolName, string newpipeFitting)
        {
            var uidoc = app.ActiveUIDocument;
            var doc = uidoc.Document;
            // 新建过滤器
            var fittingCollector = new FilteredElementCollector(doc);
            //获取管道配件类型
            var pipeCollector = fittingCollector.OfCategory(BuiltInCategory.OST_PipeFitting).OfClass(typeof(FamilySymbol));

            var creatpipeTransaction = new Transaction(doc);
            creatpipeTransaction.Start("复制新建管件类型");
            foreach (var item in pipeCollector)
            {
                if (sourceSymboolName == item.Name.ToString())
                {
                    var itemSy = item as FamilySymbol;
                    itemSy.Duplicate(newpipeFitting);

                }
            }
            creatpipeTransaction.Commit();
        }


        

     
        public string GetName()
        {
            return "NewFitting";
        }
    }
}
