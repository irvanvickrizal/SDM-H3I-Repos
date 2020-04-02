USE [HCPT_Demo]
GO

/****** Object:  Table [dbo].[HCPT_WFTransaction]    Script Date: 11/29/2018 8:03:16 PM ******/
DROP TABLE [dbo].[HCPT_WFTransaction]
GO

/****** Object:  Table [dbo].[HCPT_WFTransaction]    Script Date: 11/29/2018 8:03:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[HCPT_WFTransaction](
	[sno] [bigint] IDENTITY(1,1) NOT NULL,
	[Doc_id] [int] NULL,
	[WPID] [varchar](50) NULL,
	[WF_ID] [int] NULL,
	[Tsk_Id] [int] NULL,
	[Role_Id] [int] NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[Status] [int] NULL,
	[RStatus] [int] NULL,
	[LMBY] [int] NULL,
	[LMDT] [datetime] NULL,
	[xVal] [int] NULL,
	[yVal] [int] NULL,
	[UGP_Id] [int] NULL,
	[Page_No] [int] NULL,
	[Media] [nvarchar](10) NULL,		-- Added by Fauzan, 28 Nov 2018.
 CONSTRAINT [PK_HCPT_WFTransaction] PRIMARY KEY CLUSTERED 
(
	[sno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


