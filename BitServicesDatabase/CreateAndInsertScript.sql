-- TOOD:
--		- Add inserts for jobs, contractor_availability, contractor_skills

USE MASTER
GO 
IF EXISTS( SELECT name
		   FROM master..sysdatabases
		   WHERE name = N'BitServices_Sam'
		 )
DROP DATABASE BitServices_Sam;
GO

CREATE DATABASE BitServices_Sam;
GO

USE BitServices_Sam;
GO


CREATE TABLE assignment_reason (
                reason VARCHAR(32) NOT NULL,
                CONSTRAINT assignment_reason_pk PRIMARY KEY (reason)
)

CREATE TABLE staff_type (
                type VARCHAR(32) NOT NULL,
                CONSTRAINT staff_type_pk PRIMARY KEY (type)
)

CREATE TABLE client (
                client_id INT IDENTITY(1,1) NOT NULL,
                client_name VARCHAR(32) NOT NULL,
                email VARCHAR(254) NOT NULL,
                phone VARCHAR(10) NOT NULL,
                password VARCHAR(32) NOT NULL,
                active BIT NOT NULL,
                CONSTRAINT client_pk PRIMARY KEY (client_id)
)

CREATE TABLE client_location (
                location_id INT IDENTITY(1,1) NOT NULL,
                client_id INT NOT NULL,
                email VARCHAR(254) NOT NULL,
                phone VARCHAR(10) NOT NULL,
                street VARCHAR(32) NOT NULL,
                suburb VARCHAR(32) NOT NULL,
                postcode VARCHAR(4) NOT NULL,
                state VARCHAR(3) NOT NULL,
                active BIT NOT NULL,
                CONSTRAINT client_location_pk PRIMARY KEY (location_id)
)

CREATE TABLE job_status (
                status VARCHAR(32) NOT NULL,
                CONSTRAINT job_status_pk PRIMARY KEY (status)
)

CREATE TABLE skill (
                skill_name VARCHAR(32) NOT NULL,
                CONSTRAINT skill_pk PRIMARY KEY (skill_name)
)

CREATE TABLE job (
                job_id INT IDENTITY(1,1) NOT NULL,
                location_id INT NOT NULL,
                required_skill VARCHAR(32) NOT NULL,
                job_status VARCHAR(32) NOT NULL,
                kilometers INT NOT NULL,
                title VARCHAR(100) NOT NULL,
                description VARCHAR(1000),
                deadline_date DATE NOT NULL,
                CONSTRAINT job_pk PRIMARY KEY (job_id)
)

CREATE TABLE contractor (
                contractor_id INT IDENTITY(1,1) NOT NULL,
                first_name VARCHAR(32) NOT NULL,
                last_name VARCHAR(32) NOT NULL,
                email VARCHAR(254) NOT NULL,
                phone VARCHAR(10) NOT NULL,
                password VARCHAR(32) NOT NULL,
                street VARCHAR(32) NOT NULL,
                suburb VARCHAR(32) NOT NULL,
                postcode VARCHAR(4) NOT NULL,
                state VARCHAR(3) NOT NULL,
                licence_number VARCHAR(20) NOT NULL,
                vehicle_registration VARCHAR(6) NOT NULL,
                performance_rating VARCHAR NOT NULL,
                active BIT NOT NULL,
                CONSTRAINT contractor_pk PRIMARY KEY (contractor_id)
)

CREATE TABLE contractor_skill (
                contractor_id INT NOT NULL,
                skill_name VARCHAR(32) NOT NULL,
                CONSTRAINT contractor_skill_pk PRIMARY KEY (contractor_id, skill_name)
)

CREATE TABLE contractor_availability (
                availability_date DATE NOT NULL,
                contractor_id INT NOT NULL,
                CONSTRAINT contractor_availability_pk PRIMARY KEY (availability_date, contractor_id)
)

CREATE TABLE staff (
                staff_id INT IDENTITY(1,1) NOT NULL,
                type VARCHAR(32) NOT NULL,
                first_name VARCHAR(32) NOT NULL,
                last_name VARCHAR(32) NOT NULL,
                email VARCHAR(254) NOT NULL,
                phone VARCHAR(10) NOT NULL,
                password VARCHAR(32) NOT NULL,
                active BIT NOT NULL,
                CONSTRAINT staff_pk PRIMARY KEY (staff_id)
)

CREATE TABLE job_assignment (
                contractor_id INT NOT NULL,
                job_id INT NOT NULL,
                staff_id INT NOT NULL,
                accepted BIT,
                availability_date DATE,
                reason VARCHAR(32),
                CONSTRAINT job_assignment_pk PRIMARY KEY (contractor_id, job_id)
)

ALTER TABLE job_assignment ADD CONSTRAINT assignment_reason_job_assignment_fk
FOREIGN KEY (reason)
REFERENCES assignment_reason (reason)

ALTER TABLE staff ADD CONSTRAINT staff_type_staff_fk
FOREIGN KEY (type)
REFERENCES staff_type (type)

ALTER TABLE client_location ADD CONSTRAINT client_client_location_fk
FOREIGN KEY (client_id)
REFERENCES client (client_id)

ALTER TABLE job ADD CONSTRAINT client_location_job_fk
FOREIGN KEY (location_id)
REFERENCES client_location (location_id)

ALTER TABLE job ADD CONSTRAINT job_status_job_fk
FOREIGN KEY (job_status)
REFERENCES job_status (status)

ALTER TABLE contractor_skill ADD CONSTRAINT skill_contractor_skill_fk
FOREIGN KEY (skill_name)
REFERENCES skill (skill_name)

ALTER TABLE job ADD CONSTRAINT skill_job_fk
FOREIGN KEY (required_skill)
REFERENCES skill (skill_name)

ALTER TABLE job_assignment ADD CONSTRAINT job_contractor_assignment_fk
FOREIGN KEY (job_id)
REFERENCES job (job_id)

ALTER TABLE contractor_availability ADD CONSTRAINT contractor_contractor_availability_fk
FOREIGN KEY (contractor_id)
REFERENCES contractor (contractor_id)

ALTER TABLE contractor_skill ADD CONSTRAINT contractor_contractor_skill_fk
FOREIGN KEY (contractor_id)
REFERENCES contractor (contractor_id)

ALTER TABLE job_assignment ADD CONSTRAINT contractor_contractor_assignment_fk
FOREIGN KEY (contractor_id)
REFERENCES contractor (contractor_id)

ALTER TABLE job_assignment ADD CONSTRAINT contractor_availability_job_assignment_fk
FOREIGN KEY (availability_date, contractor_id)
REFERENCES contractor_availability (availability_date, contractor_id)

ALTER TABLE job_assignment ADD CONSTRAINT staff_contractor_assignment_fk
FOREIGN KEY (staff_id)
REFERENCES staff (staff_id)

USE BitServices_Sam;
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

CREATE TYPE tbl_skill_name AS TABLE
(
	skill_name VARCHAR(32)
	PRIMARY KEY (skill_name)
)
GO

CREATE TYPE tbl_job_status AS TABLE
(
	job_status VARCHAR(32)
	PRIMARY KEY (job_status)
)
GO

CREATE TYPE tbl_skill AS TABLE
(
	skill_name VARCHAR(32)
	PRIMARY KEY (skill_name)
)
GO