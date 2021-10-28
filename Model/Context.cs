using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    public class Context : DbContext
    {
        private const string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=prince;Integrated security=true;TrustServerCertificate=true";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get;set; }
        public DbSet<Project> Projects { get; set; }
    }

}