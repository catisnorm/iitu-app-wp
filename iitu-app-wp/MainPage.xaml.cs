using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DevApp1.ViewModels;

namespace DevApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.NewsViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.MenuViewModel.IsDataLoaded)
            {
                App.MenuViewModel.LoadData();
            }
            if (!App.NewsViewModel.IsDataLoaded)
            {
                App.NewsViewModel.LoadData();
            }
        }

    }
}