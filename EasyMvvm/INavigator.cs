using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMvvm
{
    public interface INavigator
    {
        void Show(object vm);

        void ShowDialog(object vm);

        void ShowPopup(object vm);
    }
}
