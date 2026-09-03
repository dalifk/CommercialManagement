// ============================================
// MODÈLE: Produit
// Définit la structure des données pour les produits
// ============================================

/**
 * Interface pour un produit complet (retourné par l'API)
 * Correspond à l'entité Produit du backend
 */
export interface Produit {
    identifiant: number;              // ID unique du produit
    référence: string;                // Référence unique du produit
    nom_du_produit: string;           // Nom commercial du produit
    description?: string;             // Description (optionnelle)
    prix_unitaire_HT: number;         // Prix unitaire hors taxes
    quantité_en_stock: number;        // Quantité disponible en stock
    date_de_création: Date;           // Date de création automatique
}

/**
 * Interface pour la création d'un produit
 * Utilisée lors de l'envoi d'une requête POST
 */
export interface CreateProduit {
    référence: string;                // Référence unique (obligatoire)
    nom_du_produit: string;           // Nom du produit (obligatoire)
    description?: string;             // Description (optionnelle)
    prix_unitaire_HT: number;         // Prix unitaire HT (obligatoire)
    quantité_en_stock: number;        // Quantité en stock (obligatoire)
}

/**
 * Interface pour la mise à jour d'un produit
 * Utilisée lors de l'envoi d'une requête PUT
 */
export interface UpdateProduit {
    référence: string;                // Référence unique (obligatoire)
    nom_du_produit: string;           // Nom du produit (obligatoire)
    description?: string;             // Description (optionnelle)
    prix_unitaire_HT: number;         // Prix unitaire HT (obligatoire)
    quantité_en_stock: number;        // Quantité en stock (obligatoire)
}