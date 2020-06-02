import Mock = jest.Mock;

export const createSpyObj = (baseName, methodNames): { [key: string]: Mock<any> } => {
  let obj: any = {};

  for (let i = 0; i < methodNames.length; i++) {
    obj[methodNames[i]] = jest.fn();
  }

  return obj;
};
