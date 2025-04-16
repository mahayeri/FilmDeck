using Api.Shared.Domain.MovieAggregate.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Api.Shared.Domain.Contracts;

public class MovieIdSerializer : SerializerBase<MovieId>
{
    public override MovieId Deserialize(
        BsonDeserializationContext context,
        BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadObjectId();
        return MovieId.Create(value.ToString());
    }

    public override void Serialize(
        BsonSerializationContext context,
        BsonSerializationArgs args,
        MovieId value)
    {
        context.Writer.WriteString(value.Value);
    }
}
