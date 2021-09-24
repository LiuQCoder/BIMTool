using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
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
    /// CFViewWPF.xaml 的交互逻辑
    /// </summary>
    public partial class CFViewWPF : Window
    {

        CFViewProgram CFViewCommand = null;
        ExternalEvent CFviewEvent = null;
        

        public CFViewWPF()
        {
            InitializeComponent();
            CFViewCommand = new CFViewProgram();
            CFviewEvent = ExternalEvent.Create(CFViewCommand);
          
        }

        private void ductVisiable_Click(object sender, RoutedEventArgs e)
        {
            CFViewCommand.buttonName = ductVisiable.Content.ToString();
            CFviewEvent.Raise();
        }

        private void pipVisiable_Click(object sender, RoutedEventArgs e)
        {
            CFViewCommand.buttonName = pipVisiable.Content.ToString();
            CFviewEvent.Raise();
        }

        private void cableVisiable_Click(object sender, RoutedEventArgs e)
        {
            CFViewCommand.buttonName =cableVisiable.Content.ToString();
            CFviewEvent.Raise();
        }

        private void insulationVisiable_Click(object sender, RoutedEventArgs e)
        {
            CFViewCommand.buttonName = insulationVisiable.Content.ToString();
            CFviewEvent.Raise();
        }

        private void grVisiable_Click(object sender, RoutedEventArgs e)
        {
            CFViewCommand.buttonName = grVisiable.Content.ToString();
            CFviewEvent.Raise();
        }

           

        private void nethighVisiable_Click(object sender, RoutedEventArgs e)
        {
            CFViewCommand.buttonName = nethighVisiable.Content.ToString();
            CFviewEvent.Raise();
        }

        
       

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) 
                this.Close();
              
        }

        private void seVisiable_Click(object sender, RoutedEventArgs e)
        {
            CFViewCommand.buttonName = seVisiable.Content.ToString();
            CFviewEvent.Raise();
        }
    }
}
