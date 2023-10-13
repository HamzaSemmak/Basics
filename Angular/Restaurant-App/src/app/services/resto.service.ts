import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Restaurant } from '../Model/Restaurant';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
}

@Injectable({
  providedIn: 'root'
})
export class RestoService {
  Url: string = "http://localhost:3000/restaurants";

  constructor(private HttppClient: HttpClient) { }

  All(): Observable<Restaurant[]> {
    return this.HttppClient.get<Restaurant[]>(this.Url);
  }

  Create(Restaurant: Restaurant): Observable<Restaurant> {
    return this.HttppClient.post<Restaurant>(this.Url, Restaurant, httpOptions);
  }

  Delete(Restaurant: Restaurant): Observable<Restaurant> {
    return this.HttppClient.delete<Restaurant>(`${this.Url}/${Restaurant.id}`);
  }

  Select(id: number) {
    return this.HttppClient.get<Restaurant>(`${this.Url}/${id}`);
  }

  Update(id: number, Restaurant: Restaurant): Observable<Restaurant> {
    return this.HttppClient.put<Restaurant>(`${this.Url}/${id}`, Restaurant);
  }

}
