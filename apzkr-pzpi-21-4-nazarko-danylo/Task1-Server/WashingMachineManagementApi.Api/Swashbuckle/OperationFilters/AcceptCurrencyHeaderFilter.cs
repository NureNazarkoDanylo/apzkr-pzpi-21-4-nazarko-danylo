using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WashingMachineManagementApi.Api.Swashbuckle.OperationFilters;

public class AcceptCurrencyHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Accept-Currency",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema { Type = "String" },
            Required = false
        });
    }
}
