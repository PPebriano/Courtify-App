import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginResponseType } from '../models/response/login-response-type';
import { LoginRequestType } from '../models/request/login-request-type';
import { environtment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  http = inject(HttpClient);

  private tokenKey = 'authToken';
  private userId = 'userId';

  login(payload: LoginRequestType) {
    return this.http.post<LoginResponseType>(
      `${environtment.apiUrl}/api/login`,
      {
        payload,
      },
    );
  }

  setTokenAndUserId(token: string, userId: number) {
    localStorage.setItem(this.tokenKey, token);
    localStorage.setItem(this.userId, userId.toString());
  }

  getToken() {
    return localStorage.getItem(this.tokenKey);
  }

  getUserId() {
    return localStorage.getItem(this.userId);
  }

  isLoggedIn() {
    return !!this.getToken();
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
  }
}
