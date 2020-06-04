/* tslint:disable:member-ordering */
import {Component, Input, OnChanges, OnInit} from '@angular/core';
import {CategoryTree} from '../model/category';
import {FlatTreeControl} from '@angular/cdk/tree';
import {MatTreeFlatDataSource, MatTreeFlattener, MatTreeNode, MatTreeNodeDef, MatTree} from '@angular/material/tree';
import {BehaviorSubject, Observable, Subject} from 'rxjs';
import {debounceTime, distinctUntilChanged} from 'rxjs/operators';

interface FlatNode {
  expandable: boolean;
  name: string;
  description: string;
  parentId: number;
  level: number;
  isVisible: boolean;
}

@Component({
  selector: 'app-categories-search-tree-detail-result',
  templateUrl: './categories-search-tree-detail-result.component.html',
  styleUrls: ['./categories-search-tree-detail-result.component.scss']
})
export class CategoriesSearchTreeDetailResultComponent implements OnChanges, OnInit {

  @Input() cardDetail: CategoryTree;
  selectedCard: CategoryTree;
  treeWidth = '25%';
  searchFilter: Subject<string> = new Subject<string>();

  private _transformer = (node: CategoryTree, level: number) => {
    return {
      expandable: !!node.childs && node.childs.length > 0,
      name: node.name,
      description: node.description,
      parentId: node.parentId,
      level: level,
      isVisible: true
    };
  };

  treeControl = new FlatTreeControl<FlatNode>(node => node.level, node => node.expandable);

  treeFlattener = new MatTreeFlattener(
    this._transformer,
    node => node.level,
    node => node.expandable,
    node => node.childs);

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  hasChild = (_: number, node: FlatNode) => node.expandable;
  activeNode: CategoryTree;

  ngOnInit(): void {
    this.searchFilter.pipe(debounceTime(500), distinctUntilChanged())
      .subscribe(value => {
        if (value && value.length >= 3) {
          this.filterByContent(value);
        } else {
          this.clearFilter();
        }
      });
  }

  ngOnChanges() {
    let cardsDetails: [CategoryTree];
    cardsDetails = [this.cardDetail];
    if (cardsDetails[0] !== undefined) {
      this.dataSource.data = cardsDetails;
    }
  }

  showDetail(node: CategoryTree) {
    this.activeNode = node;
    this.selectedCard = node;
  }

  toggleTreeWidth() {
    if (this.treeWidth === '25%') {
      this.treeWidth = '35%';
    } else if (this.treeWidth === '35%') {
      this.treeWidth = '45%';
    } else if (this.treeWidth === '45%') {
      this.treeWidth = '25%';
    }

    // if (this.treeWidth === '25%') {
    //   this.treeWidth = '35%';
    //   this.filterByName('in');
    // } else if (this.treeWidth === '35%') {
    //   this.treeWidth = '45%';
    //   this.filterByName('es');
    // } else if (this.treeWidth === '45%') {
    //   this.treeWidth = '25%';
    //   this.clearFilter();
    // }

  }

  private filterByContent(term: string): void {
    const filteredItems = this.treeControl.dataNodes.filter(
      x => ((x.name.toLowerCase().indexOf(term.toLowerCase()) === -1) && (x.description.toLowerCase().indexOf(term.toLowerCase()) === -1))
    );
    filteredItems.map(x => {
      x.isVisible = false;
    });

    const visibleItems = this.treeControl.dataNodes.filter(
      x => ((x.name.toLowerCase().indexOf(term.toLowerCase()) > -1) || (x.description.toLowerCase().indexOf(term.toLowerCase()) > -1))
    );
    visibleItems.map(x => {
      x.isVisible = true;
    });
    this.treeControl.expandAll();
  }

  private clearFilter(): void {
    this.treeControl.dataNodes.forEach(x => x.isVisible = true);
    this.treeControl.collapseAll();
  }

  filterByName(filter: string): void {
    this.searchFilter.next(filter);
  }

}
