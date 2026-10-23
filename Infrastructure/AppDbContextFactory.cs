
using CornerPlate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Design;

namespace CornerPlate.Infrastructure;

public class AppDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Require an explicit database environment.
        if (args.Length != 2 || args[0] != "--db-env")
        {
            throw new ArgumentException(
                "Specify --db-env Development or --db-env Test.");
        }

        var options =
            DatabaseConfiguration.CreateOptions(args[1]);

        return new AppDbContext(options);
    }
}