using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Persistence.Transaction
{
    public class TransactionFluentConfig : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.HasKey(x=> x.Id);

            builder.Property(x => x.CurrencyCode)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(x => x.TransactionId)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
