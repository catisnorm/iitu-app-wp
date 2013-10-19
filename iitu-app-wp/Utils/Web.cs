using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DevApp1.Utils
{
    class Web
    {

        static async Task<string> MakeRequest(string url)
        {
            try
            {
                WebClient client = new WebClient();

                client.DownloadStringAsync(new Uri(url));


                HttpWebRequest request = WebRequest.CreateHttp(url);

                string result;


                HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;

                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    result = reader.ReadToEnd();
                    reader.Close();
                }


                return result;
            }
            catch (Exception e)
            {

            }

            return string.Empty;
        }
    }
}
