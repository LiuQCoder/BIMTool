using Autodesk.Revit.DB.Macros;
using Autodesk.Revit.UI;
using Autodesk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Gma.System.MouseKeyHook;
using System.Windows.Forms;

namespace FamilyRename
{
    /// <summary>
    /// CadVisiableWPF.xaml 的交互逻辑
    /// </summary>
    public partial class CadVisiableWPF : Window
    {

        CadVisiable CadVisiableCommand = null;
        ExternalEvent CadEvent = null;


        private IKeyboardMouseEvents m_GlobalHook;

        public CadVisiableWPF()
        {
            InitializeComponent();
            CadVisiableCommand = new CadVisiable();
            CadEvent = ExternalEvent.Create(CadVisiableCommand);
            //注册KeyDown事件，捕获Esc来关闭WPF
           this.KeyDown += Window_KeyDown;
            //运行钩子
          
            Subscribe();
            //关闭窗体后 注销钩子
            Unsubscribe();
            //窗体激活事件
            //this.Activated += CadVisiableWPF_Activated;

        }
        //窗体激活时所有按键可用
        //private void CadVisiableWPF_Activated(object sender, EventArgs e)
        //{
        //    CADcatPartOFF.IsEnabled = true;
        //    CADcatPartOFF.Opacity = 1;
        //    CADcatPartON.IsEnabled = true;
        //    CADcatPartON.Opacity = 1;
        //    CADcatAllOn.IsEnabled = true;
        //    CADcatAllOn.Opacity = 1;
        //}


        //安装钩子
        public static class WindowsHelper
        {
            [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
            public static extern bool EnumChildWindows(IntPtr hwndParent, CallBack lpEnumFunc, IntPtr lParam);
            public delegate bool CallBack(IntPtr hwnd, int lParam);
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpText, int nCount);
            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
            [DllImport("user32.dll", EntryPoint = "SendMessageA")]
            public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        }
        //设置钩子，捕获鼠标右键单击 和空格键
        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32) { CompleteMultiSelection(); }
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) { CompleteMultiSelection(); }

            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        //获取Revit 主窗体下的所有句柄，找到“完成”按钮并发送Click消息
        private void CompleteMultiSelection()
        {
            //需要添加Revit 的AdWindows.dll引用
            var rvtwindow = Autodesk.Windows.ComponentManager.ApplicationWindow;
            var list = new List<IntPtr>();
            var flag = WindowsHelper.EnumChildWindows(rvtwindow,
                       (hwnd, l) =>
                       {
                           StringBuilder windowText = new StringBuilder(200);
                           WindowsHelper.GetWindowText(hwnd, windowText, windowText.Capacity);
                           StringBuilder className = new StringBuilder(200);
                           WindowsHelper.GetClassName(hwnd, className, className.Capacity);
                           if ((windowText.ToString().Equals("完成", StringComparison.Ordinal) ||
                          windowText.ToString().Equals("Finish", StringComparison.Ordinal)) &&
                          className.ToString().Contains("Button"))
                           {
                               list.Add(hwnd);
                               return false;
                           }
                           return true;
                       }, new IntPtr(0));

            var complete = list.FirstOrDefault();
            WindowsHelper.SendMessage(complete, 245, 0, 0);
        }


        private void CADcatPartOFF_Click(object sender, RoutedEventArgs e)
        {
           
            CadVisiableCommand.ButtonName = CADcatPartOFF.Content.ToString();
            CadEvent.Raise();
            this.Close();

        }

        private void CADcatPartON_Click(object sender, RoutedEventArgs e)
        {
            
            CadVisiableCommand.ButtonName = CADcatPartON.Content.ToString();
            CadEvent.Raise();
            this.Close();




        }

        private void CADcatAllOn_Click(object sender, RoutedEventArgs e)
        {
            
            CadVisiableCommand.ButtonName = CADcatAllOn.Content.ToString();
            CadEvent.Raise();
            this.Close();
        }

        //捕获键盘，按下ESC退出
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();

        }
        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;

            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }


    }
}
