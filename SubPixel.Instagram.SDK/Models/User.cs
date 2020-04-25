using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SubPixel.Instagram.SDK.Models
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
            string _scopes = "";
            if (scopes != null)
            {
                foreach (Scope scope in scopes)
                    _scopes += "," + scope;
                _scopes = _scopes.Remove(0, 1);
            }
            else
                _scopes = "id,username,account_type,media_count";
            url += _scopes + client.AccessToken;

            return client.DoRequest(url, typeof(User));
        }
    }
}