using Newtonsoft.Json;
using System;

namespace Sheepsteak.Echoes.Core
{
    public class Comment
    {
        [JsonProperty("body")]
        public string Body { get; set; }
        
        [JsonProperty("ctime")]
        [JsonConverter(typeof(CTimeToDateTimeOffsetConverter))]
        public DateTimeOffset PostedAt { get; set; }

        [JsonProperty("replies")]
        public Comment[] Replies { get; set; }

        [JsonProperty("up")]
        public int UpVotes { get; set; }
        
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
