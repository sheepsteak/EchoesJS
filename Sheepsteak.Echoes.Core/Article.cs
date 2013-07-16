using Newtonsoft.Json;
using System;

namespace Sheepsteak.Echoes.Core
{
    public class Article
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        public string Description
        {
            get { return this.UpVotes + " up and " + this.DownVotes + " down, posted by " + this.Username + " " + this.PostedAt.ToNaturalRelativeTime(); }
        }

        [JsonProperty("down")]
        public int DownVotes { get; set; }

        [JsonProperty("ctime")]
        [JsonConverter(typeof(CTimeToDateTimeOffsetConverter))]
        public DateTimeOffset PostedAt { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("up")]
        public int UpVotes { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
