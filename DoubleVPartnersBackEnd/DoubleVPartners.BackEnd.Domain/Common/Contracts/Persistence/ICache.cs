using ErrorOr;

namespace DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence
{
    public interface ICache
    {
        Task<ErrorOr<T>> Get<T>(string key);
        Task Set<T>(string key, T value, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null);
        Task Remove(string key);
    }
}
