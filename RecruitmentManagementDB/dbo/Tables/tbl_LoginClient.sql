CREATE TABLE [dbo].[tbl_LoginClient] (
    [LoginClientID] INT            IDENTITY (1, 1) NOT NULL,
    [ContactId]     INT            NOT NULL,
    [UserName]      VARCHAR (128)  NOT NULL,
    [Password]      NVARCHAR (512) NOT NULL,
    [IsActive]      BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([LoginClientID] ASC),
    CONSTRAINT [FK_tbl_LoginClient_tbl_ClientsContactDetails] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[tbl_ClientsContactDetails] ([ContactId])
);

