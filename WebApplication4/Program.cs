using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApplication4.Models;

namespace WebApplication4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //asdf();
            //Console.WriteLine("asdf");
            CreateHostBuilder(args).Build().Run();
        }

        public static void asdf()
        {
            var ac = "https://localhost:8081";
            var ke = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

            IDocumentClient client = new DocumentClient(new Uri(ac), ke);
            Database db = client.CreateDatabaseIfNotExistsAsync(new Database { Id = "MyDatabase" }).Result;

            client.CreateDocumentCollectionIfNotExistsAsync(db.SelfLink, new DocumentCollection { Id = "asdf" });
            
            Console.WriteLine(db);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
