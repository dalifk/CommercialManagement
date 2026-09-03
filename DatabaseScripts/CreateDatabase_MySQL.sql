-- ============================================
-- Database: CommercialManagementDb
-- Description: Database for Commercial Management Application
-- For MySQL/MariaDB
-- ============================================

-- Create Database
CREATE DATABASE IF NOT EXISTS CommercialManagementDb;
USE CommercialManagementDb;

-- ============================================
-- Create Tables (Using PDF column names)
-- ============================================

-- 1. Clients Table
DROP TABLE IF EXISTS Clients;
CREATE TABLE Clients (
    Identifiant INT AUTO_INCREMENT PRIMARY KEY,
    Nom VARCHAR(100) NOT NULL,
    `Prénom_ou_raison_sociale` VARCHAR(100) NULL,
    Email VARCHAR(255) NOT NULL,
    Téléphone VARCHAR(20) NULL,
    Adresse VARCHAR(500) NULL,
    `Date_de_création` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT UQ_Clients_Email UNIQUE (Email)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 2. Products Table
DROP TABLE IF EXISTS Produits;
CREATE TABLE Produits (
    Identifiant INT AUTO_INCREMENT PRIMARY KEY,
    Référence VARCHAR(50) NOT NULL,
    `Nom_du_produit` VARCHAR(100) NOT NULL,
    Description VARCHAR(500) NULL,
    `Prix_unitaire_HT` DECIMAL(18,2) NOT NULL,
    `Quantité_en_stock` INT NOT NULL DEFAULT 0,
    `Date_de_création` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT UQ_Produits_Référence UNIQUE (Référence),
    CONSTRAINT CHK_Produits_Prix_unitaire_HT CHECK (`Prix_unitaire_HT` >= 0),
    CONSTRAINT CHK_Produits_Quantité_en_stock CHECK (`Quantité_en_stock` >= 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 3. Orders Table
DROP TABLE IF EXISTS Commandes;
CREATE TABLE Commandes (
    Identifiant INT AUTO_INCREMENT PRIMARY KEY,
    `Numéro_de_commande` VARCHAR(20) NOT NULL,
    `Client_associé` INT NOT NULL,
    `Date_de_commande` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `Statut_de_la_commande` VARCHAR(20) NOT NULL DEFAULT 'Brouillon',
    `Total_HT` DECIMAL(18,2) NOT NULL DEFAULT 0,
    `Total_TTC` DECIMAL(18,2) NOT NULL DEFAULT 0,
    CONSTRAINT UQ_Commandes_Numéro_de_commande UNIQUE (`Numéro_de_commande`),
    CONSTRAINT FK_Commandes_Client_associé FOREIGN KEY (`Client_associé`) 
        REFERENCES Clients(Identifiant) ON DELETE RESTRICT,
    CONSTRAINT CHK_Commandes_Statut_de_la_commande CHECK (`Statut_de_la_commande` IN ('Brouillon', 'Validée', 'Annulée')),
    CONSTRAINT CHK_Commandes_Total_HT CHECK (`Total_HT` >= 0),
    CONSTRAINT CHK_Commandes_Total_TTC CHECK (`Total_TTC` >= 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 4. OrderLines Table
DROP TABLE IF EXISTS `Lignes_de_commande`;
CREATE TABLE `Lignes_de_commande` (
    Identifiant INT AUTO_INCREMENT PRIMARY KEY,
    `Commande_Identifiant` INT NOT NULL,
    `Produit_Identifiant` INT NOT NULL,
    Quantité INT NOT NULL,
    `Prix_unitaire` DECIMAL(18,2) NOT NULL,
    `Total_ligne` DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Lignes_de_commande_Commande FOREIGN KEY (`Commande_Identifiant`) 
        REFERENCES Commandes(Identifiant) ON DELETE CASCADE,
    CONSTRAINT FK_Lignes_de_commande_Produit FOREIGN KEY (`Produit_Identifiant`) 
        REFERENCES Produits(Identifiant) ON DELETE RESTRICT,
    CONSTRAINT CHK_Lignes_de_commande_Quantité CHECK (Quantité > 0),
    CONSTRAINT CHK_Lignes_de_commande_Prix_unitaire CHECK (`Prix_unitaire` >= 0),
    CONSTRAINT CHK_Lignes_de_commande_Total_ligne CHECK (`Total_ligne` >= 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 5. Users Table
CREATE TABLE IF NOT EXISTS utilisateurs (
    Identifiant INT AUTO_INCREMENT PRIMARY KEY,
    Email VARCHAR(255) NOT NULL UNIQUE,
    MotDePasse VARCHAR(255) NOT NULL,
    Nom VARCHAR(100) NOT NULL,
    Role VARCHAR(50) NOT NULL,
    DateCreation DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ============================================
-- Create Indexes for Performance
-- ============================================

CREATE INDEX IX_Clients_Email ON Clients(Email);
CREATE INDEX IX_Clients_Nom ON Clients(Nom);
CREATE INDEX IX_Produits_Référence ON Produits(Référence);
CREATE INDEX IX_Produits_Nom_du_produit ON Produits(`Nom_du_produit`);
CREATE INDEX IX_Commandes_Client_associé ON Commandes(`Client_associé`);
CREATE INDEX IX_Commandes_Numéro_de_commande ON Commandes(`Numéro_de_commande`);
CREATE INDEX IX_Commandes_Statut_de_la_commande ON Commandes(`Statut_de_la_commande`);
CREATE INDEX IX_Lignes_de_commande_Commande_Identifiant ON `Lignes_de_commande`(`Commande_Identifiant`);
CREATE INDEX IX_Lignes_de_commande_Produit_Identifiant ON `Lignes_de_commande`(`Produit_Identifiant`);

SELECT 'Database setup completed successfully!' AS Status;
SHOW TABLES;