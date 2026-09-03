// ============================================
// COMPOSANT: Liste des Produits
// Affiche la liste de tous les produits
// ============================================

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';      // ← AJOUT: Pour *ngIf, *ngFor, currency pipe
import { RouterModule } from '@angular/router';      // ← AJOUT: Pour routerLink
import { ActivatedRoute } from '@angular/router';
import { ProduitService } from '../../services/produit.service';
import { Produit } from '../../models/produit.model';

@Component({
    selector: 'app-produit-list',
    templateUrl: './produit-list.component.html',
    styleUrls: ['./produit-list.component.scss'],
    imports: [CommonModule, RouterModule]  // ← AJOUT: Import des modules nécessaires
})
export class ProduitListComponent implements OnInit {
    produits: Produit[] = [];

    constructor(
        private produitService: ProduitService,
        private route: ActivatedRoute
    ) { }

    ngOnInit(): void {
        // ✅ Recharge les données à chaque changement de route
        this.route.params.subscribe(() => {
            this.loadProduits();
        });
    }

    loadProduits(): void {
        this.produitService.getAllProduits().subscribe({
            next: (data) => {
                this.produits = data;
                console.log('✅ Produits chargés:', this.produits.length);
            },
            error: (error) => {
                console.error('❌ Erreur:', error);
                alert('Erreur: Impossible de charger les produits');
            }
        });
    }

    deleteProduit(id: number): void {
        if (confirm('Êtes-vous sûr de vouloir supprimer ce produit ?')) {
            this.produitService.deleteProduit(id).subscribe({
                next: () => {
                    this.loadProduits();
                },
                error: (error) => {
                    console.error('❌ Erreur:', error);
                    alert('Erreur: ' + error.error.message);
                }
            });
        }
    }
}