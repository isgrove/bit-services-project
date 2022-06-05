-- =============================================
-- Author:		Sam Isgrove
-- Create date: 03/06/2022
-- Description:	Adds a skill to the database. If a Contractor Id is provided a skill will be added to that contractor
-- =============================================
CREATE PROCEDURE usp_AddSkill 
	-- Add the parameters for the stored procedure here
	@SkillName VARCHAR(32),
	@ContractorId INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	IF (@ContractorId IS NULL)
		BEGIN
			INSERT skill	(skill_name)
			VALUES			(@SkillName)
		END
	ELSE
		BEGIN
			INSERT contractor_skill (contractor_id, skill_name)
			VALUES					(@ContractorId, @SkillName)
		END
END
