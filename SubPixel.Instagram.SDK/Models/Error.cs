using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubPixel.Instagram.SDK.Models
{
    public class Error : IGraphResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("error_subcode")]
        public int SubCode { get; set; }

        [JsonProperty("fbtrace_id")]
        public string FBTraceId { get; set; }
    }

    public class ErrorRootObj
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}
