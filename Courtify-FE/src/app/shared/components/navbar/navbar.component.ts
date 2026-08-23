import { Component, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { APP_ROUTES } from '../../constants/routes';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  private authService = inject(AuthService);
  private router = inject(Router);

  readonly ROUTES = APP_ROUTES;

  logout() {
    this.authService.logout();
    this.router.navigate([APP_ROUTES.LOGIN]);
    this.authService.isLoggedInSignal.set(false);
  }
}
