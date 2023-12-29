using CustomerAPI.Context;
using CustomerAPI.Interfaces;
using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Helper
{
    public class CapHelper : ICapHelper
    {
        private readonly ICapPublisher _capPublisher;
        private readonly MyDbContext _dbContext;

        public CapHelper(ICapPublisher capPublisher, MyDbContext dbContext)
        {
            _capPublisher = capPublisher;
            _dbContext = dbContext;
        }

        public async Task ExecuteWithTransactionAsync<T>(string eventName, T entity)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            await _capPublisher.PublishAsync(eventName, entity);
            transaction.Commit();


        }
    }
}
