import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-seed',
  templateUrl: './seed.component.html'
})
export class SeedComponent {
  public importDataResult: ImportDataResult = { Cities: 0, Countries: 0 };

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<ImportDataResult>(baseUrl + 'seed').subscribe(result => {
      this.importDataResult = result;

      console.log(this.importDataResult);
    }, error => console.error(error));
  }
}

interface ImportDataResult {
  Cities: number;
  Countries: number;
}
