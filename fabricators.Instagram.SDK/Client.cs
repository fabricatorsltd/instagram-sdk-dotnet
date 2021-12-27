/* 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. 
 *
 * Author: Pietro di Caprio <pietro.dicaprio@subpixel.it>
 * Please open an issue on GitHub for any problem or question.
 */

#nullable enable
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Http;
using fabricators.Instagram.SDK.Models;
using Newtonsoft.Json;

namespace fabricators.Instagram.SDK
{
    public class Client
    {
        public string APIEndpoint => "https://api.instagram.com/";
        internal string? AccessToken { get; set; }
        internal string? ClientSecret { get; }
        internal ulong? ClientId { get; }

        public Client(
            string accessToken)
        {
            if (!accessToken.StartsWith("&access_token="))
            {
                accessToken = "&access_token=" + accessToken;
            }
            AccessToken = accessToken;
        }
        
        public Client(
            ulong clientId,
            string? clientSecret,
            string? accessToken = null)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;

            if (accessToken == null) return;
            if (!accessToken.StartsWith("&access_token="))
            {
                accessToken = "&access_token=" + accessToken;
            }

            AccessToken = accessToken;
        }

        internal IGraphResponse DoRequest(string url, Type returnObj)
        {
            if (returnObj.GetInterface("IGraphResponse") == null)
            {
                return null;
            }

            WebClient webClient = new WebClient();

            try
            {
                var json = webClient.DownloadString(url);
                return (IGraphResponse)JsonConvert.DeserializeObject(json, returnObj);
            }
            catch (WebException ex)
            {
                string responseText;
                using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                }

                return JsonConvert.DeserializeObject<ErrorRootObj>(responseText).Error;
            }
            catch (Exception ex)
            {
                dynamic error = new ExpandoObject();
                error.Message = ex.Message;
                return new Error
                {
                    Code = -1,
                    Message = ex.Message
                };
            }
        }
        
        public IGraphResponse GetSTL(string code, string redirectUri)
        {
            if(ClientId is null || ClientSecret is null)
                return new Error
                {
                    Code = -1,
                    Message = "ClientId or ClientSecret missing"
                };

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", ClientId.ToString()), 
                new KeyValuePair<string, string>("client_secret", ClientSecret),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("redirect_uri", redirectUri),
                new KeyValuePair<string, string>("code", code)
            });
            
            var httpClient = new HttpClient();
            try
            {
                var request = httpClient.SendAsync(
                    new HttpRequestMessage(HttpMethod.Post,
                        APIEndpoint + "oauth/access_token")
                    {
                        Content = formContent
                    });
                if (!request.Result.IsSuccessStatusCode)
                {
                    var response = request.Result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ErrorRootObj>(response).Error;
                }
                
                var json = request.Result.Content.ReadAsStringAsync().Result;
                var stl = JsonConvert.DeserializeObject<ShortTermToken>(json);
                
                AccessToken = stl.AccessToken;
                return stl;
            }
            catch (Exception ex)
            {
                dynamic error = new ExpandoObject();
                error.Message = ex.Message;
                return new Error
                {
                    Code = -1,
                    Message = ex.Message
                };
            }
        }
    }
}
