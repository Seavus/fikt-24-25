export interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
}

export interface UserDetailsResponse {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  balance: number;
  createdOn: string;
  createdBy: string;
  updatedOn: string;
  updatedBy: string;
}

export interface UpdateUserRequest {
  userId: string;
  firstName: string;
  lastName: string;
}

export interface UpdateUserResponse {
  id: {
    value: string;
  };
  firstName: string;
  lastName: string;
  email: string;
}
