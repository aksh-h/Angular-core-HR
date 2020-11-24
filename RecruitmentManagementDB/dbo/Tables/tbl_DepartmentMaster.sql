CREATE TABLE [dbo].[tbl_DepartmentMaster] (
    [DepartmentID] INT           IDENTITY (1, 1) NOT NULL,
    [Department]   VARCHAR (128) NOT NULL,
    [IsActive]     BIT           NOT NULL,
    CONSTRAINT [PK_Admin.Department] PRIMARY KEY CLUSTERED ([DepartmentID] ASC)
);

