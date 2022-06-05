-- =============================================
-- Author:		Sam Isgrove
-- Create date: 13/05/2022
-- Description:	Aceepts a job by changing the job status and 
--				creating a record in the job acceptance table
-- =============================================
CREATE PROCEDURE [dbo].[usp_AcceptJob] 
	-- Add the parameters for the stored procedure here
	@ContractorId INT,
	@JobId INT,
	@CompletionDate DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	UPDATE	Job
	SET		job_status = 'In Progress'
	WHERE	job_id = @JobId
	
	UPDATE	job_assignment
	SET		availability_date = @CompletionDate,
			accepted = 1,
			reason = 'Accepted'
	WHERE	job_id = @JobId
	AND		contractor_id = @ContractorId
END
