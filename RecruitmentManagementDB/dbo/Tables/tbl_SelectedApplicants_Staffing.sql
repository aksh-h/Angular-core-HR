CREATE TABLE [dbo].[tbl_SelectedApplicants_Staffing] (
    [SelectedApplicantsID]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [ApplicantRequisitionId] BIGINT        NOT NULL,
    [ApplicantJoining]       VARCHAR (50)  NULL,
    [IsOnboarded]            VARCHAR (50)  NULL,
    [JoinedDate]             NVARCHAR (50) NULL,
    [ClientOnboardingDate]   NVARCHAR (50) NULL,
    [CreatedDate]            DATETIME      NULL,
    [ModifiedDate]           DATETIME      NULL,
    [CreatedBy]              BIGINT        NULL,
    [ModifiedBy]             BIGINT        NULL,
    CONSTRAINT [PK__tbl_Sele__A82BE515BB03E118] PRIMARY KEY CLUSTERED ([SelectedApplicantsID] ASC),
    CONSTRAINT [FK_tbl_SelectedApplicants_Staffing_tbl_ApplicantRequisition_Staffing] FOREIGN KEY ([ApplicantRequisitionId]) REFERENCES [dbo].[tbl_ApplicantRequisition_Staffing] ([ApplicantRequisitionId])
);

