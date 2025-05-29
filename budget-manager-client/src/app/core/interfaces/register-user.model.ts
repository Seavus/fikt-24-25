export interface RegisterUserRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  balance: number;
}

export interface RegisterUserResponse {
  id: string;
}
