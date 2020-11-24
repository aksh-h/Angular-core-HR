CREATE TABLE [dbo].[tbl_QualificationMaster] (
    [QualificationID] INT           IDENTITY (1, 1) NOT NULL,
    [Qualification]   VARCHAR (128) NULL,
    [IsActive]        BIT           NULL,
    CONSTRAINT [PK_tbl_QualificationMaster] PRIMARY KEY CLUSTERED ([QualificationID] ASC)
);

