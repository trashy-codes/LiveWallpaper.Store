using EasyMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LiveWallpaper.Store
{
    public class StoreNavigator : Navigator
    {
        object currentVM;
        public override void Show(object vm)
        {
            if (currentVM == vm)
                return;

            currentVM = vm;
            var view = EasyManager.GetView(vm);
            var control = view as UserControl;
            if (control == null)
                return;

            control.Loaded += Control_Loaded;
            control.Tag = vm;

            MainWindow.Instance.SetContent(control);
        }

        private void Control_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var control = sender as UserControl;
            control.Loaded -= Control_Loaded;

            var tmpVM = control.Tag as EasyViewModel;
            if (tmpVM != null)
            {
                tmpVM.OnViewLoaded();
            }
        }
    }
}
