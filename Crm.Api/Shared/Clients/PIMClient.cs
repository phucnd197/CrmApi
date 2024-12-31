using Crm_Api.Contracts.Response;
using Refit;

namespace Crm_Api.Shared.Clients;

public interface IPIMClient
{
    [Get("/products")]
    Task<IList<ProductResponse>> GetAllProducts(CancellationToken cancellationToken = default);

    [Get("/products/{id}/prices")]
    Task<IList<PriceResponse>> GetProductPrice(int id, CancellationToken cancellationToken = default);
}

public static class PIMClientExtensions
{
    public static IServiceCollection AddPIMClient(this IServiceCollection services)
    {
        services
            .AddRefitClient<IPIMClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5002"));
        return services;
    }
}