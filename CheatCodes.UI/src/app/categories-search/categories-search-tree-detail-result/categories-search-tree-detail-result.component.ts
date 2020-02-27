/* tslint:disable:member-ordering */
import {Component, Input, OnInit} from '@angular/core';
import {CategoryTree} from '../model/category';
import {FlatTreeControl} from '@angular/cdk/tree';
import {MatTreeFlatDataSource, MatTreeFlattener} from '@angular/material';

interface FlatNode {
  expandable: boolean;
  name: string;
  description: string;
  level: number;
}

@Component({
  selector: 'app-categories-search-tree-detail-result',
  templateUrl: './categories-search-tree-detail-result.component.html',
  styleUrls: ['./categories-search-tree-detail-result.component.scss']
})
export class CategoriesSearchTreeDetailResultComponent implements OnInit {

  @Input() cardDetail: CategoryTree;
  selectedCard: CategoryTree;

  private _transformer = (node: CategoryTree, level: number) => {
    return {
      expandable: !!node.childs && node.childs.length > 0,
      name: node.name,
      description: node.description,
      level: level,
    };
  }

  treeControl = new FlatTreeControl<FlatNode>(node => node.level, node => node.expandable);

  treeFlattener = new MatTreeFlattener(
    this._transformer, node => node.level, node => node.expandable, node => node.childs);

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  hasChild = (_: number, node: FlatNode) => node.expandable;
  activeNode: CategoryTree;

  ngOnInit(): void {
    let cardsDetails: [CategoryTree];
    cardsDetails = [this.cardDetail];
    this.dataSource.data = cardsDetails;
  }

  showDetail(node: CategoryTree) {
    this.activeNode = node;
    this.selectedCard = node;
  }
}
