import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class FileService {
  constructor(public http: HttpClient) { }

  setHeaders(){
	let headers: HttpHeaders = new HttpHeaders();
	if (localStorage.getItem('currentUser')) {
		let currentUser = JSON.parse(localStorage.getItem('currentUser'));
		if (currentUser && currentUser.authenticator) {
			headers = headers.append('Authorization', 'Bearer '+currentUser.authenticator);
		}
	}
	headers = headers.append('Content-Type', 'application/json');
	headers = headers.append('Accept', 'application/json')
	return headers;
  };
  
  getFileByContent(contentid: string) { 
    return this.http.get<any>("/api/File/"+contentid); 
  };
  
  getListSecureFile() { 
	let headers: HttpHeaders = this.setHeaders();
    return this.http.get<any>("/api/SecureFile", {headers}); 
  };
  
  getSecureFile(filename: string, secretKey: string) { 
	let headers: HttpHeaders = this.setHeaders();
	let secureFile: string = "{'filename':'"+filename+"'}";
    return this.http.post<any>("/api/SecureFile/download/"+secretKey, secureFile, { headers: headers, responseType: 'blob' });
  };
}
