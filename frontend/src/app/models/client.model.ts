// ============================================
// MODÈLE: Client
// Définit la structure des données pour les clients
// ============================================

/**
 * Interface pour un client complet (retourné par l'API)
 * Correspond à l'entité Client du backend
 */
export interface Client {
    identifiant: number;              // ID unique du client
    nom: string;                      // Nom du client
    prénom_ou_raison_sociale?: string; // Prénom ou raison sociale (optionnel)
    email: string;                    // Email (obligatoire et unique)
    téléphone?: string;               // Téléphone (optionnel)
    adresse?: string;                 // Adresse (optionnelle)
    date_de_création: Date;           // Date de création automatique
}

/**
 * Interface pour la création d'un client
 * Utilisée lors de l'envoi d'une requête POST
 */
export interface CreateClient {
    nom: string;                      // Nom du client (obligatoire)
    prénom_ou_raison_sociale?: string; // Prénom ou raison sociale (optionnel)
    email: string;                    // Email (obligatoire)
    téléphone?: string;               // Téléphone (optionnel)
    adresse?: string;                 // Adresse (optionnelle)
}

/**
 * Interface pour la mise à jour d'un client
 * Utilisée lors de l'envoi d'une requête PUT
 */
export interface UpdateClient {
    nom: string;                      // Nom du client (obligatoire)
    prénom_ou_raison_sociale?: string; // Prénom ou raison sociale (optionnel)
    email: string;                    // Email (obligatoire)
    téléphone?: string;               // Téléphone (optionnel)
    adresse?: string;                 // Adresse (optionnelle)
}