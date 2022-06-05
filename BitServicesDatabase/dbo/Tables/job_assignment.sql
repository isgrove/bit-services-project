CREATE TABLE [dbo].[job_assignment] (
    [contractor_id]     INT          NOT NULL,
    [job_id]            INT          NOT NULL,
    [staff_id]          INT          NOT NULL,
    [accepted]          BIT          NULL,
    [availability_date] DATE         NULL,
    [reason]            VARCHAR (32) NULL,
    CONSTRAINT [job_assignment_pk] PRIMARY KEY CLUSTERED ([contractor_id] ASC, [job_id] ASC),
    CONSTRAINT [assignment_reason_job_assignment_fk] FOREIGN KEY ([reason]) REFERENCES [dbo].[assignment_reason] ([reason]),
    CONSTRAINT [contractor_availability_job_assignment_fk] FOREIGN KEY ([availability_date], [contractor_id]) REFERENCES [dbo].[contractor_availability] ([availability_date], [contractor_id]),
    CONSTRAINT [contractor_contractor_assignment_fk] FOREIGN KEY ([contractor_id]) REFERENCES [dbo].[contractor] ([contractor_id]),
    CONSTRAINT [job_contractor_assignment_fk] FOREIGN KEY ([job_id]) REFERENCES [dbo].[job] ([job_id]),
    CONSTRAINT [staff_contractor_assignment_fk] FOREIGN KEY ([staff_id]) REFERENCES [dbo].[staff] ([staff_id])
);

