using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuildWarsBuildSaver.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string Account { get; set; }
        public string Key { get; set; }
        public string DatabaseName { get; set; }
        public string SkillContainerName { get; set; }
        public string BuildContainerName { get; set; }
    }
    public interface IDatabaseSettings
    {
        string Account { get; set; }
        string Key { get; set; }
        string DatabaseName { get; set; }
        string SkillContainerName { get; set; }
        string BuildContainerName { get; set; }
    }
}
