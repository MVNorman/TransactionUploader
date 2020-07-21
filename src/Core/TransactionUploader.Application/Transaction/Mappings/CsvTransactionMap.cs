using System;
using System.Globalization;
using AutoMapper;
using TransactionUploader.Application.Transaction.Models.FileReadModels;
using TransactionUploader.Application.Transaction.Models.FileReadModels.Csv;
using TransactionUploader.Domain.Transaction.Enums;

namespace TransactionUploader.Application.Transaction.Mappings
{
    public class CsvTransactionMap : Profile
    {
        public CsvTransactionMap()
        {
            const string dateFormat = "dd/MM/yyyy HH:mm:ss";
            var dateTimeProvider = CultureInfo.InvariantCulture;

            CreateMap<CsvTransaction, TransactionModel>()
                .ForMember(x => x.Type,
                    x =>
                        x.MapFrom(m => TransactionType.Csv))

                .ForMember(dest => dest.TransactionDate,
                    opt =>
                    {
                        opt.PreCondition((transaction, entity, arg3) =>
                            (DateTime.TryParseExact(transaction.TransactionDate, dateFormat, dateTimeProvider,
                                DateTimeStyles.None, out _)));

                        opt.MapFrom(m => DateTime.ParseExact(m.TransactionDate, dateFormat, dateTimeProvider));
                    });
        }
    }
}
