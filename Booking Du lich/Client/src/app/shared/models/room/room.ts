import { Hotel } from '../hotel/hotel';
import { RoomPrice } from './roomPrice';
import { RoomType } from './roomType';

export interface Room {
  Id: number;
  RoomNumber: string;
  RoomName: string;
  Description: string;
  IsAvailable: boolean;
  PhotoPath: string;
  Hotel: Hotel;
  RoomType: RoomType
  RoomPrice: RoomPrice
}
