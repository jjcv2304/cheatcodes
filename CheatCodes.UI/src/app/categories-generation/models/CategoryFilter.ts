
export class CategoryFilter {

  byParent = false;
  byChild = false;
  byName = false;
  byDescription = false;

  parentId: number = null;
  name: string = null;
  description: string = null;
  childId: number = null;

  public constructor() {
  }

  public static FilterByParent(parentId: number): CategoryFilter {
    const newFilter = new CategoryFilter();
    newFilter.byParent = true;
    newFilter.parentId = parentId;
    return newFilter;
  }
  public static FilterByChild(childId: number): CategoryFilter {
    const newFilter = new CategoryFilter();
    newFilter.byChild = true;
    newFilter.childId = childId;
    return newFilter;
  }
}

// export interface ICategory {
//   id: number;
//   name: string;
//   description: string;
//   hasParent: boolean;
//   hasChild: boolean;
// }
//
// export class Category implements ICategory {
//   public constructor(init?: Partial<Category>) {
//     Object.assign(this, init);
//   }
//
//   id: number = null;
//   name: string = null;
//   description: string = '';
//   hasParent: boolean = false;
//   hasChild: boolean = false;
//
// }
