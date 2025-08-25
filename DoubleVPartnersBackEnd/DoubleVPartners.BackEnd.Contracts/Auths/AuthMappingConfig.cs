using Mapster;

namespace DoubleVPartners.BackEnd.Contracts.Auths
{
    public class AuthMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<ActivityMember, ActivityMemberResponse>()
            //    .Map(dest => dest.MemberId, src => src.MemberId.Stringify())
            //    .Map(dest => dest.Name, src => src.Name);
        }
    }
}
