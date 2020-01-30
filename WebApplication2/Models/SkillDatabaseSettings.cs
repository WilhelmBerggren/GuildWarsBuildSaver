using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class SkillDatabaseSettings : ISkillDatabaseSettings
    {
        public string Account { get; set; }
        public string Key { get; set; }
        public string DatabaseName { get; set; }
        public string ContainerName { get; set; }
    }
    public interface ISkillDatabaseSettings
    {
        string Account { get; set; }
        string Key { get; set; }
        string DatabaseName { get; set; }
        string ContainerName { get; set; }
    }
}
