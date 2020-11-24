CREATE TABLE [dbo].[tbl_SkillMaster] (
    [SkillID]   INT           IDENTITY (1, 1) NOT NULL,
    [SkillName] VARCHAR (128) NOT NULL,
    [IsActive]  BIT           NOT NULL,
    CONSTRAINT [PK_Admin.Skill] PRIMARY KEY CLUSTERED ([SkillID] ASC)
);

