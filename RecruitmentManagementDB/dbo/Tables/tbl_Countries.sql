CREATE TABLE [dbo].[tbl_Countries] (
    [CountryId]  INT           NOT NULL,
    [name]       VARCHAR (100) NOT NULL,
    [iso3]       CHAR (3)      DEFAULT (NULL) NULL,
    [iso2]       CHAR (2)      DEFAULT (NULL) NULL,
    [phonecode]  VARCHAR (255) DEFAULT (NULL) NULL,
    [capital]    VARCHAR (255) DEFAULT (NULL) NULL,
    [currency]   VARCHAR (255) DEFAULT (NULL) NULL,
    [native]     VARCHAR (255) DEFAULT (NULL) NULL,
    [emoji]      VARCHAR (191) DEFAULT (NULL) NULL,
    [emojiU]     VARCHAR (191) DEFAULT (NULL) NULL,
    [created_at] DATETIME      DEFAULT (NULL) NULL,
    [updated_at] DATETIME      NULL,
    [flag]       TINYINT       DEFAULT ('1') NOT NULL,
    [wikiDataId] VARCHAR (255) DEFAULT (NULL) NULL
);

