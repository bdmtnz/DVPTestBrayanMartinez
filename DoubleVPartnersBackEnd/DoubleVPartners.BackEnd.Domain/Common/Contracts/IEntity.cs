using DoubleVPartners.BackEnd.Domain.Common.Base;

namespace DoubleVPartners.BackEnd.Domain.Common.Contracts
{
    public interface IEntity<EId> where EId : ValueObject;
}
