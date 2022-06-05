CREATE TABLE [dbo].[staff] (
    [staff_id]   INT           IDENTITY (1, 1) NOT NULL,
    [type]       VARCHAR (32)  NOT NULL,
    [first_name] VARCHAR (32)  NOT NULL,
    [last_name]  VARCHAR (32)  NOT NULL,
    [email]      VARCHAR (254) NOT NULL,
    [phone]      VARCHAR (10)  NOT NULL,
    [password]   VARCHAR (32)  NOT NULL,
    [active]     BIT           NOT NULL,
    CONSTRAINT [staff_pk] PRIMARY KEY CLUSTERED ([staff_id] ASC),
    CONSTRAINT [staff_type_staff_fk] FOREIGN KEY ([type]) REFERENCES [dbo].[staff_type] ([type])
);

