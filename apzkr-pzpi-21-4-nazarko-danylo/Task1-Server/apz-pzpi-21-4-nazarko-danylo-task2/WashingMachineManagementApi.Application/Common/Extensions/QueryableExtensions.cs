using WashingMachineManagementApi.Application.Common.Models;
using AutoMapper;

namespace WashingMachineManagementApi.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
        where T : class
    {
        return PaginatedList<T>.Create(queryable, pageNumber, pageSize);
    }

    public static PaginatedList<TDestination> ProjectToPaginatedList<TSource, TDestination>(

        this IQueryable<TSource> queryable,
        int pageNumber,
        int pageSize,
        IConfigurationProvider mappingConfigurationProvider)

        where TSource : class
        where TDestination : class
    {
        return PaginatedList<TSource>
            .Create<TSource, TDestination>(queryable, pageNumber, pageSize, mappingConfigurationProvider);
    }

    // public static IQueryable<T> ApplySort<T>(this IQueryable<T> entities, string? orderByQueryString)
    // {
    //     if (!entities.Any() || String.IsNullOrWhiteSpace(orderByQueryString))
    //     {
    //         return entities;
    //     }
    //
    //     var orderParams = orderByQueryString.Trim().Split(",");
    //     var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
    //     var orderQueryBuilder = new StringBuilder();
    //
    //     foreach (var param in orderParams)
    //     {
    //         if (string.IsNullOrWhiteSpace(param))
    //         {
    //             continue;
    //         }
    //
    //         var propertyFromQueryName = param[0] == '-' || param[0] == '+' ? param.Substring(1) : param;
    //         var objectProperty = propertyInfos.FirstOrDefault(pi =>
    //             pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
    //
    //         if (objectProperty == null)
    //         {
    //             continue;
    //         }
    //
    //         var sortingOrder = param[0] == '-' ? "descending" : "ascending";
    //
    //         orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
    //     }
    //
    //     var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
    //
    //     return entities.OrderBy(orderQuery);
    // }

    // public static IQueryable<ExpandoObject> ShapeData<T>(this IQueryable<T> entities, string? fieldsString)
    // {
    //     var allProperties = GetAllProperties<T>();
    //     var requiredProperties = GetRequiredProperties(fieldsString, allProperties);
    //     return FetchData(entities, requiredProperties);
    // }
    // 
    // public static ExpandoObject ShapeData<T>(this T entity, string? fieldsString)
    // {
    //     var allProperties = GetAllProperties<T>();
    //     var requiredProperties = GetRequiredProperties(fieldsString, allProperties);
    //     return FetchDataForEntity(entity, requiredProperties);
    // }
    //
    // private static IEnumerable<PropertyInfo> GetAllProperties<T>()
    // {
    //     return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
    // }
    // 
    // private static IEnumerable<PropertyInfo> GetRequiredProperties(string? fieldsString, IEnumerable<PropertyInfo> properties)
    // {
    //     var requiredProperties = new List<PropertyInfo>();
    //
    //     if (!string.IsNullOrWhiteSpace(fieldsString))
    //     {
    //         var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);
    //
    //         foreach (var field in fields)
    //         {
    //             var property = properties.FirstOrDefault(pi => pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));
    //
    //             if (property == null)
    //                 continue;
    //
    //             requiredProperties.Add(property);
    //         }
    //     }
    //     else
    //     {
    //         requiredProperties = properties.ToList();
    //     }
    //
    //     return requiredProperties;
    // }
    // 
    // private static IQueryable<ExpandoObject> FetchData<T>(IQueryable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
    // {
    //     var shapedData = new List<ExpandoObject>();
    //
    //     foreach (var entity in entities)
    //     {
    //         var shapedObject = FetchDataForEntity(entity, requiredProperties);
    //         shapedData.Add(shapedObject);
    //     }
    //
    //     return shapedData.AsQueryable();
    // }
    // 
    // private static ExpandoObject FetchDataForEntity<T>(T entity, IEnumerable<PropertyInfo> requiredProperties)
    // {
    //     var shapedObject = new ExpandoObject();
    //
    //     foreach (var property in requiredProperties)
    //     {
    //         var objectPropertyValue = property.GetValue(entity);
    //         shapedObject.TryAdd(property.Name, objectPropertyValue);
    //     }
    //
    //     return shapedObject;
    // }
}
