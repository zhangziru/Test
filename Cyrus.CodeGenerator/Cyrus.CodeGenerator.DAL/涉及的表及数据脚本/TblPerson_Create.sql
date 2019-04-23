
CREATE TABLE [TblPerson](
	[Id] [int] IDENTITY(1,1) NOT NULL primary key,
	[Name] [nvarchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Gender] [bit] NULL DEFAULT ((0))
)
