-- =============================================
-- Author:		Sam Isgrove
-- Create date: 03/06/2022
-- Description:	Inserts a new contractor to the database with the given values
-- =============================================
CREATE PROCEDURE usp_InsertContractor
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
	@PerformanceRating VARCHAR(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	INSERT INTO contractor	(first_name, last_name, email, phone, [password], street, suburb, postcode, [state], licence_number, vehicle_registration, performance_rating, active)
    VALUES					(@FirstName, @LastName, @Email, @Phone, @Password, @Street, @Suburb, @Postcode, @State, @LicenceNumber, @VehicleRegistration, @PerformanceRating, 1)
END
