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
    public partial class MenuControl : UserControl
    {
        public MenuControl()
        {
            InitializeComponent();

            DataContext = App.MenuViewModel;
            if (!App.MenuViewModel.IsDataLoaded)
                App.MenuViewModel.LoadData();
        }

        private void MenuItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel panel = sender as StackPanel;

            var item = from p in App.MenuViewModel.Items
                       where p.Title == (string)panel.Tag
                       select p.ClickAction;
            
            var action = item.FirstOrDefault<Action>();
            
            if(action != null)
                action();
        }
    }
}
