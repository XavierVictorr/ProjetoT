using Data.Implementations;
using Data.Repository;
using Domain;
using Domain.Interfaces.Services.User;
using Domain.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection;

public class ConfigureRepository
{
    public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
    }
}   