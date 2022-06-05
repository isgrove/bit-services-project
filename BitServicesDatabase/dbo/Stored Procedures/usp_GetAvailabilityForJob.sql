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
