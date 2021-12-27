/* 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. 
 *
 * Author: Pietro di Caprio <pietro.dicaprio@subpixel.it>
 * Please open an issue on GitHub for any problem or question.
 */

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace fabricators.Instagram.SDK.Models
{
    public class User : IGraphResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("account_type")]
        public string AccountType { get; set; } // BUSINESS, MEDIA_CREATOR, or PERSONAL

        [JsonProperty("media_count")]
        public int MediaCount { get; set; }

        [JsonProperty("media")]
        public UserMediaRootObj Media { get; set; }

        public enum Scope
        {
            id, username, account_type, media_count, media
        }

        public static IGraphResponse Me(Client client, List<Scope> scopes) => Get(client, 0, scopes);
        public static IGraphResponse Get(Client client, long userId, List<Scope> scopes)
        {
            string url = client.APIEndpoint;
            url += String.Format("{0}/?fields=", userId == 0 ? "me" : userId.ToString());
            StringBuilder _scopes = new StringBuilder();
            if (scopes != null)
            {
                foreach (Scope scope in scopes)
                {
                    _scopes.Append("," + scope);
                }
                _scopes = _scopes.Remove(0, 1);
            }
            else
            {
                _scopes = new StringBuilder("id,username,account_type,media_count");
            }
            url += _scopes + client.AccessToken;

            return client.DoRequest(url, typeof(User));
        }
    }
}