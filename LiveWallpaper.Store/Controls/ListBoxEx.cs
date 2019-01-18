using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LiveWallpaper.Store.Controls
{
    public class ListBoxEx : ListBox
    {
        public ListBoxEx()
        {
            Loaded += ListBoxEx_Loaded;
        }

        public event EventHandler ReachBottom;
        private DateTime lastRaiseTime;

        private void ListBoxEx_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= ListBoxEx_Loaded;
            List<ScrollBar> scrollBarList = GetVisualChildCollection<ScrollBar>(this);
            foreach (ScrollBar scrollBar in scrollBarList)
            {
                if (scrollBar.Orientation == Orientation.Horizontal)
                {
                    scrollBar.ValueChanged += new RoutedPropertyChangedEventHandler<double>(listBox_HorizontalScrollBar_ValueChanged);
                }
                else
                {
                    scrollBar.ValueChanged += new RoutedPropertyChangedEventHandler<double>(listBox_VerticalScrollBar_ValueChanged);
                }
            }
        }

        private void listBox_VerticalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScrollBar sb = sender as ScrollBar;
            if (sb.Value >= sb.Maximum - 50)
            {
                if (DateTime.Now - lastRaiseTime < TimeSpan.FromSeconds(2))
                    return;

                ReachBottom?.Invoke(this, new EventArgs());
                lastRaiseTime = DateTime.Now;
            }
        }

        private void listBox_HorizontalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        public static List<T> GetVisualChildCollection<T>(object parent) where T : Visual
        {
            List<T> visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }

        private static void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection) where T : Visual
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }
                else if (child != null)
                {
                    GetVisualChildCollection(child, visualCollection);
                }
            }
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            var oldList = oldValue as ICollection;
            var newList = newValue as ICollection;
            if ((oldList == null || oldList.Count == 0) && (newList != null && newList.Count > 0)
                || (oldList != null && newList != null && oldList.Count == newList.Count))
            {
                ScrollIntoView(Items[0]);
            }
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            //切换数据源后归位
            if (e.Action == NotifyCollectionChangedAction.Add && this.Items.Count == 1)
            {
                ScrollIntoView(this.Items[0]);
            }
        }
    }
}
