-- =============================================
-- Author:		Sam Isgrove
-- Create date: 03/06/2022
-- Description:	Get all of the client locations. If a ClientId is provided, only the locations of that client will be returned
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetClientLocations] 
	-- Add the parameters for the stored procedure here
	@ClientId INT = NULL,
	@LocaitonId INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT			cl.location_id, cl.client_id, cl.email, cl.phone, cl.street, cl.suburb, cl.postcode, cl.[state], cl.active, c.client_name
    FROM			client_location cl
	INNER JOIN		client c ON c.client_id = cl.client_id
    WHERE			cl.active = 1
	AND				(cl.client_id = @ClientId OR @ClientId IS NULL)
	AND				(cl.location_id = @LocaitonId OR @LocaitonId IS NULL)
END
