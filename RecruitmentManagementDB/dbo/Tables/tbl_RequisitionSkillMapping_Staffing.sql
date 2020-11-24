CREATE TABLE [dbo].[tbl_RequisitionSkillMapping_Staffing] (
    [RequisitionSkillID] BIGINT IDENTITY (1, 1) NOT NULL,
    [RequisitionID]      BIGINT NOT NULL,
    [SkillID]            INT    NULL,
    CONSTRAINT [PK_tbl_RequisitionSkillMapping_Staffing] PRIMARY KEY CLUSTERED ([RequisitionSkillID] ASC),
    CONSTRAINT [FK_tbl_RequisitionSkillMapping_Staffing_tbl_Requisition] FOREIGN KEY ([RequisitionID]) REFERENCES [dbo].[tbl_Requisition_Staffing] ([RequistionID]),
    CONSTRAINT [FK_tbl_RequisitionSkillMapping_Staffing_tbl_SkillMaster] FOREIGN KEY ([SkillID]) REFERENCES [dbo].[tbl_SkillMaster] ([SkillID])
);

