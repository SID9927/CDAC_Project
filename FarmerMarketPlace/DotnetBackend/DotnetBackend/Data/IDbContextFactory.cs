namespace DotnetBackend.Data;


public interface IDbContextFactory
{
    FarmersmarketContext CreateDbContext();
}