export interface UserView {
  Id: string;
  FirstName: string;
  LastName: string;
  Email: string;
  Address: string;
  EmailConfirmed: boolean;
  PhoneNumber: string | null;
  LockoutEnd: Date | null;
}
