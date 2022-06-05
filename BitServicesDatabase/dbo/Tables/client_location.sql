CREATE TABLE [dbo].[client_location] (
    [location_id] INT           IDENTITY (1, 1) NOT NULL,
    [client_id]   INT           NOT NULL,
    [email]       VARCHAR (254) NOT NULL,
    [phone]       VARCHAR (10)  NOT NULL,
    [street]      VARCHAR (32)  NOT NULL,
    [suburb]      VARCHAR (32)  NOT NULL,
    [postcode]    VARCHAR (4)   NOT NULL,
    [state]       VARCHAR (3)   NOT NULL,
    [active]      BIT           NOT NULL,
    CONSTRAINT [client_location_pk] PRIMARY KEY CLUSTERED ([location_id] ASC),
    CONSTRAINT [client_client_location_fk] FOREIGN KEY ([client_id]) REFERENCES [dbo].[client] ([client_id])
);

