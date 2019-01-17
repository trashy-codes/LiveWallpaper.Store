using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace EasyMvvm
{
    public static class EasyManager
    {
        private static Dictionary<Type, Type> cacheAssociate = new Dictionary<Type, Type>();
        private static Dictionary<object, object> cacheView = new Dictionary<object, object>();

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

            if (vm is EasyViewModel)
            {
                if ((vm as EasyViewModel).SingletonView && cacheView.ContainsKey(vm))
                {
                    var existView = cacheView[vm];
                    if (existView != null)
                        return existView;
                }
            }

            //view 构造函数不能有参数
            var viewObj = IocContainer.ActivateInstance(viewType, null);
            var view = viewObj as FrameworkElement;
            view.DataContext = vm;
            cacheView[vm] = view;
            return view;
        }

        //public static object GetViewModel(EasyView view)
        //{
        //    return new EasyViewModel();
        //}
    }
}
