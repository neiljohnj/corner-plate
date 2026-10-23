using Microsoft.EntityFrameworkCore;

namespace CornerPlate.Infrastructure.Persistence;

public static class DatabaseConfiguration
{
    public static DbContextOptions<AppDbContext> CreateOptions(
        string environment)
    {
        string variableName = environment.ToLowerInvariant() switch
        {
            "development" or "dev" => "CORNERPLATE_DB_DEV",
            "test" => "TEST_CORNERPLATE_DB",

            _ => throw new ArgumentException(
                $"Unknown database environment: {environment}")
        };

        string connectionString =
            Environment.GetEnvironmentVariable(variableName)
            ?? throw new InvalidOperationException(
                $"Environment variable {variableName} is missing.");

        return new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            .Options;
    }
}