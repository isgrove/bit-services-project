-- =============================================
-- Author:		Sam Isgrove
-- Create date: 13/05/2022
-- Description:	Returns all of the in pending jobs. If a contractor id is provided, only the pending
--				jobs assigned to that contractor will be returns
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetPendingJobs] 
	-- Add the parameters for the stored procedure here
	@ContractorId int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		c.client_name + ' ' + cl.suburb AS [Location Suburb],
				j.job_status AS [Status],
				j.required_skill AS [Job Skill],
				j.title AS [Job Title],
				j.description AS [Description],
				format(j.deadline_date, 'D') AS [Deadline Date],
				j.job_id
	FROM		job j
	INNER JOIN	job_assignment jc ON jc.job_id = j.job_id AND jc.accepted IS NULL
	INNER JOIN	client_location cl ON cl.location_id = j.location_id
	INNER JOIN	client c ON c.client_id = cl.client_id
	WHERE		j.job_status = 'Pending'
	AND			(jc.contractor_id = @ContractorId OR @ContractorId IS NULL)
END
