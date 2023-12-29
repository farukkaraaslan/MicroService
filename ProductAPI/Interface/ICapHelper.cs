namespace ProductAPI.Interface;

public interface ICapHelper
{
    Task ExecuteWithTransactionAsync<T>(string eventName, T entity);
}
