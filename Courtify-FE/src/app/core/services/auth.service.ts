import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginResponseType } from '../models/response/login-response-type';
import { LoginRequestType } from '../models/request/login-request-type';
import { environtment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenKey = 'authToken';
  http = inject(HttpClient);

  // WAITING FOR AN API
  login(payload: LoginRequestType) {
    return this.http.post<LoginResponseType>(
      `${environtment.apiUrl}/api/login`,
      {
        payload,
      },
    );
  }

  setToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken() {
    return localStorage.getItem(this.tokenKey);
  }

  isLoggedIn() {
    return !!this.getToken();
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
  }
}
