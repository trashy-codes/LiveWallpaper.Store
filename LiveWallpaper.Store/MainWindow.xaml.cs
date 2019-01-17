using LiveWallpaper.Store.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LiveWallpaper.Store
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        private object lastContent;
        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (WallpapersView.LastPlayer != null)
                WallpapersView.LastPlayer.Dispose();
            base.OnClosing(e);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= MainWindow_Loaded;
            Content.Content = lastContent;
        }

        private void LstBoxMenu_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        internal void SetContent(UserControl control)
        {
            lastContent = control;
            if (Content != null)
                Content.Content = control;
        }
    }
}
