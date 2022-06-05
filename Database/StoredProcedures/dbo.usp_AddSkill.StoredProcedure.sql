USE [BitServices_Sam]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddSkill]    Script Date: 6/06/2022 12:57:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sam Isgrove
-- Create date: 03/06/2022
-- Description:	Adds a skill to the database. If a Contractor Id is provided a skill will be added to that contractor
-- =============================================
CREATE PROCEDURE [dbo].[usp_AddSkill] 
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
GO
