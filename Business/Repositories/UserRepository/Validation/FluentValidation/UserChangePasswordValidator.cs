

using Entities.Dtos;
using FluentValidation;

namespace Business.Repositories.UserRepository.Validation.FluentValidation
{
    public class UserChangePasswordValidator : AbstractValidator<UserChangePasswordDto>
    {
        public UserChangePasswordValidator()
        {
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("Şifre boş geçilemez.");
            RuleFor(p => p.NewPassword).MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
            RuleFor(p => p.NewPassword).Matches("[A-Z]").WithMessage("Şifreniz en az bir adet büyük harf içermelidir.");
            RuleFor(p => p.NewPassword).Matches("[a-z]").WithMessage("Şifreniz en az bir adet küçük harf içermelidir.");
            RuleFor(p => p.NewPassword).Matches("[0-9]").WithMessage("Şifreniz en az bir adet sayı içermelidir.");
            RuleFor(p => p.NewPassword).Matches("[^a-zA-Z0-9]").WithMessage("Şifreniz en az bir adet sayı içermelidir.");
        }
    }
}
