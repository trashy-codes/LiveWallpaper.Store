using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace EasyMvvm.Xaml
{
    public static class EasyBind
    {
        #region Model

        public static string GetModel(DependencyObject obj)
        {
            return (string)obj.GetValue(ModelProperty);
        }

        public static void SetModel(DependencyObject obj, string value)
        {
            obj.SetValue(ModelProperty, value);
        }

        // Using a DependencyProperty as the backing store for Model.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.RegisterAttached("Model", typeof(string), typeof(EasyBind), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
            {
                var control = sender as FrameworkElement;
                var model = control.GetValue(ModelProperty);
                var vm = EasyManager.IoC.Get(model.ToString());
                control.DataContext = vm;
            })));

        //private void ModelChanged(object newValue)
        //{

        //}

        #endregion
    }
}
