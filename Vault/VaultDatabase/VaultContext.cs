using Microsoft.EntityFrameworkCore;
using VaultDatabase.Models;

namespace VaultDatabase
{
    public class VaultContext : DbContext
    {
        public VaultContext(DbContextOptions<VaultContext> options) : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { set; get; }
        public virtual DbSet<Transaction> Transactions { set; get; }
    }
}
