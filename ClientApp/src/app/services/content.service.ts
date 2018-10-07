import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class ContentService {
  constructor(public http: HttpClient) { }

  getContentByType(url: string,type: string) {
    return this.http.get<any>("/api/Content/"+url+"/"+type); 
  }
}
