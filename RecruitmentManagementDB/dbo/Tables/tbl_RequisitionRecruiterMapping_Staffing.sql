CREATE TABLE [dbo].[tbl_RequisitionRecruiterMapping_Staffing] (
    [RequisitionEmployeeAssigneeID] BIGINT IDENTITY (1, 1) NOT NULL,
    [EmployeeID]                    BIGINT NOT NULL,
    [RequisitionID]                 BIGINT NOT NULL,
    CONSTRAINT [PK_tbl_RequisitionRecruiterMapping_Staffing] PRIMARY KEY CLUSTERED ([RequisitionEmployeeAssigneeID] ASC),
    CONSTRAINT [FK_tbl_RequisitionRecruiterMapping_Staffing_tbl_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID]),
    CONSTRAINT [FK_tbl_RequisitionRecruiterMapping_Staffing_tbl_Requisition_Staffing] FOREIGN KEY ([RequisitionID]) REFERENCES [dbo].[tbl_Requisition_Staffing] ([RequistionID])
);

