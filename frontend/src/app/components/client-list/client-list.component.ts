// ============================================
// COMPOSANT: Liste des Clients
// Affiche la liste de tous les clients
// ============================================

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';      // ← AJOUT
import { RouterModule } from '@angular/router';      // ← AJOUT
import { ActivatedRoute } from '@angular/router';
import { ClientService } from '../../services/client.service';
import { Client } from '../../models/client.model';

@Component({
    selector: 'app-client-list',
    templateUrl: './client-list.component.html',
    styleUrls: ['./client-list.component.scss'],
    imports: [CommonModule, RouterModule]  // ← AJOUT
})
export class ClientListComponent implements OnInit {
    clients: Client[] = [];

    constructor(
        private clientService: ClientService,
        private route: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.route.params.subscribe(() => {
            this.loadClients();
        });
    }

    loadClients(): void {
        this.clientService.getAllClients().subscribe({
            next: (data) => {
                this.clients = data;
                console.log('✅ Clients chargés:', this.clients.length);
            },
            error: (error) => {
                console.error('❌ Erreur:', error);
                alert('Erreur: Impossible de charger les clients');
            }
        });
    }

    deleteClient(id: number): void {
        if (confirm('Êtes-vous sûr de vouloir supprimer ce client ?')) {
            this.clientService.deleteClient(id).subscribe({
                next: () => {
                    this.loadClients();
                },
                error: (error) => {
                    console.error('❌ Erreur:', error);
                    alert('Erreur: ' + error.error.message);
                }
            });
        }
    }
}