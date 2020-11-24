CREATE TABLE [dbo].[tbl_RequisitionSkillMapping] (
    [RequisitionSkillID] BIGINT IDENTITY (1, 1) NOT NULL,
    [RequisitionID]      BIGINT NOT NULL,
    [SkillID]            INT    NULL,
    CONSTRAINT [PK_tbl_RequisitionSkillMapping] PRIMARY KEY CLUSTERED ([RequisitionSkillID] ASC),
    CONSTRAINT [FK_tbl_RequisitionSkillMapping_tbl_Requisition] FOREIGN KEY ([RequisitionID]) REFERENCES [dbo].[tbl_Requisition] ([RequistionID]),
    CONSTRAINT [FK_tbl_RequisitionSkillMapping_tbl_SkillMaster] FOREIGN KEY ([SkillID]) REFERENCES [dbo].[tbl_SkillMaster] ([SkillID])
);

