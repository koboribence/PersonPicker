import { APP_INITIALIZER } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { provideHttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Draw, Person } from './person-manager/person-manager.component';
import { ConfigService } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  private baseUrl: string;
  //private baseUrl = `${this.config.backendUrl}/api/persons`;
  //should be changed to cfg
  constructor(private http: HttpClient,private config: ConfigService) {
     this.baseUrl = this.config.cfg.backendUrl;
  }

  getAll(): Observable<Person[]> {
    return this.http.get<Person[]>(this.baseUrl + '/api/persons');
  }

  add(name: string): Observable<Person> {
    const body = { name };
    return this.http.post<Person>(this.baseUrl + '/api/persons', body);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl + '/api/persons'}/${id}`);
  }

  drawRandom(): Observable<Person> {
    return this.http.get<Person>(`${this.baseUrl + '/api/persons'}/random`);
  }
  getDraws(): Observable<Draw[]> {
  return this.http.get<Draw[]>(this.baseUrl + '/api/draws');
}
}
export class Config {
  backendUrl:string = ""
}