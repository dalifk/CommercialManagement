// ============================================
// COMPOSANT PRINCIPAL
// Contient la barre de navigation et le conteneur des pages
// ============================================

import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from './auth/auth.service';

@Component({
    selector: 'app-root',
    imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
    templateUrl: './app.html',
    styleUrl: './app.scss'
})
export class App {
    // Titre affiché dans la barre de navigation
    title = 'Gestion Commerciale';

    constructor(
        public authService: AuthService,
        private router: Router
    ) { }

    /**
     * Déconnecte l'utilisateur :
     * supprime le jeton JWT puis redirige vers la page de connexion
     */
    logout(): void {
        this.authService.logout();
        this.router.navigate(['/login']);
    }
}
