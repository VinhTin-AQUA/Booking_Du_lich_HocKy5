import { City } from "../city/city";

export interface Hotel {
  Id: number;
  HotelName: string;
  Address: string;
  AvailableRoom: number;
  Description: string;
  PhotoPath: string;
  Agents: any;
  City: City;
}
