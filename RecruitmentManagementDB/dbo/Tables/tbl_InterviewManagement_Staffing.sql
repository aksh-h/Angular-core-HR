﻿CREATE TABLE [dbo].[tbl_InterviewManagement_Staffing] (
    [InterviewID]            BIGINT           IDENTITY (1, 1) NOT NULL,
    [InterviewPanel]         VARCHAR (128)    NOT NULL,
    [ApplicantID]            BIGINT           NOT NULL,
    [InterviewDate]          DATETIME         NULL,
    [Comments]               NVARCHAR (1024)  NULL,
    [IsCompleted]            BIT              NULL,
    [CreatedBy]              BIGINT           NULL,
    [CreatedDate]            DATETIME         NULL,
    [ModifiedBy]             BIGINT           NULL,
    [ModifiedDate]           DATETIME         NULL,
    [ApplicantRequisitionID] BIGINT           NOT NULL,
    [Status]                 VARCHAR (128)    NULL,
    [Venue]                  NVARCHAR (1024)  NULL,
    [FeedBack]               NVARCHAR (1024)  NULL,
    [EmailGUID]              UNIQUEIDENTIFIER NULL,
    [Communication]          INT              NULL,
    [Attitude]               INT              NULL,
    [SkillRatings]           VARCHAR (1024)   NULL,
    [RoundName]              VARCHAR (128)    NULL,
    [ModeOfInterview]        VARCHAR (128)    NULL,
    [IsClient]               BIT              NULL,
    [SendFeedbacktoClient]   BIT              NULL,
    [RecruitersFeedBack]     VARCHAR (1024)   NULL,
    CONSTRAINT [PK_tbl_InterviewManagement_Staffing] PRIMARY KEY CLUSTERED ([InterviewID] ASC),
    CONSTRAINT [FK_tbl_InterviewManagement_Staffing_tbl_ApplicantRequisition] FOREIGN KEY ([ApplicantRequisitionID]) REFERENCES [dbo].[tbl_ApplicantRequisition_Staffing] ([ApplicantRequisitionId]),
    CONSTRAINT [FK_tbl_InterviewManagement_Staffing_tbl_Applicants] FOREIGN KEY ([ApplicantID]) REFERENCES [dbo].[tbl_Applicants] ([ApplicantID]),
    CONSTRAINT [FK_tbl_InterviewManagement_Staffing_tbl_Employees1] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID]),
    CONSTRAINT [FK_tbl_InterviewManagement_Staffing_tbl_Employees2] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID])
);
