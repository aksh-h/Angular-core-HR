CREATE TABLE [dbo].[tbl_WorkFlowMaster] (
    [WorkFlowID]   INT            IDENTITY (1, 1) NOT NULL,
    [WorkFlowJson] NVARCHAR (MAX) NULL,
    [IsActive]     BIT            NULL,
    [WorkFlowName] VARCHAR (256)  NULL,
    CONSTRAINT [PK_tbl_WorkFlowMaster] PRIMARY KEY CLUSTERED ([WorkFlowID] ASC)
);

