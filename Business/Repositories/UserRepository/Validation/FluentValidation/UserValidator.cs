

using Entities.Concrete;
using FluentValidation;

namespace Business.Repositories.UserRepository.Validation.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("İsim boş geçilemez.");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email boş geçilemez.");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Geçerli bir mail adresi yazınız.");
            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("Kullanıcı resmi boş geçilemez.");          
        }
    }
}
