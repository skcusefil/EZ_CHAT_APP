using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace clientXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPageFlyout : ContentPage
    {
        public ListView ListView;

        public MainMenuPageFlyout()
        {
            InitializeComponent();

            BindingContext = new MainMenuPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class MainMenuPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainMenuPageFlyoutMenuItem> MenuItems { get; set; }

            public MainMenuPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MainMenuPageFlyoutMenuItem>(new[]
                {
                    new MainMenuPageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new MainMenuPageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new MainMenuPageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new MainMenuPageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new MainMenuPageFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}