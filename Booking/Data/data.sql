USE Booking;
GO

-- CATEGORY

INSERT INTO Category(CategoryName) VALUES (N'Chuyến tham quan');
INSERT INTO Category(CategoryName) VALUES (N'Ngoài trời');
INSERT INTO Category(CategoryName) VALUES (N'Ẩm thực');
INSERT INTO Category(CategoryName) VALUES (N'Di chuyển');
INSERT INTO Category(CategoryName) VALUES (N'Trong nhà');
INSERT INTO Category(CategoryName) VALUES (N'Sự kiện');
INSERT INTO Category(CategoryName) VALUES (N'Điểm tham quan');
INSERT INTO Category(CategoryName) VALUES (N'Sức khỏe');
INSERT INTO Category(CategoryName) VALUES (N'Văn hóa');

-- TOUR

INSERT INTO Tour(TourName,TourAddress,Overview,Schedule,DepartureLocation,DropOffLocation) VALUES(
	N'Cu Chi Tunnels VIP Tour From Ho Chi Minh City',
	N'Phú Hiệp, Củ Chi, Thành phố Hồ Chí Minh',
	N'Địa đạo Củ Chi là một hệ Thống phòng thủ trong lòng đất ở huyện Củ Chi, cách Thành phố Hồ Chí Minh 70 km về hướng tây-bắc. Hệ thống này được quân kháng chiến Việt Minh và Mặt trận Dân tộc Giải phóng miền Nam Việt Nam đào trong thời kỳ Chiến tranh Đông Dương và Chiến tranh Việt Nam.',
	N'[[Cu Chi Tunnels,Experience one of the most exciting view inside Cu Chi, which is the tunnels. Visitors can explore and role-play as Viet cong soldiers during the trip to fully feel the temping and their lifestyle during the Vietnam War.],[Vietnam Travel Group,One of the pick up point / drop off point after the tour.]]',
	N'55 Đỗ Quang Đẩu, Phường Phạm Ngũ Lão, Quận 1, Thành phố Hồ Chí Minh, Việt Nam',
	N'55 Đỗ Quang Đẩu, Phường Phạm Ngũ Lão, Quận 1, Thành phố Hồ Chí Minh, Việt Nam'
);

-- CITYTOUR

INSERT INTO CityTour VALUES (1,2);
