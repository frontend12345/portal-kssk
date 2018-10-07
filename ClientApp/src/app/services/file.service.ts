import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class FileService {
  constructor(public http: HttpClient) { }

  getFileByContent(contentid: string) { 
    return this.http.get<any>("/api/File/"+contentid); 
  }
}
