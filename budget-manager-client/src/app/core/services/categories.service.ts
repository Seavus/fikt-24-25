import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Category {
  id: string;
  name: string;
}

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private http: HttpClient) {}

  getCategories(
    PageIndex: number = 1,
    PageSize: number = 5
  ): Observable<{ items: Category[]; totalCount: number }> {
    const params = new HttpParams()
      .set('PageIndex', PageIndex.toString())
      .set('PageSize', PageSize.toString());

    return this.http.get<{ items: Category[]; totalCount: number }>(
      '/api/account/categories',
      { params }
    );
  }

  createCategory(name: string): Observable<{ categoryId: string }> {
    return this.http.post<{ categoryId: string }>('/api/categories', { name });
  }

  updateCategory(categoryId: string, name: string): Observable<void> {
    return this.http.put<void>('/api/categories', { categoryId, name });
  }

  deleteCategory(categoryId: string): Observable<{ isSuccess: boolean }> {
    return this.http.delete<{ isSuccess: boolean }>(
      `/api/categories/id:guid?id=${categoryId}`
    );
  }
}
