using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Sheepsteak.Echoes.Core
{
    public class EchoJsClient : HttpClient
    {
        public EchoJsClient()
            : base(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })
        {
            this.BaseAddress = new Uri("http://www.echojs.com/api/");
            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Comment>> GetCommentsForArticle(int articleId)
        {
            HttpResponseMessage response = null;

            response = await this.GetAsync("getcomments/" + articleId);

            response.EnsureSuccessStatusCode();

            var latest = await response.Content.ReadAsAsync<CommentsResponse>();
            return latest.Comments;
        }

        public async Task<IEnumerable<Article>> GetLatestNews(int start = 0, int count = 20)
        {
            HttpResponseMessage response = null;

            response = await this.GetAsync("getnews/latest/" + start + "/" + count);

            response.EnsureSuccessStatusCode();

            var latest = await response.Content.ReadAsAsync<NewsResponse>();
            return latest.Articles;
        }

        public async Task<IEnumerable<Article>> GetTopNews(int start = 0, int count = 20)
        {
            HttpResponseMessage response = null;

            response = await this.GetAsync("getnews/top/" + start + "/" + count);

            response.EnsureSuccessStatusCode();

            var latest = await response.Content.ReadAsAsync<NewsResponse>();
            return latest.Articles;
        }
    }
}
