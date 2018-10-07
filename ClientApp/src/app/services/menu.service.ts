import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class MenuService {
  constructor(public http: HttpClient) { }

  getMenu() {
    return this.http.get<any[]>("/api/Menu");
  }
}
