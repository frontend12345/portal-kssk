import { Component, OnInit, ViewChild, AfterViewInit, Input, Output,EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MenuService } from '../services/menu.service';
import { ContentService } from '../services/content.service';
import { FileService } from '../services/file.service';
import { DataService } from '../services/data.service';
import { UserService } from '../services/user.service';
import { NewsService } from '../services/news.service';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { IImage } from 'ng-simple-slideshow';
import * as $ from 'jquery';// import Jquery here

@Component({
	selector: 'app-cms',
	templateUrl: './cms.component.html',
	styleUrls: ['./cms.component.css'],
	providers: [MenuService,ContentService,FileService,DataService,UserService, NewsService, FormBuilder]
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
	selectedMenu: any = JSON.parse('{"id":1,"parentId":null,"title":"Beranda","isActive":true,"order":0,"url":"home","mode":"feature"}');
	menu: any[] = [];
	contentText: string;
	currentContent: any;
	currentContentIndex: number;
	listContent: any[] = [];
	listFile: any[] = [];
	listSecureFile: any[] = [];
	href: string;
	sliderUrl: (string | IImage)[] = [{url:'assets/img/slider1.jpg',caption:'Press Conference Rapat Berkala KSSK Triwulan I Tahun 2018'},{url:'assets/img/slider2.jpg',caption:'Rapat Berkala KSSK Triwulan II Tahun 2018'}];
	listSliderFile: (string | IImage)[] = [];
	listNews: any[] = [];
	latestContent: any[] = [];
	
	// for form
	contentPage: string;
	crud:any = JSON.parse('{"title":"", "data":""}');
	tempData: any[];
	fileList: FileList;
	
	constructor(
		private router: Router,
		private menuService: MenuService,
		private data: DataService,
		private contentService: ContentService,
		private fileService: FileService,
		private userService: UserService,
		private newsService: NewsService,
        private formBuilder: FormBuilder
	) {
		this.href = location.pathname;
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
		this.openContentByUrl();
		if (localStorage.getItem("currentUser") != null) {
		  this.isAuthenticated = true;
		}
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
		this.newsService.getNews().subscribe(result => {
			this.listNews = result;
		});	
		this.contentService.getLatestContentByUrl('pr').subscribe(result => {
			if(result!=null){
				this.latestContent.push(result);
			}
		});
		this.contentService.getLatestContentByUrl('peraturan').subscribe(result => {
			if(result!=null){
				this.latestContent.push(result);
			}
		});
		this.contentService.getLatestContentByUrl('rapat').subscribe(result => {
			if(result!=null){
				this.latestContent.push({'url':result.url,'title':result.title});
			}
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
			if(this.selectedMenu.mode=="single" || this.selectedMenu.mode=="full" || this.selectedMenu.mode=="feature"){
				this.contentText = result.content;
			}else{
				if(result.length>0){
					this.listContent = result;
					this.currentContent = result[0];
					this.currentContentIndex = 0;
					this.listFile = [];
					this.listSliderFile = [];
					this.fileService.getFileByContent(this.currentContent.id).subscribe(resultFile => {					
						this.listFile = resultFile;
						this.listSliderFile = resultFile.map(a=>"assets/foto/"+a.filename);
					});
				}
			}
			this.contentPage="portal";
		});
	};
	
	// CMS related
	openContentByUrl(){
		if(this.href!="/"){
			this.menuService.getMenuByUrl(this.href).subscribe(result => {
				if(result!=null){
					this.open(result);
				}
			});	
		}
	};
	openLatestContentByUrl(url:string){
		this.menuService.getMenuByUrl(url).subscribe(result => {
			if(result!=null){
				this.open(result);
			}
		});
	};
	getLatestContentByUrl(url:string){	
		let index = this.latestContent.findIndex(a => {
			return a.url == url;
		}); 
		if(index != -1){
			return this.latestContent[index].title;
		}
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
			this.listSliderFile = [];
			this.fileService.getFileByContent(this.currentContent.id).subscribe(resultFile => {					
				this.listFile = resultFile;
				this.listSliderFile = resultFile.map(a=>"assets/foto/"+a.filename);
			});
		}
	};	
	nextContent(){
		if(this.hasNextContent()){
			this.currentContentIndex = this.currentContentIndex + 1;
			this.currentContent = this.listContent[this.currentContentIndex];
			this.listFile = [];
			this.listSliderFile = [];
			this.fileService.getFileByContent(this.currentContent.id).subscribe(resultFile => {					
				this.listFile = resultFile;
				this.listSliderFile = resultFile.map(a=>"assets/foto/"+a.filename);
			});
		}
	};	
	openContent(index){
		this.currentContent = this.listContent[index];
		this.currentContentIndex = index;
		this.listFile = [];
		this.listSliderFile = [];
		this.fileService.getFileByContent(this.currentContent.id).subscribe(resultFile => {					
			this.listFile = resultFile;
			this.listSliderFile = resultFile.map(a=>"assets/foto/"+a.filename);
		});
	};
	hasChildren(menu:any[],id) {
		return menu.filter(a=>a.parentId===id).length>0;
	};
	open(item){
		this.scrollToAnchor('anchor'); 
		this.selectedMenu = item;
		this.loadContent();
	};
	scrollToAnchor(aid) {
		var aTag = $("a[name='" + aid + "']");
		$('html,body').animate({ scrollTop: aTag.offset().top }, 'slow');
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
					
			// load secure file
			this.listSecureFile = [];
			this.fileService.getListSecureFile().subscribe(resultFile => {					
				this.listSecureFile = resultFile;
			});
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
					title: ['', Validators.required],
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
			if(this.crud.title=="securefile"){
				this.crudForm = this.formBuilder.group({
					id: [''],
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
			if(this.crud.title=="news"){
				this.crudForm = this.formBuilder.group({
					id: [''],
					text: ['', Validators.required],
					url: ['', Validators.required]
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
				if(this.crud.title=="file" || this.crud.title=="securefile"){
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
		if(arr!==undefined){
			if(id!==null && arr.length>=1){
				let res: any[] = arr.filter(a=>a.id==id);
				if(res.length==1){
					return res[0].title;
				}
			}
		}
	};
	getUrl(arr: any[], id: number){
		if(arr!==undefined){
			if(id!==null && arr.length>=1){
				let res: any[] = arr.filter(a=>a.id==id);
				if(res.length==1){
					return res[0].url;
				}
			}
		}
	};
	getContent(arr: any[], id: number){
		if(arr!==undefined){
			if(id!==null && arr.length>=1){
				let res: any[] = arr.filter(a=>a.id==id);
				if(res.length==1){
					return res[0].content.substring(0,50);
				}
			}
		}
	};
	getUrlAssets(filename: string){
		if(filename!==undefined){
			var extension = filename.split('.').pop().toLowerCase();
			if(extension=="jpg"||extension=="jpeg"||extension=="png"||extension=="bmp"){
				return "/assets/foto/"+filename;
			}else{
				return "/assets/file/"+filename;
			}
		}
	};
	cleanHtmlTags(input: string){
		if(input!==null){
			return input.replace(/<[^>]*>/g, '');
		}
		return "";
	};
	copyToClipboard(copyText: string) {
		let selBox = document.createElement('textarea');
		selBox.style.position = 'fixed';
		selBox.style.left = '0';
		selBox.style.top = '0';
		selBox.style.opacity = '0';
		selBox.value = copyText;
		document.body.appendChild(selBox);
		selBox.focus();
		selBox.select();
		document.execCommand('copy');
		document.body.removeChild(selBox);

		/* Alert the copied text */
		alert("Copied to clipboard: " + copyText);
	}
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
				'title': this.crudForm.value.title,
				'content1': this.crudForm.value.content,
				'isActive': this.crudForm.value.isActive
			}
		}
		if(this.crud.title=="file"){
			let formData:FormData = new FormData();
			formData.append('file', this.fileList[0]);
			formData.append('contentId', this.crudForm.value.contentId);
			formData.append('filename', this.crudForm.value.filename);
			formData.append('description', this.crudForm.value.description);
			formData.append('order', this.crudForm.value.order);
			return formData;
		}
		if(this.crud.title=="securefile"){
			let formData:FormData = new FormData();
			formData.append('file', this.fileList[0]);
			formData.append('filename', this.crudForm.value.filename);
			formData.append('description', this.crudForm.value.description);
			formData.append('order', this.crudForm.value.order);
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
		if(this.crud.title=="news"){
			dataPost = {
				'text': this.crudForm.value.text,
				'url': this.crudForm.value.url
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
				'title': res.title,
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
		if(this.crud.title=="securefile"){
			this.crud.data.push({
				'id': res.id,
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
		if(this.crud.title=="news"){
			this.crud.data.push({
				'id': res.id,
				'text': res.text,
				'url': res.url
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
				if(this.crud.title=="file" || this.crud.title=="securefile"){
					dataPost.append('id', this.crudForm.value.id);
				}else{
					dataPost.id = this.crudForm.value.id;
				}
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
		this.fileList = files;
		this.crudForm.patchValue({
			'filename':files[0].name
		});
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
					
					// load qrcode
					this.userService.getQRCode().subscribe(resultQR => {	
						this.qrCode = resultQR;
					});
                }
			},
			error => {
				this.errors = "Gagal login";
			});
		}
	};
}
