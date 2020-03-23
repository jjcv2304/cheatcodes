import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { CategoriesService } from "../../categories/categories.service";
import { Category } from "../../models/category";

@Component({
  selector: "app-card-move-menu",
  templateUrl: "./card-move-menu.component.html",
  styleUrls: ["./card-move-menu.component.scss"]
})
export class CardMoveMenuComponent implements OnInit {
  submenuVisible = false;
  @Input()
  canMoveUp: boolean;
  @Input()
  currentCategoryId: number;
  @Output()
  moveUp: EventEmitter<void> = new EventEmitter();
  @Output()
  moveToSibling: EventEmitter<number> = new EventEmitter();
  private siblingCategories: Category[];

  constructor(private categoriesService: CategoriesService) {
  }

  ngOnInit() {
    this.siblingCategories = this.categoriesService.currentCategories;
    this.siblingCategories = this.siblingCategories.filter(item => item.id !== this.currentCategoryId);
  }

  showSubmenu() {
    this.submenuVisible = true;
  }

  hideSubmenu() {
    this.submenuVisible = false;
  }

  moveUpEvent() {
    this.moveUp.emit();
  }

  moveToSiblingEvent(id: number) {
    this.moveToSibling.emit(id);
  }
}
