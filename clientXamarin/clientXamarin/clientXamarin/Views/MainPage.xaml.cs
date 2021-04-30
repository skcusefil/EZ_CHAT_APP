using clientXamarin.Services;
using clientXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace clientXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        NavigationService _page = new NavigationService();

        public MainPage()
        {
            InitializeComponent();

            ViewModel = new MainViewModel(_page);

        }

        public MainViewModel ViewModel
        {
            get => BindingContext as MainViewModel; 
            set => BindingContext = value; 
        }

        protected override void OnAppearing()
        {
            ViewModel = new MainViewModel(_page);

            base.OnAppearing();
        }

    }
}