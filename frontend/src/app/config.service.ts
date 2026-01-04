import { Injectable } from '@angular/core';
import { Config } from './person.service';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  public cfg: Config = new Config();
  constructor(private http: HttpClient) {}

  load() {
    return this.http.get<Config>('config.json', {
      headers: { 'Cache-Control': 'no-cache' }
    }).pipe(
      tap(t => {
        this.cfg = t;
      })
    );
  }

}
