CREATE TABLE [dbo].[tbl_RequisitionClientContactMapping_Staffing] (
    [RequisitionContactId] INT    IDENTITY (1, 1) NOT NULL,
    [RequisitionID]        BIGINT NOT NULL,
    [ContactID]            INT    NOT NULL,
    PRIMARY KEY CLUSTERED ([RequisitionContactId] ASC),
    CONSTRAINT [FK_tbl_RequisitionClientContactMapping_Staffing_tbl_ClientsContactDetails] FOREIGN KEY ([ContactID]) REFERENCES [dbo].[tbl_ClientsContactDetails] ([ContactId]),
    CONSTRAINT [FK_tbl_RequisitionClientContactMapping_Staffing_tbl_Requisition_Staffing] FOREIGN KEY ([RequisitionID]) REFERENCES [dbo].[tbl_Requisition_Staffing] ([RequistionID])
);

