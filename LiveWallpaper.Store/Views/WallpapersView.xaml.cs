using MpvPlayer;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LiveWallpaper.Store.Views
{
    /// <summary>
    /// WallpapersView.xaml 的交互逻辑
    /// </summary>
    public partial class WallpapersView : UserControl
    {
        public static MpvPlayerControl LastPlayer { private set; get; }
        public WallpapersView()
        {
            InitializeComponent();
            LastPlayer = player;
            Unloaded += WallpapersView_Unloaded;
            Loaded += WallpapersView_Loaded;
            //player.Play(@"C:\Users\zy\Videos\Captures\No Man's Sky 2018_10_19 12_30_37.mp4");
        }

        private void WallpapersView_Loaded(object sender, RoutedEventArgs e)
        {
            player.Resume();
        }

        private void WallpapersView_Unloaded(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }
    }
}
