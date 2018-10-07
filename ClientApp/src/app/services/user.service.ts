import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class UserService {
  constructor(public http: HttpClient) { }

  isAuthenticated() {
    return this.http.get<any[]>("/api/User/check");
  };
  
  logout() {
    return this.http.get<any[]>("/api/User/logout");
  };
}
