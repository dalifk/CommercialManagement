// ============================================
// COMPOSANT: Liste des Commandes
// Affiche la liste de toutes les commandes
// ============================================

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';      // ← Pour les pipes (date, currency) et *ngIf, *ngFor
import { RouterModule } from '@angular/router';      // ← Pour routerLink
import { ActivatedRoute } from '@angular/router';
import { CommandeService } from '../../services/commande.service';
import { Commande } from '../../models/commande.model';

@Component({
    selector: 'app-commande-list',
    templateUrl: './commande-list.component.html',
    styleUrls: ['./commande-list.component.scss'],
    imports: [CommonModule, RouterModule]  // ← Import des modules nécessaires
})
export class CommandeListComponent implements OnInit {
    commandes: Commande[] = [];

    constructor(
        private commandeService: CommandeService,
        private route: ActivatedRoute
    ) { }

    ngOnInit(): void {
        // ✅ Recharge les données à chaque changement de route (évite le double-clic)
        this.route.params.subscribe(() => {
            this.loadCommandes();
        });
    }

    loadCommandes(): void {
        this.commandeService.getAllCommandes().subscribe({
            next: (data) => {
                this.commandes = data;
                console.log('✅ Commandes chargées:', this.commandes.length);
            },
            error: (error) => {
                console.error('❌ Erreur:', error);
                alert('Erreur: Impossible de charger les commandes');
            }
        });
    }

    validateCommande(id: number): void {
        if (confirm('Valider cette commande ? Le stock sera mis à jour.')) {
            this.commandeService.validateCommande(id).subscribe({
                next: () => {
                    this.loadCommandes();
                },
                error: (error) => {
                    console.error('❌ Erreur:', error);
                    alert('Erreur: ' + error.error.message);
                }
            });
        }
    }

    deleteCommande(id: number): void {
        if (confirm('Êtes-vous sûr de vouloir supprimer cette commande ?')) {
            this.commandeService.deleteCommande(id).subscribe({
                next: () => {
                    this.loadCommandes();
                },
                error: (error) => {
                    console.error('❌ Erreur:', error);
                    alert('Erreur: ' + error.error.message);
                }
            });
        }
    }
}