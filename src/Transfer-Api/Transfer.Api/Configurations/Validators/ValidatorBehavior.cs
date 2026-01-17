using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace Transfer.Api.Configurations.Validators;

/// <summary>
/// Validator behavior for mediator pipeline class.
/// </summary>
/// <typeparam name="TRequest"> The type of the request. </typeparam>
/// <typeparam name="TResponse"> The type of the response. </typeparam>
public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IValidator _validator;

    public ValidatorBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    /// <summary>
    /// Handles the request validations.
    /// </summary>
    /// <param name="request"> The request. </param>
    /// <param name="next"> The next. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator != null)
        {
            var errors = _validator.Validate(new ValidationContext<TRequest>(request)).Errors;

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }

        return next();
    }
}