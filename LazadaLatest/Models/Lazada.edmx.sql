
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/22/2018 20:05:05
-- Generated from EDMX file: C:\Users\dzila\Desktop\LazadaLatest\LazadaLatest\Models\Lazada.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Lazada];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__ShoppingC__prodI__286302EC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingCart] DROP CONSTRAINT [FK__ShoppingC__prodI__286302EC];
GO
IF OBJECT_ID(N'[dbo].[FK__ShoppingC__userI__29572725]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingCart] DROP CONSTRAINT [FK__ShoppingC__userI__29572725];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[ShoppingCart]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShoppingCart];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [prodId] int IDENTITY(1,1) NOT NULL,
    [prodName] varchar(50)  NULL,
    [prodDesc] varchar(50)  NULL,
    [prodPrice] float  NULL,
    [prodQuan] int  NULL
);
GO

-- Creating table 'ShoppingCarts'
CREATE TABLE [dbo].[ShoppingCarts] (
    [cartId] int IDENTITY(1,1) NOT NULL,
    [prodId] int  NULL,
    [userId] int  NULL,
    [prodName] varchar(50)  NULL,
    [prodDesc] varchar(50)  NULL,
    [prodPrice] float  NULL,
    [chosenQuan] int  NULL,
    [totalPrice] float  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [userId] int IDENTITY(1,1) NOT NULL,
    [userName] varchar(50)  NULL,
    [userPass] varchar(50)  NULL,
    [userRoles] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [prodId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([prodId] ASC);
GO

-- Creating primary key on [cartId] in table 'ShoppingCarts'
ALTER TABLE [dbo].[ShoppingCarts]
ADD CONSTRAINT [PK_ShoppingCarts]
    PRIMARY KEY CLUSTERED ([cartId] ASC);
GO

-- Creating primary key on [userId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([userId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [prodId] in table 'ShoppingCarts'
ALTER TABLE [dbo].[ShoppingCarts]
ADD CONSTRAINT [FK__ShoppingC__prodI__286302EC]
    FOREIGN KEY ([prodId])
    REFERENCES [dbo].[Products]
        ([prodId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ShoppingC__prodI__286302EC'
CREATE INDEX [IX_FK__ShoppingC__prodI__286302EC]
ON [dbo].[ShoppingCarts]
    ([prodId]);
GO

-- Creating foreign key on [userId] in table 'ShoppingCarts'
ALTER TABLE [dbo].[ShoppingCarts]
ADD CONSTRAINT [FK__ShoppingC__userI__29572725]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[Users]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__ShoppingC__userI__29572725'
CREATE INDEX [IX_FK__ShoppingC__userI__29572725]
ON [dbo].[ShoppingCarts]
    ([userId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------