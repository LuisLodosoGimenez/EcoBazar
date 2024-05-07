import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LogInUser } from '../domain/interfaces/log-in-user';
import { CompradorLogin } from '../domain/interfaces/buyer';

@Injectable({
  providedIn: 'root',
})
export class LogInApiService {
  constructor(private http: HttpClient) {}

  logIn(body: LogInUser) {
    const headers = new HttpHeaders({
      accept: '*/*',
      'Content-Type': 'application/json-patch+json',
      'Access-Control-Allow-Origin': '*',
    });

    return this.http.post<CompradorLogin>(`http://localhost:5237/api/Api/IniciarSesion`, body);
  }
}
