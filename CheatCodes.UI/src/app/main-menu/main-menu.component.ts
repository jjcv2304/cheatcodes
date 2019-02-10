import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {CategoriesService} from "../categories/categories.service";

@Component({
  selector: 'app-main-menu',
  templateUrl: './main-menu.component.html',
  styleUrls: ['./main-menu.component.scss']
})
export class MainMenuComponent implements OnInit {


  constructor(public router: Router, private categoriesService:CategoriesService) {

  }

  ngOnInit() {
  }

  newCard() {

  }

}
