USE [HotelManager]
GO
/****** Object:  Table [dbo].[Hotel]    Script Date: 11/10/2022 9:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Hotel_Name] [nvarchar](max) NULL,
	[P_Id] [int] NULL,
 CONSTRAINT [PK_Hotel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 11/10/2022 9:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Total] [money] NULL,
	[Date_Book] [date] NULL,
	[Date_Checkout] [date] NULL,
	[H_Id] [int] NOT NULL,
	[R_Id] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[U_Id] [int] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderService]    Script Date: 11/10/2022 9:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderService](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[O_Id] [int] NULL,
	[S_Id] [int] NULL,
	[Quantity] [int] NULL,
	[Price] [money] NULL,
	[U_Id] [int] NULL,
 CONSTRAINT [PK_OrderService] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Province]    Script Date: 11/10/2022 9:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Province](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Province_Name] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
 CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recharge]    Script Date: 11/10/2022 9:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recharge](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Bank_Name] [nvarchar](50) NULL,
	[Bank_Id] [int] NULL,
	[Phone] [int] NULL,
	[Deposit_Code] [int] NULL,
	[Recharge_Value] [money] NULL,
	[U_Id] [int] NULL,
 CONSTRAINT [PK_Recharge] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 11/10/2022 9:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Room_Name] [nvarchar](max) NULL,
	[Status] [nvarchar](50) NULL,
	[H_Id] [int] NOT NULL,
	[Price] [money] NULL,
	[U_Id] [int] NULL,
	[RoomType] [nvarchar](50) NULL,
 CONSTRAINT [PK_H] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 11/10/2022 9:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Service_Name] [nvarchar](max) NULL,
	[Price] [money] NULL,
	[Type] [nvarchar](50) NULL,
	[Image] [nvarchar](max) NULL,
	[H_Id] [int] NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/10/2022 9:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Role] [int] NOT NULL,
	[First_Name] [nvarchar](50) NULL,
	[Last_Name] [nvarchar](50) NULL,
	[Phone] [int] NULL,
	[Email] [nvarchar](50) NULL,
	[Money] [money] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Hotel]  WITH CHECK ADD  CONSTRAINT [FK_Hotel_Province] FOREIGN KEY([P_Id])
REFERENCES [dbo].[Province] ([Id])
GO
ALTER TABLE [dbo].[Hotel] CHECK CONSTRAINT [FK_Hotel_Province]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([U_Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderService]  WITH CHECK ADD  CONSTRAINT [FK_OrderService_Order] FOREIGN KEY([O_Id])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderService] CHECK CONSTRAINT [FK_OrderService_Order]
GO
ALTER TABLE [dbo].[OrderService]  WITH CHECK ADD  CONSTRAINT [FK_OrderService_Service1] FOREIGN KEY([S_Id])
REFERENCES [dbo].[Service] ([Id])
GO
ALTER TABLE [dbo].[OrderService] CHECK CONSTRAINT [FK_OrderService_Service1]
GO
ALTER TABLE [dbo].[OrderService]  WITH CHECK ADD  CONSTRAINT [FK_OrderService_User] FOREIGN KEY([U_Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[OrderService] CHECK CONSTRAINT [FK_OrderService_User]
GO
ALTER TABLE [dbo].[Recharge]  WITH CHECK ADD  CONSTRAINT [FK_Recharge_User] FOREIGN KEY([U_Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Recharge] CHECK CONSTRAINT [FK_Recharge_User]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Hotel] FOREIGN KEY([H_Id])
REFERENCES [dbo].[Hotel] ([Id])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Hotel]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_User] FOREIGN KEY([U_Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_User]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_Hotel] FOREIGN KEY([H_Id])
REFERENCES [dbo].[Hotel] ([Id])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_Hotel]
GO
