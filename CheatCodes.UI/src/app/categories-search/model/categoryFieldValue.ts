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
