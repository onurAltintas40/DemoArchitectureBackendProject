

using Entities.Concrete;
using FluentValidation;

namespace Business.Repositories.OperationClaimRepository.Validation
{
    public class OperationClaimValidation :AbstractValidator<OperationClaim>
    {
        public OperationClaimValidation()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Yetki adı boş olamaz.");
        }
    }
}
