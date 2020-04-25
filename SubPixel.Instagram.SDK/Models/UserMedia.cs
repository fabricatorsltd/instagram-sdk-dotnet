using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SubPixel.Instagram.SDK.Models
{
    public class UserMedia
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public class UserMediaRootObj : IGraphResponse
    {
        [JsonProperty("data")]
        public IEnumerable<UserMedia> Data { get; set; }
        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }
}
