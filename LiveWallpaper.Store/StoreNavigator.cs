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
        public override void Show(object vm)
        {
            var view = EasyManager.GetView(vm);
            var control = view as UserControl;
            if (control == null)
                return;

            base.Show(vm);
        }
    }
}
