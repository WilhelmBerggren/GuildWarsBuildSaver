using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;
using System.IO;

namespace HttpClientSample
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static List<TestSkill> ProcessSkills()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            //var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            //var msg = await stringTask;
            //Console.Write(msg);

            //var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            //var repositories = serializer.ReadObject(await streamTask) as List<Repository>;


            //var streamTask = client.GetStreamAsync("https://api.guildwars2.com/v2/skills?ids=all");
            //var repositories = await JsonSerializer.DeserializeAsync<List<Skill>>(await streamTask);

            var filename = @"C:\Users\wilhe\source\repos\GuildWarsBuildSaver\HttpClientSample\response.json";
            var jsonString = File.ReadAllText(filename);
            var skills = JsonSerializer.Deserialize<List<TestSkill>>(jsonString);

            return skills;
        }

        static async Task Main(string[] args)
        {
            var skills = ProcessSkills();

            var skillsWithProfs = from skill in skills
                              where skill.Professions != null
                              select skill;

            var skillsByProfession = from skill in skillsWithProfs
                                     where skill.Professions?.Count() == 1
                                     group skill by string.Join("", skill.Professions);

            foreach(var profession in skillsByProfession)
                Console.WriteLine($"{profession}: {string.Join(", ", profession)}");

            foreach (var skill in skillsWithProfs)
            {
                Console.WriteLine(skill.ID);
                Console.WriteLine(skill.Name);
                Console.WriteLine(skill.Desc);

                if(skill.Professions != null)
                {
                    Console.WriteLine(string.Join(", ", skill.Professions));
                }
                if(skill.Facts != null)
                {
                    Console.WriteLine(skill.Facts.Count);
                }

                //Console.WriteLine(repo.LastPush);
                //Console.WriteLine(repo.OwnerID);

                Console.WriteLine();
            }
        }
    }
}