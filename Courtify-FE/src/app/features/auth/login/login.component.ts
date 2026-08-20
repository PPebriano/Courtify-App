import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';
import { LoginRequestType } from '../../../core/models/request/login-request-type';
import { APP_ROUTES } from '../../../shared/constants/routes';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  loginForm = this.formBuilder.group({
    username: ['', []],
    password: ['', []],
  });

  onSubmit() {
    const payload: LoginRequestType = this.loginForm.getRawValue();
    this.authService.login(payload).subscribe({
      next: (response) => {
        console.log(response);
        this.authService.setToken(response.token);
        this.router.navigate([APP_ROUTES.VENUES]);
      },
    });
  }
}
