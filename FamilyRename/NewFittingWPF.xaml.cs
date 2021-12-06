using Autodesk.Revit.UI;
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
        //注册外部事件
        NewtFitting newFittingCommand = null;
        ExternalEvent newfittingEvent = null;
                
        public NewFittingWPF(List<string> CableFamilys)
        {
            InitializeComponent();
            cableTypeCB.ItemsSource = CableFamilys;
            cableTypeCB.SelectedIndex = 0;

            newFittingCommand = new NewtFitting();
            newfittingEvent = ExternalEvent.Create(newFittingCommand);
    
        }

        private void creatfit_Click(object sender, RoutedEventArgs e)
        {
            if (pipebool.IsChecked.ToString()=="True")
            {
                

                if (pipeSourceName.Text == ""  )
                {
                    TaskDialog.Show("提示", "复制源名称为空!");
                }
                else if (pipename.Text == "" )
                {
                    TaskDialog.Show("提示", "新建管件名称为空!");
                }
                else
                {
                    newFittingCommand.SourceSymboolName = pipeSourceName.Text;
                    newFittingCommand.NewpipeFitting = pipename.Text;
                    newfittingEvent.Raise();
                }
               
            }
            else
            {
               
                if (cablename.Text == "" )
                {
                    TaskDialog.Show("提示", "新建配件名称为空!");
                }
                else
                {
                    newFittingCommand.CableNewName = cablename.Text;
                    newFittingCommand.CableFamilySelect = cableTypeCB.Text;
                    newfittingEvent.Raise();
                }
                
            }
      

        }

       
        private void cableTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
           
        }

        private void pipebool_Click(object sender, RoutedEventArgs e)
        {
            //将新建配件Textbox恢复默认状态
            cablename.Text = "输入配件名称";
            //设置组件可用性
            newFittingCommand.PipeChecked = true;
            cableTypeCB.IsEnabled = false;
            cablename.IsEnabled = false;
            pipeSourceName.IsEnabled = true;
            pipename.IsEnabled = true;
            
        }

        private void cablebool_Click(object sender, RoutedEventArgs e)
        {
            //将新建管件Textbox恢复默认状态
            pipeSourceName.Text = "输入类型名称";
            pipename.Text = "输入管件名称";

            //设置组件可用性
            newFittingCommand.PipeChecked = false;
            pipeSourceName.IsEnabled = false;
            pipename.IsEnabled = false;
            cableTypeCB.IsEnabled = true;
            cablename.IsEnabled = true;
        }

        //激活TextBox时，将内容设置为空
        private void pipeSourceName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (pipeSourceName.IsFocused && pipeSourceName.Text == "输入类型名称")
            {
                pipeSourceName.Text = "";
            }     
        }


        //激活TextBox时，将内容设置为空
        private void pipename_GotFocus(object sender, RoutedEventArgs e)
        {
            if (pipename.IsFocused && pipename.Text == "输入管件名称")
            {
                pipename.Text = "";
            }
           
        }
        //激活TextBox时，将内容设置为空
        private void cablename_GotFocus(object sender, RoutedEventArgs e)
        {
            if (cablename.IsFocused && cablename.Text == "输入配件名称")
            {
                cablename.Text = "";
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();

        }



    }
    
}
