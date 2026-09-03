// ============================================
// ROUTES DE L'APPLICATION
// Définit les URLs et les composants associés
// ============================================

import { Routes } from '@angular/router';

// Importation des composants
import { ClientListComponent } from './components/client-list/client-list.component';
import { ClientFormComponent } from './components/client-form/client-form.component';
import { ProduitListComponent } from './components/produit-list/produit-list.component';
import { ProduitFormComponent } from './components/produit-form/produit-form.component';
import { CommandeListComponent } from './components/commande-list/commande-list.component';
import { CommandeFormComponent } from './components/commande-form/commande-form.component';
import { CommandeDetailComponent } from './components/commande-detail/commande-detail.component';

// Importation des composants d'authentification
import { LoginComponent } from './auth/login/login.component';
import { AuthGuard } from './auth/auth.guard';

export const routes: Routes = [
    // Page de connexion (publique - sans protection)
    { path: 'login', component: LoginComponent },

    // Routes protégées par authentification
    { path: '', redirectTo: '/clients', pathMatch: 'full' },
    { path: 'clients', component: ClientListComponent, canActivate: [AuthGuard] },
    { path: 'clients/new', component: ClientFormComponent, canActivate: [AuthGuard] },
    { path: 'clients/edit/:id', component: ClientFormComponent, canActivate: [AuthGuard] },
    { path: 'produits', component: ProduitListComponent, canActivate: [AuthGuard] },
    { path: 'produits/new', component: ProduitFormComponent, canActivate: [AuthGuard] },
    { path: 'produits/edit/:id', component: ProduitFormComponent, canActivate: [AuthGuard] },
    { path: 'commandes', component: CommandeListComponent, canActivate: [AuthGuard] },
    { path: 'commandes/new', component: CommandeFormComponent, canActivate: [AuthGuard] },
    { path: 'commandes/edit/:id', component: CommandeFormComponent, canActivate: [AuthGuard] },
    { path: 'commandes/detail/:id', component: CommandeDetailComponent, canActivate: [AuthGuard] }
];