USE [departmentsEmployees]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 2024-09-29 11:34:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DeptId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[MgrId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DeptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 2024-09-29 11:34:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmpId] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Age] [int] NOT NULL,
	[Salary] [decimal](10, 2) NOT NULL,
	[DeptId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Departments] ([DeptId], [Name], [MgrId]) VALUES (1, N'Marketing', 3)
INSERT [dbo].[Departments] ([DeptId], [Name], [MgrId]) VALUES (2, N'Accounting', 1)
INSERT [dbo].[Departments] ([DeptId], [Name], [MgrId]) VALUES (3, N'Finance', 1)
INSERT [dbo].[Departments] ([DeptId], [Name], [MgrId]) VALUES (4, N'IT', 14)
GO
INSERT [dbo].[Employees] ([EmpId], [Name], [Age], [Salary], [DeptId]) VALUES (1, N'Mary', 27, CAST(90000.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Employees] ([EmpId], [Name], [Age], [Salary], [DeptId]) VALUES (3, N'John', 32, CAST(90000.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Employees] ([EmpId], [Name], [Age], [Salary], [DeptId]) VALUES (7, N'Brian', 28, CAST(80000.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[Employees] ([EmpId], [Name], [Age], [Salary], [DeptId]) VALUES (14, N'Anne', 28, CAST(95000.00 AS Decimal(10, 2)), 4)
INSERT [dbo].[Employees] ([EmpId], [Name], [Age], [Salary], [DeptId]) VALUES (32, N'James', 29, CAST(85000.00 AS Decimal(10, 2)), 1)
GO
ALTER TABLE [dbo].[Departments]  WITH CHECK ADD FOREIGN KEY([MgrId])
REFERENCES [dbo].[Employees] ([EmpId])
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD FOREIGN KEY([DeptId])
REFERENCES [dbo].[Departments] ([DeptId])
ON UPDATE CASCADE
GO
