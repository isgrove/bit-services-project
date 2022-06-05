-- =============================================
-- Author:		Sam Isgrove
-- Create date: 14/04/2022
-- Description:	Assigns a contractor to a job
-- =============================================
CREATE PROCEDURE [dbo].[usp_AssignContractor] 
	-- Add the parameters for the stored procedure here
	@JobId INT,
	@StaffId INT,
	@ContractorId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	INSERT INTO job_assignment(contractor_id, job_id, staff_id)
	VALUES (@ContractorId, @JobId, @StaffId)
END
