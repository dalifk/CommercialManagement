# 🏢 Application de Gestion Commerciale

---

## 📌 Présentation

Application web de gestion commerciale développée avec :
- **Backend** : .NET 8
- **Frontend** : Angular
- **Base de données** : MySQL

---

## 🔑 Identifiants de connexion

| Champ | Valeur |
|-------|--------|
| **Email** | `admin@commercial.com` |
| **Mot de passe** | `Admin123!` |

> ⚠️ Ces identifiants sont stockés dans la base de données (table `utilisateurs`).

---

## 🛠️ Technologies utilisées

### Backend
- .NET 8
- Entity Framework Core
- MySQL
- JWT pour l'authentification
- Swagger pour la documentation API

### Frontend
- Angular
- Bootstrap
- RxJS

---

## 📁 Structure du projet
CommercialManagement/
├── backend/
│ └── CommercialManagement/
│ ├── CommercialManagement.API/ # API REST (contrôleurs, Program.cs)
│ ├── CommercialManagement.Application/ # DTOs, Services, Validators
│ ├── CommercialManagement.Domain/ # Entités (Clients, Produits, Commandes)
│ └── CommercialManagement.Infrastructure/ # DbContext, Migrations
├── frontend/
│ └── commercial-management/ # Application Angular
│ ├── src/app/
│ │ ├── auth/ # Authentification
│ │ ├── components/ # Composants
│ │ ├── models/ # Modèles TypeScript
│ │ └── services/ # Services API
│ └── ...
└── DatabaseScripts/
└── CreateDatabase_MySQL.sql # Script de création de la base de données

---

## 🚀 Installation et lancement

### 1. Prérequis

| Logiciel | Version |
|----------|---------|
| .NET SDK | 8.0+ |
| Node.js | 18+ |
| Angular CLI | 17+ |
| MySQL (XAMPP) | 8.0+ |

---

### 2. Base de données

#### Via phpMyAdmin (recommandé)
1. Ouvrir `http://localhost/phpmyadmin`
2. Créer une base de données `CommercialManagementDb`
3. Importer le script : `DatabaseScripts/CreateDatabase_MySQL.sql`
4. Ajouter l'utilisateur admin :
```sql
INSERT INTO utilisateurs (Email, MotDePasse, Nom, Role)
VALUES ('admin@commercial.com', 'Admin123!', 'Administrateur', 'Admin');

# Aller dans le dossier du backend
cd backend/CommercialManagement

# Restaurer les packages
dotnet restore

# Appliquer les migrations
cd CommercialManagement.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../CommercialManagement.API
dotnet ef database update --startup-project ../CommercialManagement.API

# Lancer l'API
cd ../CommercialManagement.API
dotnet run

L'API sera disponible sur : http://localhost:5230

Swagger : http://localhost:5230/swagger 


# Aller dans le dossier du frontend
cd frontend/commercial-management

# Installer les dépendances
npm install

# Lancer l'application
ng serve
L'application sera disponible sur : http://localhost:4200

Se connecter

Ouvrir http://localhost:4200

Saisir les identifiants :

Email : admin@commercial.com

Mot de passe : Admin123!

Cliquer sur "Se connecter"

Fonctionnalités
👥 Clients
Afficher la liste des clients

Ajouter un client

Modifier un client

Supprimer un client

📦 Produits
Afficher la liste des produits

Ajouter un produit

Modifier un produit

Supprimer un produit

Suivi du stock

🛒 Commandes
Afficher la liste des commandes

Créer une commande avec plusieurs produits

Modifier une commande (en brouillon)

Supprimer une commande (non validée)

Valider une commande (met à jour le stock)

Calcul automatique du total HT et TTC

🔐 Authentification
Page de connexion

JWT token

Protection des routes

Protection des endpoints API

Bouton de déconnexion

Documentation API (Swagger)
La documentation interactive est disponible à :
http://localhost:5230/swagger

📸 Captures d'écran 

login page : 

screenshots/login.png

screenshots/client.png

screenshots/produits.png

screenshots/commande.png

Test rapide : 
Tester la connexion :  
curl -X POST http://localhost:5230/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@commercial.com","password":"Admin123!"}' 

  Dépannage : Erreur : "dotnet-ef n'est pas reconnu"

dotnet tool install --global dotnet-ef

Erreur : "401 Unauthorized"
Vérifier que vous êtes connecté

Vérifier que l'admin existe en base de données

Vérifier que le token JWT est valide

Erreur : "MySQL n'est pas connecté"
Vérifier que MySQL est lancé dans XAMPP

Vérifier la chaîne de connexion dans appsettings.json

 Auteur : 
 Nom : Fkiri Mohamed Ali

Email : mohamedali.fkiri.contact@gmail.com

## 📄 Licence

Ce projet est sous licence MIT.

Copyright (c) 2026 Fkiri Mohamed Ali

Vous êtes autorisé à utiliser, modifier et distribuer ce code librement,
à condition d'inclure la mention de copyright ci-dessus. 