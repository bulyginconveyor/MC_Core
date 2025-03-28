using core_service.domain;
using core_service.domain.models;
using core_service.domain.models.valueobjects;
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
    
    private readonly string _connectionString = "";
    
    public PostgreSqlDbContext(string connectionString)
    {
        _connectionString = connectionString;
        
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
        Database.Migrate();

        if (!Currencies.Any())
        {
            Currencies.AddRange(
                Currency.Create(Guid.NewGuid(), IsoCode.Create("RUB"), Name.Create("Российский рубль"),
                    PhotoUrl.Empty),
                Currency.Create(Guid.NewGuid(), IsoCode.Create("USD"),
                    Name.Create("Американский доллар"),
                    PhotoUrl.Empty),
                Currency.Create(Guid.NewGuid(), IsoCode.Create("EUR"), Name.Create("Евро"),
                    PhotoUrl.Empty),
                Currency.Create(Guid.NewGuid(), IsoCode.Create("CNY"), Name.Create("Китайский юань"),
                    PhotoUrl.Empty),
                Currency.Create(Guid.NewGuid(), IsoCode.Create("JPY"), Name.Create("Японская иена"),
                    PhotoUrl.Empty));

            SaveChanges();
        }
    }
    public PostgreSqlDbContext(){}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString); 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddConfigurations();
        
        base.OnModelCreating(modelBuilder);
    }
}
