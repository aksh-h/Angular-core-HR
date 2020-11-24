CREATE TABLE [dbo].[tbl_PanelGroupMaster] (
    [PanelGroupID]   INT           IDENTITY (1, 1) NOT NULL,
    [PanelGroupName] VARCHAR (128) NOT NULL,
    [IsActive]       BIT           NOT NULL,
    CONSTRAINT [PK_Admin.PanelGroup] PRIMARY KEY CLUSTERED ([PanelGroupID] ASC)
);

