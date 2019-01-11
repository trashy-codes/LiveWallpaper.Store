using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace EasyMvvm
{
    public static class EasyManager
    {
        private static Dictionary<Type, Type> cacheAssociate = new Dictionary<Type, Type>();

        public static IocContainer IoC { get; private set; }
        public static INavigator Navigator { get; private set; }

        public static void Initialize(IocContainer container, INavigator navigator = null)
        {
            if (navigator == null)
                navigator = new Navigator();

            IoC = container;
            Navigator = navigator;
        }

        public static void Associate<View, VM>()
        {
            cacheAssociate[typeof(VM)] = typeof(View);
        }

        public static object GetView(object vm)
        {
            var type = vm.GetType();
            var viewType = cacheAssociate[type];
            //view 构造函数不能有参数
            var viewObj = IocContainer.ActivateInstance(viewType, null);
            var view = viewObj as FrameworkElement;
            view.DataContext = vm;
            return view;
        }

        //public static object GetViewModel(EasyView view)
        //{
        //    return new EasyViewModel();
        //}
    }
}
