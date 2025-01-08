namespace core_service.domain.logic;

public static class ServiceProviderExtensionsLogics
{
    public static void AddLogics(this IServiceCollection services)
    {
        services.AddTransient<CurrencyLogic>();
        services.AddTransient<CategoryLogic>();
        
        services.AddTransient<BankAccountLogic>();
        services.AddTransient<ActiveBankAccountLogic>();
        services.AddTransient<DebetBankAccountLogic>();
        services.AddTransient<ContributionBankAccountLogic>();
        services.AddTransient<CreditBankAccountLogic>();
        
        services.AddTransient<OperationLogic>();
    }
}
