using Microsoft.EntityFrameworkCore;
using System;

namespace GuildWarsBuildSaver.Models
{
    public class SkillContext : DbContext
    {
        public SkillContext(DbContextOptions<SkillContext> options)
            : base(options)
        {
        }

        public DbSet<Skill> Skills { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //
        //    modelBuilder.Entity<Skill>()
        //    .Property(e => e.Professions)
        //    .HasConversion(
        //        v => string.Join(',', v),
        //        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        //}
    }
}
