SET IDENTITY_INSERT [dbo].[User] ON
INSERT INTO [dbo].[User] ([ID], [Username], [Password], [EmployeeType], [Department], [UserRole]) VALUES (1, N'ayor', N'AYOR', N'Employee', N'IT', N'DataUser')
INSERT INTO [dbo].[User] ([ID], [Username], [Password], [EmployeeType], [Department], [UserRole]) VALUES (2, N'admin', N'admin', N'Employee', N'IT', N'DataOwner')
SET IDENTITY_INSERT [dbo].[User] OFF
