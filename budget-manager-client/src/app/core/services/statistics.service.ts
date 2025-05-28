import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class StatisticsService {
  private readonly apiUrl = '/api/transactions/statistics';

  constructor(private readonly http: HttpClient) {}

  getStatistics(month?: number, year?: number): Observable<any> {
    let params = new HttpParams();
    if (month !== undefined) params = params.set('month', month.toString());
    if (year !== undefined) params = params.set('year', year.toString());

    return this.http.get(this.apiUrl, { params });
  }
}
