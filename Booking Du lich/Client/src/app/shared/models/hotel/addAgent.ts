export interface Agent{
	HotelId: string | null;
	FirstName: string;
	LastName: string;
	Email: string;
	EmailConfirmed: boolean;
	LockoutEnd: Date | null;
	PhoneNumber: string
	Password: string
}