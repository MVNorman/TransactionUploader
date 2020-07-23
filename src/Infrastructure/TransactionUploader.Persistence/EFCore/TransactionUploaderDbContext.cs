using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TransactionUploader.Domain.Transaction;
using TransactionUploader.Domain.TransactionLog;

namespace TransactionUploader.Persistence.EFCore
{
    public class TransactionUploaderDbContext: DbContext
    {
        public TransactionUploaderDbContext(DbContextOptions<TransactionUploaderDbContext> options)
            : base(options)
        { }

        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<TransactionLogEntity> TransactionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
