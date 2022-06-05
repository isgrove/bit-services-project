CREATE TABLE [dbo].[contractor_availability] (
    [availability_date] DATE NOT NULL,
    [contractor_id]     INT  NOT NULL,
    CONSTRAINT [contractor_availability_pk] PRIMARY KEY CLUSTERED ([availability_date] ASC, [contractor_id] ASC),
    CONSTRAINT [contractor_contractor_availability_fk] FOREIGN KEY ([contractor_id]) REFERENCES [dbo].[contractor] ([contractor_id])
);

