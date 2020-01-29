using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GuildWarsBuildSaver.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GuildWarsBuildSaver
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //var filename = @"C:\Users\wilhe\source\repos\GuildWarsBuildSaver//\GuildWarsBuildSaver\response.json";
            //var jsonString = System.IO.File.ReadAllText(filename);
            //var skills = JsonSerializer.Deserialize<List<Skill>>(jsonString);
            ////var asdf = skills.Select((i) => i).Where((i) => i.id == 1175);
            //return;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
