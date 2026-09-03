// ============================================
// CONFIGURATION DE L'APPLICATION
// Contient les providers et configurations globales
// ============================================

import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';

import { routes } from './app.routes';
import { authInterceptor } from './auth/auth.interceptor';  // ← AJOUT

export const appConfig: ApplicationConfig = {
    providers: [
        provideRouter(routes),
        provideHttpClient(
            withInterceptors([authInterceptor])  // ← AJOUT: Intercepteur JWT
        ),
        provideAnimations()
    ]
};