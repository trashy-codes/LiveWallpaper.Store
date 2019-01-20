using DZY.DotNetUtil.Helpers;
using EasyMvvm;
using JsonConfiger;
using JsonConfiger.Models;
using Mvvm.Base;
using Newtonsoft.Json.Linq;
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
            //var config = await JsonHelper.JsonDeserializeFromFileAsync<dynamic>(_appManager.SettingPath);
            //string descPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Assets\\Configs\\setting.desc.json");
            //var descConfig = await JsonHelper.JsonDeserializeFromFileAsync<dynamic>(descPath);
            var config = await _appManager.LoadConfig();
            var desc = await _appManager.LoadDescConfig();
            JsonConfierViewModel = _jcrService.GetVM(JObject.FromObject(config), desc as JObject);
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

        #region Commands

        #region ApplyCommand

        private DelegateCommand _ApplyCommand;

        /// <summary>
        /// Gets the ApplyCommand.
        /// </summary>
        public DelegateCommand ApplyCommand
        {
            get
            {
                return _ApplyCommand ?? (_ApplyCommand = new DelegateCommand(ExecuteApplyCommand));
            }
        }

        private async void ExecuteApplyCommand()
        {
            var data = _jcrService.GetData(JsonConfierViewModel.Nodes);
            await _appManager.SaveConfig(data);
            var vm = EasyManager.IoC.Get<AppMenuViewModel>();
            vm.SelectedMenu = vm.Menus.FirstOrDefault(m => m.TargetType == typeof(WallpapersViewModel));
        }

        #endregion

        #endregion
    }
}
