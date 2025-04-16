using Api.Shared.Domain.Common.Models;

namespace Api.Shared.Domain.MovieAggregate.Events;

public sealed record MovieCreatedEvent(Movie Movie) : IDomainEvent;
