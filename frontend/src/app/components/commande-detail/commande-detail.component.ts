// ============================================
// COMPOSANT: Détail d'une Commande
// Affiche les détails d'une commande spécifique
// ============================================

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommandeService } from '../../services/commande.service';
import { Commande } from '../../models/commande.model';

@Component({
    selector: 'app-commande-detail',
    standalone: true,
    imports: [CommonModule, RouterLink],
    templateUrl: './commande-detail.component.html',
    styleUrls: ['./commande-detail.component.scss']
})
export class CommandeDetailComponent implements OnInit {
    // Commande à afficher
    commande: Commande | null = null;

    constructor(
        private commandeService: CommandeService,
        private route: ActivatedRoute
    ) { }

    /**
     * Initialise le composant en chargeant les détails de la commande
     */
    ngOnInit(): void {
        this.route.params.subscribe(params => {
            if (params['id']) {
                this.loadCommande(+params['id']);
            }
        });
    }

    /**
     * Charge les détails d'une commande depuis l'API
     */
    loadCommande(id: number): void {
        this.commandeService.getCommandeById(id).subscribe({
            next: (data) => {
                this.commande = data;
            },
            error: (error) => {
                alert('Erreur: ' + (error?.error?.message || 'Opération impossible'));
            }
        });
    }

    /**
     * Valide la commande (bouton affiché uniquement si brouillon)
     */
    validateCommande(): void {
        if (this.commande && confirm('Valider cette commande ? Le stock sera mis à jour.')) {
            this.commandeService.validateCommande(this.commande.identifiant).subscribe({
                next: () => {
                    // Recharge la commande pour voir le statut mis à jour
                    this.loadCommande(this.commande!.identifiant);
                },
                error: (error) => {
                    alert('Erreur: ' + (error?.error?.message || 'Opération impossible'));
                }
            });
        }
    }
}