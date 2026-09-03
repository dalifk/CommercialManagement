// ============================================
// COMPOSANT: Formulaire Client
// Permet de créer ou modifier un client
// ============================================

import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ClientService } from '../../services/client.service';
import { CreateClient, UpdateClient } from '../../models/client.model';

@Component({
    selector: 'app-client-form',
    standalone: true,
    imports: [FormsModule, RouterLink],
    templateUrl: './client-form.component.html',
    styleUrls: ['./client-form.component.scss']
})
export class ClientFormComponent implements OnInit {
    // Objet client pour le formulaire
    client: CreateClient | UpdateClient = {
        nom: '',
        prénom_ou_raison_sociale: '',
        email: '',
        téléphone: '',
        adresse: ''
    };

    // Mode édition ou création
    isEditMode = false;
    clientId: number | null = null;

    constructor(
        private clientService: ClientService,
        private route: ActivatedRoute,    // Pour lire les paramètres de l'URL
        private router: Router            // Pour naviguer après soumission
    ) { }

    /**
     * Initialise le composant
     * Vérifie si on est en mode édition ou création
     */
    ngOnInit(): void {
        this.route.params.subscribe(params => {
            if (params['id']) {
                // Mode édition : on a un ID dans l'URL
                this.isEditMode = true;
                this.clientId = +params['id'];
                this.loadClient(this.clientId);
            }
        });
    }

    /**
     * Charge les données du client à modifier
     */
    loadClient(id: number): void {
        this.clientService.getClientById(id).subscribe({
            next: (data) => {
                this.client = data;  // Remplit le formulaire
            },
            error: (error) => {
                alert('Erreur: ' + (error?.error?.message || 'Impossible de charger le client'));
            }
        });
    }

    /**
     * Soumet le formulaire (création ou mise à jour)
     */
    onSubmit(): void {
        if (this.isEditMode && this.clientId) {
            // Mise à jour d'un client existant
            this.clientService.updateClient(this.clientId, this.client as UpdateClient).subscribe({
                next: () => {
                    this.router.navigate(['/clients']);  // Retour à la liste
                },
                error: (error) => {
                    alert('Erreur: ' + (error?.error?.message || 'Impossible de charger le client'));
                }
            });
        } else {
            // Création d'un nouveau client
            this.clientService.createClient(this.client as CreateClient).subscribe({
                next: () => {
                    this.router.navigate(['/clients']);  // Retour à la liste
                },
                error: (error) => {
                    alert('Erreur: ' + (error?.error?.message || 'Impossible de charger le client'));
                }
            });
        }
    }
}