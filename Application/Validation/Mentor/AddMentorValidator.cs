using Application.Command.Mentor;
using FluentValidation;

namespace Application.Validation.Mentor;

public class AddMentorValidator : AbstractValidator<CreateMentorCommand>
{
    public AddMentorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş olamaz")
            .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Soyad boş olamaz")
            .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş olamaz")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz")
            .MaximumLength(100).WithMessage("Email en fazla 100 karakter olabilir");
    }
}