USE Booking;

GO

UPDATE Tour
SET
	PhotoPath = '/tours/1'
WHERE
	TourId = 1;

UPDATE Tour
SET
	PhotoPath = '/tours/2'
WHERE
	TourId = 2;

UPDATE Tour
SET
	PhotoPath = '/tours/3'
WHERE
	TourId = 3;
