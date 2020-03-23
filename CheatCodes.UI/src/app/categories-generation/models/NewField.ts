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
  description = "";
  categoryId: number;
}
