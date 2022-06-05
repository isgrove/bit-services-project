-- =============================================
-- Author:		Sam Isgrove
-- Create date: 14/05/2022
-- Description:	Gets all the jobs that need to be assigned a contractor
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetJobsToAssign] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DISTINCT		c.client_name + ' ' + cl.suburb AS [Location Suburb],
						j.job_status AS [Status],
						j.required_skill AS [Job Skill],
						j.title AS [Job Title],
						j.description AS [Description],
						format(j.deadline_date, 'D') AS [Deadline Date],
						j.job_id
	FROM				job j
	INNER JOIN			client_location cl ON cl.location_id = j.location_id
	INNER JOIN			client c ON c.client_id = cl.client_id
	WHERE				j.job_status = 'Pending'
	AND					(
							SELECT	count(*)
							FROM	job_assignment ja
							WHERE	ja.job_id = j.job_id
							AND		(ja.accepted IS NULL OR ja.accepted = 1)
						) = 0
END
