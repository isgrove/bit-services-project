-- =============================================
-- Author:		Sam Isgrove
-- Create date: 14/05/2022
-- Description:	Rejects a job by adding a record into the job rejection table
-- =============================================
CREATE PROCEDURE [dbo].[usp_RejectJob] 
	-- Add the parameters for the stored procedure here
	@ContractorId INT,
	@JobId INT,
	@Reason VARCHAR(32)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	UPDATE	job_assignment
	SET		accepted = 0,
			reason = @Reason
	WHERE	contractor_id = @ContractorId
	AND		job_id = @JobId
END
