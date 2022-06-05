-- =============================================
-- Author:		Sam Isgrove
-- Create date: 06/06/2022
-- Description:	Create a new availability date for the given contractor based on parameters.
-- =============================================
CREATE PROCEDURE usp_AddContractorAvailability 
	-- Add the parameters for the stored procedure here
	@AvailabilityDate DATE, 
	@ContractorId INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	INSERT INTO	contractor_availability	(contractor_id, availability_date)
	VALUES								(@ContractorId, @AvailabilityDate)
END
