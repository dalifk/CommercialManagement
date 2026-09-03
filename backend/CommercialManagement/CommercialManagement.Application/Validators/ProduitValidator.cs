using FluentValidation;
using CommercialManagement.Application.DTOs;

namespace CommercialManagement.Application.Validators
{
    /// <summary>
    /// Validateur pour la création d'un produit
    /// </summary>
    public class CreateProduitValidator : AbstractValidator<CreateProduitDTO>
    {
        public CreateProduitValidator()
        {
            RuleFor(x => x.Référence)
                .NotEmpty().WithMessage("La référence est requise")
                .MaximumLength(50).WithMessage("La référence ne peut pas dépasser 50 caractères");

            RuleFor(x => x.Nom_du_produit)
                .NotEmpty().WithMessage("Le nom du produit est requis")
                .MaximumLength(100).WithMessage("Le nom ne peut pas dépasser 100 caractères");

            RuleFor(x => x.Prix_unitaire_HT)
                .GreaterThanOrEqualTo(0).WithMessage("Le prix doit être supérieur ou égal à 0");

            RuleFor(x => x.Quantité_en_stock)
                .GreaterThanOrEqualTo(0).WithMessage("La quantité en stock doit être supérieure ou égale à 0");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La description ne peut pas dépasser 500 caractères");
        }
    }
}