/* 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. 
 *
 * Author: Pietro di Caprio <pietro.dicaprio@subpixel.it>
 * Please open an issue on GitHub for any problem or question.
 */

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