import {ICategoryFieldValue} from './categoryFieldValue';

export interface ICategory {
  id: number;
  name: string;
  description: string;
  hasParent: boolean;
  hasChild: boolean;
  parentId: number;
  categoryFieldValues: ICategoryFieldValue [];
}

export class Category implements ICategory {
  public constructor(init?: Partial<Category>) {
    Object.assign(this, init);
  }

  id: number = null;
  name: string = null;
  description = '';
  hasParent = false;
  hasChild = false;
  parentId: number;
  categoryFieldValues: ICategoryFieldValue[];
}
