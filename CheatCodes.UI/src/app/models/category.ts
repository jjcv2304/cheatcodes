import {ICategoryFieldValue} from "./categoryFieldValue";

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
  description: string = '';
  hasParent: boolean = false;
  hasChild: boolean = false;
  parentId: number;
  categoryFieldValues: ICategoryFieldValue[];
}
