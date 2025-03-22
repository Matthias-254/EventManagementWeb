using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventManagementMobile.Models;

namespace EventManagementMobile.Services
{
    internal class LocalDatabase : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={Path.Combine(FileSystem.AppDataDirectory, "localdata.db")}");
        }
    }
}
