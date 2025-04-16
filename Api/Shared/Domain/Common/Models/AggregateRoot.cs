namespace Api.Shared.Domain.Common.Models;

public abstract class AggregateRoot<TId, TIdType>(TId id) : Entity<TId>(id)
    where TId : AggregateRootId<TIdType>
{
    // public new AggregateRootId<TIdType> Id { get; protected set; } = id;
}
