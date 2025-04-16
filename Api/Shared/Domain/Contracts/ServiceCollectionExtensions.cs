using System.Reflection;
using Api.Shared.Domain.Common.Models;
using MongoDB.Bson.Serialization;

namespace Api.Shared.Domain.Contracts;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterSerializer(this IServiceCollection services)
    {
        // // get current assembly
        // var currentAssembly = Assembly.GetExecutingAssembly();

        // // get all the aggregate root ids with different id types
        // var aggregateRootIdTypes = currentAssembly.GetTypes()
        //     .Where(t =>
        //         t.BaseType != null &&
        //         t.BaseType.IsGenericType &&
        //         t.BaseType.GetGenericTypeDefinition() == typeof(AggregateRootId<>));

        // // register them as serializer with their id types
        // foreach (var type in aggregateRootIdTypes)
        // {
        //     var idType = type.BaseType!.GetGenericArguments()[0]; // Get TIdType (e.g., string, Guid)
        //     var serializerType = typeof(AggregateRootIdSerializer<>).MakeGenericType(idType);
        //     var serializer = Activator.CreateInstance(serializerType);

        //     BsonSerializer.RegisterSerializer(type, (IBsonSerializer)serializer!);
        // }

        BsonSerializer.RegisterSerializer(new MovieIdSerializer());

        return services;
    }
}
