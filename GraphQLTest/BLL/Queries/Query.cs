using System.Text;
using Dal;
using Domain.Entities;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BLL.Queries;

public class Query(PostgresDbContext dbContext)
{
    [GraphQLName("GetEmployee")]
    [UseSorting]
    public async Task<List<Employee>> GetEmployee() => await dbContext.Employees.ToListAsync();
    
    [GraphQLName("GetEmployeesPaging")]
    [UsePaging(MaxPageSize = 10, IncludeTotalCount = true)]
    [UseSorting]
    [UseFiltering]
    public async Task<Connection<Employee>> GetEmployeesPaging(string? after, int first = 5)
    {
        int skip = 0;

        // Декодируем курсор
        if (!string.IsNullOrEmpty(after))
        {
            try
            {
                var decodedBytes = Convert.FromBase64String(after);
                var decodedString = Encoding.UTF8.GetString(decodedBytes);
                skip = int.Parse(decodedString);
            }
            catch
            {
                throw new GraphQLException("Invalid cursor format");
            }
        }

        // Получаем данные с учётом пагинации
        var employees = await dbContext.Employees
            .OrderBy(e => e.Id)
            .Skip(skip)
            .Take(first)
            .ToListAsync();

        var totalCount = await dbContext.Employees.CountAsync();
        var hasNextPage = skip + first < totalCount;
        var hasPreviousPage = skip > 0;

        // Создаём курсоры (кодируем в Base64)
        var edges = employees
            .Select((employee, index) => new Edge<Employee>(
                employee,
                Convert.ToBase64String(Encoding.UTF8.GetBytes((skip + index + 1).ToString()))))
            .ToList();

        var pageInfo = new ConnectionPageInfo(
            hasNextPage,
            hasPreviousPage,
            edges.FirstOrDefault()?.Cursor,
            edges.LastOrDefault()?.Cursor
        );

        return new Connection<Employee>(edges, pageInfo);
    }
}