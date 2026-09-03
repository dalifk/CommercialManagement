// ============================================
// SERVICE: Client
// Gère les appels API pour les clients
// ============================================

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Client, CreateClient, UpdateClient } from '../models/client.model';

@Injectable({
    providedIn: 'root'  // Service disponible dans toute l'application
})
export class ClientService {
    // URL de base de l'API backend pour les clients
    private apiUrl = `${environment.apiUrl}/clients`;

    constructor(private http: HttpClient) { }

    /**
     * Récupère la liste de tous les clients
     * GET /api/clients
     */
    getAllClients(): Observable<Client[]> {
        return this.http.get<Client[]>(this.apiUrl);
    }

    /**
     * Récupère un client par son identifiant
     * GET /api/clients/{id}
     */
    getClientById(id: number): Observable<Client> {
        return this.http.get<Client>(`${this.apiUrl}/${id}`);
    }

    /**
     * Crée un nouveau client
     * POST /api/clients
     */
    createClient(client: CreateClient): Observable<Client> {
        return this.http.post<Client>(this.apiUrl, client);
    }

    /**
     * Met à jour un client existant
     * PUT /api/clients/{id}
     */
    updateClient(id: number, client: UpdateClient): Observable<Client> {
        return this.http.put<Client>(`${this.apiUrl}/${id}`, client);
    }

    /**
     * Supprime un client
     * DELETE /api/clients/{id}
     */
    deleteClient(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}