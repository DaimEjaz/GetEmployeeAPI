/****** Script for SelectTopNRows command from SSMS  ******/
SELECT * From(
	SELECT ROW_NUMBER() OVER (ORDER BY Id) AS row_num
			,StudentName
			, Address
			,AdmissionYear
	FROM Students
) AS sub
WHERE row_num = 11

UPDATE Students
SET StudentName = 'Johnson',
	Address = 'Lhr'
WHERE Id = 13;


DELETE FROM Students WHERE Id = 11;

SELECT * FROM Students;

INSERT INTO Students(StudentName, Address, AdmissionYear)
VALUES('test', 'test', 2022)

DBCC CHECKIDENT ('Students', RESEED, 10);


