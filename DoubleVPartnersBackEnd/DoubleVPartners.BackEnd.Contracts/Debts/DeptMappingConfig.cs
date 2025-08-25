using DoubleVPartners.BackEnd.Domain.UserAggregate.Entities.Debts;
using Mapster;

namespace DoubleVPartners.BackEnd.Contracts.Debts
{
    public class DeptMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserDebt, DebtResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.UserId, src => src.UserId.Value)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.CreatedOnUtc, src => src.CreatedOnUtc)
                .Map(dest => dest.UpdatedOnUtc, src => src.UpdatedOnUtc)
                .Map(dest => dest.PaidOnUtc, src => src.PaidOnUtc);
        }
    }
}
