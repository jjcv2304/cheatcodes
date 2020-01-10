export class Envelope<T> {
  public constructor(init?: Partial<Envelope<T>>) {
    Object.assign(this, init);
  }

  result: T = null;
  errorMessage = '';
  timeGenerated = '';
}
