
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/21/2019 19:49:22
-- Generated from EDMX file: C:\Users\amguaman\source\repos\PracticaNET\PracticaNETRoP\Models\virtualShop.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VirtualShop];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductOrder] DROP CONSTRAINT [FK_OrderProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductOrder] DROP CONSTRAINT [FK_ProductOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_Stock_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stock] DROP CONSTRAINT [FK_Stock_Products];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Invoices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Invoices];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[ProductOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductOrder];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Stock]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stock];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Invoices'
CREATE TABLE [dbo].[Invoices] (
    [Id] int  NOT NULL,
    [amount] decimal(18,2)  NOT NULL,
    [dateInvoice] datetime  NOT NULL,
    [idClient] int  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int  NOT NULL,
    [units] int  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int  NOT NULL,
    [price] decimal(19,4)  NOT NULL,
    [units] int  NOT NULL,
    [image] varbinary(max)  NOT NULL,
    [description] varchar(200)  NULL
);
GO

-- Creating table 'Stock'
CREATE TABLE [dbo].[Stock] (
    [Id] int  NOT NULL,
    [idProduct] int  NOT NULL,
    [units] int  NOT NULL
);
GO

-- Creating table 'ProductOrder'
CREATE TABLE [dbo].[ProductOrder] (
    [Orders_Id] int  NOT NULL,
    [Products_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Invoices'
ALTER TABLE [dbo].[Invoices]
ADD CONSTRAINT [PK_Invoices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Stock'
ALTER TABLE [dbo].[Stock]
ADD CONSTRAINT [PK_Stock]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Orders_Id], [Products_Id] in table 'ProductOrder'
ALTER TABLE [dbo].[ProductOrder]
ADD CONSTRAINT [PK_ProductOrder]
    PRIMARY KEY CLUSTERED ([Orders_Id], [Products_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [idProduct] in table 'Stock'
ALTER TABLE [dbo].[Stock]
ADD CONSTRAINT [FK_Stock_Products]
    FOREIGN KEY ([idProduct])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Stock_Products'
CREATE INDEX [IX_FK_Stock_Products]
ON [dbo].[Stock]
    ([idProduct]);
GO

-- Creating foreign key on [Orders_Id] in table 'ProductOrder'
ALTER TABLE [dbo].[ProductOrder]
ADD CONSTRAINT [FK_ProductOrder_Orders]
    FOREIGN KEY ([Orders_Id])
    REFERENCES [dbo].[Orders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Products_Id] in table 'ProductOrder'
ALTER TABLE [dbo].[ProductOrder]
ADD CONSTRAINT [FK_ProductOrder_Products]
    FOREIGN KEY ([Products_Id])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductOrder_Products'
CREATE INDEX [IX_FK_ProductOrder_Products]
ON [dbo].[ProductOrder]
    ([Products_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------