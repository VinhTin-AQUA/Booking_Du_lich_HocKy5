import { Hotel } from '../hotel/hotel';
import { HotelService } from './hotelService';

export interface HasService {
  HotelID: number;
  ServiceID: number;
  Hotel: Hotel;
  Service: HotelService;
}
