import {Guid} from 'guid-typescript';

export class Person {
    id: Guid = Guid.createEmpty()
    firstName: string = ""
    lastName: string = ""
    middleName: string = ""
    birthday: string = ""
  }
