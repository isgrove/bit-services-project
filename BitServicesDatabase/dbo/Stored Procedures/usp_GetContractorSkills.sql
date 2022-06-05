-- =============================================
-- Author:		Sam Isgrove
-- Create date: 14/05/2022
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetContractorSkills] 
	-- Add the parameters for the stored procedure here
	@ContractorId int = NULL,
	@SkillName VARCHAR(32) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	s.skill_name, s.contractor_id
	FROM	contractor_skill s
	WHERE	(s.contractor_id = @ContractorId OR @ContractorId IS NULL)
	AND		(s.skill_name = @SkillName OR @SkillName IS NULL)
END
