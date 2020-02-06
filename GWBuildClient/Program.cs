using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GWBuildClient.Models;

namespace GWBuildClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            new GWClient();
        }
    }

    class GWClient
    {
        HttpClient client;

        public GWClient()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        async Task RunAsync() {
            this.client = new HttpClient();
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:5001/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var res = await client.GetAsync("api/Skill");
            Console.WriteLine(res.Content);
            try
            {
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
