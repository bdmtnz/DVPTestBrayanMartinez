import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { GlobalStore } from '../stores/global.store';
import { StorageService } from './storage.service';
import { CurrentUser } from '../models/responses';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http: HttpClient = inject(HttpClient)
  private storage = inject(StorageService)
  private globalStore = inject(GlobalStore)

  login = (email: string, password: string) => {
    const offsetInMinutes = new Date().getTimezoneOffset();
    return this.http.post<CurrentUser>(`${environment.backUrl}/api/Auth`, { email, password, offsetInMinutes })
      .pipe(tap({
        next: (resp)=> {
          this.globalStore.setUser(resp);
        }
      }));
  }

  clear = () => {
    this.storage.clear()
    this.globalStore.setUser(null);
  }

  logout = () => {
    this.globalStore.setLogout();
  }  
}
