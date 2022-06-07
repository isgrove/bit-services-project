﻿-- =============================================
-- Author:		Sam Isgrove
-- Create date: 14/05/2022
-- Description:	Gets all jobs in the database. If a client id is provided, only the jobs for that client will be provided
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetAllJobs] 
	-- Add the parameters for the stored procedure here
	@ClientId INT = NULL,
	@JobStatus VARCHAR(32) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT				j.job_status AS [Status],
						c.client_name + ' ' + l.suburb AS [Client Location],
						co.first_name + ' ' + co.last_name AS [Contractor Name],
						j.required_skill AS [Required Skill],
						j.title AS [Title],
						j.description AS [Description],
						format(j.deadline_date, 'D') AS [Deadline Date],
						CASE j.job_status
							WHEN 'Completed' THEN format(ja.availability_date, 'D')
							WHEN 'Verified' THEN format(ja.availability_date, 'D')
						END AS [Completion Date],
						j.job_id,
						j.deadline_date,
						l.location_id,
						j.kilometers,
						ja.contractor_id,
						ja.availability_date
	FROM				job j
	INNER JOIN			client_location l ON l.location_id = j.location_id
	INNER JOIN			client c ON c.client_id = l.client_id
	LEFT OUTER JOIN		job_assignment ja ON ja.job_id = j.job_id AND ja.accepted = 1
	LEFT OUTER JOIN		contractor co ON ja.contractor_id = co.contractor_id
	WHERE				(c.client_id = @ClientId OR @ClientId IS NULL)
	AND					(j.job_status = @JobStatus OR @JobStatus IS NULL)
	ORDER BY			(
							CASE j.job_status
								WHEN 'Pending' THEN 1
								WHEN 'In Progress' THEN 2
								WHEN 'Completed' THEN 3
								WHEN 'Verified' THEN 4
								WHEN 'Canceled' THEN 5
							END
						) ASC
END