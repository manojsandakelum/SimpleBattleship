import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BattleshipService {

  private baseUrl = 'https://localhost:7079/api/Game';

  constructor(private http: HttpClient) {}

  getGrid(): Observable<string[][]> {
    return this.http.get<string[][]>(`${this.baseUrl}/grid`);
  }

  fireShot(x: number, y: number): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/fire`, { x, y });
  }

  resetGame(): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/reset`, {});
  }
}
