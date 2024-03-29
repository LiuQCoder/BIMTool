﻿using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FamilyRename
{
    class RibbonCreation : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            var tabName = "BIM_Lq";
            //创建选项卡
            application.CreateRibbonTab(tabName);
            var panel = application.CreateRibbonPanel(tabName, "小工具");
            //获取程序路径
            var assemblyType = new RenameMain().GetType();
            var location = assemblyType.Assembly.Location;
            //获取类名全称
            var fullName = assemblyType.FullName;

            //创建按钮 修改族名称
            var pushButtonData = new PushButtonData("rename", "修改族名称",location,fullName);
            //创建图标
            var imagePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\icon\newname.png";
            pushButtonData.LargeImage = new BitmapImage(new Uri(imagePath));
            //将图标添加到panel
            var pushButton = panel.AddItem(pushButtonData) as PushButton;

            //2.添加分隔符 创建剖面
            panel.AddSeparator();

            var createViewsType = new CreateViewSection().GetType();
            var cvslocation = createViewsType.Assembly.Location;
            //获取类名全称
            var cvsfullName = createViewsType.FullName;
            //创建按钮 创建剖面
            var cvsButtonData = new PushButtonData("createViewSection", "剖面 Pro",cvslocation, cvsfullName);
            //创建图标
            var cvsimagePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\icon\csv.png";
            cvsButtonData.LargeImage = new BitmapImage(new Uri(cvsimagePath));
            //将图标添加到panel
            var cvspushButton = panel.AddItem(cvsButtonData) as PushButton;
           


            //3.添加分隔符 VVplus
            panel.AddSeparator();

            var CFviewType = new CFVMain().GetType();
            var CFVLocation = CFviewType.Assembly.Location;
            //获取类名全称
            var CFVfullName = CFviewType.FullName;
            //创建按钮
            var CFVButtonData = new PushButtonData("cfviewProgram", "VV Plus", CFVLocation, CFVfullName);
            //创建图标
            var cfvimagePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\icon\隐藏.png";
            CFVButtonData.LargeImage = new BitmapImage(new Uri(cfvimagePath));
            //将图标添加到panel
            var cfvpushButton = panel.AddItem(CFVButtonData) as PushButton;

            //4.添加分隔符 CAD图层控制
            panel.AddSeparator();
            var CadVisiableType = new CadVisiableMain().GetType();
            var CadVisiableLocation = CadVisiableType.Assembly.Location;
            //获取类全名
            var CadvisiableFName = CadVisiableType.FullName;
            //创建按钮
            var CadVbButtonData = new PushButtonData("CadVisiable", "CAD图层控制", CadVisiableLocation, CadvisiableFName);
            //创建图标
            var CadVbImagePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\icon\图层.png";
            CadVbButtonData.LargeImage = new BitmapImage(new Uri(CadVbImagePath));
            //将图标添加到Panel
            var CadVbpushButton = panel.AddItem(CadVbButtonData) as PushButton;

            //5.添加分隔符 批量配件
            panel.AddSeparator();

            var NewFittingType = new NewFittingMain().GetType();
            var NewFittingLocation = NewFittingType.Assembly.Location;
            //获取类全名
            var NewFittingFName = NewFittingType.FullName;
            //创建按钮
            var NFButtonData = new PushButtonData("NewFitting", "批量配件", NewFittingLocation, NewFittingFName);
            //创建图标
            var NFbImagePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\icon\创建配件.png";
            NFButtonData.LargeImage = new BitmapImage(new Uri(NFbImagePath));
            //将图标添加到Panel
            var NFButton = panel.AddItem(NFButtonData) as PushButton;

            //6.添加分隔符 剖面框旋转
            panel.AddSeparator();

            var ViewTranType = new ViewsectionTran().GetType();
            var VTLocation = ViewTranType.Assembly.Location;
            //获取类全名
            var VTFName = ViewTranType.FullName;
            //创建按钮
            var VTButtonData = new PushButtonData("ViewTran", "剖面框旋转", VTLocation, VTFName);
            //创建图标
            var VTbImagePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\icon\3d.png";
            VTButtonData.LargeImage = new BitmapImage(new Uri(VTbImagePath));
            //将图标添加到Panel
            var VTButton = panel.AddItem(VTButtonData) as PushButton;
            //设置可用性
            VTButton.AvailabilityClassName = "FamilyRename.VTButtonEnable";


            return Result.Succeeded;
        }
    }
}
