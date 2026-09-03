// ============================================
// COMPOSANT: Formulaire Commande
// Permet de créer une commande avec ses lignes
// ============================================

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommandeService } from '../../services/commande.service';
import { ClientService } from '../../services/client.service';
import { ProduitService } from '../../services/produit.service';
import { CreateCommande, CreateLigneDeCommande } from '../../models/commande.model';
import { Client } from '../../models/client.model';
import { Produit } from '../../models/produit.model';

@Component({
    selector: 'app-commande-form',
    standalone: true,
    imports: [CommonModule, FormsModule, RouterLink],
    templateUrl: './commande-form.component.html',
    styleUrls: ['./commande-form.component.scss']
})
export class CommandeFormComponent implements OnInit {
    // Liste des clients pour le select
    clients: Client[] = [];
    
    // Liste des produits pour le select
    produits: Produit[] = [];
    
    // Lignes de commande (commence avec une ligne vide)
    lignes: CreateLigneDeCommande[] = [{ produit_identifiant: 0, quantité: 1 }];
    
    // Objet commande
    commande: CreateCommande = {
        client_associé: 0,
        statut_de_la_commande: 'Brouillon',
        lignes_de_commande: []
    };

    constructor(
        private commandeService: CommandeService,
        private clientService: ClientService,
        private produitService: ProduitService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    /**
     * Initialise le composant en chargeant les listes
     */
    ngOnInit(): void {
        this.loadClients();
        this.loadProduits();
    }

    /**
     * Charge la liste des clients
     */
    loadClients(): void {
        this.clientService.getAllClients().subscribe({
            next: (data) => {
                this.clients = data;
            },
            error: (error) => {
                alert('Erreur: ' + (error?.error?.message || 'Opération impossible'));
            }
        });
    }

    /**
     * Charge la liste des produits
     */
    loadProduits(): void {
        this.produitService.getAllProduits().subscribe({
            next: (data) => {
                this.produits = data;
            },
            error: (error) => {
                alert('Erreur: ' + (error?.error?.message || 'Opération impossible'));
            }
        });
    }

    /**
     * Ajoute une nouvelle ligne de commande
     */
    addLine(): void {
        this.lignes.push({ produit_identifiant: 0, quantité: 1 });
    }

    /**
     * Supprime une ligne de commande
     */
    removeLine(index: number): void {
        this.lignes.splice(index, 1);
    }

    /**
     * Récupère le prix d'un produit par son ID
     */
    getProduitPrix(produitId: number): number {
        const id = Number(produitId);
        const produit = this.produits.find(p => p.identifiant === id);
        return produit ? Number(produit.prix_unitaire_HT) : 0;
    }

    /**
     * Calcule le total HT de la commande
     */
    getTotalHT(): number {
        let total = 0;
        for (const ligne of this.lignes) {
            total += ligne.quantité * this.getProduitPrix(ligne.produit_identifiant);
        }
        return total;
    }

    /**
     * Calcule la TVA (19%)
     */
    getTVA(): number {
        return this.getTotalHT() * 0.19;
    }

    /**
     * Calcule le total TTC
     */
    getTotalTTC(): number {
        return this.getTotalHT() * 1.19;
    }

    /**
     * Soumet le formulaire
     */
    onSubmit(): void {
        if (!this.commande.client_associé) {
            alert('Veuillez sélectionner un client');
            return;
        }

        const lignesValides = this.lignes.filter(l => Number(l.produit_identifiant) > 0 && Number(l.quantité) > 0);
        if (lignesValides.length === 0) {
            alert('Ajoutez au moins un produit à la commande');
            return;
        }

        this.commande.lignes_de_commande = lignesValides.map(l => ({
            produit_identifiant: Number(l.produit_identifiant),
            quantité: Number(l.quantité)
        }));

        this.commandeService.createCommande(this.commande).subscribe({
            next: () => {
                this.router.navigate(['/commandes']);
            },
            error: (error) => {
                alert('Erreur: ' + (error?.error?.message || 'Opération impossible'));
            }
        });
    }
}