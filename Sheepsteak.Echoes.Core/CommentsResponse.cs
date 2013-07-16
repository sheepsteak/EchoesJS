using Newtonsoft.Json;

namespace Sheepsteak.Echoes.Core
{
    public class CommentsResponse
    {
        [JsonProperty("comments")]
        public Comment[] Comments { get; set; }
    }
}
