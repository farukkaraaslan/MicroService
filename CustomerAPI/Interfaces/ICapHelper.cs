namespace CustomerAPI.Interfaces;

public interface ICapHelper
{
    Task ExecuteWithTransactionAsync<T>(string eventName, T entity);
}
