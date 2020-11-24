﻿CREATE TABLE [dbo].[tbl_Clients] (
    [ClientID]          INT            IDENTITY (1, 1) NOT NULL,
    [ClientName]        VARCHAR (256)  NULL,
    [IsActive]          BIT            NULL,
    [Addressline1]      VARCHAR (256)  NULL,
    [Addressline2]      VARCHAR (256)  NULL,
    [City]              INT            NULL,
    [State]             INT            NULL,
    [Country]           INT            NULL,
    [Pincode]           INT            NULL,
    [NDAStartDate]      DATETIME       NULL,
    [NDAEndDate]        DATETIME       NULL,
    [ContractStartDate] DATETIME       NULL,
    [ContractEndDate]   DATETIME       NULL,
    [TypeOfBusiness]    VARCHAR (512)  NULL,
    [Specialization]    VARCHAR (512)  NULL,
    [AnnualRevenue]     DECIMAL (18)   NULL,
    [JobBoardURL]       VARCHAR (1024) NULL,
    [JobBoardUserID]    VARCHAR (128)  NULL,
    [JobBoardPassword]  VARCHAR (128)  NULL,
    [Website]           VARCHAR (128)  NULL,
    [CreatedBy]         BIGINT         NULL,
    [ModifiedBy]        BIGINT         NULL,
    [CreatedDate]       DATETIME       NULL,
    [ModifiedDate]      DATETIME       NULL,
    CONSTRAINT [PK_tbl_Clients] PRIMARY KEY CLUSTERED ([ClientID] ASC),
    CONSTRAINT [FK_tbl_Clients_tbl_Employees] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID]),
    CONSTRAINT [FK_tbl_Clients_tbl_Employees1] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[tbl_Employees] ([EmployeeID])
);
