using FluentValidation;
using MediatR;

namespace Api.Shared.Behaviours;

public sealed class ModelValidationBehaviour<TReaquest, IResult>(
    IEnumerable<IValidator<TReaquest>> validators)
    : IPipelineBehavior<TReaquest, IResult>
    where TReaquest : IRequest<IResult>
{
    public async Task<IResult> Handle(
        TReaquest request,
        RequestHandlerDelegate<IResult> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TReaquest>(request);

        var validationResult = validators.Select(v => v.Validate(context)).ToList();
        var groupedValidationFailures = validationResult.SelectMany(v => v.Errors)
            .GroupBy(e => e.PropertyName)
            .Select(g => new
            {
                PropertyName = g.Key,
                ValidationFailures = g.Select(v => new { v.ErrorMessage })
            }).ToList();

        if (groupedValidationFailures.Count is not 0)
        {
            var validationProblemsDictionary = new Dictionary<string, string[]>();
            foreach (var group in groupedValidationFailures)
            {
                var errorMessages = group.ValidationFailures.Select(v => v.ErrorMessage);
                validationProblemsDictionary.Add(group.PropertyName, errorMessages.ToArray());
            }
            return (IResult)Results.ValidationProblem(validationProblemsDictionary);
        }

        return await next();
    }
}
