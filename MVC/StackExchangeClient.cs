using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace MVC
{
    public struct Data<T>
    {
        public T[] Items;
    }

    public struct Tag
    {
        public string Name;
        public int Count;
    }

    public class StackExchangeClient
    {
        public readonly string Version;
        public readonly string Site;

        public StackExchangeClient(string version, string site)
        {
            Version = version;
            Site = site;
        }

        private T SendRequest<T>(string type, Dictionary<string, string> queryParams)
        {
            try
            {
                queryParams["site"] = Site;
                string query = string.Join("&", queryParams.Select(m => m.Key + "=" + m.Value));
                string url = string.Format("https://api.stackexchange.com/{0}/{1}?{2}", Version, type, query);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                object json = null;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string html = reader.ReadToEnd();
                    json = JsonConvert.DeserializeObject<T>(html);
                }
                return (T)json;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Tag[] GetTags(uint page = 1, uint pageSize = 100, string order = "desc", string sort = "popular")
        {
            var queryParams = new Dictionary<string, string>()
            {
                { "page", page.ToString() },
                { "pagesize", pageSize.ToString() },
                { "order", order },
                { "sort", sort },
            };
            Data<Tag> data = SendRequest<Data<Tag>>("tags", queryParams);
            return data.Items;
        }
    }
}
