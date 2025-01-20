using core_service.application.rest_api.DTO;
using core_service.domain.models;
using core_service.infrastructure.repository.interfaces;
using core_service.infrastructure.repository.redis.repositories.@base;
using StackExchange.Redis;

namespace core_service.infrastructure.repository.redis;

public static class ServiceProviderExtensionsRedisCache
{
    public static void AddRedisCache(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        
        string connectionRedis = Environment.GetEnvironmentVariable("REDIS_CONNECTION");
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(connectionRedis));
    }

    public static void AddRedisCacheRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICacheRepositoryWithLists<DTOCurrency>, BaseCacheRepositoryWithList<DTOCurrency>>();
        services.AddScoped<ICacheRepositoryWithLists<Category>, BaseCacheRepositoryWithList<Category>>();
        services.AddScoped<ICacheRepository<BankAccount>, BaseCacheRepository<BankAccount>>();
        services.AddScoped<ICacheRepository<DebetBankAccount>, BaseCacheRepository<DebetBankAccount>>();
        services.AddScoped<ICacheRepository<ActiveBankAccount>, BaseCacheRepository<ActiveBankAccount>>();
        services.AddScoped<ICacheRepository<ContributionBankAccount>, BaseCacheRepository<ContributionBankAccount>>();
        services.AddScoped<ICacheRepository<CreditBankAccount>, BaseCacheRepository<CreditBankAccount>>();
        services.AddScoped<ICacheRepository<Operation>, BaseCacheRepository<Operation>>();
    }
}
