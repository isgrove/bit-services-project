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
