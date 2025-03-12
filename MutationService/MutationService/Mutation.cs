using Microsoft.Extensions.Caching.Memory;

namespace MutationService;

public class Mutation
{
    private readonly IMemoryCache _memoryCache;

    public Mutation(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    [GraphQLName("addProduct")]
    public Product AddProduct(string name)
    {
        var product = new Product
        {
            Id = Random.Shared.Next(1, int.MaxValue).ToString(),
            Name = name
        };
        
        _memoryCache.Set(product.Id, product);

        return product;
    }
}

public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
}