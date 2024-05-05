
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Productos } from '../domain/interfaces/category-products';

@Injectable({
  providedIn: 'root',
})
export class ProductsByCategoryApiService {
  private API_URL = 'http://localhost:5237/api/Api/';

  constructor(private http: HttpClient) {}

  getProductsByCategory(category: string){
    return this.http.get<Productos>(`${this.API_URL}Categoria?categoria=${category}`);
  }
}
