import {Component, Input, OnInit} from '@angular/core';
import {Category, ICategory} from '../models/category';
import {CategoriesService} from '../categories/categories.service';
import {Envelope} from '../../utils/envelope';
import {Router} from '@angular/router';
import {CategoryFilter} from '../models/CategoryFilter';
import {ICategoryFieldValue} from '../models/categoryFieldValue';


@Component({
  selector: 'app-category-card',
  templateUrl: './category-card.component.html',
  styleUrls: ['./category-card.component.scss']
})
export class CategoryCardComponent implements OnInit {
  flipDiv: boolean;
  saveIsRecommended: boolean;
  showMenu = false;
  cardWidth: string;
  cardHeight: string;
  minCardWidth = '200px';
  maxCardWidth = '1000px';
  minCardHeight = '200px';
  maxCardHeight = '500px';
  @Input() card: Category;

  constructor(public router: Router, private categoriesService: CategoriesService) {
    this.flipDiv = false;
  }

  ngOnInit() {
    this.cardWidth = this.minCardWidth;
  }

  private sideNavigationRight() {
    this.flipDiv = !this.flipDiv;
  }

  private canNavigateUp() {
    return this.card.hasParent;
  }

  private navigateUp() {
    const newFilter = CategoryFilter.FilterByChild(this.card.parentId);
    this.categoriesService.SetCategoryFilter(newFilter);
  }

  moveCardUp() {
    this.categoriesService.moveCategoryUp(this.card)
      .subscribe(() => {
        this.navigateUp();
      });
  }

  moveCardToSibling(siblingId: number) {
    this.categoriesService.moveCategoryToSibling(this.card.id, siblingId)
      .subscribe(() => {
        const newFilter = CategoryFilter.FilterByParent(siblingId);
        this.categoriesService.SetCategoryFilter(newFilter);
      });
  }

  private canNavigateDown() {
    return this.card.hasChild;
  }

  private navigateDown() {
    const newFilter = CategoryFilter.FilterByParent(this.card.id);
    this.categoriesService.SetCategoryFilter(newFilter);
  }

  private setSaveIsRecommended() {
    this.saveIsRecommended = true;
  }

  private saveCard() {
    this.categoriesService.updateCategory(this.card)
      .subscribe(() => {
        this.saveIsRecommended = false;
      });
  }

  private updateFieldValue(field: ICategoryFieldValue) {
    this.categoriesService.updateCategoryField(field);
  }

  addChildCard() {
    this.router.navigateByUrl('/categoryEdit/' + this.card.id);
  }

  private deleteCard(category: Category) {
    this.categoriesService.deleteCategory(category)
      .subscribe((data: Envelope<ICategory>) => {
        if (category.parentId === null) {
          location.reload();
        } else {
           if (this.hasSiblings(category.id)) {
             const newFilter = CategoryFilter.FilterByParent(category.parentId);
             this.categoriesService.SetCategoryFilter(newFilter);
           } else {
             this.navigateUp();
           }

        }
      });
  }

  private hasSiblings(categoryId: number) {
    const siblingCategories = this.categoriesService.currentCategories.filter(item => item.id !== categoryId);
    return siblingCategories.length > 0;
  }

  private autoGrowTextZone(e) {
    e.target.style.overflow = 'hidden';
    e.target.style.height = '0px';
    e.target.style.height = (e.target.scrollHeight + 15) + 'px';
  }

  private resizeCard() {
    if (this.cardWidth === this.minCardWidth) {
      this.cardWidth = this.maxCardWidth;
      this.cardHeight = this.maxCardHeight;
    } else {
      this.cardWidth = this.minCardWidth;
      this.cardHeight = this.minCardHeight;
    }

  }
  private expandCard() {
    this.cardWidth = this.maxCardWidth;
    this.cardHeight = this.maxCardHeight;
  }

  addField() {
    this.router.navigateByUrl('/fieldEdit/' + this.card.id);
  }

  moveCardDown() {
    console.log('move down 2');
  }

  toogleMenu() {
    this.showMenu = !this.showMenu;
  }
}
