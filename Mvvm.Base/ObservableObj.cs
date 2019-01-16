using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Mvvm.Base
{
    public class ObservableObj : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyOfPropertyChange(string propertyName)
        {
            var handle = PropertyChanged;
            if (handle == null)
                return;
            handle(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
