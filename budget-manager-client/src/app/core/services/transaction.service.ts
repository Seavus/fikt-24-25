import { HttpClient, HttpParams } from '@angular/common/http';
import {
  Transaction,
  TransactionPayload,
} from '../interfaces/transaction.model';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  constructor(private readonly http: HttpClient) {}

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

  createTransaction(
    transaction: TransactionPayload
  ): Observable<{ transactionId: string }> {
    return this.http.post<{ transactionId: string }>(
      '/api/transactions',
      transaction
    );
  }

  deleteTransaction(id: string): Observable<{ isSuccess: boolean }> {
    return this.http.delete<{ isSuccess: boolean }>(`/api/transactions/${id}`);
  }
}
