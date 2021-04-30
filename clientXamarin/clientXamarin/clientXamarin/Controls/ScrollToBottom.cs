using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace clientXamarin.Controls
{
    public class ExtendedListView : ListView
    {

        private INotifyCollectionChanged _previousObservableCollection;

        public static readonly BindableProperty ItemAppearingCommandProperty =
            BindableProperty.Create(nameof(ItemAppearingCommand), typeof(ICommand), typeof(ExtendedListView), default(ICommand));

        public ExtendedListView(ListViewCachingStrategy cachingStrategy)
            : base(cachingStrategy)
        {
        }

        public ExtendedListView()
            : base()
        {
            this.ItemAppearing += OnItemAppearing;

        }

        public ICommand ItemAppearingCommand
        {
            get { return (ICommand)GetValue(ItemAppearingCommandProperty); }
            set { SetValue(ItemAppearingCommandProperty, value); }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(ItemsSource))
            {
                if (_previousObservableCollection != null)
                {
                    _previousObservableCollection.CollectionChanged -= OnItemsSourceCollectionChanged;
                    _previousObservableCollection = null;
                }

                if (ItemsSource is INotifyCollectionChanged newObservableCollection)
                {
                    _previousObservableCollection = newObservableCollection;
                    newObservableCollection.CollectionChanged += OnItemsSourceCollectionChanged;
                }
            }
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (var item in e.NewItems)
                {
                    // Scroll to the item that has just been added/updated to make it visible
                    ScrollTo(item, ScrollToPosition.MakeVisible, true);
                }
            }
        }


        protected void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (ItemAppearingCommand != null)
            {
                ItemAppearingCommand?.Execute(e.Item);
            }
        }

        public void ScrollToFirst()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    if (ItemsSource != null && ItemsSource.Cast<object>().Count() > 0)
                    {
                        var firstItem = ItemsSource.Cast<object>().FirstOrDefault();
                        if (firstItem != null)
                        {
                            ScrollTo(firstItem, ScrollToPosition.Start, false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            });
        }

        public void ScrollToLast()
        {
            try
            {
                if (ItemsSource != null && ItemsSource.Cast<object>().Count() > 0)
                {
                    var lastItem = ItemsSource.Cast<object>().LastOrDefault();
                    if (lastItem != null)
                    {
                        ScrollTo(lastItem, ScrollToPosition.End, false);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}
