-- =============================================
-- Author:		Sam Isgrove
-- Create date: 29/05/2022
-- Description:	Gets all of the completed jobs
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetCompletedJobs] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT			c.client_name + ' ' + l.suburb AS [Client Location],
					co.first_name + ' ' + co.last_name AS [Contractor Name],
                    j.description AS[Description],
					format(j.deadline_date, 'D') AS [Deadline Date],
					format(ja.completion_date, 'D') AS [Completed Date],
					j.job_id
	FROM			job j
	INNER JOIN		client_location l ON l.location_id = j.location_id
	INNER JOIN		client c ON c.client_id = l.client_id
	INNER JOIN		job_contractor jc ON jc.job_id = j.job_id
	INNER JOIN		job_acceptance ja ON jc.job_id = ja.job_id AND jc.contractor_id  = ja.contractor_id
	INNER JOIN		contractor co ON co.contractor_id = jc.contractor_id
	WHERE			j.job_status = 'Completed'
END
