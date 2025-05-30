import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from './categories.service';

export interface Transaction {
  id?: string;
  categoryName: string;
  typeTransaction: string;
  amount: number;
  description: string;
}

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  private readonly baseUrl = '/api/account/transactions';
  constructor(private http: HttpClient) {}

  getTransactions(
    pageIndex: number = 1,
    pageSize: number = 5
  ): Observable<{ items: Transaction[]; totalCount: number }> {
    const params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());

    return this.http.get<{ items: Transaction[]; totalCount: number }>(
      '/api/account/transactions',
      { params }
    );
  }

   createTransaction(transaction: Transaction): Observable<{ transactionId: string }> {
    return this.http.post<{ transactionId: string }>('/api/transactions', transaction);
  }

  updateTransaction(id: string, transaction: Transaction): Observable<void> {
    return this.http.put<void>(`$'/api/transactions'/${id}`, transaction);
  }

  deleteTransaction(id: string): Observable<{ isSuccess: boolean }> {
    return this.http.delete<{ isSuccess: boolean }>(`$/api/transactions/id:guid?id=$/${id}`);
  }
  }