using EasyMvvm;
using LiveWallpaper.Store.ViewModels;
using LiveWallpaper.Store.Views;
using MultiLanguageManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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
            //多语言初始化
            string appDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string path = Path.Combine(appDir, "Assets\\Languages");
            LanService.Init(new JsonDB(path), true, "zh");

            //mvvm初始化
            IocContainer container = new IocContainer();
            container
                .SingletonDefault<WallpapersViewModel>()
                .SingletonDefault<SettingViewModel>()
                .SingletonDefault<AppMenuViewModel>();

            EasyManager.Initialize(container, new StoreNavigator());
            EasyManager.Associate<WallpapersView, WallpapersViewModel>();
            EasyManager.Associate<SettingView, SettingViewModel>();
        }
    }
}
