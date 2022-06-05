-- =============================================
-- Author:		Sam Isgrove
-- Create date: 03/06/2022
-- Description:	Inserts a new staff memeber to the database with the given values
-- =============================================
CREATE PROCEDURE usp_InsertStaff 
	-- Add the parameters for the stored procedure here
	@FirstName VARCHAR(32),
	@LastName VARCHAR(32),
	@Email VARCHAR(254),
	@Phone VARCHAR(10),
	@Password VARCHAR(32),
	@StaffType VARCHAR(32)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	INSERT INTO staff	(first_name, last_name, email, phone, [password], active, [type])
    VALUES				(@FirstName, @LastName, @Email, @Phone, @Password, 1, @StaffType)
END
