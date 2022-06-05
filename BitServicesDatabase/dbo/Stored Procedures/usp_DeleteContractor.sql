-- =============================================
-- Author:		Sam Isgrove
-- Create date: 03/06/2022
-- Description:	Deletes a contractor with the given Contractor Id
-- =============================================
CREATE PROCEDURE usp_DeleteContractor
	-- Add the parameters for the stored procedure here
	@ContractorId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	UPDATE contractor
	SET active = 0
    WHERE contractor_id = @ContractorId
END
