using core_service.domain.models;
using core_service.infrastructure.repository.interfaces;
using core_service.infrastructure.repository.postgresql.context;
using core_service.infrastructure.repository.postgresql.models;
using core_service.infrastructure.repository.postgresql.repositories;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql;

public static class ServiceProviderExtensionsRepositories
{
    public static void AddPostgreSqlDbContext(this IServiceCollection services)
    {
        var server = Environment.GetEnvironmentVariable("DB_SERVER");
        var port = Environment.GetEnvironmentVariable("DB_PORT");
        var database = Environment.GetEnvironmentVariable("DB_NAME");
        var user = Environment.GetEnvironmentVariable("DB_USER");
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

        var connectionString = $"Server={server};Port={port};Database={database};User Id={user};Password={password}";

        services.AddScoped<DbContext, PostgreSqlDbContext>(s => new PostgreSqlDbContext(connectionString));
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

        services.AddScoped<IRepositoryForHiddenCategory<HiddenCategory>, HiddenCategoryRepository>();
    }
}
