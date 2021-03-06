USE [master]
GO
/****** Object:  Database [Pichincha]    Script Date: 26/11/2021 0:10:18 ******/
CREATE DATABASE [Pichincha]
GO

USE [Pichincha]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 26/11/2021 0:10:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Direccion] [varchar](250) NOT NULL,
	[Telefono] [varchar](50) NOT NULL,
	[Eliminado] [bit] NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuenta]    Script Date: 26/11/2021 0:10:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuenta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[Numero] [varchar](100) NOT NULL,
	[Saldo] [decimal](18, 2) NOT NULL,
	[Eliminada] [bit] NOT NULL,
 CONSTRAINT [PK_Cuenta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimiento]    Script Date: 26/11/2021 0:10:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimiento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CuentaId] [int] NOT NULL,
	[Tipo] [char](3) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[Eliminado] [bit] NOT NULL,
 CONSTRAINT [PK_Movimiento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 

INSERT [dbo].[Cliente] ([Id], [Nombre], [Direccion], [Telefono], [Eliminado]) VALUES (1, N'Juan Perez G', N'Jose Figueroa N64-102 y Machala', N'0997852411', 0)
INSERT [dbo].[Cliente] ([Id], [Nombre], [Direccion], [Telefono], [Eliminado]) VALUES (2, N'Dayana Lopez', N'Av del Maestro y Real Audiencia', N'0981234560', 0)
INSERT [dbo].[Cliente] ([Id], [Nombre], [Direccion], [Telefono], [Eliminado]) VALUES (3, N'Dan Eliminado', N'Jose Figueroa N64-102 y Machala', N'0997852425', 1)
INSERT [dbo].[Cliente] ([Id], [Nombre], [Direccion], [Telefono], [Eliminado]) VALUES (4, N'Dan Quinde', N'Jose Figueroa N64-102 y Machala', N'0997852425', 0)
SET IDENTITY_INSERT [dbo].[Cliente] OFF
SET IDENTITY_INSERT [dbo].[Cuenta] ON 

INSERT [dbo].[Cuenta] ([Id], [ClienteId], [Numero], [Saldo], [Eliminada]) VALUES (1, 1, N'33634100', CAST(40000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Cuenta] ([Id], [ClienteId], [Numero], [Saldo], [Eliminada]) VALUES (2, 1, N'41002517', CAST(1250.55 AS Decimal(18, 2)), 0)
INSERT [dbo].[Cuenta] ([Id], [ClienteId], [Numero], [Saldo], [Eliminada]) VALUES (3, 1, N'50000022', CAST(87025.90 AS Decimal(18, 2)), 0)
INSERT [dbo].[Cuenta] ([Id], [ClienteId], [Numero], [Saldo], [Eliminada]) VALUES (4, 2, N'31005800', CAST(9800.28 AS Decimal(18, 2)), 0)
INSERT [dbo].[Cuenta] ([Id], [ClienteId], [Numero], [Saldo], [Eliminada]) VALUES (5, 4, N'30004700', CAST(2000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Cuenta] ([Id], [ClienteId], [Numero], [Saldo], [Eliminada]) VALUES (6, 4, N'30004711', CAST(0.00 AS Decimal(18, 2)), 1)
SET IDENTITY_INSERT [dbo].[Cuenta] OFF
SET IDENTITY_INSERT [dbo].[Movimiento] ON 

INSERT [dbo].[Movimiento] ([Id], [CuentaId], [Tipo], [Fecha], [Valor], [Eliminado]) VALUES (1, 1, N'DEB', CAST(N'2021-01-01T00:00:00.000' AS DateTime), CAST(40000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Movimiento] ([Id], [CuentaId], [Tipo], [Fecha], [Valor], [Eliminado]) VALUES (2, 1, N'DEB', CAST(N'2021-01-07T00:00:00.000' AS DateTime), CAST(10070.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Movimiento] ([Id], [CuentaId], [Tipo], [Fecha], [Valor], [Eliminado]) VALUES (3, 2, N'DEB', CAST(N'2021-01-08T00:00:00.000' AS DateTime), CAST(1300.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Movimiento] ([Id], [CuentaId], [Tipo], [Fecha], [Valor], [Eliminado]) VALUES (4, 2, N'CRE', CAST(N'2021-01-09T00:00:00.000' AS DateTime), CAST(49.45 AS Decimal(18, 2)), 0)
INSERT [dbo].[Movimiento] ([Id], [CuentaId], [Tipo], [Fecha], [Valor], [Eliminado]) VALUES (5, 3, N'DEB', CAST(N'2021-01-09T00:00:00.000' AS DateTime), CAST(87025.90 AS Decimal(18, 2)), 0)
INSERT [dbo].[Movimiento] ([Id], [CuentaId], [Tipo], [Fecha], [Valor], [Eliminado]) VALUES (6, 4, N'DEB', CAST(N'2021-01-09T00:00:00.000' AS DateTime), CAST(8750.28 AS Decimal(18, 2)), 0)
INSERT [dbo].[Movimiento] ([Id], [CuentaId], [Tipo], [Fecha], [Valor], [Eliminado]) VALUES (7, 1, N'CRE', CAST(N'2021-11-25T21:00:00.000' AS DateTime), CAST(31.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Movimiento] ([Id], [CuentaId], [Tipo], [Fecha], [Valor], [Eliminado]) VALUES (8, 4, N'DEB', CAST(N'2021-11-25T16:22:00.000' AS DateTime), CAST(8000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Movimiento] ([Id], [CuentaId], [Tipo], [Fecha], [Valor], [Eliminado]) VALUES (9, 4, N'CRE', CAST(N'2021-11-25T16:24:00.000' AS DateTime), CAST(9000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Movimiento] ([Id], [CuentaId], [Tipo], [Fecha], [Valor], [Eliminado]) VALUES (10, 4, N'CRE', CAST(N'2021-11-25T16:55:00.000' AS DateTime), CAST(1000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Movimiento] ([Id], [CuentaId], [Tipo], [Fecha], [Valor], [Eliminado]) VALUES (11, 4, N'CRE', CAST(N'2021-11-25T16:57:00.000' AS DateTime), CAST(1000.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Movimiento] ([Id], [CuentaId], [Tipo], [Fecha], [Valor], [Eliminado]) VALUES (12, 4, N'DEB', CAST(N'2021-11-26T00:02:00.000' AS DateTime), CAST(50.00 AS Decimal(18, 2)), 0)
SET IDENTITY_INSERT [dbo].[Movimiento] OFF
ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK__Cuenta__ClienteI__3E52440B] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([Id])
GO
ALTER TABLE [dbo].[Cuenta] CHECK CONSTRAINT [FK__Cuenta__ClienteI__3E52440B]
GO
ALTER TABLE [dbo].[Movimiento]  WITH CHECK ADD  CONSTRAINT [FK__Movimient__Cuent__412EB0B6] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[Cuenta] ([Id])
GO
ALTER TABLE [dbo].[Movimiento] CHECK CONSTRAINT [FK__Movimient__Cuent__412EB0B6]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DEB=Débito, CRE=Crédito' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Movimiento', @level2type=N'COLUMN',@level2name=N'Tipo'
GO
USE [master]
GO
ALTER DATABASE [Pichincha] SET  READ_WRITE 
GO
