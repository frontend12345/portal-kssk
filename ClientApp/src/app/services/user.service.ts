import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators'

@Injectable()
export class UserService {
  constructor(public http: HttpClient) { }
  
  setHeaders(){
	let headers: HttpHeaders = new HttpHeaders();
	if (localStorage.getItem('currentUser')) {
		let currentUser = JSON.parse(localStorage.getItem('currentUser'));
		if (currentUser && currentUser.authenticator) {
			headers = headers.append('Authorization', 'Bearer '+currentUser.authenticator);
		}
	}
	return headers;
  };
  
  login(username:string,password:string): Observable<any> {
	let userLogin: string = "{'username':'"+username+"','password':'"+password+"'}";
	let headers: HttpHeaders = this.setHeaders();
	headers = headers.append('Content-Type', 'application/json');
	headers = headers.append('Accept', 'application/json')
    return this.http.post("/api/User/login", userLogin, {headers})
	.pipe(
      catchError(this.handleError)
    );
  };
  
  getQRCode() { 
	let headers: HttpHeaders = this.setHeaders();
    return this.http.get("/api/User/qrcode", { headers: headers, responseType: 'text' });
  };
  
  getData(item:string) { 
	let headers: HttpHeaders = this.setHeaders();
    return this.http.get("/api/"+item, { headers: headers, responseType: 'text' });
  };
  
  postData(item:string, data:any) { 
	let headers: HttpHeaders = this.setHeaders();
	if(item==="file"){
		headers = headers.append('Content-Type', 'multipart/form-data');
	}
    return this.http.post("/api/"+item, data, { headers: headers, responseType: 'text' });
  };
  
  putData(item:string, data:any) { 
	let headers: HttpHeaders = this.setHeaders();
    return this.http.put("/api/"+item+"/"+data.id, data, { headers: headers, responseType: 'text' });
  };
  
  deleteData(item:string, id:number) { 
	let headers: HttpHeaders = this.setHeaders();
    return this.http.delete("/api/"+item+"/"+id, { headers: headers, responseType: 'text' });
  };
  
  handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  }
}
