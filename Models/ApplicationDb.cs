using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    internal class ApplicationDb:DbContext
    {
        public static ApplicationDb Current { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=Database.db");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Person> People { get; set; }
    }
}
