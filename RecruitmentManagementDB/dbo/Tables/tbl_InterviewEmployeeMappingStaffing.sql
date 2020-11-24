CREATE TABLE [dbo].[tbl_InterviewEmployeeMappingStaffing] (
    [InterviewEmployeeMappingID] INT    IDENTITY (1, 1) NOT NULL,
    [InterviewID]                BIGINT NULL,
    [EmployeeID]                 BIGINT NULL,
    [IsActive]                   BIT    NULL,
    CONSTRAINT [PK_tbl_InterviewEmployeeMappingStaffing] PRIMARY KEY CLUSTERED ([InterviewEmployeeMappingID] ASC),
    CONSTRAINT [FK_tbl_InterviewEmployeeMappingStaffing_tbl_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID]),
    CONSTRAINT [FK_tbl_InterviewEmployeeMappingStaffing_tbl_InterviewManagement_Staffing] FOREIGN KEY ([InterviewID]) REFERENCES [dbo].[tbl_InterviewManagement_Staffing] ([InterviewID])
);

