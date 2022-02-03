import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'seed',
  templateUrl: './seed.component.html'
})
export class SeedComponent {
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get(baseUrl + 'Seed/Import').subscribe(result => {

    }, error => console.error(error));
  }
}
