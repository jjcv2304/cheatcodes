import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';


import { AppComponent } from './app.component';
import { CategoriesListComponent } from './categories-list/categories-list.component';
import {CategoriesService} from "./categories/categories.service";


@NgModule({
  declarations: [
    AppComponent,
    CategoriesListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
  ],
  providers: [CategoriesService],
  bootstrap: [AppComponent]
})
export class AppModule { }
