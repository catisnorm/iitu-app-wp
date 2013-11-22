using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DevApp1.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using DevApp1.Utils;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;

namespace DevApp1.ViewModels
{
    public class NewsViewModel : INotifyPropertyChanged
    {
        public NewsViewModel()
        {
            this.Items = new ObservableCollection<NewsItemViewModel>();
        }

        public ObservableCollection<NewsItemViewModel> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            Web.MakeRequest("http://www.iitu.kz/lang/ru/feed/news/", onLoadCompleted);
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

        private void onLoadCompleted(string obj)
        {
            XDocument doc = XDocument.Parse(obj);

            foreach (XElement item in doc.Descendants("item"))
            {
                DateTime date = DateTime.ParseExact(item.Element("pubDate").Value.ToString(), "ddd, dd MMM yyyy HH:mm:ss K", CultureInfo.InvariantCulture);
                this.Items.Add(new NewsItemViewModel()
                {
                    Image = @"http://www.iitu.kz/uploads/news/" + date.Year + "/" + date.Month + "_" + date.Day + "/" + item.Element("img").Value.ToString() + ".png",
                    Title = item.Element("title").Value,
                    Description = item.Element("description").Value,
                    Published = date
                });
            }

            this.IsDataLoaded = true;
        }
        /*
        class NewsItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime Published { get; set; }
            public string Link { get; set; }
            public string Image { get; set; }
        }*/
    }
}