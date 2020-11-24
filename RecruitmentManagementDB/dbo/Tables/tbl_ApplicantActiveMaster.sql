CREATE TABLE [dbo].[tbl_ApplicantActiveMaster] (
    [ApplicantActiveID] INT           IDENTITY (1, 1) NOT NULL,
    [ApplicantActive]   VARCHAR (128) NULL,
    [IsActive]          BIT           NULL,
    CONSTRAINT [PK_tbl_ApplicantActiveMaster] PRIMARY KEY CLUSTERED ([ApplicantActiveID] ASC)
);

