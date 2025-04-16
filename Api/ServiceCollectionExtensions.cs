using FluentValidation;
using System.Reflection;
using Api.Shared.Behaviours;
using Api.Shared.Slices;
using Api.Shared.Persistence;
using Api.Shared.Persistence.Repositories;
using Api.Shared.Domain.Contracts;
using MongoDB.Bson.Serialization.Conventions;

namespace Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.RegisterSlices();
        var currentAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(currentAssembly);
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(currentAssembly)
                .RegisterServicesFromAssemblies(currentAssembly)
                .AddOpenRequestPreProcessor(typeof(LoggingBehaviour<>))
                .AddOpenBehavior(typeof(ModelValidationBehaviour<,>));
        });
        services.AddValidatorsFromAssembly(currentAssembly);
        // custom serializer for mongo db to map aggregate root id value
        services.RegisterSerializer();

        var conventionPack = new ConventionPack{
            new CamelCaseElementNameConvention()
        };
        ConventionRegistry.Register("CamelCaseConvention", conventionPack, type => true);

        return services;
    }

    public static IServiceCollection RegisterPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // populate the settings
        services.Configure<FilmDeckDbSettings>(
            configuration.GetSection("MongoDbSettings"));

        services.AddSingleton<FilmDeckDbContext>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        return services;
    }
}
