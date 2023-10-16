import { Hotel } from '../hotel/hotel';

export interface Room {
  Id: number;
  RoomNumber: string;
  Name: string;
  Description: string;
  IsAvailable: boolean;
  PhotoPath: string;
  Hotel: Hotel;
}
