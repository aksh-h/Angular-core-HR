CREATE TABLE [dbo].[tbl_NoticePeriodMaster] (
    [NoticePeriodID] INT           IDENTITY (1, 1) NOT NULL,
    [NoticePeriod]   VARCHAR (128) NULL,
    [IsActive]       BIT           NULL,
    CONSTRAINT [PK_tbl_NoticePeriodMaster] PRIMARY KEY CLUSTERED ([NoticePeriodID] ASC)
);

