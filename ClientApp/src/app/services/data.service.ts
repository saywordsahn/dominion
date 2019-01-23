import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders }    from '@angular/common/http';
import {Observable, Subject} from 'rxjs';
import {Supply} from "../models/supply";
import {Player} from "../models/player";
import {Lobby} from "../models/lobby";

@Injectable({
  providedIn: 'root',
})
export class DataService {

    private baseUrl: string;
    public supplySubject: Subject<Supply> = new Subject();
    public playerSubject: Subject<Player> = new Subject();
    public LobbySubject: Subject<Lobby[]> = new Subject();

    constructor(private http: HttpClient,
        @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    getData() : Observable<WeatherForecast[]> {
       return this.http.get<WeatherForecast[]>(this.baseUrl + 'api/SampleData/WeatherForecasts');
    }

    getLobbies() : Observable<Lobby[]> {
       return this.http.get<Lobby[]>(this.baseUrl + 'api/Lobby');
    }

    getCardCosts() : Observable<number[]> {
      return this.http.get<number[]>(this.baseUrl + 'api/Card/CardCosts');
    }

    register() : Observable<object> {
      return this.http.post(this.baseUrl + 'account/register',  { Email: 'maria@gmail.com', Password: 'b!1bmtickleP'});
    }

    login(email: string, password: string) : Observable<object> {
      return this.http.post(this.baseUrl + 'account/token',  { Email: email, Password: password});
    }

    




}


interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
