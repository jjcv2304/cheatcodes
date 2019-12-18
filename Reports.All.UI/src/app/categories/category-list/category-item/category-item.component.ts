import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Category} from '../../category.model';

@Component({
  selector: 'app-category-item',
  templateUrl: './category-item.component.html',
  styleUrls: ['./category-item.component.scss']
})
export class CategoryItemComponent implements OnInit {
  @Input() cat: Category;
  @Output() categorySelected = new EventEmitter<void>();
  constructor() { }

  ngOnInit() {
  }

  onSelected() {
    this.categorySelected.emit();
  }
}
