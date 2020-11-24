CREATE TABLE [dbo].[tbl_EmployeePanelMapping] (
    [EmployeePanelMappingID] INT    IDENTITY (1, 1) NOT NULL,
    [EmployeeID]             BIGINT NULL,
    [PanelGroupID]           INT    NOT NULL,
    [IsActive]               BIT    NOT NULL,
    CONSTRAINT [PK_Admin.EmployeePanelMapping] PRIMARY KEY CLUSTERED ([EmployeePanelMappingID] ASC),
    CONSTRAINT [FK_tbl_EmployeePanelMapping_tbl_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID]),
    CONSTRAINT [FK_tbl_EmployeePanelMapping_tbl_PanelGroupMaster] FOREIGN KEY ([PanelGroupID]) REFERENCES [dbo].[tbl_PanelGroupMaster] ([PanelGroupID])
);

