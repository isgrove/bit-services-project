USE BitServicesDatabase;
GO

SET DATEFORMAT dmy;

INSERT INTO job_status(status) VALUES
('Pending'),
('In Progress'),
('Completed'),
('Verified'),
('Canceled');

INSERT INTO staff_type(type) VALUES
('Coordinator'),
('Admin');

INSERT INTO skill(skill_name) VALUES
('C# Programmer'),
('Database Administrator'),
('Java Programmer'),
('Front End Developer'),
('SQL Programmer'),
('Systems Architect'),
('HTML/CSS Developer'),
('ASP.Net Developer'),
('Entity Framework Developer'),
('SQL Server Administrator'),
('C++ Programmer'),
('C Programmer'),
('Network Administrator'),
('Windows Server Administrator');

INSERT INTO staff([type], first_name, last_name, email, phone, [password], active) VALUES
('Coordinator', 'Darla', 'Proctor', 'darla.proctor@bitservices.com', '0455573663', 'helloworld', 1),
('Coordinator', 'Douglas',' Finley', 'douglas.finley@bitservices.com', '0455511305', 'helloworld', 1),
('Coordinator', 'Ana', 'Stevenson', 'ana.stevenson@bitservices.com', '0455596376', 'helloworld', 1),
('Admin', 'Duane', 'Small', 'duane.small@bitservices.com', '0455590520', 'helloworld', 1),
('Admin', 'Kristi', 'Calhoun', 'kristi.calhoun@bitservices.com', '0455559670', 'helloworld', 1),
('Admin', 'Marian', 'Villanueva', 'marian.villanueva@bitservices.com', '0455519613', 'helloworld', 1);

INSERT INTO contractor(first_name, last_name, email, phone, password, street, suburb, postcode, state, licence_number, vehicle_registration, performance_rating, active) VALUES
('Bessie', 'Camacho', 'bessie.camacho@bitservices.com', '0455520384', 'helloworld', '80 Druitt St', 'Sydney', '2000', 'NSW', '9387212549', 'CB33GO', 8, 1),	
('Valarie', 'Palmer', 'valarie.palmer@bitservices.com', '0455546672', 'helloworld', '6/155 King St', 'Sydney', '2000', 'NSW', '7899293455', 'PSY845', 7, 1),	
('Barrett', 'Snyder', 'barrett.snyder@bitservices.com', '0455578787', 'helloworld', '397 George St', 'Sydney', '2000', 'NSW', '0062351808', 'AA56QH', 8, 1),
('Sonja', 'Barber', 'sonja.barber@bitservices.com', '0455576095', 'helloworld','175 Pitt St', 'Sydney', '2000', 'NSW', '4866479511', 'MRJ163', 5, 1),
('Barney', 'Murillo', 'barney.murillo@bitservices.com', '0455579643', 'helloworld', '40/100 Miller St', 'North Sydney', '2059', 'NSW', '7515817779', 'BNA234', 4, 1),
('Franklin', 'Atkinson','franklin.atkinson@bitservices.com', '0455507305', 'helloworld', '2/17 Federation Rd', 'Newtown', '2045', 'NSW', '3130318978', 'BN08GD', 8 ,1);

INSERT INTO client(client_name, email, phone, password, active) VALUES
('Target', 'contact@target.com.au', '0282171600', 'helloworld', 1),
('Woolworths', 'woolworths.com.au', '0293087367', 'helloworld', 1),
('JB Hi-Fi', 'contact@jbhifi.com', '0289186700', 'helloworld', 1);

INSERT INTO client_location(client_id, email, phone, street, suburb, postcode, state, active) VALUES
(1, 'broadway@target.com.au', '0282171600', 'Cnr. Broadway &, Bay St', 'Glebe', '2037', 'NSW', 1),
(1, 'chatswood@target.com.au', '0284405300', 'Spring St', 'Chatswood', '2067', 'NSW', 1),
(1, 'hornsby@target.com.au', '0294727800', 'George St', 'Hornsby', '2077', 'NSW', 1),
(2, 'neutralbay@woolworths.com.au', '0293087367', '1-7 Rangers Rd', 'Neutral Bay', '2089', 'NSW', 1),
(2, 'crowsnest@woolworths.com.au', '0285659336', '10 Falcon St', 'Crows Nest', '2065', 'NSW', 1),
(2, 'northsydney@woolworths.com.au', '0293051097', '100 Miller St', 'North Sydney', '2060', 'NSW', 1),
(3, 'northsydney@jbhifi.com', '0289186700', '99 Mount St', 'North Sydney', '2060', 'NSW', 1),
(3, 'artarmon@jbhifi.com', '0284237900', '1 Frederick St', 'Artarmon', '2064', 'NSW', 1),
(3, 'hornsby@jbhifi.com', '0294802400', 'Level 3/236 Pacific Hwy', 'Hornsby', '2077', 'NSW', 1);

INSERT INTO assignment_reason(reason) VALUES
('Schedule Conflict'),
('Unwell'),
('Personal Reasons'),
('Other'),
('Accepted')