
using FluentValidation.Results;

namespace Ordering.Application.Exceptions
{
    public class ValidationException:ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }
        public ValidationException():base("One or more Validation error occured.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures):this()
        {
            Errors=failures.GroupBy(f=>f.PropertyName,e=>e.ErrorMessage)
                .ToDictionary(failureGroup=> failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
    }
}
