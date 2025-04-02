using core_service.infrastructure.repository.postgresql.configurations;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql;

public static class ModelBuilderExtensionsConfigurations
{
    public static void AddConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new TermConfiguration());
        modelBuilder.ApplyConfiguration(new PeriodConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new BankAccountConfiguration());
        modelBuilder.ApplyConfiguration(new DebetBankAccountConfiguration());
        modelBuilder.ApplyConfiguration(new ActiveBankAccountConfiguration());
        modelBuilder.ApplyConfiguration(new ContributionBankAccountConfiguration());
        modelBuilder.ApplyConfiguration(new CreditBankAccountConfiguration());
        modelBuilder.ApplyConfiguration(new OperationConfiguration());

        modelBuilder.ApplyConfiguration(new HiddenCategoryConfiguration());
    }
}
