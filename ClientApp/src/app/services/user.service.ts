import { Injectable } from '@angular/core';
import {User} from "../models/user";



@Injectable({
  providedIn: 'root',
})
export class UserService {
  private user: User;
  public userName: string;
  public loginToken: string;

  constructor() {
    this.user = new User(1, 'Ben');
  }

  //temporary switch to play around with different users
  public setUser(id: number)
  {
    if (id == 1) {
      this.user = new User(1, 'Ben');
    } else {
      this.user = new User(2, 'Maria');
    }
  }

  public getUser() : User {
    return this.user;
  }

}
