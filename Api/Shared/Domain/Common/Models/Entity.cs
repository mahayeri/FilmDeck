using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Shared.Domain.Common.Models;

public abstract class Entity<TId>(TId id) : IEquatable<Entity<TId>>, IHasDomainEvents
    where TId : notnull
{
    [BsonId]
    public TId Id { get; protected set; } = id;
    private readonly List<IDomainEvent> _domainEvents = [];
    public override bool Equals(object? obj) => obj is Entity<TId> entity && Id.Equals(entity.Id);
    public static bool operator ==(Entity<TId> left, Entity<TId> right) => Equals(left, right);
    public static bool operator !=(Entity<TId> left, Entity<TId> right) => Equals(left, right);
    public bool Equals(Entity<TId>? other) => Equals((object?)other);
    public override int GetHashCode() => Id.GetHashCode();
    [JsonIgnore]
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}
