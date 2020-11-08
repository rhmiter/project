/************************************ DATABASE *************************************/

IF NOT EXISTS (
	 SELECT 1  
		FROM master.dbo.sysdatabases  
		WHERE name = 'TesteDeloitte')
BEGIN
	CREATE DATABASE TesteDeloitte;
END
GO

USE [TesteDeloitte]
GO

/************************************* TABLES *************************************/

IF NOT EXISTS (
    SELECT 1 
		FROM SYS.TABLES 
		INNER JOIN sys.schemas on sys.schemas.schema_id = sys.tables.schema_id
		WHERE SYS.TABLES.NAME = 'Clientes' AND Sys.schemas.name = 'dbo')
BEGIN
	CREATE TABLE [dbo].[Clientes](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Nome] [varchar](100) NOT NULL
		CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
	) ON [PRIMARY]

	INSERT INTO [dbo].[Clientes] (Nome) VALUES
	('Marcelo'),
	('Camila'),
	('Maria'),
	('Mateus')
END 
GO

IF NOT EXISTS (
    SELECT 1 
		FROM SYS.TABLES 
		INNER JOIN sys.schemas on sys.schemas.schema_id = sys.tables.schema_id
		WHERE SYS.TABLES.NAME = 'Fornecedores' AND Sys.schemas.name = 'dbo')
BEGIN
	CREATE TABLE [dbo].[Fornecedores](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Nome] [varchar](100) NOT NULL
		CONSTRAINT [PK_Fornecedores] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
	) ON [PRIMARY]

	INSERT INTO [dbo].[Fornecedores] (Nome) VALUES
	('Nestle'),
	('Bauducco'),
	('Renata')
END 
GO

IF NOT EXISTS (
    SELECT 1 
		FROM SYS.TABLES 
		INNER JOIN sys.schemas on sys.schemas.schema_id = sys.tables.schema_id
		WHERE SYS.TABLES.NAME = 'Produtos' AND Sys.schemas.name = 'dbo')
BEGIN
	CREATE TABLE [dbo].[Produtos](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[FornecedorId] [int] NOT NULL,
		[Nome] [varchar](100) NOT NULL
		CONSTRAINT [PK_Produtos] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Produtos]  WITH CHECK ADD CONSTRAINT [FK_Produtos_Fornecedores] FOREIGN KEY([FornecedorId])
	REFERENCES [dbo].[Fornecedores] ([Id])

	INSERT INTO [dbo].[Produtos] (FornecedorId, Nome) VALUES
	(1, 'Chocolate'),
	(2, 'Biscoito'),
	(3, 'Farinha')
END 
GO

IF NOT EXISTS (
    SELECT 1 
		FROM SYS.TABLES 
		INNER JOIN sys.schemas on sys.schemas.schema_id = sys.tables.schema_id
	    WHERE SYS.TABLES.NAME = 'NotaFiscal' AND Sys.schemas.name = 'dbo')
BEGIN
	CREATE TABLE [dbo].[NotaFiscal](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[ClienteId] [int] NOT NULL,
		[ProdutoId] [int] NOT NULL,
		[Valor] [DECIMAL](10,2) NOT NULL DEFAULT 0,
		[Observacao] [varchar](100) NULL
		CONSTRAINT [PK_NotaFiscal] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
	) ON [PRIMARY]

	ALTER TABLE [dbo].[NotaFiscal]  WITH CHECK ADD CONSTRAINT [FK_NotaFiscal_Clientes] FOREIGN KEY([ClienteId])
	REFERENCES [dbo].[Clientes] ([Id])

	ALTER TABLE [dbo].[NotaFiscal]  WITH CHECK ADD CONSTRAINT [FK_NotaFiscal_Produtos] FOREIGN KEY([ProdutoId])
	REFERENCES [dbo].[Produtos] ([Id])
END

GO
/************************************* PROCEDURES *************************************/

IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[usp_Clientes_SelectAll]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
	BEGIN
		DROP PROCEDURE [dbo].[usp_Clientes_SelectAll]
	END
GO

CREATE PROCEDURE [dbo].[usp_Clientes_SelectAll]
AS
	BEGIN
		SELECT Id, Nome
		FROM [dbo].[Clientes]
	END
GO

IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[usp_Produtos_SelectAll]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
	BEGIN
		DROP PROCEDURE [dbo].[usp_Produtos_SelectAll]
	END
GO

CREATE PROCEDURE [dbo].[usp_Produtos_SelectAll]
AS
	BEGIN
		SELECT 
			produto.Id, 
			CONCAT(produto.Nome, ' - ' , fornecedor.Nome) as NomeProduto
		FROM [dbo].[Produtos] as produto
		INNER JOIN [dbo].[Fornecedores] as fornecedor ON fornecedor.Id = produto.FornecedorId
	END
GO

IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[usp_NotaFiscal_SelectAll]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
	BEGIN
		DROP PROCEDURE [dbo].[usp_NotaFiscal_SelectAll]
	END
GO

CREATE PROCEDURE [dbo].[usp_NotaFiscal_SelectAll]
AS
	BEGIN
		SELECT 
			nota.Id,
			cliente.Nome as NomeCliente,
			CONCAT(produto.Nome, ' - ' , fornecedor.Nome) as NomeProduto,
			nota.Valor,
			nota.Observacao
			
		FROM [dbo].[NotaFiscal] as nota
		INNER JOIN [dbo].[Clientes] as cliente ON cliente.Id = nota.ClienteId
		INNER JOIN [dbo].[Produtos] as produto ON produto.Id = nota.ProdutoId
		INNER JOIN [dbo].[Fornecedores] as fornecedor ON fornecedor.Id = produto.FornecedorId
	END
GO

IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[usp_NotaFiscal_SelectSpecific]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
	BEGIN
		DROP PROCEDURE [dbo].[usp_NotaFiscal_SelectSpecific]
	END
GO

CREATE PROCEDURE [dbo].[usp_NotaFiscal_SelectSpecific]
	@Id			[int]
AS
	BEGIN
		SELECT 
			Id,
			ClienteId,
			ProdutoId,
			Valor
		FROM [dbo].[NotaFiscal]
		WHERE Id = @Id
	END
GO

IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[usp_Relatorio_Vendas_Produto]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
	BEGIN
		DROP PROCEDURE [dbo].[usp_Relatorio_Vendas_Produto]
	END
GO

CREATE PROCEDURE [dbo].[usp_Relatorio_Vendas_Produto]
AS
	BEGIN
		SELECT 
			nota.ProdutoId, 
			CONCAT(produto.Nome, ' - ' , fornecedor.Nome) as NomeProduto,
			COUNT(nota.ProdutoId) as QtdeVendas, 
			SUM(nota.Valor)  as TotalVendas 
		FROM NotaFiscal as nota
		INNER JOIN Produtos as produto ON produto.Id = nota.ProdutoId
		INNER JOIN Fornecedores fornecedor ON fornecedor.Id = produto.FornecedorId
		GROUP BY ProdutoId, produto.Nome, fornecedor.Nome
	END
GO


IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[usp_NotaFiscal_Insert]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
	BEGIN
		DROP PROCEDURE [dbo].[usp_NotaFiscal_Insert]
	END
GO

CREATE PROCEDURE [dbo].[usp_NotaFiscal_Insert]
    @ClienteId  [int],
    @ProdutoId  [int],
    @Valor      [decimal](10,2)
AS
	BEGIN
		INSERT INTO [dbo].[NotaFiscal] ([ClienteId], [ProdutoId], [Valor], [Observacao]) VALUES  (
		@ClienteId, 
		@ProdutoId, 
		@Valor,
		CONCAT('Incuido via sp_NotaFiscal_Insert - ', CURRENT_TIMESTAMP)
		)
	END
GO

IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[usp_NotaFiscal_Update]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
	BEGIN
		DROP PROCEDURE [dbo].[usp_NotaFiscal_Update]
	END
GO 

CREATE PROCEDURE [dbo].[usp_NotaFiscal_Update]
	@Id			[int],
    @ClienteId  [int],
    @ProdutoId  [int],
    @Valor      [decimal](10,2)
AS
	BEGIN
		UPDATE [dbo].[NotaFiscal] SET 
					[ClienteId] = COALESCE(@ClienteId, ClienteId),
					[ProdutoId] = COALESCE(@ProdutoId, ProdutoId),
					[Valor] = COALESCE(@Valor, 0), 
					[Observacao] = CONCAT('Atualizado via sp_NotaFiscal_Update - ', CURRENT_TIMESTAMP)
		WHERE Id = @Id
	END
GO

IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[usp_NotaFiscal_Delete]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
	BEGIN
		DROP PROCEDURE [dbo].[usp_NotaFiscal_Delete]
	END
GO 

CREATE PROCEDURE [dbo].[usp_NotaFiscal_Delete]
    @Id			[int]
AS
	BEGIN
		DELETE FROM [dbo].[NotaFiscal]
		WHERE Id	 = @Id
	END
GO


