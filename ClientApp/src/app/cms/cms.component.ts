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
    crudForm: FormGroup;
	isAuthenticated: any;
	userLogin: any;
	qrCode: string;
	errors: string;
	
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
	contentPage: string;
	crud:any = JSON.parse('{"title":"", "data":""}');
	tempData: any[];
	
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
        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });
        this.secureDownloadForm = this.formBuilder.group({
            secretKey: ['', Validators.required]
        });
	};
	
	public textOptions: Object = {
		charCounterCount: true,
		toolbarButtons: ['fullscreen', 'bold', 'italic', 'underline', 'strikeThrough', 'subscript', 'superscript', '|', 'fontFamily', 'fontSize', 'color', 'inlineStyle', 'paragraphStyle', '|', 'paragraphFormat', 'align', 'formatOL', 'formatUL', 'outdent', 'indent', 'quote', '-', 'insertLink', 'insertImage', 'insertVideo', 'insertFile', 'insertTable', '|', 'emoticons', 'specialCharacters', 'insertHR', 'selectAll', 'clearFormatting', '|', 'print', 'help', 'html', '|', 'undo', 'redo'],
		toolbarButtonsXS: ['bold', 'italic', 'underline', 'paragraphFormat','insertImage', 'html', '|', 'undo', 'redo'],
		toolbarButtonsSM: ['bold', 'italic', 'underline', 'paragraphFormat','insertImage', 'html', '|', 'undo', 'redo'],
		toolbarButtonsMD: ['bold', 'italic', 'underline', 'paragraphFormat','insertImage', 'html', '|', 'undo', 'redo'],
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
			this.contentPage="portal";
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
	};

	// for navigation
	openLogin(){
		if(!this.isAuthenticated){
			this.contentPage="login";
		}
	};
	openDownload(){
		this.errors = "";
		if(this.isAuthenticated){
			this.contentPage="download";
		}
	};
	openAdmin(){
		if(this.isAuthenticated){
			this.contentPage="admin";
		}
	};
	openCrud(item:string){
		if(this.isAuthenticated){
			this.contentPage="crud";
			this.crud.title = item;	
			this.userService.getData(item).subscribe(result => {
				this.crud.data = JSON.parse(result);
			});
			if(this.crud.title=="content"){
				this.userService.getData("menu").subscribe(result => {
					this.tempData = JSON.parse(result);
				});
			}
			if(this.crud.title=="file"){
				this.userService.getData("content").subscribe(result => {
					this.tempData = JSON.parse(result);
				});
			}
		}
	};
	openForm(){
		if(this.isAuthenticated){
			this.contentPage="form";
			if(this.crud.title=="menu"){
				this.crudForm = this.formBuilder.group({
					id: [''],
					title: ['', Validators.required],
					parentId: [],
					isActive: ['true', Validators.required],
					order: [''],
					url: ['', Validators.required],
					mode: ['single', Validators.required]
				});
				this.tempData = this.crud.data.filter(a=>a.parentId===null);
			}
			if(this.crud.title=="content"){
				this.crudForm = this.formBuilder.group({
					id: [''],
					menuId: ['', Validators.required],
					url: ['', Validators.required],
					content: [''],
					isActive: ['true', Validators.required]
				});
			}
			if(this.crud.title=="file"){
				this.crudForm = this.formBuilder.group({
					id: [''],
					contentId: ['', Validators.required],
					filename: ['', Validators.required],
					description: [''],
					order: ['', Validators.required],
					file: [null, Validators.required]
				});
			}
			if(this.crud.title=="user"){
				this.crudForm = this.formBuilder.group({
					id: [''],
					username: ['', Validators.required],
					password: ['', Validators.required],
					role: ['Pengguna', Validators.required],
					authenticator: ['', Validators.required]
				});
			}
		}
	};
	edit(id: number){
		if(this.isAuthenticated){
			let res: any[] = this.crud.data.filter(a=>a.id==id);
			if(res.length==1){
				this.openForm();
				let defaultValue: any = res[0];
				if(this.crud.title=="file"){
					defaultValue.file = "";
				}
				this.crudForm.setValue(defaultValue);
			}		
		}
	};
	delete(id: number){
		if(this.isAuthenticated){
			this.userService.deleteData(this.crud.title,id).subscribe(result => {
				this.crud.data.splice(this.crud.data.findIndex(v => v.id === id), 1);
			});			
		}
	};
	getTitle(arr: any[], id: number){
		if(id!==null && arr.length>=1){
			let res: any[] = arr.filter(a=>a.id==id);
			if(res.length==1){
				return res[0].title;
			}
		}
	};
	getUrl(arr: any[], id: number){
		if(id!==null && arr.length>=1){
			let res: any[] = arr.filter(a=>a.id==id);
			if(res.length==1){
				return res[0].url;
			}
		}
	};
	getContent(arr: any[], id: number){
		if(id!==null && arr.length>=1){
			let res: any[] = arr.filter(a=>a.id==id);
			if(res.length==1){
				return res[0].content.substring(0,50);
			}
		}
	};
	setDataPost():any {
		let dataPost: any = {};
		if(this.crud.title=="menu"){
			dataPost = {
				'title': this.crudForm.value.title,
				'parentId': this.crudForm.value.parentId,
				'isActive': this.crudForm.value.isActive,
				'order': this.crudForm.value.order,
				'url': this.crudForm.value.url,
				'mode': this.crudForm.value.mode
			}
		}
		if(this.crud.title=="content"){
			dataPost = {
				'menuId': this.crudForm.value.menuId,
				'url': this.getUrl(this.tempData,this.crudForm.value.menuId),
				'content1': this.crudForm.value.content,
				'isActive': this.crudForm.value.isActive
			}
		}
		if(this.crud.title=="file"){
			dataPost = {
				'contentId': this.crudForm.value.contentId,
				'filename': this.crudForm.value.filename,
				'description': this.crudForm.value.description,
				'order': this.crudForm.value.order
			}
			let formData:FormData = new FormData();
			formData.append('uploadFiles', this.crud.data.file, this.crud.data.filename);
			formData.append('files', dataPost);
			return formData;
		}
		if(this.crud.title=="user"){
			dataPost = {
				'username': this.crudForm.value.username,
				'password': this.crudForm.value.password,
				'role': this.crudForm.value.role,
				'authenticator': '123'
			}
		}
		return dataPost;
	};
	setDataCrud(res: any){
		if(this.crud.title=="menu"){
			this.crud.data.push({
				'id': res.id,
				'title': res.title,
				'parentId': res.parentId,
				'isActive': res.isActive,
				'order': res.order,
				'url': res.url,
				'mode': res.mode
			});
		}
		if(this.crud.title=="content"){
			this.crud.data.push({
				'id': res.id,
				'menuId': res.menuId,
				'url': res.url,
				'content': res.content1,
				'isActive': res.isActive
			});
			this.crud.data = this.crud.data.sort((a, b) => a.id - b.id);
		}
		if(this.crud.title=="file"){
			this.crud.data.push({
				'id': res.id,
				'contentId': res.contentId,
				'filename': res.filename,
				'description': res.description,
				'order': res.order
			});
			this.crud.data = this.crud.data.sort((a, b) => a.id - b.id);
		}
		if(this.crud.title=="user"){
			this.crud.data.push({
				'id': res.id,
				'username': res.username,
				'password': res.password,
				'role': res.role,
				'authenticator': res.authenticator
			});
			this.crud.data = this.crud.data.sort((a, b) => a.id - b.id);
		}
	};
	addUpdate(){
		if(this.isAuthenticated){
			let dataPost: any = this.setDataPost();
			if(this.crudForm.value.id==""){
				this.userService.postData(this.crud.title,dataPost).subscribe(result => {
					let res = JSON.parse(result);
					this.setDataCrud(res);
				});	
			} else {
				dataPost.id = this.crudForm.value.id;
				this.userService.putData(this.crud.title,dataPost).subscribe(result => {
					this.crud.data.splice(this.crud.data.findIndex(v => v.id === this.crudForm.value.id), 1);
					let res = JSON.parse(result);
					this.setDataCrud(res);
				});	
			}
			this.contentPage="crud";			
		}
	};
	backToCrud(){
		if(this.isAuthenticated){
			this.contentPage="crud";
		}
	};
	handleFile(files: FileList) {
		this.crud.data.filename = files[0].name;
	}
	
	secureDownload(filename: string){
		this.fileService.getSecureFile(filename,this.secureDownloadForm.value.secretKey).subscribe(result => {	
			const downloadLink = document.createElement("a");
			downloadLink.style.display = "none";
			document.body.appendChild(downloadLink);
			downloadLink.setAttribute("href", window.URL.createObjectURL(result));
			downloadLink.setAttribute("download", filename); 
			downloadLink.click();
			document.body.removeChild(downloadLink);
		},
		error => {
			this.errors = "Gagal mendownload";
		});
	};
	
	logout(){
		localStorage.removeItem('currentUser');
		this.isAuthenticated = false;
		this.contentPage="portal";
	};
	
	login(){
		if(this.loginForm.value.username!==null && this.loginForm.value.password!==null){
			this.userService.login(this.loginForm.value.username,this.loginForm.value.password).subscribe(result => {
				if (result && result.authenticator) {
					this.isAuthenticated = true;
					this.userLogin = result;
					this.contentPage="admin";
					
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
