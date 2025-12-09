import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Person } from './person-manager/person-manager.component';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  private baseUrl = 'https://localhost:7259/api/persons';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Person[]> {
    return this.http.get<Person[]>(this.baseUrl);
  }

  add(name: string): Observable<Person> {
    const body = { name };
    return this.http.post<Person>(this.baseUrl, body);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  drawRandom(): Observable<Person> {
    return this.http.get<Person>(`${this.baseUrl}/random`);
  }
}