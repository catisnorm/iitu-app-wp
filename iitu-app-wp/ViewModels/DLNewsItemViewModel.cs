using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DevApp1.ViewModels
{
    [JsonObject]
    public class DLNewsItemViewModel : INotifyPropertyChanged
    {
        private string _title;
        
        [JsonProperty("title")]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private string _content;
        
        [JsonProperty("content")]
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (value != _content)
                {
                    _content = value;
                    NotifyPropertyChanged("Content");
                }
            }
        }


        private DateTime _published;

        [JsonProperty("published")]
        public DateTime Published
        {
            get
            {
                return _published;
            }
            set
            {
                if (value != _published)
                {
                    _published = value;
                    NotifyPropertyChanged("Published");
                }
            }
        }

        /*private string _filelink;
        
        [JsonProperty]
        public string FileLink
        {
            get
            {
                return _filelink;
            }
            set
            {
                if (value != _filelink)
                {
                    _filelink = value;
                    NotifyPropertyChanged("FileLink");
                }
            }
        }*/

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
