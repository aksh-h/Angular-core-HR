CREATE TABLE [dbo].[tbl_GMapMaster] (
    [GmapID]    INT            IDENTITY (1, 1) NOT NULL,
    [AddressID] INT            NOT NULL,
    [Lattiude]  NVARCHAR (100) NOT NULL,
    [Longitude] NVARCHAR (100) NOT NULL,
    [Address]   NVARCHAR (300) NULL,
    [IsActive]  BIT            NOT NULL,
    CONSTRAINT [PK_tbl_GMapMaster] PRIMARY KEY CLUSTERED ([GmapID] ASC)
);

