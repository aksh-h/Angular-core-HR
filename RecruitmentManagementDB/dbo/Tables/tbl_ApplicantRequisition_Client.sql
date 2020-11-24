CREATE TABLE [dbo].[tbl_ApplicantRequisition_Client] (
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
    [TentativeOnBoardingDate] DATE            NULL,
    CONSTRAINT [PK_tbl_ApplicantRequisition_Client1] PRIMARY KEY CLUSTERED ([ApplicantRequisitionId] ASC),
    CONSTRAINT [FK_tbl_ApplicantRequisition_Client_tbl_Applicants] FOREIGN KEY ([ApplicantRequisitionId]) REFERENCES [dbo].[tbl_Applicants] ([ApplicantID]),
    CONSTRAINT [FK_tbl_ApplicantRequisition_Client_tbl_Applicants1] FOREIGN KEY ([ApplicantId]) REFERENCES [dbo].[tbl_Applicants] ([ApplicantID]),
    CONSTRAINT [FK_tbl_ApplicantRequisition_Client_tbl_Employees] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID]),
    CONSTRAINT [FK_tbl_ApplicantRequisition_Client_tbl_Employees1] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID]),
    CONSTRAINT [FK_tbl_ApplicantRequisition_Client_tbl_Requisition] FOREIGN KEY ([RequisitionId]) REFERENCES [dbo].[tbl_Requisition] ([RequistionID])
);

