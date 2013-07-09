using Newtonsoft.Json.Linq;
using Sheepsteak.Echo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sheepsteak.Echo
{
    public class EchoJsClient : HttpClient
    {
        public EchoJsClient()
        {
            this.BaseAddress = new Uri("http://www.echojs.com/api/");
        }

        public async Task<IEnumerable<Article>> GetLatestNews(int start = 0, int count = 32)
        {
            HttpResponseMessage response = null;

            response = await this.GetAsync("getnews/latest/" + start + "/" + count);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var jObject = JObject.Parse(json);

            var articles = (from a in jObject["news"]
                            select new Article()
                            {
                                Id = (int)a["id"],
                                DownVotes = (int)a["down"],
                                PostedAt = ConvertFromUnixTimestamp((long)a["ctime"]),
                                Title = (string)a["title"],
                                UpVotes = (int)a["up"],
                                Url = (string)a["url"],
                                Username = (string)a["username"]
                            }).ToList();

            return articles;
        }

        public async Task<IEnumerable<Article>> GetTopNews(int start = 0, int count = 32)
        {
            HttpResponseMessage response = null;

            response = await this.GetAsync("getnews/top/" + start + "/" + count);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var jObject = JObject.Parse(json);

            var articles = (from a in jObject["news"]
                            select new Article()
                            {
                                Id = (int)a["id"],
                                DownVotes = (int)a["down"],
                                PostedAt = ConvertFromUnixTimestamp((long)a["ctime"]),
                                Title = (string)a["title"],
                                UpVotes = (int)a["up"],
                                Url = (string)a["url"],
                                Username = (string)a["username"]
                            }).ToList();

            return articles;
        }

        private static DateTime ConvertFromUnixTimestamp(long timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


    }
}
