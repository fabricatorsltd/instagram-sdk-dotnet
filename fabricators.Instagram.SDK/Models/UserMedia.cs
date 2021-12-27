/* 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. 
 *
 * Author: Pietro di Caprio <pietro.dicaprio@subpixel.it>
 * Please open an issue on GitHub for any problem or question.
 */

using System.Collections.Generic;
using Newtonsoft.Json;

namespace fabricators.Instagram.SDK.Models
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
