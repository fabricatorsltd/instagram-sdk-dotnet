using SubPixel.Instagram.SDK;
using SubPixel.Instagram.SDK.Models;
using System;
using System.Collections.Generic;

namespace SubPixel.Instagram.Tester
{
    class Program
    {
        static Client igClient;
        const string AUTH_TOKEN = "";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            igClient = new Client(AUTH_TOKEN);

            List<User.Scope> userScopes = new List<User.Scope>();
            userScopes.Add(User.Scope.id);
            userScopes.Add(User.Scope.username);
            userScopes.Add(User.Scope.account_type);
            userScopes.Add(User.Scope.media_count);
            userScopes.Add(User.Scope.media);

            GetMe(igClient, userScopes);
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
    }
}
