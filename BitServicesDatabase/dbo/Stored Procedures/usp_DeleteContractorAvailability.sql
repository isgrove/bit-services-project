-- =============================================
-- Author:		Sam Isgrove
-- Create date: 06/06/2022
-- Description:	Deletes a contractotr availability date for the given contractor based on parameters.
--				If the availability is being used for a job, the availability will not be deleted.
-- =============================================
CREATE PROCEDURE usp_DeleteContractorAvailability 
	-- Add the parameters for the stored procedure here
	@AvailabilityDate DATE, 
	@ContractorId INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	IF NOT EXISTS (
					SELECT	*
					FROM	job_assignment
					WHERE	contractor_id = @ContractorId
					AND		availability_date = @AvailabilityDate
					)
	BEGIN
		DELETE	contractor_availability
		WHERE	contractor_id = @ContractorId
		AND	availability_date = @AvailabilityDate
	END
END
