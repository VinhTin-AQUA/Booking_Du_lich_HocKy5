import { City } from "../city/city";
import { UserView } from "../userManager/userView";

export interface Hotel {
  Id: number;
  HotelName: string;
  Address: string;
  Description: string;
  PhotoPath: string;
  City: City;
  PostingDate: Date;
  ApprovalDate: Date;
  Poster: UserView
  Approver: UserView
}
