using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionUploader.Domain.TransactionLog;

namespace TransactionUploader.Persistence.TransactionLog
{
    public class TransactionLogFluentConfig : IEntityTypeConfiguration<TransactionLogEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionLogEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
