using AutoMapper;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Models.ResponseModels;
using BlueHarvest.Modules.Transactions.Core.Domain.Entities;
using BlueHarvest.Shared.Application.Models.RequestModels.Transactions;

namespace BlueHarvest.Modules.Transactions.Core.Application.Transactions.Mappings
{
	public class TransactionMappingProfile : Profile
    {
        public TransactionMappingProfile()
        {
            CreateMap<CreateTransactionRequest, Transaction>(MemberList.Source)
                .ForMember(dest => dest.CreatedAt, options => options.MapFrom(src => DateTimeOffset.UtcNow));

            CreateMap<Transaction, TransactionResponse>()
                .ForMember(
                    dest => dest.Operation, 
                    options => options.MapFrom(src => src.Operation.ToString()));
        }
    }
}
