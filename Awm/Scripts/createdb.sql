
# SQL Script for creating tables for AWM

# Creating table Aircraft
CREATE TABLE `awm`.`Aircraft` (
  `aircraftId` INT NOT NULL,
  `registrationNumber` VARCHAR(150) NULL,
  `name` VARCHAR(50) NULL,
  `serialNumber` VARCHAR(150) NULL,
  `engine` VARCHAR(100) NULL,
  `lastServiceDate` DATE NULL,
  PRIMARY KEY (`aircraftId`));

# Creating table AircraftImage
CREATE TABLE `awm`.`AircraftImage` (
  `imageID` INT NOT NULL AUTO_INCREMENT,
`aircraftId` INT NOT NULL,
  `dateTime` DATETIME NULL,
  `comment` LONGTEXT NULL,
  `s3Path` VARCHAR(250) NULL,
PRIMARY KEY (`imageID`),
FOREIGN KEY (aircraftId) REFERENCES Aircraft(aircraftId));

# Creating table Flight
CREATE TABLE `awm`.`Flight` (
  `flightId` INT NOT NULL,
`aircraftId` INT NOT NULL,
  `date` DATETIME NULL,
  `hours` VARCHAR(150) NULL,
  PRIMARY KEY (`flightId`),
FOREIGN KEY (aircraftId) REFERENCES Aircraft(aircraftId));

# Creating table ServiceTimer
CREATE TABLE `awm`.`ServiceTimer` (
  `serviceTimerId` INT NOT NULL,
`aircraftId` INT NOT NULL,
  `nextServiceDate` DATE NULL,
  `status` TINYINT DEFAULT 0,
  PRIMARY KEY (`serviceTimerId`),
FOREIGN KEY (aircraftId) REFERENCES Aircraft(aircraftId));

# Creating table Client
CREATE TABLE `awm`.`Client` (
  `clientId` INT NOT NULL,
  `address` VARCHAR(200) NULL,
  `contactNmuber` VARCHAR(50) NULL,
  PRIMARY KEY (`clientId`));

# Creating table ClientNotes
CREATE TABLE `awm`.`ClientNotes` (
  `clientNotesId` INT NOT NULL,
`aircraftId` INT NOT NULL,
  `clientId` INT NOT NULL,
  `description` TEXT,
  `date` DATE NULL,
  PRIMARY KEY (`clientNotesId`),
FOREIGN KEY (clientId) REFERENCES Client(clientId),
FOREIGN KEY (aircraftId) REFERENCES Aircraft(aircraftId));

# Creating table Service
CREATE TABLE `awm`.`Service` (
  `serviceId` INT NOT NULL,
`aircraftId` INT NOT NULL,
  `date` DATE NULL,
  `name` VARCHAR(50) NULL,
  `description` TEXT,
  `clientQuotesHrs` INT NULL DEFAULT 0,
  PRIMARY KEY (`serviceId`),
FOREIGN KEY (aircraftId) REFERENCES Aircraft(aircraftId));

# Creating table UserAccount
CREATE TABLE `awm`.`UserAccount` (
  `emailAddressId` VARCHAR(100) NOT NULL,
`aircraftId` INT NOT NULL,
`clientId` INT NOT NULL,
  `password` VARCHAR(100) NULL,
  `firstName` VARCHAR(100) NULL,
  `lastName` VARCHAR(100) NULL,
  `type` VARCHAR(50) NULL,
  PRIMARY KEY (`emailAddressId`),
FOREIGN KEY (aircraftId) REFERENCES Aircraft(aircraftId),
FOREIGN KEY (clientId) REFERENCES Client(clientId));

# Creating table Shift
CREATE TABLE `awm`.`Shift` (
  `shiftId` INT NOT NULL,
`emailAddressId` VARCHAR(100) NOT NULL,
  `date` DATE NULL,
  `startTime` TIME NULL,
  `endTime` TIME NULL,
  `durationHours` INT NULL,
 PRIMARY KEY (`shiftId`),
FOREIGN KEY (emailAddressId) REFERENCES UserAccount(emailAddressId));

# Creating table Job
CREATE TABLE `awm`.`Job` (
  `jobId` INT NOT NULL,
`emailAddressId` VARCHAR(100) NOT NULL,
  `shiftId` INT NOT NULL,
`serviceId` INT NOT NULL,
  `jobDescription` TEXT NULL,
  `allocatedHours` INT NULL,
  `cumulativeHours` INT NULL,
`status` TINYINT DEFAULT 0,
  PRIMARY KEY (`jobId`),
FOREIGN KEY (emailAddressId) REFERENCES UserAccount(emailAddressId),
FOREIGN KEY (`serviceId`) REFERENCES Service (`serviceId`),
FOREIGN KEY (`shiftId`) REFERENCES Shift (`shiftId`));

# Creating table Timesheet
CREATE TABLE `awm`.`Timesheet` (
  `timesheetId` INT NOT NULL,
  `aircraftId` INT NULL,
  `shiftId` INT NULL,
  `jobId` INT NULL,
  `date` DATE NULL,
  `startTime` TIME NULL,
  `endTime` TIME NULL,
  `actualHours` VARCHAR(45) NULL,
  PRIMARY KEY (`timesheetId`),
FOREIGN KEY (`aircraftId`) REFERENCES Aircraft (`aircraftId`),
FOREIGN KEY (`jobId`) REFERENCES Job (`jobId`),
FOREIGN KEY (`shiftId`) REFERENCES Shift (`shiftId`));

# Creating table SparePart
CREATE TABLE `awm`.`SparePart` (
  `partId` INT NOT NULL,
  `jobId` INT NOT NULL,
  `intakeDate` DATE NULL,
  `bestBeforeDate` DATE NULL,
  `gnr` VARCHAR(150) NULL,
  PRIMARY KEY (`partId`),
FOREIGN KEY (jobId) REFERENCES Job (jobId));

# Creating table Material
CREATE TABLE `awm`.`Material` (
  `materialId` INT NOT NULL,
  `jobId` INT NOT NULL,
  `intakeDate` DATE NULL,
  `bestBeforeDate` DATE NULL,
  `gnr` VARCHAR(150) NULL,
  PRIMARY KEY (`materialId`),
FOREIGN KEY (jobId) REFERENCES Job (jobId));


# SQL Script for data insertion into the tables

-- Insert data into table Aircraft
insert into awm.Aircraft values 
(1, 'N904DE', 'Boxkite', '123456, 3456, F4U-1, Sea Dart', 'B-707', '2019-6-21'),
(2, 'H904WE', 'Blériot XI', '51-11012, 51-1012, 1-1012, 1012', 'R-207', '2019-3-21'),
(3, 'U304DR', 'Boxkite XV', '20+63 (aircraft)‎ (3 C)', 'B-807', '2019-3-1'),
(4, 'D309PE', 'CA-25 Winjeel', '20+08 (aircraft)‎ (2 P, 2 F)', 'A-207', '2019-1-21'),
(5, 'G904TY', 'DH.1a', '30+00 (aircraft)‎ (2 F)', 'U-127', '2017-3-11');

-- Insert data into table AircraftImage
INSERT INTO `awm`.`AircraftImage` VALUES 
('1', '1', '2017-4-5, 2:20:22', 'rear', 'asdf.sdfd@abc.com'),
('2', '2', '2017-3-5, 3:20:22', 'front', 'https://unsplash.com/photos/n2NBgIx3A28'),
('3', '3', '2018-6-22, 6:20:12', 'rear', 'https://unsplash.com/photos/n2NBgIx3A28'),
('4', '4', '2019-1-5, 8:10:22', 'front', 'sdasdf.sdfd@abc.com'),
('5', '5', '2020-8-5, 1:20:22', 'rear', 'qweasdf.sdfd@abc.com');

-- Insert data into table Flight
insert into awm.Flight values 
(1,1,'2018-3-15,2:20:22', 12),
(2,2,'2016-1-5,3:20:22', 10),
 (3,3,'2019-6-15,6:20:12', 14),
 (4,4,'2017-7-25,8:10:22', 18),
 (5,5,'2020-2-17,1:20:22', 17);
 
 -- Insert data into table ServiceTimer
insert into awm.ServiceTimer values
 (1,1,'2020-12-22', 0),
 (2,2,'2020-9-30', 1),
 (3,3,'2020-11-22', 0),
 (4,4,'2020-10-05', 0),
 (5,5,'2020-11-12', 1);
 
-- Insert data into table ClientNotes
insert into awm.ClientNotes values
(1,1,1, 'check the landing gear', '2020-12-22'),
 (2,2,2, 'some noise from the rear', '2020-10-05'),
 (3,3,3, 'needs new paint work', '2020-07-15'),
(4,4,4, 'must be delivered in 2 days', '2020-09-17'),
 (5,5,5, 'check the leaks', '2020-12-22'); 

-- Insert data into table Service
insert into awm.Service values
 (1,1,'2020-12-28','Boxkite','problem in landing gear', 22),
 (2,2,'2020-10-18','DH.1a','need repair in hydraulic systems', 15),
 (3,3,'2020-11-12','Boxkite XV','cracks in some part', 10),
 (4,4,'2020-09-10','Blériot XI','leaks in some part', 17),
 (5,5,'2020-11-2','CA-25 Winjeel','need paint in aircraft', 19);
 
 -- Insert data into table Client
insert into awm.Client values 
(1, '29 kinarra ave wyoming 2250', '0412566378'),
 (2, '16 beane street gosford 2250', '0412564378'),
 (3, '4 hills street carlton 2340', '0415566378'),
 (4, '26 kinarra ave rockdale 2234', '0432566378'),
 (5, '27 kinarra ave inglebun 3067', '0412565678');

--  Insert data into table UserAccount
insert into awm.UserAccount values
 ('Narayan.Pudasaini@uon.edu.au', 1, 1,'1234', 'Narayan', 'Pudasaini', 'teamlead'),
 ('Tongzhe.Xu@uon.edu.au', 2, 2, '4567', 'Tongzhe', 'Xu', 'worker'),
('Anton.Polkanov@uon.edu.au ', 3, 3, 'ASDE', 'Anton', 'Polkanov', 'client' ),
 ('Ajaya.Jangam@uon.edu.au', 4, 4, 'NHGF', 'Ajaya', 'Jangam', 'client'),
 ('jason.phua@uon.edu.au', 5, 5,'SDFR', 'Jason', 'Phua', 'manager');
 
  -- Insert data into table shift
insert into awm.Shift values
(1, 'Narayan.Pudasaini@uon.edu.au','2020-9-13', '6:2:11', '2:03:11', 8),
(2, 'Tongzhe.Xu@uon.edu.au',  '2020-7-15', '10:00:14', '6:01:45', 8),
(3, 'Anton.Polkanov@uon.edu.au ','2020-3-16', '8:00:14', '2:01:23', 6),
(4, 'Ajaya.Jangam@uon.edu.au','2020-8-24', '7:00:14', '5:01:45', 11),
(5, 'jason.phua@uon.edu.au','2020-7-12', '10:00:14', '6:01:45', 8);

-- Insert data into table Job
insert into awm.Job values 
(1, 'Narayan.Pudasaini@uon.edu.au', 1, 1, 'Aircraft and Avionics Mechanic', 5, 5.5,0),
 (2, 'Tongzhe.Xu@uon.edu.au', 2, 2, 'Airport Manager', 8, 9.2,1),
(3, 'Anton.Polkanov@uon.edu.au ', 3, 3, 'Transportation Security Screener', 6, 6.5,0),
 (4,'Ajaya.Jangam@uon.edu.au', 4, 4, 'Airfield Operations Specialist', 7, 8,1),
 (5, 'jason.phua@uon.edu.au', 5, 5, 'Aeronautical Engineer', 4, 5.5,1);

-- Insert data into table Timesheet
insert into awm.Timesheet values
(001, 1, 1, 1,'2020-9-13', '6:2:11', '2:03:11', 8),
(002, 2, 2, 2, '2020-7-15', '10:00:14', '6:01:45', 8),
(003, 3, 3, 3, '2020-3-16', '8:00:14', '2:01:23', 6),
(004, 4, 4, 4, '2020-8-24', '7:00:14', '5:01:45', 11),
(005, 5, 5, 5, '2020-7-12', '10:00:14', '6:01:45', 8);

-- Insert data into table SparePart
insert into awm.SparePart values
(1,1, '2017-5-13', '2020-12-20', 'TR207'),
 (2,2, '2018-3-15', '2021-2-12', '56Y45'),
 (3,3, 2019-5-16, 2022-1-22, 'AS5674'),
 (4,4, 2016-2-12, 2020-11-01, '89YT'),
 (5,5, 2013-02-13, 2020-10-12, '45NM34');
 
 -- Insert data into table material
insert into awm.Material values
(1, 1,'2017-5-13', '2020-12-20', 'XR2020' ),
(2,2, '2018-3-15', '2021-2-12', '66I6I'),
(3, 3,'2019-5-16', '2022-1-22', 'I9I44'),
(4,4, '2016-2-12', '2020-11-01', '2020TR'),
(5,5, '2013-02-13', '2020-10-12', 'XM607');



