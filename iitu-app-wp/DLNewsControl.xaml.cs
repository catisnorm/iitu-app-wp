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
    public partial class DLNewsControl : UserControl
    {
        public DLNewsControl()
        {
            InitializeComponent();

            DataContext = App.DLNewsViewModel;
            if (!App.DLNewsViewModel.IsDataLoaded)
                App.DLNewsViewModel.LoadData();
        }
    }
}
