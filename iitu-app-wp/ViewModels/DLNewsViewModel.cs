using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevApp1.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace DevApp1.ViewModels
{
    public class DLNewsViewModel
    {
        public static string htmlContent = "";
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
            
            string curDir = Directory.GetCurrentDirectory();

            using (Stream stream1 = File.Open(String.Format(@"{0}\html_dl_item.html", curDir), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                StreamWriter wr = new StreamWriter(stream1);
                wr.WriteLine("<div class=\"post\">");
                wr.WriteLine("  <div class=\"header\">");
                wr.WriteLine("      <div class=\"title\">{title}</div>");
                wr.WriteLine("      <div class=\"date\">{date}</div>");
                wr.WriteLine("  </div>");
                wr.WriteLine("  <div class=\"text\">{text}</div>");
                wr.WriteLine("</div>");
            }
            
            Stream stream = File.Open(String.Format(@"{0}\html_dl_item.html", curDir), FileMode.Open);
            StreamReader streamReader = new StreamReader(stream);
            string itemtpl = streamReader.ReadToEnd();
            
            StringReader strReader = new StringReader(obj);
            JsonTextReader reader = new JsonTextReader(strReader);

            string title = null;
            string content;
            string published = null;

            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    if (reader.Value.Equals("title"))
                    {
                        reader.Read();
                        title = (string)reader.Value;

                    }
                    else
                    {
                        if (reader.Value.Equals("published"))
                        {
                            reader.Read();
                            published = (string)reader.Value;

                        }
                        else
                        {
                            if (reader.Value.Equals("content"))
                            {
                                reader.Read();
                                content = (string)reader.Value;
                                this.Items.Add(new DLNewsItemViewModel()
                                {
                                    Title = title,
                                    Content = content,
                                    Published = DateTime.Parse(published),
                                });

                                string currentItem = itemtpl;
                                currentItem = currentItem.Replace("{title}", title).Replace("{text}", content).Replace("{date}", published);
                                htmlContent += currentItem;
                            }
                        }
                    }
                }
            }

            this.IsDataLoaded = true;
        }
    }
}