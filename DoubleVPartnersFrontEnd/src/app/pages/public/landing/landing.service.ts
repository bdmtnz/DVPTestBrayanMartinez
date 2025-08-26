import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { EntityId } from "@app/shared/models/responses";
import { environment } from "@environment/environment";

@Injectable({
  providedIn: 'root'
})
export class LandingService {
  private http: HttpClient = inject(HttpClient)

  signup = (name: string, email: string, password: string) => 
    this.http.post<EntityId>(`${environment.backUrl}/api/User`, { name, email, password });
}