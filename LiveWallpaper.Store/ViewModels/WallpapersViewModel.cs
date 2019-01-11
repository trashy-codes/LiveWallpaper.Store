using EasyMvvm;
using LiveWallpaper.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveWallpaper.Store.ViewModels
{
    public class WallpapersViewModel : EasyViewModel
    {
        LocalServer _server;
        public WallpapersViewModel(LocalServer server)
        {
            _server = server;
        }
    }
}
