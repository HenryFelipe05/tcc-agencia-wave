import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private tokenKey = 'authToken';
  private userCodeKey = 'userCode';

  login(token: string, userCode: string): void {
    localStorage.setItem(this.tokenKey, token);
    localStorage.setItem(this.userCodeKey, userCode);
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userCodeKey);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem(this.tokenKey);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getUserCode(): string | null {
    return localStorage.getItem(this.userCodeKey);
  }
}