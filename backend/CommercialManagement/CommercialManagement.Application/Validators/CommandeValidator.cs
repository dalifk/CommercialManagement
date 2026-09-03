using FluentValidation;
using CommercialManagement.Application.DTOs;

namespace CommercialManagement.Application.Validators
{
    /// <summary>
    /// Validateur pour la création d'une commande
    /// </summary>
    public class CreateCommandeValidator : AbstractValidator<CreateCommandeDTO>
    {
        public CreateCommandeValidator()
        {
            RuleFor(x => x.Client_associé)
                .GreaterThan(0).WithMessage("Un client doit être sélectionné");

            RuleFor(x => x.Lignes_de_commande)
                .NotEmpty().WithMessage("La commande doit contenir au moins un produit");

            RuleFor(x => x.Statut_de_la_commande)
                .Must(statut => statut == "Brouillon" || statut == "Validée" || statut == "Annulée")
                .WithMessage("Le statut doit être Brouillon, Validée ou Annulée");

            RuleForEach(x => x.Lignes_de_commande).SetValidator(new CreateLigneDeCommandeValidator());
        }
    }

    /// <summary>
    /// Validateur pour la création d'une ligne de commande
    /// </summary>
    public class CreateLigneDeCommandeValidator : AbstractValidator<CreateLigneDeCommandeDTO>
    {
        public CreateLigneDeCommandeValidator()
        {
            RuleFor(x => x.Produit_Identifiant)
                .GreaterThan(0).WithMessage("Un produit doit être sélectionné");

            RuleFor(x => x.Quantité)
                .GreaterThan(0).WithMessage("La quantité doit être supérieure à 0");
        }
    }
}