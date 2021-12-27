/* 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. 
 *
 * Author: Pietro di Caprio <pietro.dicaprio@subpixel.it>
 * Please open an issue on GitHub for any problem or question.
 */

using Newtonsoft.Json;

namespace fabricators.Instagram.SDK.Models
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
