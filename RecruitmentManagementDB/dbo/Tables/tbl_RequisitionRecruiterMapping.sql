CREATE TABLE [dbo].[tbl_RequisitionRecruiterMapping] (
    [RequisitionEmployeeAssigneeID] BIGINT IDENTITY (1, 1) NOT NULL,
    [EmployeeID]                    BIGINT NOT NULL,
    [RequisitionID]                 BIGINT NOT NULL,
    CONSTRAINT [PK_tbl_RequisitionRecruiterMapping] PRIMARY KEY CLUSTERED ([RequisitionEmployeeAssigneeID] ASC),
    CONSTRAINT [FK_tbl_RequisitionRecruiterMapping_tbl_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID]),
    CONSTRAINT [FK_tbl_RequisitionRecruiterMapping_tbl_Requisition] FOREIGN KEY ([RequisitionID]) REFERENCES [dbo].[tbl_Requisition] ([RequistionID])
);

