using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Persistence
{
    public class TransactionUploaderDbContext: DbContext
    {
        public TransactionUploaderDbContext(DbContextOptions<TransactionUploaderDbContext> options)
            : base(options)
        { }

        public DbSet<TransactionEntity> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
