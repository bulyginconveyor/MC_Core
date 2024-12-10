using core_service.domain;
using core_service.infrastructure.repository.postgresql.configurations;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql.context;

public sealed class PostgreSqlDbContext : DbContext
{
    public DbSet<BankAccount> BankAccounts { get; set; }

    public DbSet<DebetBankAccount> DebetBankAccounts { get; set; }
    public DbSet<ActiveBankAccount> ActiveBankAccounts { get; set; }
    public DbSet<ContributionBankAccount> СontributionBankAccounts { get; set; }
    public DbSet<CreditBankAccount> CreditBankAccounts { get; set; }

    public DbSet<Currency> Currencies { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Operation> Operations { get; set; }
    
    private readonly string _connectionString;
    
    public PostgreSqlDbContext()
    {
        _connectionString = "Server=localhost;Port=5432;Database=CoreService;User Id=postgres;Password=postgres;";
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //TODO: Придумать - где и когда брать строку подключения из .env файла
        optionsBuilder.UseNpgsql(_connectionString); 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new TermConfiguration());
        modelBuilder.ApplyConfiguration(new PeriodConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new BankAccountConfiguration());
        modelBuilder.ApplyConfiguration(new ActiveBankAccountConfiguration());
        modelBuilder.ApplyConfiguration(new ContributionBankAccountConfiguration());
        modelBuilder.ApplyConfiguration(new CreditBankAccountConfiguration());
        modelBuilder.ApplyConfiguration(new OperationConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}