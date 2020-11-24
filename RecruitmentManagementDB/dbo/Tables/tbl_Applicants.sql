CREATE TABLE [dbo].[tbl_Applicants] (
    [ApplicantID]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]                 VARCHAR (128) NULL,
    [DOB]                  DATETIME      NULL,
    [EmailAddress]         NVARCHAR (64) NULL,
    [Qualification]        VARCHAR (32)  NULL,
    [Experience]           VARCHAR (32)  NULL,
    [RelevantExperience]   VARCHAR (32)  NULL,
    [CurrentCTC]           DECIMAL (18)  NULL,
    [ExpectedCTC]          DECIMAL (18)  NULL,
    [JoiningTime]          VARCHAR (32)  NULL,
    [SkillsandProficiency] VARCHAR (128) NULL,
    [LocationPreference]   VARCHAR (128) NULL,
    [ReferedBy]            INT           NULL,
    [JobType]              VARCHAR (64)  NULL,
    [Status]               VARCHAR (64)  NULL,
    [NoticePeriod]         VARCHAR (64)  NULL,
    [ApplicantActive]      VARCHAR (64)  NULL,
    [ShortlistedBy]        VARCHAR (64)  NULL,
    [Source]               VARCHAR (64)  NULL,
    CONSTRAINT [PK_tbl_Applicants] PRIMARY KEY CLUSTERED ([ApplicantID] ASC)
);

