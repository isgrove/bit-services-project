-- =============================================
-- Author:		Sam Isgrove
-- Create date: 14/05/2022
-- Description:	Gets all of the contractors that with the required skill that
--				also have the availability to take on a new job
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetContractorsForJob] 
	-- Add the parameters for the stored procedure here
	@JobId INT = NULL,
	@JobSkillName VARCHAR(32) = NULL,
	@DeadlineDate DATE = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	
	IF (@JobId IS NOT NULL)
		BEGIN
			SELECT	@JobSkillName = required_skill,
					@DeadlineDate = deadline_date
			FROM	job
			WHERE	job_id = @JobId
		END

	SELECT DISTINCT		c.first_name + ' ' + c.last_name AS [Contractor Name],
						c.*
	FROM				contractor c
	INNER JOIN			contractor_skill cs ON c.contractor_id = cs.contractor_id AND cs.skill_name = @JobSkillName
	INNER JOIN			contractor_availability ca 
						ON		c.contractor_id = ca.contractor_id
						AND		ca.availability_date BETWEEN FORMAT (getdate(), 'yyyy-MM-dd') AND @DeadlineDate
						AND		ca.availability_date NOT IN
															(
																SELECT	ja.availability_date
																FROM	job_assignment ja
																WHERE	ja.contractor_id = ca.contractor_id
																AND		ja.availability_date IS NOT NULL
															)
	WHERE				c.active = 1
	AND					(c.contractor_id NOT IN (
							SELECT	c.contractor_id
							FROM	job_assignment ja
							WHERE	@JobId = ja.job_id
							AND		c.contractor_id = ja.contractor_id
						)
						OR @JobId IS NULL)
	ORDER BY			c.performance_rating

	--	SELECT DISTINCT	c.first_name + ' ' + c.last_name AS [Contractor Name],
	--				c.*
	--FROM			contractor c 
	--INNER JOIN		@Skills s ON s.contractor_id = c.contractor_id
	--INNER JOIN		@Availabilitiy a ON a.contractor_id = c.contractor_id
	--WHERE			c.active = 1
	--ORDER BY		c.performance_rating

END