CREATE TABLE [dbo].[tbl_DesignationMaster] (
    [DesignationID] INT           IDENTITY (1, 1) NOT NULL,
    [Designation]   VARCHAR (128) NOT NULL,
    [IsActive]      BIT           NOT NULL,
    CONSTRAINT [PK_Admin.Designation] PRIMARY KEY CLUSTERED ([DesignationID] ASC)
);

