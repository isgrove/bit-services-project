CREATE TABLE [dbo].[contractor] (
    [contractor_id]        INT           IDENTITY (1, 1) NOT NULL,
    [first_name]           VARCHAR (32)  NOT NULL,
    [last_name]            VARCHAR (32)  NOT NULL,
    [email]                VARCHAR (254) NOT NULL,
    [phone]                VARCHAR (10)  NOT NULL,
    [password]             VARCHAR (32)  NOT NULL,
    [street]               VARCHAR (32)  NOT NULL,
    [suburb]               VARCHAR (32)  NOT NULL,
    [postcode]             VARCHAR (4)   NOT NULL,
    [state]                VARCHAR (3)   NOT NULL,
    [licence_number]       VARCHAR (20)  NOT NULL,
    [vehicle_registration] VARCHAR (6)   NOT NULL,
    [performance_rating]   VARCHAR (1)   NOT NULL,
    [active]               BIT           NOT NULL,
    CONSTRAINT [contractor_pk] PRIMARY KEY CLUSTERED ([contractor_id] ASC)
);

