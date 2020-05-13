import {NewField} from './NewField';
import {GetRandom} from '../../test-utils/GetRandom';

export interface ICategoryFieldValue {

  fieldId: number;
  categoryId: number;
  fieldName: string;
  value: string;
}

export class CategoryFieldValue implements ICategoryFieldValue {
  public constructor(init?: Partial<CategoryFieldValue>) {
    Object.assign(this, init);
  }

  fieldId: number;
  categoryId: number;
  fieldName: string;
  value: string;
}

export class CategoryFieldValueBuilder implements  ICategoryFieldValue {
  fieldId: number;
  categoryId: number;
  fieldName: string;
  value: string;

  constructor() {
  }
  build() {
    return new CategoryFieldValue(this);
  }
  simple() {
    this.fieldId = GetRandom.Number();
    this.categoryId = GetRandom.Number();
    this.fieldName = GetRandom.String();
    this.value = GetRandom.String();
    return this;
  }
}
