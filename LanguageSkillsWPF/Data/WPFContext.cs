using LanguageSkillsWPF.Data.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace LanguageSkillsWPF.Data
{
    public class WPFContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardTranslation> CardTranslations { get; set; }
        //public string DbPath { get; }
        //public WPFContext()
        //{
        //    //var folder =// Environment.SpecialFolder.LocalApplicationData;
        //    var path = $"Filename =C:/Users/Home/source/repos/LanguageSkillsWPF/LanguageSkillsWPF.db";// Environment.GetFolderPath(folder);
        //    DbPath = System.IO.Path.Join(path, "blogging.db");
        //}

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:/Users/Home/source/repos/LanguageSkillsWPF/LanguageSkillsWPF.db");
        }

    }

}
