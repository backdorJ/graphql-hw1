using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dal;

public static class Entry
{
    public static void AddDal(this IServiceCollection services, PostgresConfig pgConfig)
    {
        services.AddDbContext<PostgresDbContext>(options => options.UseNpgsql(pgConfig.ConnectionString));
    }
}