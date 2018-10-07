import { Component, OnInit, ViewChild, AfterViewInit, Input, Output,EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MenuService } from '../services/menu.service';
import { ContentService } from '../services/content.service';
import { FileService } from '../services/file.service';
import { DataService } from '../services/data.service';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
	selector: 'app-cms',
	templateUrl: './cms.component.html',
	styleUrls: ['./cms.component.css'],
	providers: [MenuService,ContentService,FileService,DataService,UserService, FormBuilder]
})
export class CmsComponent implements OnInit {
    loginForm: FormGroup;
	isAuthenticated: any;
	showLogin: boolean;
	menu: any[] = [];
	contentText: string;
	currentContent: any;
	currentContentIndex: number;
	listContent: any[] = [];
	listFile: any[] = [];
	selectedMenu: any = JSON.parse('{"id":1,"parentId":null,"title":"Beranda","isActive":true,"order":0,"url":"home","mode":"full"}');
	constructor(
		private route: Router,
		private menuService: MenuService,
		private data: DataService,
		private contentService: ContentService,
		private fileService: FileService,
		private userService: UserService,
        private formBuilder: FormBuilder
	) {
	};

	ngOnInit() {
		this.loadMenu();
		this.loadContent();	
		this.isGuest();
		this.showLogin = false;
        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });
	};

	loadMenu() {
		this.menuService.getMenu().subscribe(result => {
			this.menu = result;
		});	
	};
	
	loadContent(){
		this.contentService.getContentByType(this.selectedMenu.url,this.selectedMenu.mode).subscribe(result => {
			if(this.selectedMenu.mode=="single" || this.selectedMenu.mode=="full"){
				this.contentText = result.content;
			}else{
				this.listContent = result;
				this.currentContent = result[0];
				this.currentContentIndex = 0;
				this.listFile = [];
				this.fileService.getFileByContent(this.currentContent.id).subscribe(resultFile => {					
					this.listFile = resultFile;
				});
			}
		});
	};
	
	hasPrevContent(){
		return (this.currentContentIndex-1)>=0;
	};
	
	hasNextContent(){
		return (this.currentContentIndex+1)<this.listContent.length;
	};
	
	prevContent(){
		if(this.hasPrevContent()){
			this.currentContentIndex = this.currentContentIndex - 1;
			this.currentContent = this.listContent[this.currentContentIndex];
			this.listFile = [];
			this.fileService.getFileByContent(this.currentContent.id).subscribe(resultFile => {					
				this.listFile = resultFile;
			});
		}
	};
	
	nextContent(){
		if(this.hasNextContent()){
			this.currentContentIndex = this.currentContentIndex + 1;
			this.currentContent = this.listContent[this.currentContentIndex];
			this.listFile = [];
			this.fileService.getFileByContent(this.currentContent.id).subscribe(resultFile => {					
				this.listFile = resultFile;
			});
		}
	};
	
	openContent(index){
		this.currentContent = this.listContent[index];
		this.currentContentIndex = index;
		this.listFile = [];
		this.fileService.getFileByContent(this.currentContent.id).subscribe(resultFile => {					
			this.listFile = resultFile;
		});
	};

	hasChildren(menu:any[],id) {
		return menu.filter(a=>a.parentId===id).length>0;
	};

	open(item){
		this.selectedMenu = item;
		this.loadContent();
		this.showLogin = false;
	};

	openLogin(){
		if(!this.isAuthenticated){
			this.showLogin = true;
		}
	};
	
	isGuest(){
		this.userService.isAuthenticated().subscribe(result => {
			this.isAuthenticated = result;
		});
	};
	
	logout(){
		this.userService.logout().subscribe(result => {					
			this.isAuthenticated = result;
		});
	};
}
