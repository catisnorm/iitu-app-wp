using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DevApp1.Resources;
using Microsoft.Phone.Controls;

namespace DevApp1.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public MenuViewModel()
        {
            this.Items = new ObservableCollection<MenuItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<MenuItemViewModel> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            // Sample data; replace with real data
            this.Items.Add(new MenuItemViewModel()
            {
                Title = "Новости iitu.kz",
                ClickAction = delegate
                {
                    App.Current.RootVisual = new MainPage().IITUnews;
                }
            });

            this.Items.Add(new MenuItemViewModel()
            {
                Title = "Новости DL",
                ClickAction = delegate
                {
                    App.Current.RootVisual = new MainPage();
                }
            });
            this.Items.Add(new MenuItemViewModel()
            {
                Title = "Выход",
                ClickAction = () =>
                {
                    App.Current.Terminate();
                }
            });

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}