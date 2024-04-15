using Domain.Interfaces.Services.User;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;


namespace CrossCutting.DependencyInjection;

public class ConfigureService
{
    public static void ConfigureDependecieService( IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUserService, UserServices> ();
        serviceCollection.AddTransient<IloginService, LoginService> ();
    }
}
