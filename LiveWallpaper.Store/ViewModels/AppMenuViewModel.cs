using EasyMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveWallpaper.Store.ViewModels
{
    public class MenuObj
    {
        public string Name { get; set; }

        public Type TargetType { get; set; }
    }

    public class AppMenuViewModel : EasyObservableObject
    {
        public AppMenuViewModel()
        {
            Menus = new List<MenuObj>()
            {
                new MenuObj(){
                    Name="壁纸",
                    TargetType=typeof(WallpapersViewModel)
                },
                new MenuObj(){
                    Name ="设置",
                    TargetType=typeof(SettingViewModel)
                }
            };
        }

        #region Menus

        /// <summary>
        /// The <see cref="Menus" /> property's name.
        /// </summary>
        public const string MenusPropertyName = "Menus";

        private List<MenuObj> _Menus;

        /// <summary>
        /// Menus
        /// </summary>
        public List<MenuObj> Menus
        {
            get { return _Menus; }

            set
            {
                if (_Menus == value) return;

                _Menus = value;
                NotifyOfPropertyChange(MenusPropertyName);
            }
        }

        #endregion

        #region SelectMenuCommand

        private DelegateCommand<MenuObj> _SelectMenuCommand;

        /// <summary>
        /// Gets the SelectMenuCommand.
        /// </summary>
        public DelegateCommand<MenuObj> SelectMenuCommand
        {
            get
            {
                return _SelectMenuCommand ?? (_SelectMenuCommand = new DelegateCommand<MenuObj>(ExecuteSelectMenuCommand, CanExecuteSelectMenuCommand));
            }
        }

        private void ExecuteSelectMenuCommand(MenuObj parameter)
        {
            var vm = EasyManager.IoC.GetDefault(parameter.TargetType);
            EasyManager.Navigator.Show(vm);
        }

        private bool CanExecuteSelectMenuCommand(MenuObj parameter)
        {
            return true;
        }

        #endregion
    }
}
