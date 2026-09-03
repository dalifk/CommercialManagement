// ============================================
// GUARD: Protection des Routes
// Empêche l'accès aux pages sans authentification
// ============================================

import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {

    constructor(
        private authService: AuthService,
        private router: Router
    ) { }

    canActivate(): boolean {
        if (this.authService.isAuthenticated()) {
            console.log('✅ Utilisateur authentifié - Accès autorisé');
            return true;
        }

        console.warn('⚠️ Utilisateur non authentifié - Redirection vers /login');
        this.router.navigate(['/login']);
        return false;
    }
}