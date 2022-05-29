USE [BitServices_Sam]
GO
/****** Object:  StoredProcedure [dbo].[usp_AcceptJob]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[usp_AddContractor]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sam Isgrove
-- Create date: 30/05/2022
-- Description:	Creates a new contractor and their skills
-- =============================================
CREATE PROCEDURE [dbo].[usp_AddContractor] 
	-- Add the parameters for the stored procedure here
	@FirstName VARCHAR(32),
	@LastName VARCHAR(32),
	@Email VARCHAR(254),
	@Phone VARCHAR(10),
	@Password VARCHAR(32),
	@Street VARCHAR(32),
	@Suburb VARCHAR(32),
	@Postcode VARCHAR(4),
	@State VARCHAR(3),
	@LicenceNumber VARCHAR(20),
	@VehicleRegistration VARCHAR(6),
	@PerformanceRating VARCHAR(1),
	@Skills tbl_skill READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

	DECLARE @NewContractorID INT;

    -- Insert statements for procedure here
	INSERT INTO CONTRACTOR	(
								first_name,
								last_name,
								email,
								phone,
								password,
								street,
								suburb,
								postcode,
								state,
								licence_number,
								vehicle_registration,
								performance_rating,
								active
							)
	VALUES					(
								@FirstName,
								@LastName,
								@Email ,
								@Phone ,
								@Password,
								@Street,
								@Suburb,
								@Postcode,
								@State,
								@LicenceNumber,
								@VehicleRegistration,
								@PerformanceRating,
								1
							);

	SET @NewContractorID = @@IDENTITY;

	INSERT INTO		contractor_skill(contractor_id, skill_name) 
	SELECT			@NewContractorID,
					skill_name
	FROM			@Skills;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AssignContractor]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllJobs]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
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
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllJobsFromStatuses]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sam Isgrove
-- Create date: 23/05/2022
-- Description:	Gets all of the jobs based on the provided jobs statuses. If a client id or contractor id is provided,
--				only the jobs for that provided user will be returned.
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetAllJobsFromStatuses]
	-- Add the parameters for the stored procedure here
	@ClientId INT = NULL,
	@ContractorId INT = NULL,
	@JobStatuses tbl_job_status READONLY
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
	AND					(ja.contractor_id = @ContractorId OR @ContractorId IS NULL)
	AND					(
							j.job_status IN	( 
												SELECT	*
												FROM	@JobStatuses
											)
						)
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
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllRejectedJobs]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllRejectionReasons]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sam Isgrove
-- Create date: 14/05/2022
-- Description:	Gets all of the job rejection reasons
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetAllRejectionReasons]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		* 
	FROM		assignment_reason
	WHERE		reason != 'Accepted'
	ORDER BY	(
					CASE reason
						WHEN 'Schedule Conflict' THEN 1
						WHEN 'Personal Reasons' THEN 2
						WHEN 'Unwell' THEN 3
						WHEN 'Other' THEN 4
					END
				) ASC
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAvailabilityForJob]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sam Isgrove
-- Create date: 13/05/2022
-- Description:	Gets the dates that a contractor is availible to complete
--				a job based on the job's deadline date.
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetAvailabilityForJob]
	-- Add the parameters for the stored procedure here
	@ContractorId INT = NULL,
	@DeadlineDate DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	a.availability_date AS [Date],
			format(a.availability_date, 'D') AS [Formatted Date],
			a.contractor_id
	FROM	contractor_availability a
	WHERE	(a.contractor_id = @ContractorId OR @ContractorId IS NULL)
	AND		a.availability_date BETWEEN FORMAT (getdate(), 'yyyy-MM-dd') AND @DeadlineDate
	AND		a.availability_date NOT IN
				(
					SELECT	ja.availability_date
					FROM	job_assignment ja
					WHERE	ja.contractor_id = a.contractor_id
					AND		ja.availability_date IS NOT NULL
				)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetCompletedJobs]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[usp_GetContractorsForJob]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sam Isgrove
-- Create date: 14/05/2022
-- Description:	Gets all of the contractors that with the required skill that
--				also have the availability to take on a new job
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetContractorsForJob] 
	-- Add the parameters for the stored procedure here
	@JobSkillName VARCHAR(32),
	@DeadlineDate DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here

	DECLARE @Availabilitiy TABLE([date] DATE, formatted_date NVARCHAR(64), contractor_id INT)

	INSERT	@Availabilitiy
	EXEC	usp_GetAvailabilityForJob @DeadlineDate = @DeadlineDate;

	
	DECLARE @Skills TABLE(skill_name VARCHAR(32), contractor_id INT)

	INSERT	@Skills
	EXEC	usp_GetContractorSkills @SkillName = @JobSkillName;

	SELECT DISTINCT	c.first_name + ' ' + c.last_name AS [Contractor Name],
					c.*
	FROM			contractor c 
	INNER JOIN		@Skills s ON s.contractor_id = c.contractor_id
	INNER JOIN		@Availabilitiy a ON a.contractor_id = c.contractor_id
	WHERE			c.active = 1
	ORDER BY		c.performance_rating

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetContractorSkills]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sam Isgrove
-- Create date: 14/05/2022
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetContractorSkills] 
	-- Add the parameters for the stored procedure here
	@ContractorId int = NULL,
	@SkillName VARCHAR(32) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	s.skill_name, s.contractor_id
	FROM	contractor_skill s
	WHERE	(s.contractor_id = @ContractorId OR @ContractorId IS NULL)
	AND		(s.skill_name = @SkillName OR @SkillName IS NULL)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetInProgressJobs]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sam Isgrove
-- Create date: 13/05/2022
-- Description:	Returns all of the in progress jobs. If a contractor id is provided, only the in progress
--				jobs assigned to that contractor will be returns
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetInProgressJobs] 
	-- Add the parameters for the stored procedure here
	@ContractorId int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT		cl.suburb AS [Location Suburb],
			j.job_status AS [Status],
			j.required_skill [Job Skill],
            j.title AS [Title],
            j.[description] AS [Description],
			format(j.deadline_date, 'D') AS [Deadline Date],
			j.job_id
FROM		job j
INNER JOIN	job_assignment jc ON jc.job_id = j.job_id AND jc.accepted = 1
INNER JOIN	client_location cl ON cl.location_id = j.location_id
INNER JOIN	client c ON c.client_id = cl.client_id
WHERE		j.job_status = 'In Progress'
AND			(jc.contractor_id = @ContractorId OR @ContractorId IS NULL)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetJobsToAssign]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[usp_GetPendingJobs]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertJob]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sam Isgrove
-- Create date: 15/05/2022
-- Description:	Inserts a row into the jobs table. If a @StaffId and
--				@ContractorId is not provided, a record in the
--				job_contractor table will also be made.
-- =============================================
CREATE PROCEDURE [dbo].[usp_InsertJob] 
	-- Add the parameters for the stored procedure here
	@LocationId INT,
	@RequiredSkill VARCHAR(32),
	@JobStatus VARCHAR(32),
	@Kilometers INT,
	@Title VARCHAR(64),
	@Description VARCHAR(1000),
	@DeadlineDate DATE,
	@StaffId INT = NULL,
	@ContractorId INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	INSERT INTO job	(location_id, required_skill, job_status, kilometers, title, [description], deadline_date)
	VALUES			(@LocationId, @RequiredSkill, @JobStatus, @Kilometers, @Title, @Description, @DeadlineDate)

	DECLARE @JobId INT = @@IDENTITY

	IF		@StaffId IS NOT NULL AND @ContractorId IS NOT NULL
	BEGIN
			EXEC usp_AssignContractor @JobID, @StaffID, @ContractorID
	END
													
END
GO
/****** Object:  StoredProcedure [dbo].[usp_RejectJob]    Script Date: 30/05/2022 2:52:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
