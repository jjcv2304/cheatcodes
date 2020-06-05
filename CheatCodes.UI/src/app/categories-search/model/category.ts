import {ICategoryFieldValue} from '../../categories-generation/models/categoryFieldValue';
import {GetRandom} from '../../test-utils/GetRandom';
import {Category, ICategory} from '../../categories-generation/models/category';

export interface ICategoryBasic {
  id: number;
  name: string;
  description: string;
  parentId: number;
  color: string;
}

export class CategoryBasic implements ICategoryBasic {
  public constructor(init?: Partial<CategoryBasic>) {
    Object.assign(this, init);
  }

  id: number = null;
  name: string = null;
  description = '';
  parentId: number;
  color = 'lightskyblue';
}


export class CategoryBasicBuilder implements ICategoryBasic {
  id: number = null;
  name: string = null;
  description = '';
  parentId: number;
  color = 'lightskyblue';

  static basic() {
    const categoryBuilder = new CategoryBasicBuilder();
    categoryBuilder.id = GetRandom.Number();
    categoryBuilder.name = GetRandom.String();
    return categoryBuilder;
  }

  constructor() {
  }

  build() {
    return new Category(this);
  }

  simple() {
    this.id = GetRandom.Number();
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

  setDescription(description: string) {
    this.description = description;
    return this;
  }

  setParentId(parentId: number) {
    this.parentId = parentId;
    return this;
  }
}

export interface ICategoryTree {
  id: number;
  name: string;
  description: string;
  parentId: number;
  color: string;
  childs: ICategoryTree[];
}

export class CategoryTree implements ICategoryTree {
  public constructor(init?: Partial<CategoryTree>) {
    Object.assign(this, init);
  }

  id: number = null;
  name: string = null;
  description = '';
  parentId: number;
  color = 'lightskyblue';
  childs: CategoryTree[];
}
