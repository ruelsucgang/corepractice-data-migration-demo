using Demo.Application.Abstractions;
using Demo.Infrastructure.Persistence;
using Demo.Infrastructure.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var cs = configuration.GetConnectionString("SQLite") ?? "Data Source=corepractice.db";
        var builder = new SqliteConnectionStringBuilder(cs);

        services.AddDbContext<CorePracticeDbContext>(opt =>
        {
            opt.UseSqlite(builder.ToString());
        });

        // Repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Services
        services.AddScoped<IMigrationService, MigrationService>();
        services.AddScoped<ICsvReaderService, CsvReaderService>();

        return services;
    }
}
