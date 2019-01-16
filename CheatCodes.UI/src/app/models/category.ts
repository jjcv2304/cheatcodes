export interface ICategory {
  id: number;
  name: string;
  description: string;
  childcategories: ICategory[];
}

export class Category implements ICategory {
  public constructor(init?: Partial<Category>) {
    Object.assign(this, init);
  }

  id: number = null;
  name: string = null;
  description: string = '';
  childcategories: Category[] = [];

}
