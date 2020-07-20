using System;
using System.Globalization;
using AutoMapper;
using TransactionUploader.Application.Transaction.Models.Xml;
using TransactionUploader.Domain.Transaction;
using TransactionUploader.Domain.Transaction.Enums;

namespace TransactionUploader.Application.Transaction.Mappings
{
    public class XmlTransactionMap: Profile
    {
        public XmlTransactionMap()
        {
            const string dateFormat = "yyyy-MM-ddThh:mm:ss";
            var dateTimeProvider = CultureInfo.InvariantCulture;

            CreateMap<XmlTransactionItem, TransactionEntity>()

                .ForMember(x => x.Id,
                    config => config.Ignore())
                .ForMember(x => x.Type,
                    x =>
                        x.MapFrom(m => TransactionType.Xml))

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

                //TODO: Implement parsing for this field
                //.ForMember(x => x.TransactionDate,
                //    x =>
                //        x.MapFrom(m => DateTime.ParseExact(m.TransactionDate, dateFormat, dateTimeProvider)))

                .ForMember(x => x.Amount,
                    x =>
                        x.MapFrom(m => m.PaymentDetails.Amount))

                .ForMember(x => x.CurrencyCode,
                    x =>
                        x.MapFrom(m => m.PaymentDetails.CurrencyCode))
                ;
        }
    }

}
