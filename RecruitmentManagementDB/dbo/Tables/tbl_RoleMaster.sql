CREATE TABLE [dbo].[tbl_RoleMaster] (
    [RoleID]   INT           IDENTITY (1, 1) NOT NULL,
    [RoleName] VARCHAR (128) NOT NULL,
    [IsActive] BIT           NOT NULL,
    CONSTRAINT [PK_Admin.Role] PRIMARY KEY CLUSTERED ([RoleID] ASC)
);

