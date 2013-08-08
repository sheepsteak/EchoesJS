using Newtonsoft.Json;

namespace Sheepsteak.EchoesJS.Core
{
    public class CommentsResponse
    {
        [JsonProperty("comments")]
        public Comment[] Comments { get; set; }
    }
}
