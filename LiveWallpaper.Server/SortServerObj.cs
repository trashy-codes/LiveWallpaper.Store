using Mvvm.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveWallpaper.Server
{
    public class SortServerObj : ObservableObj
    {
        public int ID { get; set; }

        #region Name

        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private string _Name;

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get { return _Name; }

            set
            {
                if (_Name == value) return;

                _Name = value;
                NotifyOfPropertyChange(NamePropertyName);
            }
        }

        #endregion
    }
}
