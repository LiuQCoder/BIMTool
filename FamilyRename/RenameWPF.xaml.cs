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
    /// RenameWPF.xaml 的交互逻辑
    /// </summary>
    public partial class RenameWPF : Window
    {
        
        //注册外部事件
        RenameDemo reCommand = null;
        ExternalEvent reEvent = null;
        public RenameWPF()
        {
            InitializeComponent();

            reCommand = new RenameDemo();
            reEvent = ExternalEvent.Create(reCommand);
        }

        private void btnRename_Click(object sender, RoutedEventArgs e)
        {
            if (newname.Text !="")
            {
                //传递值到RenameDemo
                reCommand.newFaName = newname.Text;

                reEvent.Raise();
            }
            else
            {
                MessageBox.Show("输入的值为空！");
            }

        }
        //捕捉ESC退出WPF

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }
    }
}
