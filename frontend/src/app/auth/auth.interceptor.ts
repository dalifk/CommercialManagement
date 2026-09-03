// ============================================
// INTERCEPTEUR: Authentification JWT (Standalone)
// Ajoute automatiquement le token à toutes les requêtes HTTP
// ============================================

import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from './auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthService);
    const token = authService.getToken();

    // Si un token existe, l'ajoute à l'en-tête Authorization
    if (token) {
        const cloned = req.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });
        console.log('✅ Token JWT ajouté à la requête:', req.url);
        return next(cloned);
    }

    // Pas de token → requête sans authentification
    console.log('⚠️ Aucun token JWT trouvé pour:', req.url);
    return next(req);
};