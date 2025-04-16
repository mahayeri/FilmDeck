using Api.Shared.Domain.Common.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Api.Shared.Domain.Contracts;

public class AggregateRootIdSerializer<TIdType> : SerializerBase<AggregateRootId<TIdType>>
{
    public override AggregateRootId<TIdType> Deserialize(
        BsonDeserializationContext context,
        BsonDeserializationArgs args)
    {
        var bsonType = context.Reader.GetCurrentBsonType();

        // Handle the different bson types (objectId, string, ...)
        TIdType value = bsonType switch
        {
            BsonType.ObjectId => (TIdType)(object)context.Reader.ReadObjectId(),
            BsonType.String => (TIdType)(object)context.Reader.ReadString(),
            _ => throw new NotSupportedException($"BsonType {bsonType} is not supported.")
        };

        // create an instance of the concerete AggregateRootId<TIdType>
        return CreateAggregateRootId(value);
    }

    public override void Serialize(
        BsonSerializationContext context,
        BsonSerializationArgs args,
        AggregateRootId<TIdType> value)
    {
        // Write the underlying value (e.g., string, Guid) to MongoDB
        switch (value.Value)
        {
            case string s:
                context.Writer.WriteString(s);
                break;
            case Guid g:
                context.Writer.WriteString(g.ToString());
                break;
            default:
                throw new NotSupportedException($"Type {typeof(TIdType)} is not supported.");
        }
    }

    private static AggregateRootId<TIdType> CreateAggregateRootId(TIdType value)
    {
        // Use reflection to create the concrete type
        var idType = typeof(AggregateRootId<TIdType>);
        return (AggregateRootId<TIdType>)Activator.CreateInstance(idType, value)!;
    }
}
