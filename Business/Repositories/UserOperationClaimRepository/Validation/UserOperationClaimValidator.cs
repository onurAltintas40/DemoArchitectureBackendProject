

using Entities.Concrete;
using FluentValidation;

namespace Business.Repositories.UserOperationClaimRepository.Validation
{
    public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(p=>p.UserId).NotEmpty().Must(IsIdValid).WithMessage("Yetki ataması için kullanıcı seçmelisiniz");           
            RuleFor(p => p.OperationClaimId).Must(IsIdValid).GreaterThan(0).WithMessage("Yetki ataması için yetki seçmelisiniz");          
        }

        private bool IsIdValid(int id)
        {
            if (id>0 && id != null) 
            {
                return true;
            }
            return false;
        }
    }
}
