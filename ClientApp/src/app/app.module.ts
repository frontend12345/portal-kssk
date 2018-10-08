import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { CmsComponent } from './cms/cms.component';
import { SafePipe } from './cms/safe.pipe';
import { DataService } from './services/data.service';

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
		RouterModule.forRoot([
		])
	],
	providers: [DataService],
	bootstrap: [
		CmsComponent
	]
})
export class AppModule { }
