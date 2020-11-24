CREATE TABLE [dbo].[tbl_LocationMaster] (
    [LocationID]   INT           IDENTITY (1, 1) NOT NULL,
    [StateID]      INT           NULL,
    [CountryID]    INT           NULL,
    [LocationName] VARCHAR (100) NULL,
    [IsActive]     BIT           CONSTRAINT [DF_Location_IsActive] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationID] ASC)
);

