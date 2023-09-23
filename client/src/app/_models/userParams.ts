import { User } from './user';

export class UserParams {
  gender: string;
  minAge: number = 18;
  maxAge: number = 99;
  pageNumber = 1;
  pageSize = 5;
  orderBy = 'lastActive';
  constructor(user: User) {
    debugger;
    console.log(user.gender);
    this.gender = user.gender === 'male' ? 'female' : 'male';
  }
}
//why not interface? because we need to initialize the values using constructor
