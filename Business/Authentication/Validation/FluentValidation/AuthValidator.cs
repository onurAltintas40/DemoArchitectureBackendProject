
using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class AuthValidator : AbstractValidator<RegisterAuthDto>
    {
        public AuthValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("İsim boş geçilemez.");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email boş geçilemez.");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Geçerli bir mail adresi yazınız.");
            RuleFor(p => p.Image).NotEmpty().WithMessage("Kullanıcı resmi boş geçilemez.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre boş geçilemez.");
            RuleFor(p => p.Password).MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
            RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Şifreniz en az bir adet büyük harf içermelidir.");
            RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Şifreniz en az bir adet küçük harf içermelidir.");
            RuleFor(p => p.Password).Matches("[0-9]").WithMessage("Şifreniz en az bir adet sayı içermelidir.");
            RuleFor(p => p.Password).Matches("[^a-zA-Z0-9]").WithMessage("Şifreniz en az bir adet sayı içermelidir.");
        }
    }
}
