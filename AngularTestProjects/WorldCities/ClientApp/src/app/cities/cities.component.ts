import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

import { City } from './city';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.css']
})
export class CitiesComponent {
  public cities: City[] = [];
  public displayableColumns: string[] = ["id", "name", "latitude", "longitude"];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit() {
    this.http.get<City[]>(this.baseUrl + 'cities')
      .subscribe(result => {

        this.cities = result;

      }, error => console.error(error));
  }
}
