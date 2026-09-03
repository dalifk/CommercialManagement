using FluentValidation;
using CommercialManagement.Application.DTOs;

namespace CommercialManagement.Application.Validators
{
    /// <summary>
    /// Validateur pour la création d'un client
    /// </summary>
    public class CreateClientValidator : AbstractValidator<CreateClientDTO>
    {
        public CreateClientValidator()
        {
            RuleFor(x => x.Nom)
                .NotEmpty().WithMessage("Le nom est requis")
                .MaximumLength(100).WithMessage("Le nom ne peut pas dépasser 100 caractères");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("L'email est requis")
                .EmailAddress().WithMessage("Format d'email invalide")
                .MaximumLength(255).WithMessage("L'email ne peut pas dépasser 255 caractères");

            RuleFor(x => x.Téléphone)
                .MaximumLength(20).WithMessage("Le téléphone ne peut pas dépasser 20 caractères")
                .Matches(@"^[0-9+\s\-\(\)]*$").WithMessage("Format de téléphone invalide")
                .When(x => !string.IsNullOrEmpty(x.Téléphone));

            RuleFor(x => x.Adresse)
                .MaximumLength(500).WithMessage("L'adresse ne peut pas dépasser 500 caractères");
        }
    }
}