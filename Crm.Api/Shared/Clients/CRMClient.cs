using Crm_Api.Contracts.Request;
using Refit;

namespace Crm_Api.Shared.Clients;

public interface ICRMClient
{
    [Post("/customers")]
    Task<int> CreateCustomer(CreateCustomerRequest request);

    [Get("/customers")]
    Task<int?> GetCustomerId(string firstName, string lastName, string email, string phoneNumber, DateOnly dateOfBirth);

    [Post("/customers/{id}/agreement")]
    Task RegisterAgreement(int customerId, [Body] AgreementRegistrationData agreementRegistrationData);
}

public static class CRMClientExtensions
{
    public static IServiceCollection AddCRMClient(this IServiceCollection services)
    {
        services
            .AddRefitClient<ICRMClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5004"));
        return services;
    }
}
