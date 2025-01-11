using core_service.domain.models;
using core_service.infrastructure.repository.interfaces;
using core_service.infrastructure.repository.postgresql.context;
using core_service.infrastructure.repository.postgresql.repositories;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql;

public static class ServiceProviderExtensionsRepositories
{
    public static void AddPostgreSqlDbContext(this IServiceCollection services)
    {
        services.AddScoped<DbContext, PostgreSqlDbContext>();
    }
    
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDbRepository<Currency>, BaseRepository<Currency>>();
        services.AddScoped<IDbRepository<Category>, CategoryRepository>();
        
        services.AddScoped<IDbRepository<BankAccount>, BaseBankAccountRepository<BankAccount>>();
        services.AddScoped<IDbRepository<DebetBankAccount>, BaseBankAccountRepository<DebetBankAccount>>();
        services.AddScoped<IDbRepository<ActiveBankAccount>, BaseBankAccountRepository<ActiveBankAccount>>();
        services.AddScoped<IDbRepository<ContributionBankAccount>, BaseBankAccountRepository<ContributionBankAccount>>();
        services.AddScoped<IDbRepository<CreditBankAccount>, CreditBankAccountRepository>();
        
        services.AddScoped<IDbRepository<Operation>, OperationRepository>();
    }
}
