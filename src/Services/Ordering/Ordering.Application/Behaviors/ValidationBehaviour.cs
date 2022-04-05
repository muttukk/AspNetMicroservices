﻿using FluentValidation;
using MediatR;
using ValidationException = Ordering.Application.Exceptions.ValidationException;

namespace Ordering.Application.Behaviors
{
    public class ValidationBehaviour<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, 
                                      RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

                var validationResults=await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context,cancellationToken)));
                var failures=validationResults.SelectMany(r=>r.Errors).Where(r=>r!=null).ToList();

                if (failures.Count>0)
                {
                    throw new ValidationException(failures);
                }
            }
            return await next();
        }
    }
}
