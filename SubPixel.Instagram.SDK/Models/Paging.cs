using Newtonsoft.Json;

namespace SubPixel.Instagram.SDK.Models
{
    public class Paging
    {
        [JsonProperty("cursors")]
        public Cursor Cursors { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }

    public class Cursor
    {
        [JsonProperty("after")]
        public string After { get; set; }

        [JsonProperty("before")]
        public string Aefore { get; set; }
    }
}