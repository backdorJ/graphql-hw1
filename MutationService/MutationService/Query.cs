using Microsoft.Extensions.Caching.Memory;

namespace MutationService;

public class Query
{
    private readonly IMemoryCache _memoryCache;

    public Query(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    [GraphQLName("GetProduct")]
    public Product GetProducts(string id)
    {
        if (!_memoryCache.TryGetValue(id, out Product product))
            throw new ArgumentNullException(nameof(id));
        
        return product;
    }
}