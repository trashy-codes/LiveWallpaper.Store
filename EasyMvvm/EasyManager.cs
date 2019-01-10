using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMvvm
{
    public static class EasyManager
    {
        public static IocContainer IoC { get; private set; }

        public static void Initialize(IocContainer container)
        {
            IoC = container;
        }

        public static void Associate<View, VM>(View view, VM viewModel) where View : EasyView where VM : EasyViewModel
        {

        }

        public static View GetView<View, VM>(VM vm) where View : EasyView where VM : EasyViewModel
        {
            return new EasyView() as View;
        }

        public static VM GetViewModel<View, VM>(View view) where View : EasyView where VM : EasyViewModel
        {
            return new EasyViewModel() as VM;
        }

    }
}
