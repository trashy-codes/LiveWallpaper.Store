using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiveWallpaper.Server
{
    public interface IWallpaperServer
    {
        void UnLock(string pwd);

        Task<List<TagServerObj>> GetTags();
        Task<List<SortServerObj>> GetSorts();
        Task<List<WallpaperServerObj>> GetWallpapers(int tag, int sort, int page);
    }
}
