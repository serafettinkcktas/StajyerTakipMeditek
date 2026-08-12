using Application.Command.Intern;
using FluentValidation;

namespace Application.Validation.Intern;

public class AddInternValidator : AbstractValidator<CreateInternCommand>
{
    public AddInternValidator()
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

        RuleFor(x => x.PhoneNumber)
            .Matches("^[0-9+\\s-]{10,20}$")
            .WithMessage("Telefon numarası geçersiz")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

        RuleFor(x => x.University)
            .NotEmpty().WithMessage("Üniversite boş olamaz")
            .MaximumLength(100).WithMessage("Üniversite en fazla 100 karakter olabilir");

        RuleFor(x => x.Department)
            .NotEmpty().WithMessage("Bölüm boş olamaz")
            .MaximumLength(100).WithMessage("Bölüm en fazla 100 karakter olabilir");

        RuleFor(x => x.Class)
            .InclusiveBetween(1, 6).WithMessage("Sınıf 1 ile 6 arasında olmalıdır");
    }
}