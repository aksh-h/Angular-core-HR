CREATE TABLE [dbo].[tbl_Employees] (
    [EmployeeID]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [EmployeeName]       VARCHAR (128)  NOT NULL,
    [DesignationID]      INT            NOT NULL,
    [DepartmentID]       INT            NOT NULL,
    [ReportingManagerID] INT            NOT NULL,
    [RoleID]             INT            NOT NULL,
    [IsActive]           BIT            NULL,
    [CreatedDate]        DATETIME       NULL,
    [CreatedBy]          INT            NULL,
    [ModifiedBy]         INT            NULL,
    [ModifiedDate]       DATETIME       NULL,
    [EmailID]            NVARCHAR (128) NULL,
    CONSTRAINT [PK_tbl_Employees] PRIMARY KEY CLUSTERED ([EmployeeID] ASC),
    CONSTRAINT [FK_tbl_Employees_tbl_DepartmentMaster] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tbl_DepartmentMaster] ([DepartmentID]),
    CONSTRAINT [FK_tbl_Employees_tbl_DesignationMaster] FOREIGN KEY ([DesignationID]) REFERENCES [dbo].[tbl_DesignationMaster] ([DesignationID]),
    CONSTRAINT [FK_tbl_Employees_tbl_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID]),
    CONSTRAINT [FK_tbl_Employees_tbl_RoleMaster] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[tbl_RoleMaster] ([RoleID])
);

