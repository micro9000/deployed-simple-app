CREATE DATABASE GoalThreeDb;
USE GoalThreeDb;

CREATE TABLE Cities (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	CityName VARCHAR(255),
	Country VARCHAR(255),
	IsActive BIT
)

CREATE TABLE Schools (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	SchoolName VARCHAR(255),
	City VARCHAR(255), -- City Id or just City name?
	IsActive BIT
)

CREATE TABLE Persons (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	FirstName VARCHAR(255),
	LastName VARCHAR(255),
	EmailAddress VARCHAR(255),
	CityId INT NOT NULL,
	SchoolId INT NOT NULL,
	IsActive BIT,
	FOREIGN KEY (CityId) REFERENCES Cities (Id),
	FOREIGN KEY (SchoolId) REFERENCES Schools (Id)
)

MERGE [Cities] AS Target
USING ( values 
	('Tarlac City', 'Philippines', 1)
	, ('Manila', 'Philippines', 1)
	, ('Beijing', 'China', 1)
	, ('Shanghai', 'China', 1)
) AS seed (CityName, Country, IsActive)
ON target.CityName=seed.CityName AND target.Country = seed.Country
WHEN NOT MATCHED THEN
INSERT (CityName, Country, IsActive)
VALUES (seed.CityName, seed.Country, seed.IsActive);

select * from Cities;

MERGE [Schools] AS Target
USING ( values 
	('Tarlac National High School', 'Tarlac City', 1)
	, ('Manila High School', 'Manila', 1)
	, ('Somewhere National High School', 'Shanghai', 1)
) AS seed (SchoolName, City, IsActive)
ON target.SchoolName = seed.SchoolName AND target.City = Seed.City
WHEN NOT MATCHED THEN
INSERT (SchoolName, City, IsActive)
VALUES (seed.SchoolName, seed.City, seed.IsActive);

select * from Schools

MERGE [Persons] AS Target
USING ( values 
	('Raniel', 'Garcia', 'ranielgarcia@gmail.com', 
		(SELECT TOP 1 Id FROM Cities WHERE CityName='Tarlac City' and Country='Philippines' AND isActive=1), 
		(SELECT TOP 1 Id FROM Schools WHERE SchoolName = 'Tarlac National High School'), 1)
	,('Ronel', 'Garcia', 'ronelgarcia@gmail.com', 
		(SELECT TOP 1 Id FROM Cities WHERE CityName='Manila' and Country='Philippines' AND isActive=1), 
		(SELECT TOP 1 Id FROM Schools WHERE SchoolName = 'Manila High School'), 1)
	,('Alice', 'Guo', 'aliceguo@gmail.com', 
		(SELECT TOP 1 Id FROM Cities WHERE CityName='Beijing' and Country='China' AND isActive=1), 
		(SELECT TOP 1 Id FROM Schools WHERE SchoolName = 'Somewhere National High School'), 1)
) AS seed (FirstName, LastName, Email, CityId, SchoolId, IsActive)
ON target.EmailAddress = seed.Email
WHEN NOT MATCHED THEN
INSERT (FirstName, LastName, EmailAddress, CityId, SchoolId, IsActive)
VALUES (seed.FirstName, seed.LastName, seed.Email, seed.CityId, seed.SchoolId, seed.IsActive);

SELECT * FROM Persons;


----- Stored Proc

CREATE OR ALTER PROC [dbo].[SearchPerson]
(
	@fname VARCHAR(255),
	@lname VARCHAR(255),
	@schoolName VARCHAR(255)
)
AS 
BEGIN
	SELECT P.FirstName, P.LastName, C.CityName, S.SchoolName
	FROM Persons P
	JOIN Cities C ON C.Id=P.CityId
	JOIN Schools S ON S.Id=P.SchoolId
	WHERE P.FirstName LIKE '%' + @fname + '%'
		AND P.LastName LIKE '%' + @lname + '%'
		AND S.SchoolName LIKE '%' + @schoolName + '%'
		AND S.City = C.CityName AND C.Country='Philippines'
		AND S.IsActive=1 AND P.IsActive=1 AND C.IsActive=1
	ORDER BY P.LastName, P.FirstName
END

EXEC SearchPerson 'raniel', 'garcia', 'tarlac'