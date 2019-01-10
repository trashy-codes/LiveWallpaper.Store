using EasyMvvm;
using LiveWallpaper.Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LiveWallpaper.Store
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            IocContainer container = new IocContainer();
            container
                .Singleton<WallpapersViewModel>()
                .Singleton<AppMenuViewModel>();

            EasyManager.Initialize(container);
        }
    }
}
