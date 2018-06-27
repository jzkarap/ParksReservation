-- Clean database
DELETE FROM reservation;
DELETE FROM site;
DELETE from campground;
DELETE from park;

-- Insert parks
SET IDENTITY_INSERT park ON;
INSERT INTO park (park_id, name, location, establish_date, area, visitors, description) VALUES (1, 'Cuyahoga Valley', 'Ohio', '2000-10-11', 32860, 2189849, 'This park along the Cuyahoga River has waterfalls, hills, trails, and exhibits on early rural living. The Ohio and Erie Canal Towpath Trail follows the Ohio and Erie Canal, where mules towed canal boats. The park has numerous historic homes, bridges, and structures, and also offers a scenic train ride.');
INSERT INTO park (park_id, name, location, establish_date, area, visitors, description) VALUES (2, 'Everglades', 'Florida', '1934-05-30', 1508934, 500000, 'The Everglades are the largest tropical wilderness in the United States. This mangrove and tropical rainforest ecosystem and marine estuary is home to 36 protected species, including the Florida panther, American crocodile, and West Indian manatee. Some areas have been drained and developed; restoration projects aim to restore the ecology.');
INSERT INTO park (park_id, name, location, establish_date, area, visitors, description) VALUES (3, 'Great Sand Dunes', 'Colorado', '2004-09-13', 107341, 3000000, 'The tallest sand dunes in North America, up to 750 feet (230 m) tall, were formed by deposits of the ancient Rio Grande in the San Luis Valley. Abutting a variety of grasslands, shrublands, and wetlands, the park also has alpine lakes, six 13,000-foot mountains, and old-growth forests.');
SET IDENTITY_INSERT park OFF;

-- Insert campgrounds
SET IDENTITY_INSERT campground ON;
INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (1, 2, 'Flamingo Campground', 1, 12, 100.00);
INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (2, 1, 'Blackwoods', 1, 12, 35.00);
INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (3, 1, 'Seawall', 5, 9, 30.00);
INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (4, 3, 'Piñon Flats Campground', 5, 9, 45.00);
INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (5, 3, 'Great Sand Dunes Oasis', 5, 10, 120.00);
INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (6, 3, 'Zapata Falls Campground', 4, 9, 35.00);
SET IDENTITY_INSERT campground OFF;

-- Insert sites
SET IDENTITY_INSERT site ON;
INSERT INTO site (site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES (1, 1, 1, 24, 1, 20, 1);
INSERT INTO site (site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES (2, 2, 1, 6, 0, 0, 0);
INSERT INTO site (site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES (3, 2, 2, 6, 1, 0, 1);
INSERT INTO site (site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES (4, 2, 3, 6, 0, 20, 0);
INSERT INTO site (site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES (5, 3, 1, 6, 1, 0, 1);
INSERT INTO site (site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES (6, 4, 1, 24, 1, 20, 1);
INSERT INTO site (site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES (7, 5, 1, 6, 0, 20, 0);
INSERT INTO site (site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES (8, 6, 1, 6, 0, 0, 1);
SET IDENTITY_INSERT site OFF;

-- Insert reservations
SET IDENTITY_INSERT reservation ON;
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date, create_date) VALUES (1, 1, 'Stark Family Reservation', '2018-07-22', '2018-07-26', '2018-06-26');
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date, create_date) VALUES (2, 2, 'Targaryen Family Reservation', '2018-08-11', '2018-08-14', '2018-06-27');
SET IDENTITY_INSERT reservation OFF;