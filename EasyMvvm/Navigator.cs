using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace EasyMvvm
{
    public class Navigator : INavigator
    {
        public virtual void Show(object vm)
        {
            var view = EasyManager.GetView(vm);
            var window = view as Window;
            if (window == null)
                return;

            window.Show();
        }

        public virtual void ShowDialog(object vm)
        {
            var view = EasyManager.GetView(vm);
            var window = view as Window;
            if (window == null)
                return;

            window.ShowDialog();
        }

        public virtual void ShowPopup(object vm)
        {
            throw new NotImplementedException();
        }
    }
}
