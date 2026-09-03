// ============================================
// SERVICE: Authentification
// Gère la connexion, le token JWT et l'état de l'utilisateur
// ============================================

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

export interface LoginRequest {
    email: string;
    password: string;
}

export interface LoginResponse {
    token: string;
    message: string;
}

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    // URL de l'API d'authentification
    private apiUrl = 'http://localhost:5230/api/auth';

    // Subject pour suivre l'état de connexion
    private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.hasToken());
    public isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

    constructor(private http: HttpClient) { }

    /**
     * Connecte l'utilisateur et stocke le token JWT
     * POST /api/auth/login
     */
    login(credentials: LoginRequest): Observable<LoginResponse> {
        return this.http.post<LoginResponse>(`${this.apiUrl}/login`, credentials)
            .pipe(
                tap(response => {
                    // Stocke le token dans le localStorage
                    localStorage.setItem('token', response.token);
                    // Met à jour l'état de connexion
                    this.isAuthenticatedSubject.next(true);
                })
            );
    }

    /**
     * Déconnecte l'utilisateur
     * Supprime le token du localStorage
     */
    logout(): void {
        localStorage.removeItem('token');
        this.isAuthenticatedSubject.next(false);
    }

    /**
     * Vérifie si un token existe dans le localStorage
     */
    private hasToken(): boolean {
        return !!localStorage.getItem('token');
    }

    /**
     * Récupère le token JWT
     */
    getToken(): string | null {
        return localStorage.getItem('token');
    }

    /**
     * Vérifie si l'utilisateur est authentifié
     */
    isAuthenticated(): boolean {
        return this.hasToken();
    }
}