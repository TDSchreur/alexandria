import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {}

  getData() {
    const person = <Person>{
      firstName: 'Dennis',
      lastName: 'Schreur',
    };

    console.log(this.baseUrl);
    return this.http.get<string>(this.baseUrl + 'api/GenerateException');
  }
}

export interface Person {
  firstName: string;
  lastName: string;
}
