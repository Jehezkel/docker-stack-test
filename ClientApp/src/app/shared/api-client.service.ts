import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiClientService {
  webApiBase = environment.API_URL_BASE
  httpClient = inject(HttpClient)
  constructor() { }
  getWeather = () => this.httpClient.get(this.webApiBase + "/weather-forecast")
}
