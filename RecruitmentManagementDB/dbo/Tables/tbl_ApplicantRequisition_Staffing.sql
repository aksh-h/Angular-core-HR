CREATE TABLE [dbo].[tbl_ApplicantRequisition_Staffing] (
    [ApplicantRequisitionId]  BIGINT          IDENTITY (1, 1) NOT NULL,
    [ApplicantId]             BIGINT          NOT NULL,
    [RequisitionId]           BIGINT          NOT NULL,
    [Status]                  VARCHAR (64)    NULL,
    [CreatedBy]               BIGINT          NULL,
    [ModifiedBy]              BIGINT          NULL,
    [CreatedDate]             DATETIME        NULL,
    [ModifiedDate]            DATETIME        NULL,
    [RecruiterComment]        NVARCHAR (1024) NULL,
    [CurrentCtc]              DECIMAL (18, 5) NULL,
    [ExpectedCtc]             DECIMAL (18, 5) NULL,
    [NegotiatedCtc]           DECIMAL (18, 5) NULL,
    [TentativeJoiningDate]    DATE            NULL,
    [TentativeOnboardingDate] DATE            NULL,
    CONSTRAINT [PK_tbl_ApplicantRequisition_Staffing] PRIMARY KEY CLUSTERED ([ApplicantRequisitionId] ASC),
    CONSTRAINT [FK_tbl_ApplicantRequisition_Staffing_tbl_Applicants] FOREIGN KEY ([ApplicantId]) REFERENCES [dbo].[tbl_Applicants] ([ApplicantID]),
    CONSTRAINT [FK_tbl_ApplicantRequisition_Staffing_tbl_Employees] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID]),
    CONSTRAINT [FK_tbl_ApplicantRequisition_Staffing_tbl_Employees1] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID]),
    CONSTRAINT [FK_tbl_ApplicantRequisition_Staffing_tbl_Requisition] FOREIGN KEY ([RequisitionId]) REFERENCES [dbo].[tbl_Requisition_Staffing] ([RequistionID])
);

