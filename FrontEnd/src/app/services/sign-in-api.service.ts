import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LogInUser } from '../domain/interfaces/log-in-user';
import { CompradorLogin } from '../domain/interfaces/buyer';
import { SignInUser } from '../domain/interfaces/sign-in-user';

@Injectable({
  providedIn: 'root',
})
export class SignInApiService {
  constructor(private http: HttpClient) {}

  signIn(body: SignInUser) {
    const headers = new HttpHeaders({
      accept: '*/*',
      'Content-Type': 'application/json-patch+json',
      'Access-Control-Allow-Origin': '*',
    });

    return this.http.post(`http://localhost:5237/api/Api/Registrarse`, body);
  }
}
