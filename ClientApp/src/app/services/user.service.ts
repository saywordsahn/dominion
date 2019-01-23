import { Injectable } from '@angular/core';
import {User} from "../models/user";



@Injectable({
  providedIn: 'root',
})
export class UserService {
    public user: User;
    public userName: string;
  public loginToken: string;

  constructor() {
    this.user = new User('21d52470-9a75-4012-ad6b-da787c348f09', 'Ben');
  }

  //temporary switch to play around with different users
  public setUser(id: string)
  {
    if (id == '21d52470-9a75-4012-ad6b-da787c348f09') {
      this.user = new User('21d52470-9a75-4012-ad6b-da787c348f09', 'Ben');
    } else {
      this.user = new User('add58dab-0f7a-495e-98c6-13f060b367dc', 'Maria');
    }
  }

  public getUser() : User {
    return this.user;
  }

}
