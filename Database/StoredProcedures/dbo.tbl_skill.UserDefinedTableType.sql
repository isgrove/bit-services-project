USE [BitServices_Sam]
GO
/****** Object:  UserDefinedTableType [dbo].[tbl_skill]    Script Date: 6/06/2022 12:57:09 AM ******/
CREATE TYPE [dbo].[tbl_skill] AS TABLE(
	[skill_name] [varchar](32) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[skill_name] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
