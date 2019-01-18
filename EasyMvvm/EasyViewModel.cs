using Mvvm.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMvvm
{
    public class EasyViewModel : ObservableObj
    {
        /// <summary>
        /// view是否启动单实例
        /// </summary>
        public bool SingletonView { get; set; } = false;

        /// <summary>
        /// view已加载
        /// </summary>
        public virtual void OnViewLoaded()
        {

        }
    }
}
