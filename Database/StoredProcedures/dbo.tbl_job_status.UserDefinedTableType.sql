USE [BitServices_Sam]
GO
/****** Object:  UserDefinedTableType [dbo].[tbl_job_status]    Script Date: 6/06/2022 12:57:09 AM ******/
CREATE TYPE [dbo].[tbl_job_status] AS TABLE(
	[job_status] [varchar](32) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[job_status] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
