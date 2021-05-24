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
    /// CadVisiableWPF.xaml 的交互逻辑
    /// </summary>
    public partial class CadVisiableWPF : Window
    {

        CadVisiable CadVisiableCommand = null;
        ExternalEvent CadEvent = null;
        public CadVisiableWPF()
        {
            InitializeComponent();
            CadVisiableCommand = new CadVisiable();
            CadEvent = ExternalEvent.Create(CadVisiableCommand);
        }

        private void CADcatPartOFF_Click(object sender, RoutedEventArgs e)
        {
            CadVisiableCommand.ButtonName = CADcatPartOFF.Content.ToString();
            CadEvent.Raise();
        }

        private void CADcatPartON_Click(object sender, RoutedEventArgs e)
        {
            CadVisiableCommand.ButtonName = CADcatPartON.Content.ToString();
            CadEvent.Raise();
        }

        private void CADcatAllOn_Click(object sender, RoutedEventArgs e)
        {
            CadVisiableCommand.ButtonName = CADcatAllOn.Content.ToString();
            CadEvent.Raise();
        }

        //捕获键盘，按下ESC退出
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();

        }



    }
}
