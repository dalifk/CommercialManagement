import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent {
    credentials = {
        email: '',
        password: ''
    };

    errorMessage: string = '';
    loading: boolean = false;

    constructor(
        private authService: AuthService,
        private router: Router
    ) { }

    onSubmit(): void {
        this.errorMessage = '';
        this.loading = true;

        this.authService.login(this.credentials).subscribe({
            next: (response) => {
                console.log('✅ Connexion réussie:', response.message);
                this.loading = false;
                this.router.navigate(['/clients']);
            },
            error: (error) => {
                this.loading = false;
                console.error('❌ Erreur de connexion:', error);
                if (error.status === 401) {
                    this.errorMessage = 'Email ou mot de passe incorrect !';
                } else {
                    this.errorMessage = 'Une erreur est survenue. Veuillez réessayer.';
                }
            }
        });
    }
}