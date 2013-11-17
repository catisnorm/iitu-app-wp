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
            GetNews();
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

        private List<NewsItem> GetNews()
        {
            List<NewsItem> result = new List<NewsItem>();

            Web.MakeRequest("http://www.iitu.kz/lang/ru/feed/news/", onLoadCompleted);

           // HttpWebResponse response = await request.BeginGetResponse();


            return result;
        }

        private void onLoadCompleted(string obj)
        {
            string response = obj;
            //XmlReader reader = XmlReader.Create(response);
            NewsItem news = new NewsItem();
            XDocument doc = XDocument.Parse(response);

            foreach (XElement channel in doc.Descendants("channel"))
            {
                foreach(XElement item in doc.Descendants("item"))
                {
                    DateTime date = DateTime.ParseExact(item.Element("pubDate").Value.ToString(), "ddd, dd MMM yyyy HH:mm:ss K", CultureInfo.InvariantCulture);
                    this.Items.Add(new NewsItemViewModel()
                    {
                        Image = @"http://www.iitu.kz/uploads/news/" + date.Year + "/"+ date.Month + "_" + date.Day + "/" + item.Element("img").Value.ToString() + ".png",
                        Title = item.Element("title").Value,
                        Description = item.Element("description").Value,
                        Published = date
                    });
                }
            }
            //this.Items.Add(new NewsItemViewModel());

            this.IsDataLoaded = true;
        }

        class NewsItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime Published { get; set; }
            public string Link { get; set; }
            public string Image { get; set; }
        }

        public object loadCompleted { get; set; }
    }
}