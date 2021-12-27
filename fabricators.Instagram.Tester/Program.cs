/* 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. 
 *
 * Author: Pietro di Caprio <pietro.dicaprio@subpixel.it>
 * Please open an issue on GitHub for any problem or question.
 */

using fabricators.Instagram.SDK;
using fabricators.Instagram.SDK.Models;
using System;
using System.Collections.Generic;

namespace fabricators.Instagram.Tester
{
    class Program
    {
        const string AUTH_TOKEN = "";
        private static ushort test = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("fabricators Instagram SDK tester");

            if (test == 0)
            {
                var client = new Client(0, "");
                Console.WriteLine("Code:");
                var code = Console.ReadLine();
                GetSTL(client, 
                    code,
                    "https://localhost/test");
            }

            if (test == 1)
            {
                var igClient = new Client(AUTH_TOKEN);

                var userScopes = new List<User.Scope>();
                userScopes.Add(User.Scope.id);
                userScopes.Add(User.Scope.username);
                userScopes.Add(User.Scope.account_type);
                userScopes.Add(User.Scope.media_count);
                userScopes.Add(User.Scope.media);

                GetMe(igClient, userScopes);
            }
        }

        static void GetMe(Client client, List<User.Scope> scopes)
        {
            var response = User.Me(client, scopes);

            if(response == null)
            {
                Console.WriteLine("response is null!");
                return;
            }

            if (response.GetType() == typeof(User))
            {
                User user = (User)response;
                Console.WriteLine(user.Username);
            }
            else if (response.GetType() == typeof(Error))
            {
                Console.WriteLine(((Error)response).Message);
            }
            else
            {
                Console.WriteLine("Incorrect type:" + response.GetType());
            }
        }

        static void GetSTL(Client client, string code, string redirectUri)
        {
            var response = client.GetSTL(code, redirectUri);

            if (response.GetType() == typeof(ShortTermToken))
            {
                var stl = (ShortTermToken)response;
                Console.WriteLine($"UserId:{stl.UserId}");
                Console.WriteLine($"Access Token:{stl.AccessToken}");
            }
            else if (response.GetType() == typeof(Error))
            {
                Console.WriteLine(((Error)response).Message);
            }
            else
            {
                Console.WriteLine("Incorrect type:" + response.GetType());
            }
        }
    }
}
