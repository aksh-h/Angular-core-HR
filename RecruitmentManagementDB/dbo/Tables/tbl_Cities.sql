CREATE TABLE [dbo].[tbl_Cities] (
    [CityId]       INT             NOT NULL,
    [name]         VARCHAR (255)   NOT NULL,
    [state_id]     INT             NOT NULL,
    [state_code]   VARCHAR (255)   NOT NULL,
    [country_id]   INT             NOT NULL,
    [country_code] CHAR (2)        NOT NULL,
    [latitude]     DECIMAL (10, 8) NOT NULL,
    [longitude]    DECIMAL (11, 8) NOT NULL,
    [flag]         TINYINT         NULL,
    CONSTRAINT [PK_cities] PRIMARY KEY CLUSTERED ([CityId] ASC)
);

