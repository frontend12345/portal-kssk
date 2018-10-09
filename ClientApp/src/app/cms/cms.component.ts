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
    secureDownloadForm: FormGroup;
	isAuthenticated: any;
	userLogin: any;
	qrCode: string;
	
	// for menu & content
	selectedMenu: any = JSON.parse('{"id":1,"parentId":null,"title":"Beranda","isActive":true,"order":0,"url":"home","mode":"full"}');
	menu: any[] = [];
	contentText: string;
	currentContent: any;
	currentContentIndex: number;
	listContent: any[] = [];
	listFile: any[] = [];
	listSecureFile: any[] = [];
	
	// for form
	showLogin: boolean;
	showDownload: boolean;
	showAdmin: boolean;
	showSecureDownload: boolean;
	
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
		this.loadData();
		this.loadMenu();
		this.showLogin = false;
        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });
        this.secureDownloadForm = this.formBuilder.group({
            secretKey: ['', Validators.required]
        });
	};

	// for loading data
	loadData() {
		this.menuService.getLoadData().subscribe(result => {
		});	
	};

	// loading menu
	loadMenu() {
		this.menuService.getMenu().subscribe(result => {
			this.menu = result;
			
			// loading home content
			this.loadContent();	
		});	
	};
	
	// loading content
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
	
	// CMS related
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
		this.showDownload = false;
		this.showAdmin = false;
	};

	// for navigation
	openLogin(){
		this.showLogin = false;
		if(!this.isAuthenticated){
			this.showLogin = true;
		}
	};
	openDownload(){
		this.showDownload = false;
		if(this.isAuthenticated){
			this.showAdmin = false;
			this.showDownload = true;
			this.showLogin = false;
		}
	};
	openAdmin(){
		this.showAdmin = false;
		if(this.isAuthenticated){
			this.showAdmin = true;
			this.showDownload = false;
			this.showLogin = false;
		}
	};
	openSecureDownload(){
		this.showAdmin = false;
		if(this.isAuthenticated){
			this.showAdmin = true;
		}
	};
	
	secureDownload(filename: string){
		this.fileService.getSecureFile(filename,this.secureDownloadForm.value.secretKey).subscribe(result => {	
			const downloadLink = document.createElement("a");
			downloadLink.style.display = "none";
			document.body.appendChild(downloadLink);
			downloadLink.setAttribute("href", window.URL.createObjectURL(result));
			downloadLink.setAttribute("download", filename); 
			downloadLink.click();
			document.body.removeChild(downloadLink);
		});
	};
	
	logout(){
		localStorage.removeItem('currentUser');
		this.isAuthenticated = false;
		this.showLogin = false;
		this.showDownload = false;
		this.showAdmin = false;
	};
	
	login(){
		if(this.loginForm.value.username!==null && this.loginForm.value.password!==null){
			this.userService.login(this.loginForm.value.username,this.loginForm.value.password).subscribe(result => {
				if (result && result.authenticator) {
					this.isAuthenticated = true;
					this.userLogin = result;
					this.showLogin = false;
					this.showAdmin = true;
					
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(result));
					
					// load secure file
					this.listSecureFile = [];
					this.fileService.getListSecureFile().subscribe(resultFile => {					
						this.listSecureFile = resultFile;
					});
					
					// load qrcode
					this.userService.getQRCode().subscribe(resultQR => {	
						this.qrCode = resultQR;
					});
                }
			});
		}
	};
}
