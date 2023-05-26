import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Restaurant } from '../Model/Restaurant';

@Injectable({
  providedIn: 'root'
})
export class RestoService {
  Url: string = "http://localhost:3000/restaurants";

  constructor(private HttppClient: HttpClient) { }

  getAll(): Observable<Restaurant[]> {
    return this.HttppClient.get<Restaurant[]>(this.Url);
  }
}
