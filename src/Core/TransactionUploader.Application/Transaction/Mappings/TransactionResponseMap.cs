using AutoMapper;
using TransactionUploader.Application.Transaction.Models;
using TransactionUploader.Domain.Extensions;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Application.Transaction.Mappings
{
    public class TransactionResponseMap : Profile
    {
        public TransactionResponseMap()
        {
            CreateMap<TransactionEntity, TransactionResponse>()
                .ForMember(x => x.Id,
                    map =>
                        map.MapFrom(x => x.TransactionId))

                .ForMember(x => x.Payment,
                    map => 
                        map.MapFrom(x=> $"{x.Amount} {x.CurrencyCode}"))

                .ForMember(x => x.Status,
                    map =>
                        map.MapFrom(x => x.Status.ToUnifiedFormat()));
        }
    }
}
