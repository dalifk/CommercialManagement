// ============================================
// SERVICE: Commande
// Gère les appels API pour les commandes
// ============================================

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Commande, CreateCommande, UpdateCommande } from '../models/commande.model';

@Injectable({
    providedIn: 'root'  // Service disponible dans toute l'application
})
export class CommandeService {
    // URL de base de l'API backend pour les commandes
    private apiUrl = `${environment.apiUrl}/commandes`;

    constructor(private http: HttpClient) { }

    /**
     * Récupère la liste de toutes les commandes
     * GET /api/commandes
     */
    getAllCommandes(): Observable<Commande[]> {
        return this.http.get<Commande[]>(this.apiUrl);
    }

    /**
     * Récupère une commande par son identifiant avec toutes ses lignes
     * GET /api/commandes/{id}
     */
    getCommandeById(id: number): Observable<Commande> {
        return this.http.get<Commande>(`${this.apiUrl}/${id}`);
    }

    /**
     * Crée une nouvelle commande
     * POST /api/commandes
     */
    createCommande(commande: CreateCommande): Observable<Commande> {
        return this.http.post<Commande>(this.apiUrl, commande);
    }

    /**
     * Met à jour une commande existante
     * PUT /api/commandes/{id}
     */
    updateCommande(id: number, commande: UpdateCommande): Observable<Commande> {
        return this.http.put<Commande>(`${this.apiUrl}/${id}`, commande);
    }

    /**
     * Supprime une commande
     * DELETE /api/commandes/{id}
     */
    deleteCommande(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }

    /**
     * Valide une commande (met à jour le stock des produits)
     * PATCH /api/commandes/{id}/validate
     */
    validateCommande(id: number): Observable<void> {
        return this.http.patch<void>(`${this.apiUrl}/${id}/validate`, {});
    }
}