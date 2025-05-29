import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserStateService {
  private readonly balanceSubject = new BehaviorSubject<number>(
    this.getInitialBalance()
  );
  balance$ = this.balanceSubject.asObservable();

  private getInitialBalance(): number {
    const stored = localStorage.getItem('balance');
    return stored ? Number(stored) : 0;
  }

  setBalance(balance: number) {
    this.balanceSubject.next(balance);
    localStorage.setItem('balance', balance.toString());
  }

  clearBalance() {
    this.balanceSubject.next(0);
    localStorage.removeItem('balance');
  }
}
