CREATE TABLE [dbo].[tbl_Login] (
    [LoginID]    INT            IDENTITY (1, 1) NOT NULL,
    [EmployeeID] BIGINT         NOT NULL,
    [UserName]   VARCHAR (128)  NOT NULL,
    [Password]   NVARCHAR (512) NOT NULL,
    [IsActive]   BIT            NOT NULL,
    CONSTRAINT [PK_tbl_Login] PRIMARY KEY CLUSTERED ([LoginID] ASC),
    CONSTRAINT [FK_tbl_Login_tbl_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID])
);

