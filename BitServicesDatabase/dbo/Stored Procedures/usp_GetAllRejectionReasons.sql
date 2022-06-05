-- =============================================
-- Author:		Sam Isgrove
-- Create date: 14/05/2022
-- Description:	Gets all of the job rejection reasons
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetAllRejectionReasons]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		* 
	FROM		assignment_reason
	WHERE		reason != 'Accepted'
	ORDER BY	(
					CASE reason
						WHEN 'Schedule Conflict' THEN 1
						WHEN 'Personal Reasons' THEN 2
						WHEN 'Unwell' THEN 3
						WHEN 'Other' THEN 4
					END
				) ASC
END
