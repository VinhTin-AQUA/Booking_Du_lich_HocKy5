import { City } from "../city/city";
import { TourType } from "./tourType";

export interface Tour {
	TourId: number;
	TourName: string;
	TourAddress: string;
	Overview: string;
	Schedule: string;
	DepartureLocation: string;
	DropOffLocation: string
	PhotoPath: string;
	PostingDate: Date
	City: City
	TourType: TourType
}