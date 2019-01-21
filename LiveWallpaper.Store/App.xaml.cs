using EasyMvvm;
using LiveWallpaper.Server;
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
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public App()
        {
            //异常捕获
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;

            //多语言初始化
            string appDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string path = Path.Combine(appDir, "Assets\\Languages");
            LanService.Init(new JsonDB(path), true, "zh");

            //mvvm初始化
            IocContainer container = new IocContainer();
            container
                .Singleton<LocalServer>()
                .Singleton<WallpapersViewModel>()
                .Singleton<SettingViewModel>()
                .Singleton<AppMenuViewModel>();

            AppManager appManager = new AppManager();
            container.Instance(appManager);

            EasyManager.Initialize(container, new StoreNavigator());
            EasyManager.Associate<WallpapersView, WallpapersViewModel>();
            EasyManager.Associate<SettingView, SettingViewModel>();
        }


        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            logger.Error(ex);
            MessageBox.Show(ex.Message);
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var ex = e.Exception;
            logger.Error(ex);
            MessageBox.Show(ex.Message);
        }
    }
}
