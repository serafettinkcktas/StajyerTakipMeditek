using Application.DTOs.Auth;
using FluentValidation;

namespace Application.Validation.Auth;

public class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş olamaz")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz")
            .MaximumLength(100).WithMessage("Email en fazla 100 karakter olabilir");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş olamaz")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır")
            .MaximumLength(255).WithMessage("Şifre en fazla 255 karakter olabilir");
    }
}