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
    /// NewFittingWPF.xaml 的交互逻辑
    /// </summary>
    public partial class NewFittingWPF : Window
    {
        public string SeleFamilyName { get; set; }

        public List<string> AAA { get; set; }


        public NewFittingWPF(List<string> CableFamilys)
        {
            InitializeComponent();
            AAA  = CableFamilys;
           
        }

        private void creatfit_Click(object sender, RoutedEventArgs e)
        {
               

        }

        private void pipebool_Checked(object sender, RoutedEventArgs e)
        {
            //cableTypeCB.IsEnabled = false;
            //cablename.IsEnabled = false;
        }

        private void cablebool_Checked(object sender, RoutedEventArgs e)
        {
            //pipeSourceName.IsEnabled = false;
            //pipename.IsEnabled = false;
        }

        private void cableTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    
}
