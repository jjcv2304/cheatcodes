import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FlipModule } from 'ngx-flip';

import { AppComponent } from './app.component';
import { CategoriesListComponent } from './categories-list/categories-list.component';
import {CategoriesService} from "./categories/categories.service";
import { MainMenuComponent } from './main-menu/main-menu.component';
import { CategoryCardComponent } from './category-card/category-card.component';


@NgModule({
  declarations: [
    AppComponent,
    CategoriesListComponent,
    MainMenuComponent,
    CategoryCardComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FlipModule,
  ],
  providers: [CategoriesService],
  bootstrap: [AppComponent]
})
export class AppModule { }
