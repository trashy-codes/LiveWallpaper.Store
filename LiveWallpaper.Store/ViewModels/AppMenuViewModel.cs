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
    }
}
