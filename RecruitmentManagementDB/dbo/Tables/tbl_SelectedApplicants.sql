CREATE TABLE [dbo].[tbl_SelectedApplicants] (
    [SelectedApplicantsID]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [ApplicantRequisitionId] BIGINT        NOT NULL,
    [ApplicantJoining]       VARCHAR (50)  NULL,
    [JoinedDate]             NVARCHAR (50) NULL,
    [CreatedDate]            DATETIME      NULL,
    [ModifiedDate]           DATETIME      NULL,
    [CreatedBy]              BIGINT        NULL,
    [ModifiedBy]             BIGINT        NULL,
    PRIMARY KEY CLUSTERED ([SelectedApplicantsID] ASC),
    CONSTRAINT [FK_tbl_SelectedApplicants_tbl_ApplicantRequisition] FOREIGN KEY ([ApplicantRequisitionId]) REFERENCES [dbo].[tbl_ApplicantRequisition] ([ApplicantRequisitionId])
);

