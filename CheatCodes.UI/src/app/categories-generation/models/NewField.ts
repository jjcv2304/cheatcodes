import {GetRandom} from '../../test-utils/GetRandom';
import {Category} from './category';

export interface INewField {

  name: string;
  description: string;
  categoryId: number;
}

export class NewField implements INewField {
  public constructor(init?: Partial<NewField>) {
    Object.assign(this, init);
  }

  name: string;
  description = '';
  categoryId: number;
}

export class NewFieldBuilder implements INewField {
  name: string;
  description = '';
  categoryId: number;

  constructor() {
  }
  build() {
    return new NewField(this);
  }
  simple() {
    this.name = GetRandom.String();
    return this;
  }
  setDescription(description: string) {
    this.description = description;
    return this;
  }
  setCategoryId(categoryId: number) {
    this.categoryId = categoryId;
    return this;
  }
}

