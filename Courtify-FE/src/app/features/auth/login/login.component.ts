import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';
import { LoginRequestType } from '../../../core/models/request/login-request-type';
import { APP_ROUTES } from '../../../shared/constants/routes';
import {
  passwordValidator,
  usernameValidator,
} from '../../../core/validator/validator';

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

  showError: boolean = false;
  loginForm = this.formBuilder.group({
    username: ['', [usernameValidator]],
    password: ['', [passwordValidator]],
  });

  onSubmit() {
    const payload: LoginRequestType = this.loginForm.getRawValue();
    this.authService.login(payload).subscribe({
      next: (response) => {
        console.log(response);
        this.authService.setTokenAndUserId(
          response.token,
          response.admin.adminId,
        );
        this.router.navigate([APP_ROUTES.VENUES]);
      },
      error: (err) => {
        this.showError = true;
      },
    });
  }
}
