// ============================================
// COMPOSANT PRINCIPAL
// Contient la barre de navigation et le conteneur principal
// ============================================

import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './auth/auth.service';

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [CommonModule, RouterModule],
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    // Titre de l'application
    title = 'Gestion Commerciale';

    constructor(
        private authService: AuthService,
        private router: Router
    ) { }

    /**
     * Déconnecte l'utilisateur
     * Supprime le token du localStorage et redirige vers la page de connexion
     */
    logout(): void {
        // Appelle le service pour déconnecter l'utilisateur
        this.authService.logout();
        
        // Redirige vers la page de connexion
        this.router.navigate(['/login']);
        
        // Affiche un message dans la console
        console.log('👋 Utilisateur déconnecté');
    }
}