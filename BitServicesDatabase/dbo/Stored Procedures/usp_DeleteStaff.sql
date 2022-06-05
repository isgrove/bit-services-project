-- =============================================
-- Author:		Sam Isgrove
-- Create date: 03/06/2022
-- Description:	Deletes a staff memeber with the given Staff Id
-- =============================================
CREATE PROCEDURE usp_DeleteStaff 
	-- Add the parameters for the stored procedure here
	@StaffId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	UPDATE staff
	SET active = 0
    WHERE staff_id = @StaffId
END
