import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
 
@Injectable({
	providedIn: 'root'
})
export class DataService {
 
	private dataSubject = new BehaviorSubject(JSON.parse('{"id":1,"parentId":null,"title":"Beranda","isActive":true,"order":0,"url":"home","mode":"full"}'));
	currentMessage = this.dataSubject.asObservable();

	constructor() {
	}
	updateMessage(message: any) {
		this.dataSubject.next(message)
	}
}