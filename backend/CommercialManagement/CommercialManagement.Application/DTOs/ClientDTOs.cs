#nullable enable

using System;

namespace CommercialManagement.Application.DTOs
{
    /// <summary>
    /// DTO pour afficher un client
    /// </summary>
    public class ClientDTO
    {
        public int Identifiant { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string? Prénom_ou_raison_sociale { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Téléphone { get; set; }
        public string? Adresse { get; set; }
        public DateTime Date_de_création { get; set; }
    }

    /// <summary>
    /// DTO pour créer un client
    /// </summary>
    public class CreateClientDTO
    {
        public string Nom { get; set; } = string.Empty;
        public string? Prénom_ou_raison_sociale { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Téléphone { get; set; }
        public string? Adresse { get; set; }
    }

    /// <summary>
    /// DTO pour mettre à jour un client
    /// </summary>
    public class UpdateClientDTO
    {
        public string Nom { get; set; } = string.Empty;
        public string? Prénom_ou_raison_sociale { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Téléphone { get; set; }
        public string? Adresse { get; set; }
    }
}