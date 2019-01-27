import { Injectable } from '@angular/core';
import {User} from "../models/user";

@Injectable({
  providedIn: 'root',
})
export class GameLogService {
    public log: string;
}
