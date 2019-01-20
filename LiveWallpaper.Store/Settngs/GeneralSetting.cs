using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveWallpaper.Store.Settngs
{
    public class GeneralSetting
    {
        public string WallpaperSaveDir { get; set; }
        public string SecretKey { get; set; }

        internal static GeneralSetting GetDefault()
        {
            string saveDir = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            saveDir = Path.Combine(saveDir, "LivewallpaperCache");
            return new GeneralSetting()
            {
                WallpaperSaveDir = saveDir
            };
        }
    }
}
