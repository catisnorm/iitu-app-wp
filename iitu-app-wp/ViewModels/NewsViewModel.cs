using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DevApp1.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;

namespace DevApp1.ViewModels
{
    public class NewsViewModel : INotifyPropertyChanged
    {
        public NewsViewModel()
        {
            this.Items = new ObservableCollection<NewsItemViewModel>();
        }

        public ObservableCollection<NewsItemViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            await GetNews();
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

        private async Task<List<NewsItem>> GetNews()
        {
            List<NewsItem> result = new List<NewsItem>();

            HttpWebRequest request = HttpWebRequest.CreateHttp("http://www.iitu.kz/lang/ru/feed/news/");

            HttpWebResponse response = await request.BeginGetResponse();


            return result;
        }

        class NewsItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime Published { get; set; }
            public string Link { get; set; }
            public string Image { get; set; }
        }
    }
}