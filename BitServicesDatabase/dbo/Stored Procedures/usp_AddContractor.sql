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
