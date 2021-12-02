using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FamilyRename
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class CreatViewSectionWPF : Window
    {
        //返回值
        public ViewFamilyType SelectViewType { get; private set; }
        public CreatViewSectionWPF(List<ViewFamilyType> viewType)
        {
            InitializeComponent();
            
            viewtype.ItemsSource= viewType;
            viewtype.SelectedValuePath = "Id";
            viewtype.DisplayMemberPath = "Name";
            viewtype.SelectedIndex = 0;
            
        }

        private void viewtype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count>0)
            {
                var viewFamilyType = e.AddedItems?.Count > 0 ? (e.AddedItems[0] as ViewFamilyType) : null;
                if (viewFamilyType != null)
                    SelectViewType = viewFamilyType;
            }
            
            

        }

    }

   

}
