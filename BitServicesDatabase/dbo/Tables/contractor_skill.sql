CREATE TABLE [dbo].[contractor_skill] (
    [contractor_id] INT          NOT NULL,
    [skill_name]    VARCHAR (32) NOT NULL,
    CONSTRAINT [contractor_skill_pk] PRIMARY KEY CLUSTERED ([contractor_id] ASC, [skill_name] ASC),
    CONSTRAINT [contractor_contractor_skill_fk] FOREIGN KEY ([contractor_id]) REFERENCES [dbo].[contractor] ([contractor_id]),
    CONSTRAINT [skill_contractor_skill_fk] FOREIGN KEY ([skill_name]) REFERENCES [dbo].[skill] ([skill_name])
);

