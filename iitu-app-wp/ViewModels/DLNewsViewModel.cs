using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevApp1.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevApp1.ViewModels
{
    public class DLNewsViewModel
    {
        public DLNewsViewModel()
        {
            this.Items = new ObservableCollection<DLNewsItemViewModel>();
        }

        public ObservableCollection<DLNewsItemViewModel> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            Web.MakeRequest(@"http://appiitu.hikki.kz/api/dlnews_list?os=wp&uid=6a5b02ea266958b2a2552561abc5078a", onLoadCompleted);
        }
        
        private void onLoadCompleted(string obj)
        {
            var jUserObject = JsonConvert.DeserializeObject<DLNewsItemViewModel>(obj);
            

            this.Items.Add(new DLNewsItemViewModel()
            {
                Title = jUserObject.Title,
                Content = jUserObject.Content,
                Published = jUserObject.Published,
                //FileLink = jUserObject.FileLink
            });

            this.IsDataLoaded = true;
        }
        /*
        [JsonObject]
        class DlNewsItem
        {
            [JsonProperty]
            public string Title { get; set; }
            [JsonProperty]
            public string Content { get; set; }
            [JsonProperty]
            public DateTime Published { get; set; }
            [JsonProperty]
            public string FileLink { get; set; }
        }*/
    }
}
