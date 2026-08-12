using Application.Command.Mentor;
using FluentValidation;

namespace Application.Validation.Mentor;

public class AddMentorValidator : AbstractValidator<CreateMentorCommand>
{
    public AddMentorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş olamaz")
            .MinimumLength(2).WithMessage("Ad en az 2 karakter olmalıdır")
            .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir")
            .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ]+$").WithMessage("Ad sadece harflerden oluşmalıdır");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Soyad boş olamaz")
            .MinimumLength(2).WithMessage("Soyad en az 2 karakter olmalıdır")
            .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir")
            .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ]+$").WithMessage("Soyad sadece harflerden oluşmalıdır");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş olamaz")
            .MinimumLength(5).WithMessage("Email en az 5 karakter olmalıdır")
            .MaximumLength(100).WithMessage("Email en fazla 100 karakter olabilir")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz");
    }
}