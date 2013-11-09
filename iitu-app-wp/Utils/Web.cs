using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevApp1.Utils
{
    class Web
    {
        public static void MakeRequest(string url, Action<string> callback)
        {
            try
            {
                WebClient client = new WebClient();

                client.DownloadStringCompleted += client_DownloadStringCompleted;
                client.DownloadStringAsync(new Uri(url), callback);
            }
            catch (Exception e)
            {

            }
        }

        private static void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string result = e.Result;
            var callback = (Action<string>)e.UserState;
            if (callback != null)
                callback(result);
        }

        private static void endResponse(IAsyncResult ar)
        {
            throw new NotImplementedException();
        }
    }
}
