using DZY.DotNetUtil.Helpers;
using EasyMvvm;
using JsonConfiger;
using JsonConfiger.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveWallpaper.Store.ViewModels
{
    public class SettingViewModel : EasyViewModel
    {
        JCrService _jcrService = new JCrService();
        AppManager _appManager;

        public SettingViewModel(AppManager appManager)
        {
            _appManager = appManager;
        }

        public override async void OnViewLoaded()
        {
            var config = await JsonHelper.JsonDeserializeFromFileAsync<dynamic>(_appManager.SettingPath);
            string descPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Assets\\Configs\\setting.desc.json");
            var descConfig = await JsonHelper.JsonDeserializeFromFileAsync<dynamic>(descPath);
            JsonConfierViewModel = _jcrService.GetVM(config, descConfig);
        }

        #region properties

        #region JsonConfierViewModel

        /// <summary>
        /// The <see cref="JsonConfierViewModel" /> property's name.
        /// </summary>
        public const string JsonConfierViewModelPropertyName = "JsonConfierViewModel";

        private JsonConfierViewModel _JsonConfierViewModel;

        /// <summary>
        /// JsonConfierViewModel
        /// </summary>
        public JsonConfierViewModel JsonConfierViewModel
        {
            get { return _JsonConfierViewModel; }

            set
            {
                if (_JsonConfierViewModel == value) return;

                _JsonConfierViewModel = value;
                NotifyOfPropertyChange(JsonConfierViewModelPropertyName);
            }
        }

        #endregion

        #endregion
    }
}
