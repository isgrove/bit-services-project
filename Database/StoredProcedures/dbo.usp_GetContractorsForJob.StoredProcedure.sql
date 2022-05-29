USE [BitServices_Sam]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetContractorsForJob]    Script Date: 30/05/2022 2:54:04 AM ******/
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
