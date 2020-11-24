CREATE TABLE [dbo].[tbl_InterviewEmployeeMapping] (
    [InterviewEmployeeMappingID] INT    IDENTITY (1, 1) NOT NULL,
    [InterviewID]                BIGINT NULL,
    [EmployeeID]                 BIGINT NULL,
    [IsActive]                   BIT    NULL,
    CONSTRAINT [PK_tbl_InterviewEmployeeMapping] PRIMARY KEY CLUSTERED ([InterviewEmployeeMappingID] ASC),
    CONSTRAINT [FK_tbl_InterviewEmployeeMapping_tbl_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID]),
    CONSTRAINT [FK_tbl_InterviewEmployeeMapping_tbl_InterviewManagement] FOREIGN KEY ([InterviewID]) REFERENCES [dbo].[tbl_InterviewManagement] ([InterviewID])
);

