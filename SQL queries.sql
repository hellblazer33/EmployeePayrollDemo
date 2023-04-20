create table employeepayroll
(
	EmployeeId int IDENTITY(1,1) PRIMARY KEY,
	EmployeeName varchar(255) not null,
    Salary int,   
	DA int,
	HRA int,
	Bonus int
	
);


GO



CREATE PROCEDURE AddEmployee(
	@employeeName varchar(255),
    @salary int,   
	@da int,
	@hra int,
	@bonus int
	 
) AS
BEGIN TRY
	INSERT INTO employeepayroll(EmployeeName,Salary,DA,HRA,Bonus)
	VALUES (@employeeName,@salary,@da,@hra,@bonus);
END TRY
BEGIN CATCH
  SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_SEVERITY() AS ErrorSeverity,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH;
GO




CREATE PROCEDURE DeleteEmployee(
	@employeeId int
) AS
BEGIN TRY
	DELETE FROM employeepayroll WHERE EmployeeId = @employeeId;
END TRY
BEGIN CATCH
  SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_SEVERITY() AS ErrorSeverity,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH;
GO



CREATE PROCEDURE UpdateEmployee(
	@employeeName varchar(255),
    @salary int,   
	@da int,
	@hra int,
	@bonus int,
    @employeeId int
	 
)  AS
BEGIN TRY
   Update employeepayroll set EmployeeName = @employeeName, 
					Salary = @salary, 
					DA = @da, 
					HRA= @hra,
					Bonus = @bonus
					
				where 
					EmployeeId = @employeeId;
END TRY
BEGIN CATCH
  SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_SEVERITY() AS ErrorSeverity,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH;


GO


CREATE PROCEDURE GetemployeeByemployeeId(
   @employeeId int
) AS
BEGIN TRY
	SELECT * FROM employeepayroll WHERE EmployeeId = @employeeId;
END TRY
BEGIN CATCH
  SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_SEVERITY() AS ErrorSeverity,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH;