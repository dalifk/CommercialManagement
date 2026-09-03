// ============================================
// SERVICE: Produit
// Gère les appels API pour les produits
// ============================================

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Produit, CreateProduit, UpdateProduit } from '../models/produit.model';

@Injectable({
    providedIn: 'root'  // Service disponible dans toute l'application
})
export class ProduitService {
    // URL de base de l'API backend pour les produits
    private apiUrl = `${environment.apiUrl}/produits`;

    constructor(private http: HttpClient) { }

    /**
     * Récupère la liste de tous les produits
     * GET /api/produits
     */
    getAllProduits(): Observable<Produit[]> {
        return this.http.get<Produit[]>(this.apiUrl);
    }

    /**
     * Récupère un produit par son identifiant
     * GET /api/produits/{id}
     */
    getProduitById(id: number): Observable<Produit> {
        return this.http.get<Produit>(`${this.apiUrl}/${id}`);
    }

    /**
     * Crée un nouveau produit
     * POST /api/produits
     */
    createProduit(produit: CreateProduit): Observable<Produit> {
        return this.http.post<Produit>(this.apiUrl, produit);
    }

    /**
     * Met à jour un produit existant
     * PUT /api/produits/{id}
     */
    updateProduit(id: number, produit: UpdateProduit): Observable<Produit> {
        return this.http.put<Produit>(`${this.apiUrl}/${id}`, produit);
    }

    /**
     * Supprime un produit
     * DELETE /api/produits/{id}
     */
    deleteProduit(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}