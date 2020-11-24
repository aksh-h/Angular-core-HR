CREATE TABLE [dbo].[tbl_VendorContacts] (
    [ContactID]            INT           IDENTITY (1, 1) NOT NULL,
    [VendorID]             INT           NOT NULL,
    [ContactphoneNo]       INT           NULL,
    [ContactPersonEmailID] VARCHAR (256) NULL,
    [Designation]          VARCHAR (256) NULL,
    [IsActive]             BIT           NULL,
    [ContactPerson]        VARCHAR (512) NULL,
    CONSTRAINT [PK_tbl_VendorContacts] PRIMARY KEY CLUSTERED ([ContactID] ASC),
    CONSTRAINT [FK_tbl_VendorContacts_tbl_Vendors] FOREIGN KEY ([VendorID]) REFERENCES [dbo].[tbl_Vendors] ([VendorID])
);

