namespace DotnetBackend.Data;


public class DbContextFactory : IDbContextFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DbContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public FarmersmarketContext CreateDbContext()
    {
        return _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<FarmersmarketContext>();
    }
}