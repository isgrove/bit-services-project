-- =============================================
-- Author:		Sam Isgrove
-- Create date: 03/06/2022
-- Description:	Updates a new contractor to the database with the given values
-- =============================================
CREATE PROCEDURE usp_UpdateContractor
	-- Add the parameters for the stored procedure here
	@ContractorId INT,
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
	@PerformanceRating VARCHAR(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	UPDATE	contractor
	SET		first_name = @FirstName,
			last_name = @LastName,
			email = @Email,
			phone = @Phone,
			[password] = @Password,
			street = @Street,
			suburb = @Suburb,
			postcode = @Postcode,
			[state] = @State,
			licence_number = @LicenceNumber,
			vehicle_registration = @VehicleRegistration,
			performance_rating = @PerformanceRating
	WHERE	contractor_id = @ContractorId
END
