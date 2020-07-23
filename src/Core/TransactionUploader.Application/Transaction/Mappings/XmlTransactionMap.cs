using System;
using System.Globalization;
using AutoMapper;
using TransactionUploader.Application.Transaction.Models.FileReadModels;
using TransactionUploader.Application.Transaction.Models.FileReadModels.Xml;
using TransactionUploader.Domain.Transaction.Enums;

namespace TransactionUploader.Application.Transaction.Mappings
{
    public class XmlTransactionMap: Profile
    {
        public XmlTransactionMap()
        {
            const string dateFormat = "yyyy-MM-ddTHH:mm:ss";

            CreateMap<XmlTransactionItem, TransactionModel>()
                .ForMember(x => x.Type,
                    x =>
                        x.MapFrom(m => TransactionType.Xml))

                .ForMember(dest => dest.Amount,
                    opt =>
                    {
                        opt.PreCondition((transaction, entity, arg3) => transaction.PaymentDetails != null);
                        opt.MapFrom(m => m.PaymentDetails.Amount);
                    })

                .ForMember(dest => dest.CurrencyCode,
                    opt =>
                    {
                        opt.PreCondition((transaction, entity, arg3) => transaction.PaymentDetails != null);
                        opt.MapFrom(m => m.PaymentDetails.CurrencyCode);
                    })

                .ForMember(dest => dest.TransactionDate,
                    opt =>
                    {
                        opt.PreCondition((transaction, entity, arg3) =>
                            (DateTime.TryParseExact(transaction.TransactionDate, dateFormat, CultureInfo.InvariantCulture,
                                DateTimeStyles.None, out _)));

                        opt.MapFrom(m => DateTime.ParseExact(m.TransactionDate, dateFormat, CultureInfo.InvariantCulture));
                    });
        }
    }
}
