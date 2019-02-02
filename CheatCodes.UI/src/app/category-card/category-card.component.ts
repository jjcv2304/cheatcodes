import {Component, Input, OnInit} from '@angular/core';


@Component({
  selector: 'app-category-card',
  templateUrl: './category-card.component.html',
  styleUrls: ['./category-card.component.scss']
})
export class CategoryCardComponent implements OnInit {
  flipDiv: boolean;
  @Input() card: any;

  constructor() {
    this.flipDiv = false;
  }

  ngOnInit() {
  }

  private sideNavigationLeft() {
    this.flipDiv = !this.flipDiv;
  }

  private sideNavigationRight() {
    console.log('expand click');
    this.flipDiv = !this.flipDiv;
  }

  private deepNavigationUp() {
    console.log('nav up');
  }

  private deepNavigationDown() {
    console.log('nav down');
  }

  private saveDescription(editedDescription) {
    console.log(editedDescription);
  }

  private saveHeader(editedHeader) {
    console.log(editedHeader);
  }



}
