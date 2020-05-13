import {ICategoryFieldValue} from './categoryFieldValue';
import {GetRandom} from '../../test-utils/GetRandom';


export interface ICategory {
  id: number;
  name: string;
  description: string;
  hasParent: boolean;
  hasChild: boolean;
  parentId: number;
  color: string;
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
  color = 'lightskyblue';
  categoryFieldValues: ICategoryFieldValue[];
}

export class CategoryBuilder implements ICategory {
  id: number = null;
  name: string = null;
  description = '';
  hasParent = false;
  hasChild = false;
  parentId: number;
  color = 'lightskyblue';
  categoryFieldValues: ICategoryFieldValue[];

  constructor() {
  }
  build() {
    return new Category(this);
  }
  simple() {
    this.id =  GetRandom.Number();
    this.name = GetRandom.String();
    return this;
  }

  setId(id: number) {
    this.id = id;
    return this;
  }

  setName(name: string) {
    this.name = name;
    return this;
  }

  setHasParent(hasParent: boolean) {
    this.hasParent = hasParent;
    return this;
  }

  setHasChild(hasChild: boolean) {
    this.hasChild = hasChild;
    return this;
  }

  setDescription(description: string) {
    this.description = description;
    return this;
  }
}
