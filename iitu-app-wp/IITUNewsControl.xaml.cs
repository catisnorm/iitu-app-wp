using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace DevApp1
{
    public partial class IITUNewsControl : UserControl
    {
        public IITUNewsControl()
        {
            InitializeComponent();

            DataContext = App.NewsViewModel;
            if (!App.NewsViewModel.IsDataLoaded)
                App.NewsViewModel.LoadData();
        }

        private void Item_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
        }
    }
}
