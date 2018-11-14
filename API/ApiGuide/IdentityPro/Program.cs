using System;
using System.Net.Http;
using System.Net.Http.Headers;
using IdentityModel.Client;

namespace ClientPro
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start。。。");            //设置token 访问API

            //访问授权服务器获取token
            var disco = DiscoveryClient.GetAsync("https://localhost:44362/").Result;
          
             var tokenClient = new TokenClient(disco.TokenEndpoint, "pwd_client", "pwd_secret");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("zeroapi").Result;
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error); return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("==============================");            //设置token 访问API
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken); var response = client.GetAsync("https://localhost:44309/api/values").Result; if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            var content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
            Console.WriteLine("Hello World!ClientPro");
            Console.ReadKey();
      
        }

    }
}
