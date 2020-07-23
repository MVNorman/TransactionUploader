using System;
using AutoMapper;
using TransactionUploader.Application.Transaction.Models.FileReadModels;
using TransactionUploader.Domain.Transaction;

namespace TransactionUploader.Application.Transaction.Mappings
{
    public class TransactionMap : Profile
    {
        public TransactionMap()
        {
            CreateMap<TransactionModel, TransactionEntity>()
                .ForMember(x => x.Id,
                    config => config.Ignore())

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
                    });
        }
    }
}
