using Microsoft.EntityFrameworkCore;

namespace CornerPlate.Infrastructure.Persistence.Helpers;

public static class QueryExtensions
{
    public static IQueryable<T> Track<T>(this IQueryable<T> query, bool track)
        where T : class
        => track ? query : query.AsNoTracking();
}