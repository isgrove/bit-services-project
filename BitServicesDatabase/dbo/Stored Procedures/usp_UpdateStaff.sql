-- =============================================
-- Author:		Sam Isgrove
-- Create date: 03/06/2022
-- Description:	Updates a staff member with the given values
-- =============================================
CREATE PROCEDURE usp_UpdateStaff 
	-- Add the parameters for the stored procedure here
	@StaffId INT,
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
	UPDATE	staff
	SET		first_name = @FirstName,
			last_name = @LastName,
			email = @Email,
			phone = @Phone,
			[password] = @Password,
			[type] = @StaffType
	WHERE	staff_id = @StaffId
END
