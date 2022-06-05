CREATE TABLE [dbo].[job] (
    [job_id]         INT            IDENTITY (1, 1) NOT NULL,
    [location_id]    INT            NOT NULL,
    [required_skill] VARCHAR (32)   NOT NULL,
    [job_status]     VARCHAR (32)   NOT NULL,
    [kilometers]     INT            NOT NULL,
    [title]          VARCHAR (100)  NOT NULL,
    [description]    VARCHAR (1000) NULL,
    [deadline_date]  DATE           NOT NULL,
    CONSTRAINT [job_pk] PRIMARY KEY CLUSTERED ([job_id] ASC),
    CONSTRAINT [client_location_job_fk] FOREIGN KEY ([location_id]) REFERENCES [dbo].[client_location] ([location_id]),
    CONSTRAINT [job_status_job_fk] FOREIGN KEY ([job_status]) REFERENCES [dbo].[job_status] ([status]),
    CONSTRAINT [skill_job_fk] FOREIGN KEY ([required_skill]) REFERENCES [dbo].[skill] ([skill_name])
);

