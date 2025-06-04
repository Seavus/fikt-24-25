export interface Transaction {
  id?: string;
  category: {
    id: string;
    name: string;
  };
  transactionType: string;
  transactionDate: string;
  amount: number;
  description: string;
}

export interface TransactionPayload {
  categoryId: string;
  transactionType: number;
  transactionDate: Date;
  amount: number;
  description: string;
}
