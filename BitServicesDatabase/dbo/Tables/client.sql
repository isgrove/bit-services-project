CREATE TABLE [dbo].[client] (
    [client_id]   INT           IDENTITY (1, 1) NOT NULL,
    [client_name] VARCHAR (32)  NOT NULL,
    [email]       VARCHAR (254) NOT NULL,
    [phone]       VARCHAR (10)  NOT NULL,
    [password]    VARCHAR (32)  NOT NULL,
    [active]      BIT           NOT NULL,
    CONSTRAINT [client_pk] PRIMARY KEY CLUSTERED ([client_id] ASC)
);

