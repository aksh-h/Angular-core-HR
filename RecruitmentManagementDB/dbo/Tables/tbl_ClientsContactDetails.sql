CREATE TABLE [dbo].[tbl_ClientsContactDetails] (
    [ContactId]                INT           IDENTITY (1, 1) NOT NULL,
    [ContactPerson]            VARCHAR (128) NULL,
    [ContactPhoneNo]           INT           NULL,
    [ContactPersonEmailId]     VARCHAR (256) NULL,
    [ContactPersonDesignation] VARCHAR (128) NULL,
    [IsActive]                 BIT           NOT NULL,
    [ClientID]                 INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ContactId] ASC),
    FOREIGN KEY ([ClientID]) REFERENCES [dbo].[tbl_Clients] ([ClientID])
);

