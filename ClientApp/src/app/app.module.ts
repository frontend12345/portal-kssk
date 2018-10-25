import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { DataTableModule } from "angular-6-datatable";

import { CmsComponent } from './cms/cms.component';
import { SafePipe } from './cms/safe.pipe';
import { DataService } from './services/data.service';
import { FroalaEditorModule, FroalaViewModule } from 'angular-froala-wysiwyg';
import { SlideshowModule } from 'ng-simple-slideshow';
import { NgxGalleryModule } from 'ngx-gallery';

@NgModule({
	declarations: [
		CmsComponent,
		SafePipe
	],
    exports: [SafePipe],
	imports: [
		BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
		HttpClientModule,
		FormsModule,
		ReactiveFormsModule,
		DataTableModule,
		RouterModule.forRoot([
			{path: '**', component: CmsComponent }
		]),
		FroalaEditorModule.forRoot(), 
		FroalaViewModule.forRoot(),
		SlideshowModule,
		NgxGalleryModule
	],
	providers: [DataService],
	bootstrap: [
		CmsComponent
	]
})
export class AppModule { }
