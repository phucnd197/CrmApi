using Refit;

namespace Crm_Api.Shared.Clients;

public interface IUserClient
{
    [Get("/users/{id:int}")]
    Task<bool> GetUserByIdAsync(int id);

}

public static class UserClientRegister
{
    public static IServiceCollection AddUserClient(this IServiceCollection services)
    {
        services.AddRefitClient<IUserClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5003"));
        return services;
    }
}