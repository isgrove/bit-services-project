-- =============================================
-- Author:		Sam Isgrove
-- Create date: 25/05/2022
-- Description:	Gets every time a contractor has rejected a job and what their reason was.
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetAllRejectedJobs] 
	-- Add the parameters for the stored procedure here
	@ContractorId INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		c.client_name + ' ' + cl.suburb AS [Client Location],
				co.first_name + ' ' + co.last_name AS [Contractor Name],
				s.first_name + ' ' + s.last_name AS [Assigned By],
				j.required_skill AS [Required Skill],
				j.title AS [Title],
				j.description AS [Description],
				format(j.deadline_date, 'D') AS [Deadline Date],
				ja.reason AS [Rejection Reason],
				j.job_id,
				j.deadline_date,
				cl.location_id,
				j.kilometers,
				ja.contractor_id,
				s.staff_id
	FROM		job_assignment ja
	INNER JOIN	contractor co ON ja.contractor_id = co.contractor_id
	INNER JOIN	staff s ON ja.staff_id = s.staff_id
	INNER JOIN	job j ON ja.job_id = j.job_id
	INNER JOIN	client_location cl ON j.location_id = cl.location_id
	INNER JOIN	client c ON cl.client_id = c.client_id
	WHERE		ja.accepted = 0
	AND			(ja.contractor_id = @ContractorId OR @ContractorId IS NULL)
END
