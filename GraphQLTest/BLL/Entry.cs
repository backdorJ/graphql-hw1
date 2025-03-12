using BLL.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BLL;

public static class Entry
{
    public static void AddGraphQlQueryTypes(this IServiceCollection services)
    {
        services.AddGraphQLServer()
            .AddQueryType<Query>();
    }
}