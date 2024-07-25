using Microsoft.EntityFrameworkCore;
using System.Configuration;
using VaultDatabase.Models;

namespace VaultDatabase
{
    public class VaultContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                string host = ConfigurationManager.AppSettings["Host"] ?? string.Empty;
                string port = ConfigurationManager.AppSettings["Port"] ?? string.Empty;
                string database = ConfigurationManager.AppSettings["Database"] ?? string.Empty;
                string username = ConfigurationManager.AppSettings["UserName"] ?? string.Empty;
                string password = ConfigurationManager.AppSettings["Password"] ?? string.Empty;

                optionsBuilder.UseNpgsql($"Host={host};Port={port};Database={database};Username={username};Password={password}");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Account> Accounts { set; get; }
        public virtual DbSet<Operation> Operations { set; get; }
    }
}
