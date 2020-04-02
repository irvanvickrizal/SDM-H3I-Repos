USE [HCPT_Demo]
GO

/****** Object:  Table [dbo].[ApprovalCode]    Script Date: 11/29/2018 8:01:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ApprovalCode](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [uniqueidentifier] NOT NULL,
	[USRID] [bigint] NOT NULL,
	[RoleID] [bigint] NOT NULL,
	[NextSNO] [bigint] NOT NULL,
	[WPID] [bigint] NOT NULL,
	[SiteID] [bigint] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[RStatus] [tinyint] NOT NULL,
 CONSTRAINT [PK_ApprovalCode] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ApprovalCode] ADD  CONSTRAINT [DF_ApprovalCode_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[ApprovalCode] ADD  CONSTRAINT [DF_ApprovalCode_RStatus]  DEFAULT ((1)) FOR [RStatus]
GO


