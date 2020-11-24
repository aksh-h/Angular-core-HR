CREATE TABLE [dbo].[tbl_States] (
    [StateId]      INT           NOT NULL,
    [name]         VARCHAR (255) NOT NULL,
    [country_id]   INT           NOT NULL,
    [country_code] CHAR (2)      NOT NULL,
    [fips_code]    VARCHAR (255) DEFAULT (NULL) NULL,
    [iso2]         VARCHAR (255) DEFAULT (NULL) NULL,
    [created_at]   DATETIME      DEFAULT (NULL) NULL,
    [updated_at]   DATETIME      NOT NULL,
    [flag]         TINYINT       DEFAULT ('1') NOT NULL,
    [wikiDataId]   VARCHAR (255) DEFAULT (NULL) NULL
);

