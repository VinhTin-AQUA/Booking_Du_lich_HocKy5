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

-- Ho Chi Minh
		-- Chuyen tham quan
			-- Tour
INSERT INTO Tour(TourName,TourAddress,Overview,Schedule,DepartureLocation,DropOffLocation,PhotoPath) VALUES(
	N'Mekong Delta Tour from HCM City - Discover the Delta''s Charms',
	N'...',
	N'This tour is very suitable for all people who loves to visit the rural area of the Mekong Delta in Southern Vietnam. You will be visiting some attractions like: Vinh Trang temple, Visit My Tho and Ben Tre where are the heart of the Mekong Delta region. Cruising on the motor boat along Tien River, paddle along the small canals on a rowing boat.',
	N'[["Vinh Trang Temple","Around 7:45am. Get picked up from your hotel in center of Ho Chi Minh City and head to the Mekong Delta. During the 1.5 car ride, we will pass through by green rice fields before arriving at the beautiful rural My Tho.","The first stop is Vinh Trang pagoda. It is the biggest pagoda in the Mekong Delta region. You will be guided to explore the most beautiful, ancient and significant temple in the Mekong Delta. This religious landmark dates back to the 19th century and features a harmonious fusion of Vietnamese, Khmer, and Chinese architectural styles.","Thời gian: 1 tiếng (xấp xỉ)"],
		["My Tho","Head to the nearby pier, where you''ll board a traditional Mekong Delta boat for a scenic cruise on the Mekong River. You will pass by the Turtle, Dragon, Phoenix Islets and arrive at the Unicorn Islet. As you embark on this journey, you''ll be captivated by the lush greenery and bustling river life. Continue your exploration by riding a traditional sampan boat through the narrow waterways with coconut palms lining both sides. This up-close experience allows you to appreciate the unique ecosystem and local way of life. Stop at a bee farm, savor honey tea, taste local fruits and enjoy traditional music performed by the villagers and learn about the daily life of the locals.","Thời gian: 2 tiếng (xấp xỉ)"],
		["Ben Tre","Enjoy a local lunch with Vietnamese dishes (vegan food available) After lunch, we take a boat trip to Ben Tre - known as the coconut kingdom - a charming town that is famous for its coconut plantations, fruit orchards. You can take a leisurely bike ride through the countryside. Learn about how coconut products are produced in the region. Around 3:00pm head back to Ho Chi Minh City. Arrive approximately at 4:50PM.","Thời gian: 2 tiếng (xấp xỉ)"]]',
	N'123 Lý Tự Trọng, Phường Bến Thành, Quận 1, Thành phố Hồ Chí Minh, Việt Nam',
	N'123 Lý Tự Trọng, Phường Bến Thành, Quận 1, Thành phố Hồ Chí Minh, Việt Nam',
	N'/tous/1'
);

INSERT INTO Tour(TourName,TourAddress,Overview,Schedule,DepartureLocation,DropOffLocation,PhotoPath) VALUES(
	N'Cu Chi Tunnels Luxury Tour - Morning or Afternoon',
	N'...',
	N'On this half-day Morning or Afternoon Cu Chi Tunnels tour, you will see the Cu Chi complex with its network of tunnels and traps, and learn about how society functioned underground during times of war. You can also try your hand at shooting range with an AK-47',
	N'[["Cu Chi Tunnels","Around 7:35AM or 12:10PM. Start with pickup from the center of Ho Chi Minh City or meet at the meeting point then depart for Cu Chi Tunnels. After 1.5 hour drive, we arrive at the Tunnels where You''ll have the opportunity to explore the tunnel system, which includes narrow passageways, hidden entrances, and underground chambers. Learn about the daily life of the Cu Chi guerrilla fighters and how they managed to survive in the tunnels. You can crawl distances through the tunnels that were used by the guerrilla fighters during the Vietnam War. You may see kitchens, living quarters, among other things used during the war. Learn about how different types traps were created and set up. Visit the weapon rooms and learn how the ingenious soldiers made them. You can also safely try your hand at the shooting range with an AK-47. After exploration, we travel back to Ho Chi Minh City. Arrive approximately at 3:00pm for the morning tour and 6:50pm for the afternoon tour.","Thời gian: 3 tiếng (xấp xỉ)","Bao gồm vé vào cửa"]]',
	N'123 Lý Tự Trọng, Phường Bến Thành, Quận 1, Thành phố Hồ Chí Minh, Việt Nam',
	N'123 Lý Tự Trọng, Phường Bến Thành, Quận 1, Thành phố Hồ Chí Minh, Việt Nam',
	N'/tous/2'
);

INSERT INTO Tour(TourName,TourAddress,Overview,Schedule,DepartureLocation,DropOffLocation,PhotoPath) VALUES(
	N'Private Full Day Tour of Ho Chi Minh City including Lunch',
	N'...',
	N'This private tour of Ho Chi Minh City including lunch gives you a fascinating insight into the culture, history and the daily life of the locals in Ho Chi Minh City.',
	N'[["The Independence Palace","Reunification Palace was the base of Vietnamese General Ngo Dinh Diem until his death in 1963. It made its name in global history in 1975. A tank belonging to the North Vietnamese Army crashed through its main gate, ending the Vietnam War. Today, it''s a must-visit for tourists in Ho Chi Minh City. The palace is like a time capsule frozen in 1975. You can see two of the original tanks used in the capture of the palace parked in the grounds. Reunification Palace was the home and workplace of the French Governor of Cochin-China. It has lush gardens, secret rooms, antique furniture, and a command bunker. It''s still in use to host important occasions in Ho Chi Minh, including APEC summits.","Thời gian: 1.5 tiếng (xấp xỉ)","Bao gồm vé vào cửa"],
	   ["War Remnants Museum","The War Remnants Museum in Ho Chi Minh City first opened to the public in 1975. Once known as the ‘Museum of American War Crimes’, it''s a shocking reminder of the long and brutal Vietnam War. Graphic photographs and American military equipment are on display. There''s a helicopter with rocket launchers, a tank, a fighter plane, a single-seater attack aircraft. You can also see a conventional bomb that weighs at 6,800kg. American troops had used these weapons against the Vietnamese between 1945 and 1975.","Thời gian: 1.5 tiếng (xấp xỉ)","Bao gồm vé vào cửa"],
	   ["Saigon Notre Dame Cathedral","(Notes: Maintenance) Saigon Notre Dame Cathedral, built in the late 1880s by French colonists, is one of the few remaining strongholds of Catholicism in the largely Buddhist Vietnam. Located in Paris Square, the name Notre Dame was given after the installation of the statue ‘Peaceful Notre Dame’ in 1959. In 1962, the Vatican conferred the Cathedral status as a basilica and gave it the official name of Saigon Notre-Dame Cathedral Basilica. Measuring almost 60 metres in height, the cathedral’s distinctive neo-Romanesque features include the all-red brick façade (which were imported from Marseille), stained glass windows, two bell towers containing six bronze bells that still ring to this day, and a peaceful garden setting in the middle of downtown Ho Chi Minh City District 1.","Thời gian: 30 phút (xấp xỉ)","Bao gồm vé vào cửa"],
	   ["Central Post Office","The Central Post Office in Ho Chi Minh is a beautifully preserved remnant of French colonial times and perhaps the grandest post office in all of Southeast Asia. Located next door to Notre Dame Cathedral, the two cultural sites can be visited together and offers visitors a chance to imagine life in Vietnam during the times of the Indochinese Empire. The building was designed by Alfred Foulhoux and features arched windows and wooden shutters, just as it would have in its heyday in the late 19th Century.","Thời gian: 30 phút (xấp xỉ)","Bao gồm vé vào cửa"],
	   ["Saigon Opera House (Ho Chi Minh Municipal Theater)","The Saigon Opera House in Ho Chi Minh is an elegant colonial building at the intersection of Le Loi and Dong Khoi Street in District 1, very close to the famous Notre Dame Cathedral and the classic Central Post Office. The restored three-storey 800-seat Opera House was built in 1897 and is used for staging not only opera but also a wide range of performing arts including ballet, musical concerts, Vietnamese traditional dance and plays. Performances are advertised around the building and information can be found in the state-operated tourist information centre close by","Thời gian: 30 phút (xấp xỉ)","Bao gồm vé vào cửa"],
	   ["Chinatown (Cho Lon) - District 5","Ho Chi Minh City’s Cholon is Vietnam’s largest Chinatown with roots dating back to 1778; it’s also a place of great historical and cultural importance. Chinese minorities hid here from the Tay Son and subsequently had to rebuild the area twice following attack with as many as 70% estimated to have died trying to escape on boats. Those who survived settled and began selling a variety of Chinese products. Cholon is an interesting place to see classical Chinese architecture reminiscent of years gone by with plenty of Chinese restaurants. The Binh Tay market at the centre is busy, crowded and messy with small aisles selling all manner of goods. This market sometimes disappoints tourists when compared to other markets in Ho Chi Minh as the products are not that varied, but the main draw to Cholon is not to shop but to enjoy the authentic Chinese atmosphere that has existed here for hundreds of years.","Thời gian: 1 tiếng (xấp xỉ)","Bao gồm vé vào cửa"],
	   ["Ba Thien Hau Temple","Ba Thien Hau Temple in Saigon is a Buddhist temple dedicated to the Chinese sea goddess, Mazu. It’s believed that she protects and rescues ships and people on the sea by flying around on a mat or cloud. Mazuism is connected with traditions and beliefs from both Taoism and Buddhism. Mazuism is therefore an incorporation of different aspects and traditions which have merged to form a new belief. You will find this temple in ‘Cholon’ (Chinatown) in District 5, which is roughly a twenty minute drive from the city centre. Ba Thien Hau temple was built in 1760 to honour Mazu the ‘Lady of the Sea’ and when you enter through the iron gate you will see massive stone incense burners in front of the entrance of Mazu’s altar. The exterior is beautifully designed with the traditional curvy roof on which small porcelain figures are standing symbol for themes from Chinese religion and legends.","Thời gian: 50 phút (xấp xỉ)","Bao gồm vé vào cửa"],
	   ["Ben Thanh Market","Ben Thanh Market in Ho Chi Minh City''s District 1 is a great place to buy local handicrafts, branded goods, Vietnamese art and other souvenirs. Here, you’ll find eating stalls inside the market where you can get a taste of hawker-style Vietnamese cuisine or simply cool off with a cold drink when the bargaining becomes too much.","The market is big, difficult to navigate at times and certainly best avoided during the hottest part of the day but all the same its well worth a look. When night falls, restaurants around the perimeter of the market open their doors creating a vibrant street side scene filling the air with the scents of wok-fried noodles, barbecued fish and meats. One of Saigon’s oldest landmarks, Ben Thanh offers a great atmosphere that is absolutely authentically Vietnamese","Thời gian: 50 phút (xấp xỉ)","Bao gồm vé vào cửa"]]',
	N'Depends on package',
	N'Depends on package',
	N'/tous/3'
);

INSERT INTO Tour(TourName,TourAddress,Overview,Schedule,DepartureLocation,DropOffLocation,PhotoPath) VALUES(
	N'Saigon River Dinner Cruise: Buffet, Set Menu, Fine Dining (3hrs)',
	N'...',
	N'Saigon at night, especially along the city''s spectacular river, is a wonder to behold. For proof of this, look no further than one of some available dinner cruises. While you dine in traditional performance surroundings, you''ll get a true taste of just how much this city dazzles at night. This river cruise reflects the most beautiful side, relaxing and romantic with the bright lights of Saigon.',
	N'[["Mekong River Tours [Asiana Link Travel]","Our guide and driver will pick you up at your hotel, we drive among the busy traffic to the city harbor."],
	   ["Saigon River","You will embark on one of the boats (traditional or modern boats will be subject to availability). We start cruising between 1 to 1,5 hours along the Sài Gòn River while dinner is served with a five-course Vietnamese food. At the same time, you will also enjoy listening to traditional music and watching the beautiful lights of the city at night. Disembark the boat by 9 pm and transfer back to your hotel or drop off somewhere in District 1 on request. End of services.","Thời gian: 2 tiếng (xấp xỉ)","Bao gồm vé vào cửa"],
	   ["Bach Dang","As night falls, the cruise will glide along the city skyscrapers and Bach Dang Wharf (Ben Bach Dang), creating a captivating scene. The Ho Chi Minh City skyline, once gray and subdued, will transform into a vibrant display of neon lights, enveloping the surroundings in a riot of brilliant colors."]]',
	N'60 Tôn Thất Đạm, Bến Nghé, Quận 1, Thành phố Hồ Chí Minh 700000, Việt Nam',
	N'60 Tôn Thất Đạm, Bến Nghé, Quận 1, Thành phố Hồ Chí Minh 700000, Việt Nam',
	N'/tours/4'
);

INSERT INTO Tour(TourName,TourAddress,Overview,Schedule,DepartureLocation,DropOffLocation,PhotoPath) VALUES(
	N'Ho Chi Minh Street Food Tour By Motorbike with Local Student',
	N'...',
	N'Throughout the tour, you will visit 5 different districts by motorbike and sample 10 Saigon''s best authentic street foods, drinks and dessert. Our first stop is PHO or Beef Noodle Soup - The most famous food in Vietnam and try Jamine Tea in Dist 3. Secondly, we will visit the biggest flower market in dist 10 and sample the best "Saigon Pizza". Then we walk into the corloful street food market and sample the most favourite snack of locals. Next stop, we visit Thich Quang Duc memorial and learn the story about a monk who set himself on fire to protest the persecution of Buddhists in Vietnam. Get refreshed with cold sugar cane drink! Then we drive to Nguyen Thien Thuat - the oldest apartment in Saigon. Climb up to the buildings and see the contrasting architecture between the modern and the traditional. We also visit a pagoda which was built inside an old apartment. Drive to Dist 10 to enjoy the No.1 street food in Vietnam – Banh My. The last stop is Flan Cake for dessert in Dist 4.',
	N'[["Saigon Adventure - Food Tour & City Tours","This is the best way to spend a night out in Saigon!
Throughout the tour,visit diddirent district and sample over 7 Saigon''s best authentic street foods, drinks and dessert. Our first stop is PHO or Beef Noodle Soup - The most famous food in Vietnam and try Jamine Tea in Dist 4. Secondly, we will visit the biggest flower market, We will sample the best Saigon Pizza.
Then we walk into the corloful street food market behind and try the most favourite snacks - Crispy Banana Cracker. Next stop, we visit Thich Quang Duc memorial and learn the story about a monk who set himself on fire to protest the persecution of Buddhists in Vietnam. Then we drive to Nguyen Thien Thuat - the oldest apartment in Saigon. Climb up to the buildings and see the contrasting architecture between the modern and the traditional. We also visit a pagoda which was built inside an old apartment. Drive to Dist 10 to enjoy the No.1 street food in Vietnam – Banh My.
The last stop is Flan Cake for dessert in Dist 4.","Thời gian: 4 tiếng (xấp xỉ)","Bao gồm vé vào cửa"],
	   ["The Venerable Thich Quang Duc Monument","We also stop to visit the Venerable Thich Quang Duc Monument: A memorial to the monk who set himself on fire to protest the persecution of Buddhists in Vietnam. Our guide will tell you about this special monk and his story and show you about our culture ( how we burn incense )","Thời gian: 20 phút (xấp xỉ)"],
	   ["Ho Thi Ky Flower Market","Ho Thi Ky Flower Marke: Ho Thi Ky Flower Market is the place which supplies the fresh and beautiful flowers with quite low a price in Ho Chi Minh City . We will walk in to the market and stop at street food market behind to try the best Saigon Pizza and the most famous local snack : Banana Crispy Cracker. ( promise you will love it )","Thời gian: 40 phút (xấp xỉ)","Bao gồm vé vào cửa"],
	   ["Nguyen Thien Thuat Apartment Buildings","we drive to Nguyen Thien Thuat - the oldest apartment in Saigon. Climb up to the buildings and see the contrasting architecture between the modern and the traditional. We also visit a pagoda which was built inside an old apartment.","Thời gian: 40 phút (xấp xỉ)","Bao gồm vé vào cửa"],
	   ["Quận 4","Xom chieu Market","If you want to learn about the "snack culture" of Saigon, then you definitely have to visit Xom Chieu market (also known as Market 200). The dishes’ taste remains unchanged throughout the year and especially the price does not change either on weekdays or holidays. We stop here for our delicious dessert - Flan Cake","Thời gian: 40 phút (xấp xỉ)"]]',
	N'...',
	N'...',
	N'/tours/5'
);

INSERT INTO Tour(TourName,TourAddress,Overview,Schedule,DepartureLocation,DropOffLocation,PhotoPath) VALUES(
	N'Adventure Cu Chi Tunnels and Mekong Delta limousine tour from HCM',
	N'...',
	N'Explore the Cu Chi tunnels system to know how Vietnamese soldiers won the war. Experience shooting using a real gun. Go to the Mekong delta and move by motorboat and rowing boat. Have a local lunch and enjoy local culture life as honey tea, seasonal fruit, fresh coconut candy and Southern Vietnamese folk music. Pick up and drop off at your accommodation. This tour is suitable for vegetarians as well.',
	N'[["Cu Chi Tunnels","From your hotel, make the scenic drive to the historic city of Cu Chi. Explore the city''s intricate system of tunnels, with the chance to crawl through the narrow spaces and witness the intertwining kitchens, bunkers, hospitals, and meeting rooms throughout. Also, sit back and watch an insightful documentary to learn more about the tunnel system, and its locally crafted traps.","Thời gian: 5 tiếng (xấp xỉ)","Bao gồm vé vào cửa"],
	   ["Mekong Delta","Around midday, take a break to enjoy a traditional Vietnamese lunch before making your way to My Tho, a province of the Mekong Delta region. Upon your arrival, board a boat to cruise up the Mekong. Experience the region of the Delta that is comprised of 4 animal named islands—Dragon, Unicorn, Phoenix, and Turtle. Along the ride, witness the daily lifestyle of riverside locals.","Next, journey into smaller waterways to see the agricultural richness of the area covered with fruit orchards, coconut groves, and bee-keeping farms. Then, experience some of the local flavors as you enjoy some honey tea, seasonal fruits, and coconut candy as you listen to the accompanying Vietnamese folk music. Finally, sit back in your comfortable van for a convenient ride back to your hotel at 6:00 PM.","Thời gian: 5 tiếng (xấp xỉ)"]]',
	N'55 Đỗ Quang Đẩu, Phường Phạm Ngũ Lão, Quận 1, Thành phố Hồ Chí Minh, Việt Nam',
	N'55 Đỗ Quang Đẩu, Phường Phạm Ngũ Lão, Quận 1, Thành phố Hồ Chí Minh, Việt Nam',
	N'/tours/6'
);

INSERT INTO Tour(TourName,TourAddress,Overview,Schedule,DepartureLocation,DropOffLocation,PhotoPath) VALUES(
	N'Cooking Class with Cyclo Experience, Optional Wet Market Tour, Cyclo Resto Restaurant',
	N'...',
	N'Explore Vietnamese cuisine with Chef Vu at Cyclo Resto Restaurant. Learn local practices and cooking techniques, plus enjoy a cyclo tour of Saigon''s urban gems. ',
	N'[["Standard Cooking Class with Cyclo Tour","8:30 am: Begin your day with a hotel pick-up and enjoy a 30-minute cyclo trip. It''s the start of your exploration of the highlights in Ho Chi Minh","10:30 am: Transition to Cyclo Resto Restaurant for an immersive cooking class. Here, you''ll acquire the skills to craft delicious Vietnamese dishes","11:30 am: Revel in your culinary accomplishments. As a special treat, indulge in Vietnamese Egg coffee for dessert","12:00 pm: Conclude this memorable experience"],
	   ["Premium Cooking Class with Cyclo Tour & Wet Market Trip","8:00 am: Pick up at the hotel and enjoy a 30-minute cyclo trip","8:30 am: Arrive at a designated meeting point for a training session before the market trip","8:45 am: Embark on the market trip with a tour guide. It''s the perfect time for you to practice how to pay in Vietnamese Dong, bargain with local vendors, and choose the freshest ingredients","9:15 am: Begin the class. Learn essential skills for creating authentic Vietnamese dishes, including knife techniques, marinating methods, and decoration Prepare delicious dishes from the menu","11:30 am: Savor your culinary achievements, and don''t forget to indulge in a special reward: Vietnamese Egg coffee for dessert","12:00 pm: End of the experience"]]',
	N'57 Le Thi Hong Gam, District 1, Ho Chi Minh City',
	N'Xin lỗi, thông tin này không có sẵn',
	N'/tours/7'
);

INSERT INTO Tour(TourName,TourAddress,Overview,Schedule,DepartureLocation,DropOffLocation,PhotoPath) VALUES(
	N'Saigon Evening Tour With Water Puppet Show And Dinner Cruise',
	N'...',
	N'Experience a 1-hour traditional puppetry art show popular for adults and children alike. Take a 2-hour scenic cruise along the Saigon River in a traditional junk served Vietnamese or Western dinner. Enjoy the Saigon atmosphere by night through the sights, sounds, and smells of this historic metropolis to explore the city.',
	N'[["Golden Dragon Water Puppet Theater","Pick you up from the hotel for your first stop and enjoy the busy street of Saigon in the late evening, getting yourself into the Saigon atmosphere. Next, enjoy the unique art of the water puppet show, a traditional art form closely connected with the long-standing spiritual life of the Vietnamese people. It is performed in a pool of water with the water surface being the stage. A traditional Vietnamese orchestra provides background music accompaniment. Singers with origin in north Vietnam sing songs which tell the story being acted out by the puppets (which are carved out of wood). Although the authentic shows are exclusively in Vietnamese, the tales depict rural life in villages and are easy to understand. The shows typically last for one hour and offer a colorful way to enjoy an ancient tradition.","Thời gian: 50 phút (xấp xỉ)"],
	   ["Saigon River","Then, continue to the harbor for a dinner cruise, and enjoy the city lights and panoramic views of Ho Chi Minh City. Enjoy sumptuous Vietnamese cuisine on this magical dinner cruise aboard a traditional-style Dragon Boat or the Modern Style Boat (depending on the day) The trip ends back at your hotel around 9:30 pm.","Thời gian: 2 tiếng (xấp xỉ)"]]',
	N'Xin lỗi, thông tin này không có sẵn',
	N'Xin lỗi, thông tin này không có sẵn',
	N'/tours/8'
);

INSERT INTO Tour(TourName,TourAddress,Overview,Schedule,DepartureLocation,DropOffLocation,PhotoPath) VALUES(
	N'Aman Spa & Wellness Experience | Ho Chi Minh',
	N'Aman Spa & Wellness - 8/14 Le Thanh Ton, Ben Nghe, District 1, Ho Chi Minh City',
	N'Indulge in the elegant and classy atmosphere design of Aman Spa. Convenient location in the heart of Ho Chi Minh. Professional masseurs and excellent service. Delicate therapeutic massage service, combining the use of medical herbs and infrared massager.',
	N'[["Service hours: 9:00 - 21:00 (daily)","Last Order receiving time on KKday: 19:30"]]',
	N'Aman Spa & Wellness - 8/14 Le Thanh Ton, Ben Nghe, District 1, Ho Chi Minh City',
	N'Aman Spa & Wellness - 8/14 Le Thanh Ton, Ben Nghe, District 1, Ho Chi Minh City',
	N'/tours/9'
);

--INSERT INTO Tour(TourName,TourAddress,Overview,Schedule,DepartureLocation,DropOffLocation,PhotoPath) VALUES(
--	N'',
--	N'...',
--	N'',
--	N'[[]]',
--	N'',
--	N'',
--	N''
--);
			-- CityTour(TourId,CityId)
INSERT INTO CityTour VALUES (1,2);
INSERT INTO CityTour VALUES (2,2);
INSERT INTO CityTour VALUES (3,2);
INSERT INTO CityTour VALUES (4,2);
INSERT INTO CityTour VALUES (5,2);
INSERT INTO CityTour VALUES (6,2);
INSERT INTO CityTour VALUES (7,2); -- Tour ID = 7
INSERT INTO CityTour VALUES (8,2); -- Tour ID = 8
INSERT INTO CityTour VALUES (9,2); -- Tour ID = 9

			-- TourCategory(TourId,CategoryId)
INSERT INTO TourCategory VALUES (1,1);
INSERT INTO TourCategory VALUES (1,2);
INSERT INTO TourCategory VALUES (2,1);
INSERT INTO TourCategory VALUES (3,2);
INSERT INTO TourCategory VALUES (4,3);
INSERT INTO TourCategory VALUES (5,3);
INSERT INTO TourCategory VALUES (6,4);
INSERT INTO TourCategory VALUES (5,4);
INSERT INTO TourCategory VALUES (7,5); -- Tour Id = 7
INSERT INTO TourCategory VALUES (8,5); -- Tour Id = 8
INSERT INTO TourCategory VALUES (8,6); -- Tour Id = 8
INSERT INTO TourCategory VALUES (2,7);
INSERT INTO TourCategory VALUES (3,7);
INSERT INTO TourCategory VALUES (9,8);-- Tour Id = 9
INSERT INTO TourCategory VALUES (6,9);
INSERT INTO TourCategory VALUES (12,9);
			-- Package
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	1,
	N'Regular Group max 25 people',
	N'Shared group: of max 25 people. Hotel pickup in center of District 1 only. Pickup included',
	25
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	1,
	N'Luxury Group max 12 people',
	N'Small-group of max 12 people: Hotel pickup in center of District 1, 3 and 4. Pickup included',
	12
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	1,
	N'VIP Group by Limousine',
	N'VIP Group of max 9 people: with transfer by VIP Limousine. Hotel pickup & drop off in the center of District 1, 3 and 4. Pickup included',
	9
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	2,
	N'Regular Group Tour',
	N'Shared group of max 25 people: Hotel pickup in the center of District 1 only. Pickup included',
	25
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	2,
	N'Non-Touristy Tour',
	N'Small-group of max 12 people: and go off the beaten path to explore the Ben Duoc tunnel of Cu Chi tunnels. Hotel pickup in the center of District 1, 3 & 4. Pickup included',
	12
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	2,
	N'VIP Non-touristy Tour',
	N'VIP Group of max 9 people: with transfer by Limousine including Lunch. Hotel pickup and drop off in the center of District 1, 3 and 4. Pickup included',
	9
);

-- Tour3
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	3,
	N'Pick up/ drop off in Centrally',
	N'Ho Chi Minh City tour full day: 8:30AM - Tour guide and private model car will pick- up at your hotel in Ho Chi Minh city center - Depart the tour Pickup included',
	20
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	3,
	N'Pick / Drop Hiep Phuoc Port',
	N'Pick up/ Drop off Hiep Phuoc : In the moring, Tour guide will pick up at Hiep Phuoc Port - depart to Ho Chi Minh City Centrally, at 15:00 - go back the port Name of Cruise : Please send the Name of your Cruise ! Pickup included',
	20
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	3,
	N'Pick Up/ Drop off: Phu My Port',
	N'Phu My Port - Ho Chi Minh City: In the morning; Tour guide will pick you up at Inside The Port, depart to Ho Chi Minh City, 14:30 - go back to the Port Name of Cruise : Please send the Name of your Cruise ! Pickup included',
	20
);
-- end Tour3
--tour 4
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	4,
	N'SetMenu_Floor 1_Wooden Junk',
	N'The 1st-floor dining room: is the lowest floor Wooden Boutique Junk : has open-air dining rooms, features electric fans & open windows. Capacity: 200 guests. Pickup included',
	200
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	4,
	N'Vege Set Menu_Wooden Junk',
	N'Wooden Boutique Junk: has open-air dining rooms, features electric fans & open windows. Capacity: 200 guests. Pickup included',
	200
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	4,
	N'Buffet_Floor1_Ironclad Cruise',
	N'The 1st-floor dining room: is the lowest floor is air-conditioning and windows to view the Saigon River. Modern Ironclad Cruise: capability 600 guests Pickup included',
	600
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	4,
	N'Buffet_Floor2_Ironclad Cruise',
	N'The 2nd-floor dining room: is the middle floor which has open-air and balconies to view the Saigon River. Modern Ironclad Cruise: capability 600 guests Pickup included',
	600
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	4,
	N'SetMenu_VIPhall_IroncladCruise',
	N'The VIP Dining Hall: located on the top floor which has air-conditioning & balconies to view the Saigon River. Modern Ironclad Cruise: capability 600 guests Pickup included',
	600
);
--end tour 4
-- tour 5
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	5,
	N'Small Group Tour Option',
	N'Duration: 4 hours Full Meal as mentioned Motorbike : Motorbike with Gas included Morning / Afternoon / Evening: This tour can depart in the morning also. Please contact us after booking to choose the best for your tour Pickup included',
	10
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	5,
	N'PRIVATE OPTION',
	N'Motorbike / Scooter Tour Duration: 4 hours Motorbike / Scooter Pickup included',
	0
);
-- end tour 5
-- tour 6
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	6,
	N'Regular Vans pick up',
	N'Vans Pickup included',
	10
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	6,
	N'Limousine Bus upgraded',
	N'Limousine bus Pickup included',
	10
);

-- end tour 6
-- tour 7
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	7,
	N'Cooking Class with Cyclo Experience',
	N'',
	10
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	7,
	N'Cooking Class with Cyclo Experience and Wet Market Tour',
	N'',
	10
);
-- end tour 7
-- tour 8

INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	8,
	N'Saigon Evening Tour With Water Puppet Show And Dinner Cruise',
	N'Pickup included',
	50
);

-- end tour 8
-- tour 9

INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	9,
	N'Hairwash | 60 Mins',
	N'',
	0
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	9,
	N'Hairwash | 75 Mins',
	N'',
	0
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	9,
	N'Relaxing Body Massage | Hot Stone Shiatsu Massage | 60 Mins',
	N'',
	0
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	9,
	N'Relaxing Body Massage | Hot Stone Shiatsu Massage | 75 Mins',
	N'',
	0
);
INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
	9,
	N'SIGNATURE | Therapeutic Full Body Massage | 75 Mins',
	N'',
	0
);
--INSERT INTO Package(TourID,PackageName,Description,MaxPeople) VALUES(
--	0,
--	N'',
--	N'',
--	0
--);

-- end tour 9
			-- PackagePrice
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	1,
	439077,
	502913
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	2,
	790777,
	878155
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	3,
	1185922,
	1295631
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	4,
	373301,
	393204
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	5,
	636893,
	702670
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	6,
	1185922,
	1295631
);
--
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	7,
	844660,
	2914078
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	8,
	2069418,
	6166262
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	9,
	2532953,
	7074029
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	10,
	493566,
	1192280
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	11,
	493566,
	1192280
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	12,
	493566,
	1192280
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	13,
	892206,
	1274581
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	14,
	1102015,
	1377519
);

INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	15,
	0,
	549162
);

INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	16,
	0,
	637048
);

INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	17,
	648895,
	811119
);

INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	18,
	1081427,
	1351784
);

INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	19,
	0,
	625637
);

INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	20,
	0,
	782229
);

INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	21,
	943923,
	1232095
);

--

INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	22,
	0,
	262928
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	23,
	0,
	356883
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	24,
	0,
	459820
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	25,
	0,
	516387
);
INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
	26,
	0,
	610100
);
--INSERT INTO PackagePrice(PackageId,ChildPrice,AdultPrice) VALUES(
--	0,
--	0,
--	0
--);