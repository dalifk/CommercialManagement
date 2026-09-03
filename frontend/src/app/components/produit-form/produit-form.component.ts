// ============================================
// COMPOSANT: Formulaire Produit
// Permet de créer ou modifier un produit
// ============================================

import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ProduitService } from '../../services/produit.service';
import { CreateProduit, UpdateProduit } from '../../models/produit.model';

@Component({
    selector: 'app-produit-form',
    standalone: true,
    imports: [FormsModule, RouterLink],
    templateUrl: './produit-form.component.html',
    styleUrls: ['./produit-form.component.scss']
})
export class ProduitFormComponent implements OnInit {
    // Objet produit pour le formulaire
    produit: CreateProduit | UpdateProduit = {
        référence: '',
        nom_du_produit: '',
        description: '',
        prix_unitaire_HT: 0,
        quantité_en_stock: 0
    };

    isEditMode = false;
    produitId: number | null = null;

    constructor(
        private produitService: ProduitService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    /**
     * Initialise le composant
     */
    ngOnInit(): void {
        this.route.params.subscribe(params => {
            if (params['id']) {
                this.isEditMode = true;
                this.produitId = +params['id'];
                this.loadProduit(this.produitId);
            }
        });
    }

    /**
     * Charge les données du produit à modifier
     */
    loadProduit(id: number): void {
        this.produitService.getProduitById(id).subscribe({
            next: (data) => {
                this.produit = data;
            },
            error: (error) => {
                alert('Erreur: ' + (error?.error?.message || "Impossible d'enregistrer le produit"));
            }
        });
    }

    /**
     * Soumet le formulaire
     */
    onSubmit(): void {
        if (this.isEditMode && this.produitId) {
            this.produitService.updateProduit(this.produitId, this.produit as UpdateProduit).subscribe({
                next: () => {
                    this.router.navigate(['/produits']);
                },
                error: (error) => {
                    alert('Erreur: ' + (error?.error?.message || "Impossible d'enregistrer le produit"));
                }
            });
        } else {
            this.produitService.createProduit(this.produit as CreateProduit).subscribe({
                next: () => {
                    this.router.navigate(['/produits']);
                },
                error: (error) => {
                    alert('Erreur: ' + (error?.error?.message || "Impossible d'enregistrer le produit"));
                }
            });
        }
    }
}