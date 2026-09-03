-- ============================================
-- Insertion de l'administrateur
-- ============================================

USE CommercialManagementDb;

-- Insérer l'admin
INSERT INTO utilisateurs (Email, MotDePasse, Nom, Role)
VALUES ('admin@commercial.com', 'Admin123!', 'Administrateur', 'Admin')
ON DUPLICATE KEY UPDATE 
    MotDePasse = VALUES(MotDePasse),
    Nom = VALUES(Nom),
    Role = VALUES(Role);

-- Vérifier
SELECT * FROM utilisateurs;