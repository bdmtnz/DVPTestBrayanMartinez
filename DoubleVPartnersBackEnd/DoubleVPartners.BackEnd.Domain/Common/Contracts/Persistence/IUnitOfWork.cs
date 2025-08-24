namespace DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        IGenericRepository<TD> GenericRepository<TD>() where TD : class;
    }
}
