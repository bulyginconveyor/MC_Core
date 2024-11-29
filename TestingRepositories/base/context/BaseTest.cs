using core_service.infrastructure.repository.postgresql.context;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using TestingRepositories.context;
using TestContext = TestingRepositories.context.TestContext;

namespace TestingRepositories;

public class BaseTest
{
    protected DbContext _context;
    private PostgreSqlContainer _postgres;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var builder  = new PostgreSqlBuilder();
        builder.WithDatabase("test");
        builder.WithUsername("postgres");
        builder.WithPassword("postgres");

        var postgres = builder.Build();
        postgres.StartAsync().Wait();
        _postgres = postgres;
        
        _context = new TestContext(postgres.GetConnectionString());
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _postgres.StopAsync().Wait();
        
        _context.Dispose();
    }
}