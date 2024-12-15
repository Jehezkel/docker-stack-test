import { Component, inject, OnInit } from '@angular/core';
import { ApiClientService } from '../shared/api-client.service';
import { Observable } from 'rxjs';
import { AsyncPipe, JsonPipe } from '@angular/common';

@Component({
  selector: 'app-home-page',
  imports: [AsyncPipe, JsonPipe],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent implements OnInit {
  apiClient = inject(ApiClientService)
  weatherValue = new Observable<any>();
  ngOnInit(): void {
    this.weatherValue = this.apiClient.getWeather();
  }


}
