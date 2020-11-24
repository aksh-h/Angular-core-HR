CREATE TABLE [dbo].[tbl_JobTypeMaster] (
    [JobTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [JobType]   VARCHAR (128) NULL,
    [IsActive]  BIT           NULL,
    CONSTRAINT [PK_tbl_JobTypeMaster] PRIMARY KEY CLUSTERED ([JobTypeID] ASC)
);

