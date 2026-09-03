// ============================================
// MODÈLE: Commande et Lignes de commande
// Définit la structure des données pour les commandes
// ============================================

/**
 * Interface pour une commande complète (retournée par l'API)
 * Correspond à l'entité Commande du backend
 */
export interface Commande {
    identifiant: number;                    // ID unique de la commande
    numéro_de_commande: string;             // Numéro unique (format: ORD-YYYYMMDD-XXXXXX)
    client_associé: number;                 // ID du client associé
    clientNom: string;                      // Nom complet du client (affichage)
    date_de_commande: Date;                 // Date de la commande
    statut_de_la_commande: string;          // Statut: Brouillon, Validée, Annulée
    total_HT: number;                       // Total hors taxes
    total_TTC: number;                      // Total toutes taxes comprises (HT × 1.19)
    lignes_de_commande: LigneDeCommande[];  // Liste des lignes de la commande
}

/**
 * Interface pour une ligne de commande
 * Représente un produit commandé avec sa quantité
 */
export interface LigneDeCommande {
    identifiant: number;                // ID unique de la ligne
    produit_identifiant: number;        // ID du produit commandé
    produitNom: string;                 // Nom du produit (affichage)
    produitReference: string;           // Référence du produit (affichage)
    quantité: number;                   // Quantité commandée
    prix_unitaire: number;              // Prix unitaire au moment de la commande
    total_ligne: number;                // Total = quantité × prix unitaire
}

/**
 * Interface pour la création d'une commande
 * Utilisée lors de l'envoi d'une requête POST
 */
export interface CreateCommande {
    client_associé: number;                     // ID du client (obligatoire)
    statut_de_la_commande: string;              // Statut par défaut: Brouillon
    lignes_de_commande: CreateLigneDeCommande[]; // Liste des produits commandés
}

/**
 * Interface pour la création d'une ligne de commande
 * Utilisée lors de la création d'une commande
 */
export interface CreateLigneDeCommande {
    produit_identifiant: number;        // ID du produit (obligatoire)
    quantité: number;                   // Quantité commandée (obligatoire, > 0)
}

/**
 * Interface pour la mise à jour d'une commande
 * Utilisée lors de l'envoi d'une requête PUT
 */
export interface UpdateCommande {
    client_associé: number;                     // ID du client (obligatoire)
    statut_de_la_commande: string;              // Nouveau statut
    lignes_de_commande: UpdateLigneDeCommande[]; // Lignes mises à jour
}

/**
 * Interface pour la mise à jour d'une ligne de commande
 * Utilisée lors de la modification d'une commande
 */
export interface UpdateLigneDeCommande {
    identifiant?: number;               // ID existant (pour les lignes à modifier)
    produit_identifiant: number;        // ID du produit (obligatoire)
    quantité: number;                   // Nouvelle quantité (obligatoire, > 0)
}