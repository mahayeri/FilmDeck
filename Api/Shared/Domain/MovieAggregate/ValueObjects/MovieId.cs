using Api.Shared.Domain.Common.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Shared.Domain.MovieAggregate.ValueObjects;

public sealed class MovieId : AggregateRootId<string>
{
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public override string Value { get; protected set; }
    private MovieId(string value) => Value = value;
    public static MovieId CreateUnique() => new(ObjectId.GenerateNewId().ToString());
    public static MovieId Create(string value) => new(value);
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public override string ToString()
    {
        return Value.ToString();
    }


    // Convert the string Value to an ObjectId
    public ObjectId ToObjectId() => ObjectId.Parse(Value);
}
