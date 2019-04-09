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
        public const string TipMessage = "官网有口令自己找，或者加qq群  641405255  找志愿者协助解锁。";
        public string WallpaperSaveDir { get; set; }
        public string SecretKey { get; set; }

        internal static GeneralSetting GetDefault()
        {
            string saveDir = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            saveDir = Path.Combine(saveDir, "LivewallpaperCache");
            return new GeneralSetting()
            {
                WallpaperSaveDir = saveDir,
                SecretKey = TipMessage
            };
        }
    }
}
