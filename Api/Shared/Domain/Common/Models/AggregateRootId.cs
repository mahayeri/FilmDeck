using MongoDB.Bson;

namespace Api.Shared.Domain.Common.Models;

public abstract class AggregateRootId<TIdType> : ValueObject
{
    //[BsonRepresentation(BsonType.ObjectId)] // Default to ObjectId
    public abstract TIdType Value { get; protected set; }
    protected AggregateRootId() { }
}
