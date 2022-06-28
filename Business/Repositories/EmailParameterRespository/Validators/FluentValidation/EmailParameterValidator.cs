using Entities.Concrete;
using FluentValidation;

namespace Business.Repositories.EmailParameterRespository.Validators.FluentValidation
{
    public class EmailParameterValidator:AbstractValidator<EmailParameter>
    {
        public EmailParameterValidator()
        {
            RuleFor(p=>p.Smtp).NotEmpty().WithMessage("SMTP adresi boş olamaz.");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email adresi boş olamaz.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password boş olamaz.");
            RuleFor(p => p.Port).NotEmpty().WithMessage("Port boş olamaz.");
          
        }
    }
}
