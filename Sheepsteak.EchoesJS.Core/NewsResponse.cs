using Newtonsoft.Json;

namespace Sheepsteak.EchoesJS.Core
{
    public class NewsResponse
    {
        [JsonProperty("news")]
        public Article[] Articles { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
