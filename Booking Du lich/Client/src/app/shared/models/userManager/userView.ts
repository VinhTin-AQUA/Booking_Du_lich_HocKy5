export interface UserView {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  emailConfirmed: boolean;
  phoneNumber: string;
  lockoutEnd: Date;
}
