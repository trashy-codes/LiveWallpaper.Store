using DZY.DotNetUtil.CommonHelpers;
using DZY.DotNetUtil.WPF;
using LiveWallpaper.Store.Settngs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LiveWallpaper.Store
{
    public class AppManager : BaseAppManager
    {
        public AppManager() : base("LiveWallpaperStore")
        {
            Init();
        }

        #region private methods

        private async void Init()
        {
            await LoadConfig();
            ConfigDescFilePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Assets\\Configs\\setting.desc.json");
            ConfigDescConfig = await JsonHelper.JsonDeserializeFromFileAsync<dynamic>(ConfigDescFilePath);
        }

        #endregion

        #region public methods

        public async Task<SettingObject> LoadConfig()
        {
            var config = await JsonHelper.JsonDeserializeFromFileAsync<SettingObject>(SettingPath);

            if (config == null)
            {
                config = SettingObject.GetDefaultSetting();
            }
            config.CheckDefaultSetting();

            Setting = config;
            return config;
        }

        internal async Task SaveConfig(object data)
        {
            await JsonHelper.JsonSerializeAsync(data, SettingPath);
            await LoadConfig();
        }

        public async Task<Object> LoadDescConfig()
        {
            var config = await JsonHelper.JsonDeserializeFromFileAsync<dynamic>(ConfigDescFilePath);
            return config;
        }

        #endregion

        #region properties

        public string ConfigDescFilePath { get; private set; }
        public dynamic ConfigDescConfig { get; private set; }
        public SettingObject Setting { get; private set; }

        #endregion

    }
}
