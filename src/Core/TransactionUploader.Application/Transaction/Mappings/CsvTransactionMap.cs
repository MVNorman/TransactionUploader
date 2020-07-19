using System;
using System.Globalization;
using AutoMapper;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Domain.Transaction;
using TransactionUploader.Domain.Transaction.Enums;

namespace TransactionUploader.Application.Transaction.Mappings
{
    public class CsvTransactionMap : Profile
    {
        public CsvTransactionMap()
        {
            const string dateFormat = "dd/MM/yyyy HH:mm:ss";
            var dateTimeProvider = CultureInfo.InvariantCulture;

            CreateMap<CsvTransaction, TransactionEntity>()

                .ForMember(x => x.Id,
                    config => config.Ignore())

                .ForMember(x => x.Type,
                    x =>
                        x.MapFrom(m => TransactionType.Csv))

                .ForMember(dest => dest.UpdatedAt,
                    opt =>
                    {
                        opt.PreCondition((transaction, entity, arg3) => (entity.CreatedAt != default));
                        opt.MapFrom(m => DateTime.UtcNow);
                    })

                .ForMember(dest => dest.CreatedAt,

                    opt =>
                        {
                            opt.PreCondition((transaction, entity, arg3) => (entity.CreatedAt == default));
                            opt.MapFrom(m => DateTime.UtcNow);
                        })

                .ForMember(x => x.TransactionDate,
                    x =>
                        x.MapFrom(m => DateTime.ParseExact(m.TransactionDate, dateFormat, dateTimeProvider)))
                ;
        }
    }
}
